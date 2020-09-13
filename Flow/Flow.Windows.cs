using System;
using System.Collections.Generic;
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
           
            //TODO: Appearing, Focus, etc

            //Initialize this window if it's the first time we are begining this window this frame
            if(FirstBeginOfFrame)
            {
                InitWindowForFrame(w);
                DrawWindowBackground(w);
            }

            OpenButton = true;
            return true;
        }

        private static void InitWindowForFrame(Window window)
        {
            window.Active = true;
            window.DrawList.ResetForNewFrame();
        }

        private static void DrawWindowBackground(Window window)
        {
            window.DrawList.AddRectFilled(new System.Numerics.Vector2(50, 50), new System.Numerics.Vector2(100, 100), GlobalContext.CurrentStyle.WindowBackground, 0);
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
