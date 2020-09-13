using System;
using System.Collections.Generic;
using System.Text;

namespace Watertight.FlowUI
{
    public static partial class Flow
    {
        public static void TestDrawing()
        {
            Begin(FlowContext.DefaultWindowName);

            GetCurrentDrawList().AddRect(new System.Numerics.Vector2(50, 50), new System.Numerics.Vector2(100, 100), System.Drawing.Color.White, 0);

            End();
        }

    }
}
