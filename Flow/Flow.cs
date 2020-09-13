using System;
using System.ComponentModel.Design;
using System.Runtime.Serialization;
using Watertight.FlowUI.Draw;
using Watertight.FlowUI.Draw;

namespace Watertight.FlowUI
{
    public static partial class Flow
    {        
        private static FlowContext GlobalContext
        {
            get;
            set;
        }

        private static DrawData[] DrawLists
        {
            get;
            set;
        }

        public static DrawData DrawData
        {
            get
            {
                return DrawLists[GlobalContext.LastRenderFrame % DrawLists.Length];
            }
        }


        static Flow()
        {
            Initialize();
        }

        public static void Initialize()
        {
            if(GlobalContext != null)
            {
                throw new FlowException("Flow has already been initialized!");
               
            }

            GlobalContext = new FlowContext();
            GlobalContext.Initialize();

            DrawLists = new DrawData[]
            {
                new DrawData(),
                new DrawData(),
            };
        }

        public static void NewFrame()
        {
            NewFrameSanityCheck();

            GlobalContext.IsInFrame = true;
            GlobalContext.FrameCount += 1;
  
            GlobalContext.ResetForNewFrame();

            //Invalidate the current frame, as we are building a new one
            DrawData.Invalidate();

        }      

        public static void Render()
        {
            EndFrameSanityCheck();
            EndFrame();
            
            
            GlobalContext.LastRenderFrame = GlobalContext.FrameCount;
            DrawData.ResetForRenderFrame();

            //TODO: Add all windows to the draw data

            AddWindowToDrawData(GlobalContext.FallbackWindow, DrawData);
        }
             
    }

    public class FlowException : Exception
    {
        public FlowException()
        {
        }

        public FlowException(string message) : base(message)
        {
        }

        public FlowException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FlowException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
