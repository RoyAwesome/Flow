using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace Watertight.FlowUI
{
    public static partial class Flow
    {
        public static void TestDrawing()
        {
            Begin(FlowContext.DefaultWindowName);

            //GetCurrentDrawList().AddRect(new System.Numerics.Vector2(50, 50), new System.Numerics.Vector2(100, 100), System.Drawing.Color.White, 0);

            End();
        }

        internal static void RenderFrame(Vector2 Min, Vector2 Max, Color FillColor, bool border = true, float rounding = 0.0f)
        {
            FlowContext.Window w = GlobalContext.CurrentWindow;

            w.DrawList.AddRectFilled(Min, Max, FillColor, rounding);
            float border_size = CurrentStyle.FrameBorderSize;
            if(border && border_size > 0.0f)
            {
                w.DrawList.AddRect(Min + Vector2.One, Max + Vector2.One, CurrentStyle.BorderShadow, rounding, border_size);
                w.DrawList.AddRect(Min, Max, CurrentStyle.Border, rounding, border_size);
            }
        }

        internal static void RenderFrameBorder(Vector2 Min, Vector2 Max, float rounding)
        {
            FlowContext.Window w = GlobalContext.CurrentWindow;

            float border_size = CurrentStyle.FrameBorderSize;
            if (border_size > 0.0f)
            {
                w.DrawList.AddRect(Min + Vector2.One, Max + Vector2.One, CurrentStyle.BorderShadow, rounding, border_size);
                w.DrawList.AddRect(Min, Max, CurrentStyle.Border, rounding, border_size);
            }
        }

    }
}
