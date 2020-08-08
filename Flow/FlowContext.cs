using System;
using System.Collections.Generic;
using System.Text;

namespace Watertight.Flow
{
    public class FlowContext
    {
        internal FlowContext()
        {

        }

        public bool Initialized
        {
            get;
            private set;
        }

        internal uint FrameCount
        {
            get;
            set;
        }

        internal uint LastEndFrameCount
        {
            get;
            set;
        }

        internal uint LastRenderFrame
        {
            get;
            set;
        }

        internal bool IsInFrame
        {
            get;
            set;
        }



        internal void Initialize()
        {
            Initialized = true;
            FrameCount = 0;
            LastEndFrameCount = 0;
            IsInFrame = false;
        }

        internal void ResetForNewFrame()
        {

        }
    }
}
