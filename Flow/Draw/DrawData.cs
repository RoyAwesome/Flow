using System;
using System.Collections.Generic;
using System.Text;

namespace Watertight.Flow.Draw
{
    public class DrawData
    {
        
        public bool Valid
        {
            get;
            internal set;
        }

        internal void Invalidate()
        {
            Valid = false;
        }

        internal void Reset()
        {
            Valid = true;

        }
    }
}
