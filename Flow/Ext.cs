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
    }
}
