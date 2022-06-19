using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    public class Brush
    {
        public float Opacity { get; set; } = 1.0f;
    }

    public class SolidColorBrush : Brush
    {
        public Color Color { get; set; }

        public SolidColorBrush() { }

        public SolidColorBrush(Color color)
        {
            Color = color;
        }

        public SolidColorBrush(uint color)
        {
            Color = new Color(color);
        }
    }
}
