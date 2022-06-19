using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    public sealed class RadialGradientBrush : GradientBrush
    {
        public PointF Center { get; set; }
        public float Radius { get; set; }
    }
}
