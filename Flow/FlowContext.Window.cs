using System;
using System.Collections.Generic;
using System.Text;
using Watertight.FlowUI.Draw;

namespace Watertight.FlowUI
{
    public partial class FlowContext
    {
        public const string DefaultWindowName = "##DefaultWindow";

        List<Window> AllWindows
        {
            get;
        } = new List<Window>();

        internal Stack<Window> WindowStack
        {
            get;
        } = new Stack<Window>();

        internal Window FallbackWindow;

        internal Window CurrentWindow
        {
            get
            {
                Window RetWindow = null;
                if (WindowStack.TryPeek(out RetWindow))
                {
                    return RetWindow;
                }

                return FallbackWindow;
            }
        }

        internal Window GetWindowByName(string Name)
        {
            int id = Name.GetHashCode();
            for (int i = 0; i < AllWindows.Count; i++)
            {
                if (AllWindows[i].NameId == id)
                {
                    return AllWindows[i];
                }
            }

            return null;
        }

        internal Window GetOrCreateWindow(string Name)
        {
            Window ret = GetWindowByName(Name);
            if (ret != null)
            {
                return ret;
            }

            //Otherwise create the window
            ret = new Window(Name, SharedData);
            AllWindows.Add(ret);
            return ret;
        }

        internal class Window
        {
            public Window(string Name, DrawListSharedData sharedData)
            {
                DrawList = new DrawList(sharedData);
                this.Name = Name;
                NameId = Name.GetHashCode();
            }

            public DrawList DrawList
            {
                get;
                private set;
            }

            public string Name
            {
                get;
                private set;
            }

            internal int NameId
            {
                get;
                private set;
            }

            internal uint LastActiveFrame
            {
                get;
                set;
            }

            internal bool Active
            {
                get;
                set;
            }
        }
    }
}

