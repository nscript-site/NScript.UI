using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Input
{
    public class ScrollEventArgs : BaseEventArgs
    {
        public ScrollDirection Direction { get; set; }
        public float Value { get; set; }
    }
}
