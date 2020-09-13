using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Watertight.FlowUI
{
    internal static class Ext
    {
        public static Color FromFloats(float r, float g, float b, float a)
        {
            return Color.FromArgb((int)(255 * a), (int)(255 * r), (int)(255 * g), (int)(255 * b));
        }

        public static Color FromFloats(float r, float g, float b)
        {
            return FromFloats(r, g, b, 1.0f);
        }

        public static int Lerp(int a, int b, float alpha)
        {
            //return (1 - t) * v0 + t * v1;
            return (int)((1 - alpha) * a + alpha * b);
        }

        public static Color Lerp(Color A, Color B, float alpha)
        {
            return Color.FromArgb(
                Lerp(A.A, B.A, alpha),
                Lerp(A.R, B.R, alpha),
                Lerp(A.G, B.G, alpha),
                Lerp(A.B, B.B, alpha));
        }
    }
}
