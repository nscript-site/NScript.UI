using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    public struct Size
    {
        public int Width;
        public int Height;

        public Size(int width = 0, int height = 0)
        {
            Width = width;Height = height;
        }

        public Size(float width = 0, float height = 0)
        {
            Width = (int)width; Height = (int)height;
        }

        public static bool operator ==(Size a, Size b)
        {
            return a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(Size a, Size b)
        {
            return !(a==b);
        }
    }

    public struct SizeF
    {
        public static SizeF Infinity = new SizeF(float.MaxValue, float.MaxValue);

        public float Width;
        public float Height;

        public SizeF(float width = 0, float height = 0)
        {
            Width = width; Height = height;
        }

        public SizeF(float width = 0, float height = 0, float scale = 1.0f)
        {
            Width = width * scale; Height = height * scale;
        }

        public static SizeF operator +(SizeF a, SizeF b)
        {
            return new SizeF(a.Width + b.Width, a.Height + b.Height);
        }

        public static SizeF operator -(SizeF a, SizeF b)
        {
            return new SizeF(a.Width - b.Width, a.Height - b.Height);
        }

        public static SizeF operator *(SizeF a, float b)
        {
            return new SizeF(a.Width * b, a.Height * b);
        }
    }
}
