using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Watertight.FlowUI
{
    public class Style
    {

        #region Styles Control

        #endregion

        #region Colors

        public Color Text
        {
            get;
            set;
        }
        public Color DisabledText
        {
            get;
            set;
        }

        public Color WindowBackground
        {
            get;
            set;
        }

        public Color ChildWindowBackground
        {
            get;
            set;
        }

        public Color PopupBackground
        {
            get;
            set;
        }

        public Color Border
        {
            get;
            set;
        }
        #endregion


        #region Built In Styles

        public static Style Default
        {
            get;
        } = new Style()
        {
            Text                        = Ext.FromFloats(0.90f, 0.90f, 0.90f, 1.00f),
            DisabledText                = Ext.FromFloats(0.60f, 0.60f, 0.60f, 1.00f),
            WindowBackground            = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.70f),
            ChildWindowBackground       = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
            PopupBackground             = Ext.FromFloats(0.11f, 0.11f, 0.14f, 0.92f),
            Border                      = Ext.FromFloats(0.50f, 0.50f, 0.50f, 0.50f),
        };

        public static Style Dark
        {
            get;
        } = new Style()
        {
            Text                        = Ext.FromFloats(1.00f, 1.00f, 1.00f, 1.00f),
            DisabledText                = Ext.FromFloats(0.50f, 0.50f, 0.50f, 1.00f),
            WindowBackground            = Ext.FromFloats(0.06f, 0.06f, 0.06f, 0.94f),
            ChildWindowBackground       = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
            PopupBackground             = Ext.FromFloats(0.08f, 0.08f, 0.08f, 0.94f),
            Border                      = Ext.FromFloats(0.43f, 0.43f, 0.50f, 0.50f),
        };

        public static Style Light
        {
            get;
        } = new Style()
        {
            Text                        = Ext.FromFloats(0.00f, 0.00f, 0.00f, 1.00f),
            DisabledText                = Ext.FromFloats(0.60f, 0.60f, 0.60f, 1.00f),
            WindowBackground            = Ext.FromFloats(0.94f, 0.94f, 0.94f, 1.00f),
            ChildWindowBackground       = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.00f),
            PopupBackground             = Ext.FromFloats(1.00f, 1.00f, 1.00f, 0.98f),
            Border                      = Ext.FromFloats(0.00f, 0.00f, 0.00f, 0.30f),
        };

        #endregion

    }
}
