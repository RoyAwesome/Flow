using System;
using System.Collections.Generic;
using System.Text;

namespace Watertight.FlowUI.Draw
{
    public class DrawData
    {
        
        public bool Valid
        {
            get;
            internal set;
        }

        public List<DrawList> DrawLists
        {
            get;
        } = new List<DrawList>();
                
        internal void Invalidate()
        {
            Valid = false;
            DrawLists.Clear();
        }

        internal void ResetForRenderFrame()
        {
            Valid = true;

        }

        
    }
}
