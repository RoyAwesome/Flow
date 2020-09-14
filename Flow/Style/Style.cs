using System;
using System.Drawing;
using System.Numerics;

namespace Watertight.FlowUI
{
    public enum EGUIDirection
    {
        None,
        Left,
        Right,
    }

    public class Style
    {

        #region Styles Control
        public float Alpha
        {
            get;
            set;
        } = 1.0f;

        public Vector2 WindowPadding
        {
            get;
            set;
        } = new Vector2(8, 8);

        public float WindowRounding
        {
            get;
            set;
        } = 7.0f;

        public float WindowBorderSize
        {
            get;
            set;
        } = 1.0f;

        public Vector2 WindowMinSize
        {
            get;
            set;
        } = new Vector2(32, 32);

        public Vector2 WindowTitleAlign
        {
            get;
            set;
        } = new Vector2(0, 0.5f);



        public EGUIDirection WindowMenuButtonPosition
        {
            get;
            set;
        } = EGUIDirection.Left;

        public float ChildRounding
        {
            get;
            set;
        } = 0.0f;

        public float ChildBorderSize
        {
            get;
            set;
        } = 1.0f;

        public float PopupRounding
        {
            get;
            set;
        } = 0.0f;

        public float PopupBorderSize
        {
            get;
            set;
        } = 1.0f;

        public Vector2 FramePadding
        {
            get;
            set;
        } = new Vector2(4, 3);

        public float FrameRounding
        {
            get;
            set;
        } = 0.0f;

        public float FrameBorderSize
        {
            get;
            set;
        } = 0.0f;

        public Vector2 ItemSpacing
        {
            get;
            set;
        } = new Vector2(8, 4);

        public Vector2 ItemInnerSpacing
        {
            get;
            set;
        } = new Vector2(4, 4);

        public Vector2 TouchExtraPadding
        {
            get;
            set;
        } = new Vector2(0, 0);

        public float IndentSpacing
        {
            get;
            set;
        } = 21.0f;

        public float ColumnsMinSpacing
        {
            get;
            set;
        } = 6.0f;

        public float ScrollbarSize
        {
            get;
            set;
        } = 14.0f;

        public float ScrollbarRounding
        {
            get;
            set;
        } = 9.0f;

        public float GrabMinSize
        {
            get;
            set;
        } = 10.0f;

        public float GrabRounding
        {
            get;
            set;
        } = 0.0f;

        public float TabRounding
        {
            get;
            set;
        } = 4.0f;

        public float TabBorderSize
        {
            get;
            set;
        } = 0.0f;

        public float TabMinWidthForUnselectedCloseButton
        {
            get;
            set;
        } = 0.0f;

        public EGUIDirection ColorButtonPosition
        {
            get;
            set;
        } = EGUIDirection.Right;

        public Vector2 ButtonTextAlign
        {
            get;
            set;
        } = new Vector2(.5f, .5f);

        public Vector2 SelectableTextAlign
        {
            get;
            set;
        } = new Vector2(0.0f, 0.0f);

        public Vector2 DisplayWindowPadding
        {
            get;
            set;
        } = new Vector2(19, 19);

        public Vector2 DisplaySafeAreaPadding
        {
            get;
            set;
        } = new Vector2(3, 3);

        public float MouseCursorScale
        {
            get;
            set;
        } = 1.0f;

        public bool AntiAliasedLines
        {
            get;
            set;
        } = true;

        public bool AntiAliasedLinesUseTexture
        {
            get;
            set;
        } = true;

        public bool AntiAliasedFill
        {
            get;
            set;
        } = true;

        public float CurveTesselationTolerance
        {
            get;
            set;
        } = 1.25f;

        public float CircleSegmentMaxError
        {
            get;
            set;
        } = 1.60f;      


        #endregion

        #region Colors

        public Color Text
        {
            get;
            set;
        }
        public Color TextDisabled
        {
            get;
            set;
        }
        public Color WindowBackground
        {
            get;
            set;
        }
        public Color ChildBackground
        {
            get;
            set;
        }
        public Color PopupBackground
        {
            get;
            set;
        }
        public Color Border
        {
            get;
            set;
        }
        public Color BorderShadow
        {
            get;
            set;
        }
        public Color FrameBackground
        {
            get;
            set;
        }
        public Color FrameBackgroundHovered
        {
            get;
            set;
        }
        public Color FrameBackgroundActive
        {
            get;
            set;
        }
        public Color TitleBackground
        {
            get;
            set;
        }
        public Color TitleBackgroundActive
        {
            get;
            set;
        }
        public Color TitleBackgroundCollapsed
        {
            get;
            set;
        }
        public Color MenuBarBackground
        {
            get;
            set;
        }
        public Color ScrollbarBackground
        {
            get;
            set;
        }
        public Color ScrollbarGrab
        {
            get;
            set;
        }
        public Color ScrollbarGrabHovered
        {
            get;
            set;
        }
        public Color ScrollbarGrabActive
        {
            get;
            set;
        }
        public Color CheckMark
        {
            get;
            set;
        }
        public Color SliderGrab
        {
            get;
            set;
        }
        public Color SliderGrabActive
        {
            get;
            set;
        }
        public Color Button
        {
            get;
            set;
        }
        public Color ButtonHovered
        {
            get;
            set;
        }
        public Color ButtonActive
        {
            get;
            set;
        }
        public Color Header
        {
            get;
            set;
        }
        public Color HeaderHovered
        {
            get;
            set;
        }
        public Color HeaderActive
        {
            get;
            set;
        }
        public Color Separator
        {
            get;
            set;
        }
        public Color SeparatorHovered
        {
            get;
            set;
        }
        public Color SeparatorActive
        {
            get;
            set;
        }
        public Color ResizeGrip
        {
            get;
            set;
        }
        public Color ResizeGripHovered
        {
            get;
            set;
        }
        public Color ResizeGripActive
        {
            get;
            set;
        }
        public Color Tab
        {
            get;
            set;
        }
        public Color TabHovered
        {
            get;
            set;
        }
        public Color TabActive
        {
            get;
            set;
        }
        public Color TabUnfocused
        {
            get;
            set;
        }
        public Color TabUnfocusedActive
        {
            get;
            set;
        }
        public Color PlotLines
        {
            get;
            set;
        }
        public Color PlotLinesHovered
        {
            get;
            set;
        }
        public Color PlotHistogram
        {
            get;
            set;
        }
        public Color PlotHistogramHovered
        {
            get;
            set;
        }
        public Color TextSelectedBackground
        {
            get;
            set;
        }
        public Color DragDropTarget
        {
            get;
            set;
        }
        public Color NavHighlight
        {
            get;
            set;
        }
        public Color NavWindowingHighlight
        {
            get;
            set;
        }
        public Color NavWindowingDimBackground
        {
            get;
            set;
        }
        public Color ModalWindowDimBackground
        {
            get;
            set;
        }

        #endregion


        #region Built In Styles
        static Style()
        {
            {
                Light = new Style()
                {
                    Text = Ext.FromFloats(0.00f, 0.00f, 0.00f, 1.00f),
                    TextDisabled = Ext.FromFloats(0.60f, 0.60f, 0.60f, 1.00f),
                    WindowBackground = Ext.FromFloats(0.94f, 0.94f, 0.94f, 1.00f),
                    ChildBackground = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
                    PopupBackground = Ext.FromFloats(1.00f, 1.00f, 1.00f, 0.98f),
                    Border = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.30f),
                    BorderShadow = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
                    FrameBackground = Ext.FromFloats(1.00f, 1.00f, 1.00f, 1.00f),
                    FrameBackgroundHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.40f),
                    FrameBackgroundActive = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.67f),
                    TitleBackground = Ext.FromFloats(0.96f, 0.96f, 0.96f, 1.00f),
                    TitleBackgroundActive = Ext.FromFloats(0.82f, 0.82f, 0.82f, 1.00f),
                    TitleBackgroundCollapsed = Ext.FromFloats(1.00f, 1.00f, 1.00f, 0.51f),
                    MenuBarBackground = Ext.FromFloats(0.86f, 0.86f, 0.86f, 1.00f),
                    ScrollbarBackground = Ext.FromFloats(0.98f, 0.98f, 0.98f, 0.53f),
                    ScrollbarGrab = Ext.FromFloats(0.69f, 0.69f, 0.69f, 0.80f),
                    ScrollbarGrabHovered = Ext.FromFloats(0.49f, 0.49f, 0.49f, 0.80f),
                    ScrollbarGrabActive = Ext.FromFloats(0.49f, 0.49f, 0.49f, 1.00f),
                    CheckMark = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),
                    SliderGrab = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.78f),
                    SliderGrabActive = Ext.FromFloats(0.46f, 0.54f, 0.80f, 0.60f),
                    Button = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.40f),
                    ButtonHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),
                    ButtonActive = Ext.FromFloats(0.06f, 0.53f, 0.98f, 1.00f),
                    Header = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.31f),
                    HeaderHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.80f),
                    HeaderActive = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),
                    Separator = Ext.FromFloats(0.39f, 0.39f, 0.39f, 0.62f),
                    SeparatorHovered = Ext.FromFloats(0.14f, 0.44f, 0.80f, 0.78f),
                    SeparatorActive = Ext.FromFloats(0.14f, 0.44f, 0.80f, 1.00f),
                    ResizeGrip = Ext.FromFloats(0.80f, 0.80f, 0.80f, 0.56f),
                    ResizeGripHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.67f),
                    ResizeGripActive = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.95f),
                    PlotLines = Ext.FromFloats(0.39f, 0.39f, 0.39f, 1.00f),
                    PlotLinesHovered = Ext.FromFloats(1.00f, 0.43f, 0.35f, 1.00f),
                    PlotHistogram = Ext.FromFloats(0.90f, 0.70f, 0.00f, 1.00f),
                    PlotHistogramHovered = Ext.FromFloats(1.00f, 0.45f, 0.00f, 1.00f),
                    TextSelectedBackground = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.35f),
                    DragDropTarget = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.95f),
                    NavWindowingHighlight = Ext.FromFloats(0.70f, 0.70f, 0.70f, 0.70f),
                    NavWindowingDimBackground = Ext.FromFloats(0.20f, 0.20f, 0.20f, 0.20f),
                    ModalWindowDimBackground = Ext.FromFloats(0.20f, 0.20f, 0.20f, 0.35f),
                };

                Light.Tab = Ext.Lerp(Light.Header, Light.TitleBackgroundActive, 0.90f);
                Light.TabHovered = Light.HeaderHovered;
                Light.TabActive = Ext.Lerp(Light.HeaderActive, Light.TitleBackgroundActive, 0.60f);
                Light.TabUnfocused = Ext.Lerp(Light.Tab, Light.TitleBackground, 0.80f);
                Light.TabUnfocusedActive = Ext.Lerp(Light.TabActive, Light.TitleBackground, 0.40f);
                Light.NavHighlight = Light.HeaderHovered;
            }

            {
                Dark = new Style()
                {
                    Text = Ext.FromFloats(1.00f, 1.00f, 1.00f, 1.00f),
                    TextDisabled = Ext.FromFloats(0.50f, 0.50f, 0.50f, 1.00f),
                    WindowBackground = Ext.FromFloats(0.06f, 0.06f, 0.06f, 0.94f),
                    ChildBackground = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
                    PopupBackground = Ext.FromFloats(0.08f, 0.08f, 0.08f, 0.94f),
                    Border = Ext.FromFloats(0.43f, 0.43f, 0.50f, 0.50f),
                    BorderShadow = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
                    FrameBackground = Ext.FromFloats(0.16f, 0.29f, 0.48f, 0.54f),
                    FrameBackgroundHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.40f),
                    FrameBackgroundActive = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.67f),
                    TitleBackground = Ext.FromFloats(0.04f, 0.04f, 0.04f, 1.00f),
                    TitleBackgroundActive = Ext.FromFloats(0.16f, 0.29f, 0.48f, 1.00f),
                    TitleBackgroundCollapsed = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.51f),
                    MenuBarBackground = Ext.FromFloats(0.14f, 0.14f, 0.14f, 1.00f),
                    ScrollbarBackground = Ext.FromFloats(0.02f, 0.02f, 0.02f, 0.53f),
                    ScrollbarGrab = Ext.FromFloats(0.31f, 0.31f, 0.31f, 1.00f),
                    ScrollbarGrabHovered = Ext.FromFloats(0.41f, 0.41f, 0.41f, 1.00f),
                    ScrollbarGrabActive = Ext.FromFloats(0.51f, 0.51f, 0.51f, 1.00f),
                    CheckMark = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),
                    SliderGrab = Ext.FromFloats(0.24f, 0.52f, 0.88f, 1.00f),
                    SliderGrabActive = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),
                    Button = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.40f),
                    ButtonHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),
                    ButtonActive = Ext.FromFloats(0.06f, 0.53f, 0.98f, 1.00f),
                    Header = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.31f),
                    HeaderHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.80f),
                    HeaderActive = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),

                    SeparatorHovered = Ext.FromFloats(0.10f, 0.40f, 0.75f, 0.78f),
                    SeparatorActive = Ext.FromFloats(0.10f, 0.40f, 0.75f, 1.00f),
                    ResizeGrip = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.25f),
                    ResizeGripHovered = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.67f),
                    ResizeGripActive = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.95f),
                   
                    PlotLines = Ext.FromFloats(0.61f, 0.61f, 0.61f, 1.00f),
                    PlotLinesHovered = Ext.FromFloats(1.00f, 0.43f, 0.35f, 1.00f),
                    PlotHistogram = Ext.FromFloats(0.90f, 0.70f, 0.00f, 1.00f),
                    PlotHistogramHovered = Ext.FromFloats(1.00f, 0.60f, 0.00f, 1.00f),
                    TextSelectedBackground = Ext.FromFloats(0.26f, 0.59f, 0.98f, 0.35f),
                    DragDropTarget = Ext.FromFloats(1.00f, 1.00f, 0.00f, 0.90f),
                    NavHighlight = Ext.FromFloats(0.26f, 0.59f, 0.98f, 1.00f),
                    NavWindowingHighlight = Ext.FromFloats(1.00f, 1.00f, 1.00f, 0.70f),
                    NavWindowingDimBackground = Ext.FromFloats(0.80f, 0.80f, 0.80f, 0.20f),
                    ModalWindowDimBackground = Ext.FromFloats(0.80f, 0.80f, 0.80f, 0.35f),
                };

                Dark.Separator = Dark.Border;
                Dark.Tab = Ext.Lerp(Dark.Header, Dark.TitleBackgroundActive, 0.80f);
                Dark.TabHovered = Dark.HeaderHovered;
                Dark.TabActive = Ext.Lerp(Dark.HeaderActive, Dark.TitleBackgroundActive, 0.60f);
                Dark.TabUnfocused = Ext.Lerp(Dark.Tab, Dark.TitleBackground, 0.80f);
                Dark.TabUnfocusedActive = Ext.Lerp(Dark.TabActive, Dark.TitleBackground, 0.40f);
            }

            {
                Default = new Style()
                {
                    Text = Ext.FromFloats(0.90f, 0.90f, 0.90f, 1.00f),
                    TextDisabled = Ext.FromFloats(0.60f, 0.60f, 0.60f, 1.00f),
                    WindowBackground = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.70f),
                    ChildBackground = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
                    PopupBackground = Ext.FromFloats(0.11f, 0.11f, 0.14f, 0.92f),
                    Border = Ext.FromFloats(0.50f, 0.50f, 0.50f, 0.50f),
                    BorderShadow = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
                    FrameBackground = Ext.FromFloats(0.43f, 0.43f, 0.43f, 0.39f),
                    FrameBackgroundHovered = Ext.FromFloats(0.47f, 0.47f, 0.69f, 0.40f),
                    FrameBackgroundActive = Ext.FromFloats(0.42f, 0.41f, 0.64f, 0.69f),
                    TitleBackground = Ext.FromFloats(0.27f, 0.27f, 0.54f, 0.83f),
                    TitleBackgroundActive = Ext.FromFloats(0.32f, 0.32f, 0.63f, 0.87f),
                    TitleBackgroundCollapsed = Ext.FromFloats(0.40f, 0.40f, 0.80f, 0.20f),
                    MenuBarBackground = Ext.FromFloats(0.40f, 0.40f, 0.55f, 0.80f),
                    ScrollbarBackground = Ext.FromFloats(0.20f, 0.25f, 0.30f, 0.60f),
                    ScrollbarGrab = Ext.FromFloats(0.40f, 0.40f, 0.80f, 0.30f),
                    ScrollbarGrabHovered = Ext.FromFloats(0.40f, 0.40f, 0.80f, 0.40f),
                    ScrollbarGrabActive = Ext.FromFloats(0.41f, 0.39f, 0.80f, 0.60f),
                    CheckMark = Ext.FromFloats(0.90f, 0.90f, 0.90f, 0.50f),
                    SliderGrab = Ext.FromFloats(1.00f, 1.00f, 1.00f, 0.30f),
                    SliderGrabActive = Ext.FromFloats(0.41f, 0.39f, 0.80f, 0.60f),
                    Button = Ext.FromFloats(0.35f, 0.40f, 0.61f, 0.62f),
                    ButtonHovered = Ext.FromFloats(0.40f, 0.48f, 0.71f, 0.79f),
                    ButtonActive = Ext.FromFloats(0.46f, 0.54f, 0.80f, 1.00f),
                    Header = Ext.FromFloats(0.40f, 0.40f, 0.90f, 0.45f),
                    HeaderHovered = Ext.FromFloats(0.45f, 0.45f, 0.90f, 0.80f),
                    HeaderActive = Ext.FromFloats(0.53f, 0.53f, 0.87f, 0.80f),
                    Separator = Ext.FromFloats(0.50f, 0.50f, 0.50f, 0.60f),
                    SeparatorHovered = Ext.FromFloats(0.60f, 0.60f, 0.70f, 1.00f),
                    SeparatorActive = Ext.FromFloats(0.70f, 0.70f, 0.90f, 1.00f),
                    ResizeGrip = Ext.FromFloats(1.00f, 1.00f, 1.00f, 0.16f),
                    ResizeGripHovered = Ext.FromFloats(0.78f, 0.82f, 1.00f, 0.60f),
                    ResizeGripActive = Ext.FromFloats(0.78f, 0.82f, 1.00f, 0.90f),                   
                    PlotLines = Ext.FromFloats(1.00f, 1.00f, 1.00f, 1.00f),
                    PlotLinesHovered = Ext.FromFloats(0.90f, 0.70f, 0.00f, 1.00f),
                    PlotHistogram = Ext.FromFloats(0.90f, 0.70f, 0.00f, 1.00f),
                    PlotHistogramHovered = Ext.FromFloats(1.00f, 0.60f, 0.00f, 1.00f),
                    TextSelectedBackground = Ext.FromFloats(0.00f, 0.00f, 1.00f, 0.35f),
                    DragDropTarget = Ext.FromFloats(1.00f, 1.00f, 0.00f, 0.90f),                    
                    NavWindowingHighlight = Ext.FromFloats(1.00f, 1.00f, 1.00f, 0.70f),
                    NavWindowingDimBackground = Ext.FromFloats(0.80f, 0.80f, 0.80f, 0.20f),
                    ModalWindowDimBackground = Ext.FromFloats(0.20f, 0.20f, 0.20f, 0.35f),
                };

                Default.Tab = Ext.Lerp(Default.Header, Default.TitleBackgroundActive, 0.80f);
                Default.TabHovered = Default.HeaderHovered;
                Default.TabActive = Ext.Lerp(Default.HeaderActive, Default.TitleBackgroundActive, 0.60f);
                Default.TabUnfocused = Ext.Lerp(Default.Tab, Default.TitleBackground, 0.80f);
                Default.TabUnfocusedActive = Ext.Lerp(Default.TabActive, Default.TitleBackground, 0.40f);
                Default.NavHighlight = Default.HeaderHovered;
            }
        }


        public static Style Default
        {
            get;
        }

        public static Style Dark
        {
            get;
        } 

        public static Style Light
        {
            get;
        } 

        #endregion

    }
}
