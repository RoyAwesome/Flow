using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Watertight.FlowUI.Draw;
using static Watertight.FlowUI.FlowContext;

namespace Watertight.FlowUI
{
    public static partial class Flow
    {
        public static bool Begin(string Name)
        {
            bool discard;
            return Begin(Name, out discard);
        }

        public static bool Begin()
        {
            return Begin(FlowContext.DefaultWindowName);
        }

        public static bool Begin(string Name, out bool OpenButton)
        {
            Window w = GlobalContext.GetOrCreateWindow(Name);
            GlobalContext.WindowStack.Push(w);

            bool FirstBeginOfFrame = w.LastActiveFrame != GlobalContext.FrameCount;

            if(FirstBeginOfFrame)
            {
                w.LastActiveFrame = GlobalContext.FrameCount;
            }

            if(GlobalContext.NextWindow.NextWindowFlags.HasFlag(ENextWindowFlags.HasPosition))
            {

            }
           
            //TODO: Appearing, Focus, etc

            //Initialize this window if it's the first time we are begining this window this frame
            if(FirstBeginOfFrame)
            {
                InitWindowForFrame(w);

                //TODO: Size this window to content from last frame
                //For now, just use the default size

                Rectangle OuterRectable = w.Rect;
               

                w.SizeFull = /*GlobalContext.CurrentStyle.WindowMinSize*/ new System.Numerics.Vector2(100,100);
                w.Size = w.SizeFull;

                DrawWindowDecorations(w);
            }


            GlobalContext.ConsumeNextWindowData();
            OpenButton = true;
            return true;
        }

        private static void InitWindowForFrame(Window window)
        {
            window.Active = true;
            window.DrawList.ResetForNewFrame();
            window.WindowRounding = GlobalContext.CurrentStyle.WindowRounding;
        }

        private static void DrawWindowDecorations(Window window)
        {

            if(window.Collasped)
            {
                //Draw the title bar
                float backupBorderSize = CurrentStyle.FrameBorderSize;
                CurrentStyle.FrameBorderSize = window.WindowBorderSize;
                Rectangle titleBarRect = window.TitleBarRect;

                RenderFrame(titleBarRect.MinPoint, titleBarRect.MaxPoint, CurrentStyle.TitleBackgroundCollapsed, true, window.WindowRounding);

                CurrentStyle.FrameBorderSize = backupBorderSize;

            }
            else
            {
                if(!window.WindowBehaviorFlags.HasFlag(EWindowBehaviorFlags.NoBackground))
                {
                    DrawWindowBackground(window);
                }             
                
                if(!window.WindowBehaviorFlags.HasFlag(EWindowBehaviorFlags.NoTitleBar))
                {
                    DrawTitleBar(window);
                }
            }

        }

        private static void DrawWindowBackground(Window window)
        {
            Style style = GlobalContext.CurrentStyle;

            Color bgColor = style.WindowBackground;
            //Todo: Override the window color if it's set

            float TitleBarHeight = style.FramePadding.Y * 2.0f;

            Rectangle DrawRect = Rectangle.FromMinMax(window.Position + new System.Numerics.Vector2(0, TitleBarHeight),
                window.Position + window.Size);

            window.DrawList.AddRectFilled(DrawRect.MinPoint, 
                DrawRect.MaxPoint, 
                bgColor, 
                window.WindowRounding,
                window.WindowBehaviorFlags.HasFlag(EWindowBehaviorFlags.NoTitleBar) ? EDrawCornerFlags.All : EDrawCornerFlags.Bot);
        }

        private static void DrawTitleBar(Window w)
        {
            Style style = GlobalContext.CurrentStyle;
            Rectangle titleBarRect = w.TitleBarRect;

            w.DrawList.AddRectFilled(titleBarRect.MinPoint,
                titleBarRect.MaxPoint,
                style.TitleBackground,
                w.WindowRounding,
                EDrawCornerFlags.Top);
        }

        public static void End()
        {
            if(GlobalContext.WindowStack.Count == 0)
            {
                throw new FlowException("Cannot End the current window as there is no window pushed into the window stack!");
            }

            Window w = GlobalContext.WindowStack.Pop();
        }
             

        public static DrawList GetCurrentDrawList()
        {
            return GlobalContext.CurrentWindow.DrawList;
        }

    }
}
