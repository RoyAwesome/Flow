using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Watertight.FlowUI.Draw;

namespace Watertight.FlowUI
{
    [Flags]
    public enum EWindowBehaviorFlags
    {
        None = 0,
        NoTitleBar = 1 << 0,   // Disable title-bar
        NoResize = 1 << 1,   // Disable user resizing with the lower-right grip
        NoMove = 1 << 2,   // Disable user moving the window
        NoScrollbar = 1 << 3,   // Disable scrollbars (window can still scroll with mouse or programmatically)
        NoScrollWithMouse = 1 << 4,   // Disable user vertically scrolling with mouse wheel. On child window, mouse wheel will be forwarded to the parent unless NoScrollbar is also set.
        NoCollapse = 1 << 5,   // Disable user collapsing window by double-clicking on it
        AlwaysAutoResize = 1 << 6,   // Resize every window to its content every frame
        NoBackground = 1 << 7,   // Disable drawing background color (WindowBg, etc.) and outside border. Similar as using SetNextWindowBgAlpha(0.0f).
        NoSavedSettings = 1 << 8,   // Never load/save settings in .ini file
        NoMouseInputs = 1 << 9,   // Disable catching mouse, hovering test with pass through.
        MenuBar = 1 << 10,  // Has a menu-bar
        HorizontalScrollbar = 1 << 11,  // Allow horizontal scrollbar to appear (off by default). You may use SetNextWindowContentSize(ImVec2(width,0.0f)); prior to calling Begin() to specify width. Read code in imgui_demo in the "Horizontal Scrolling" section.
        NoFocusOnAppearing = 1 << 12,  // Disable taking focus when transitioning from hidden to visible state
        NoBringToFrontOnFocus = 1 << 13,  // Disable bringing window to front when taking focus (e.g. clicking on it or programmatically giving it focus)
        AlwaysVerticalScrollbar = 1 << 14,  // Always show vertical scrollbar (even if ContentSize.y < Size.y)
        AlwaysHorizontalScrollbar = 1 << 15,  // Always show horizontal scrollbar (even if ContentSize.x < Size.x)
        AlwaysUseWindowPadding = 1 << 16,  // Ensure child windows without border uses style.WindowPadding (ignored by default for non-bordered child windows, because more convenient)
        NoNavInputs = 1 << 18,  // No gamepad/keyboard navigation within the window
        NoNavFocus = 1 << 19,  // No focusing toward this window with gamepad/keyboard navigation (e.g. skipped by CTRL+TAB)
        UnsavedDocument = 1 << 20,  // Append '*' to title without affecting the ID, as a convenience to avoid using the ### operator. When used in a tab/docking context, tab is selected on closure and closure is deferred by one frame to allow code to cancel the closure (with a confirmation popup, etc.) without flicker.
        NoNav = NoNavInputs | NoNavFocus,
        NoDecoration = NoTitleBar | NoResize | NoScrollbar | NoCollapse,
        NoInputs = NoMouseInputs | NoNavInputs | NoNavFocus,

        // [Internal]
        NavFlattened = 1 << 23,  // [BETA] Allow gamepad/keyboard navigation to cross over parent border to this child (only use on child that have no scrolling!)
        ChildWindow = 1 << 24,  // Don't use! For internal use by BeginChild()
        Tooltip = 1 << 25,  // Don't use! For internal use by BeginTooltip()
        Popup = 1 << 26,  // Don't use! For internal use by BeginPopup()
        Modal = 1 << 27,  // Don't use! For internal use by BeginPopupModal()
        ChildMenu = 1 << 28   // Don't use! For internal use by BeginMenu()
    }

    public partial class FlowContext
    {
        public const string DefaultWindowName = "##DefaultWindow";

        List<Window> AllWindows
        {
            get;
        } = new List<Window>();

        internal Stack<Window> WindowStack
        {
            get;
        } = new Stack<Window>();

        internal Window FallbackWindow;

        internal Window CurrentWindow
        {
            get
            {
                Window RetWindow = null;
                if (WindowStack.TryPeek(out RetWindow))
                {
                    return RetWindow;
                }

                return FallbackWindow;
            }
        }

        internal Window GetWindowByName(string Name)
        {
            int id = Name.GetHashCode();
            for (int i = 0; i < AllWindows.Count; i++)
            {
                if (AllWindows[i].NameId == id)
                {
                    return AllWindows[i];
                }
            }

            return null;
        }

        internal Window GetOrCreateWindow(string Name)
        {
            Window ret = GetWindowByName(Name);
            if (ret != null)
            {
                return ret;
            }

            //Otherwise create the window
            ret = new Window(Name, SharedData);
            AllWindows.Add(ret);
            return ret;
        }

        [Flags]
        internal enum ENextWindowFlags
        {
            None = 0,
            HasPosition = 1 << 0,
            HasSize = 1 << 1,
            HasContentSize = 1 << 2,
            HasCollapsed = 1 << 3,
            HasSizeConstraint = 1 << 4,
            HasFocus = 1 << 5,
            HasBackgroundAlpha = 1 << 6,
            HasScroll = 1 << 7,
        }

        internal struct NextWindowData
        {
            public ENextWindowFlags NextWindowFlags;

            public Vector2 Position;
            public Vector2 PositionPivot;
            public Vector2 Size;
            public Vector2 ContentSize;
            public Vector2 Scroll;
            public bool Collapsed;
            public float BackgroundAlpha;

        }

        internal NextWindowData NextWindow = new NextWindowData();
       

        public void ConsumeNextWindowData()
        {
            NextWindow.NextWindowFlags = ENextWindowFlags.None;
        }

        internal class Window
        {
            public Window(string Name, DrawListSharedData sharedData)
            {
                DrawList = new DrawList(sharedData);
                this.Name = Name;
                NameId = Name.GetHashCode();
            }

            public DrawList DrawList
            {
                get;
                private set;
            }

            public string Name
            {
                get;
                private set;
            }

            internal int NameId
            {
                get;
                private set;
            }

            internal uint LastActiveFrame
            {
                get;
                set;
            }

            internal bool Active
            {
                get;
                set;
            }

            public bool Collasped
            {
                get;
                set;
            } = false;

            public EWindowBehaviorFlags WindowBehaviorFlags
            {
                get;
                set;
            }

            public Vector2 Position
            {
                get;
                set;
            } = new Vector2(60, 60);

            public Vector2 Size
            {
                get;
                set;
            }

            public Vector2 SizeFull
            {
                get;
                set;
            }

           
            internal Rectangle Rect
            {
                get
                {
                    return Rectangle.FromMinAndSize(Position, Size);
                }
            }

            internal float TitleBarHeight
            {
                get
                {
                    if(WindowBehaviorFlags.HasFlag(EWindowBehaviorFlags.NoTitleBar))
                    {
                        return 0;
                    }
                    return Flow.CurrentStyle.FramePadding.Y * 2 + FontSize;
                }
            }



            internal Rectangle TitleBarRect
            {
                get
                {
                    return Rectangle.FromMinAndSize(Position, new Vector2(SizeFull.X, TitleBarHeight));
                }
            }

            internal float FontSize
            {
                get
                {
                    return 0;
                }
            }

            internal float WindowRounding
            {
                get;
                set;
            }

            internal float WindowBorderSize
            {
                get;
                set;
            }
        }
    }
}

