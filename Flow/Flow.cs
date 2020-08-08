using System;
using System.ComponentModel.Design;
using System.Runtime.Serialization;
using Watertight.Flow.Draw;

namespace Watertight.Flow
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
            GetDrawData().Invalidate();

        }      

        public static void Render()
        {
            EndFrameSanityCheck();
            EndFrame();
            
            
            GlobalContext.LastRenderFrame = GlobalContext.FrameCount;
            DrawData DL = GetDrawData();
            DL.Reset();
            
        }

        public static DrawData GetDrawData()
        {
            return DrawLists[GlobalContext.LastRenderFrame % DrawLists.Length];
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
