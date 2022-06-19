using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    using SharpDX.Direct2D1;

    public class D2DPen
    {
        public Brush Brush { get; set; }
        public float StrokeWidth { get; set; }
        public StrokeStyle StrokeStyle { get; set; }
    }
}
