using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Watertight.FlowUI.Draw;

namespace Watertight.FlowUI
{
    public partial class FlowContext
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

        DrawListSharedData SharedData
        {
            get;
        } = new DrawListSharedData();



        internal void Initialize()
        {
            Initialized = true;
            FrameCount = 0;
            LastEndFrameCount = 0;
            IsInFrame = false;

            FallbackWindow = GetOrCreateWindow(DefaultWindowName);
        }

        internal void ResetForNewFrame()
        {

        }

    }
}
