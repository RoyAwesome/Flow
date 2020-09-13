using System;
using System.Collections.Generic;
using System.Text;
using Watertight.FlowUI.Draw;

namespace Watertight.FlowUI
{
    public static partial class Flow
    {

        private static void NewFrameSanityCheck()
        {
            if(GlobalContext == null)
            {
                throw new FlowException("Global Context is null (did you Initialize?)");
            }

            if(GlobalContext.Initialized == false)
            {
                throw new FlowException("Context is not initialize (did you call Initialize?)");
            }

            if(GlobalContext.IsInFrame)
            {
                throw new FlowException("Cannot start a frame while a frame is recording!");
            }
        }

        private static void EndFrameSanityCheck()
        {
            if(GlobalContext == null)
            {
                throw new FlowException("Global Context is null (did you initialize?)");
            }
            if (GlobalContext.Initialized == false)
            {
                throw new FlowException("Context is not initialize (did you call Initialize?)");
            }

            if (!GlobalContext.IsInFrame)
            {
                throw new FlowException("Cannot start a end a frame that hasn't started!!");
            }
        }

        private static void EndFrame()
        {
            GlobalContext.LastEndFrameCount = GlobalContext.FrameCount;
            GlobalContext.IsInFrame = false;
        }

        private static void AddDrawListToDrawData(DrawList List, DrawData Data)
        {
            //TODO: Sanity Checks
            Data.DrawLists.Add(List);
        }

        private static void AddWindowToDrawData(FlowContext.Window Window, DrawData Data)
        {
            //TODO: Metrics

            AddDrawListToDrawData(Window.DrawList, Data);
        }

    }
}
