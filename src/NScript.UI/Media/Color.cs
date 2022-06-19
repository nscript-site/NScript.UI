using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    public struct Color
    {
        public static readonly Color RED = new Color(0,0,1.0f);
        public static readonly Color BLUE = new Color(1.0f, 0, 0f);
        public static readonly Color GREEN = new Color(0f, 1.0f, 0f);
        public static readonly Color WHITE = new Color(1.0f, 1.0f, 1.0f);
        public static readonly Color BLACK = new Color(0f, 0f, 0f);
        public static readonly Color TRANSPARENT = new Color(0,0,0,0);

        public float Blue;
        public float Green;
        public float Red;
        public float Alpha;

        public Color(float blue, float green, float red, float alpha = 1.0f)
        {
            Blue = blue;
            Green = green;
            Red = red;
            Alpha = alpha;
        }

        public Color(uint value) : this(
                (value & 0xff) / 255.0f,
                ((value >> 8) & 0xff) / 255.0f,
                ((value >> 16) & 0xff) / 255.0f,
                ((value >> 24) & 0xff) / 255.0f
            )
        { }

        public Color(uint value, float alpha) : this(
            (value & 0xff) / 255.0f,
            ((value >> 8) & 0xff) / 255.0f,
            ((value >> 16) & 0xff) / 255.0f,
            alpha / 255.0f
        )
        { }
    }
}
