using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI
{
    public class BaseEventArgs : EventArgs
    {
        public bool Handled { get; set; }
    }
}
