using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    public struct Rect
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width;
        public int Height;

        public int Right => X + Width;
        public int Bottom => Y + Height;
        public int Top => Y;
        public int Left => X;

        public Rect(int x = 0, int y = 0, int width = 0, int height = 0)
        {
            X = x; Y = y;
            Width = width; Height = height;
        }

        public Rect(float x = 0, float y = 0, float width = 0, float height = 0)
        {
            X = (int)x; Y = (int)y;
            Width = (int)width; Height = (int)height;
        }

        public static Rect operator +(Rect a, Point b)
        {
            return new Rect(a.X + b.X, a.Y + b.Y, a.Width, a.Height);
        }

        public static Rect operator -(Rect a, Point b)
        {
            return new Rect(a.X - b.X, a.Y - b.Y, a.Width, a.Height);
        }

        public RectF ToRect()
        {
            return new RectF(X, Y, Width, Height);
        }
    }

    public struct RectF
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width;
        public float Height;

        public float Right => X + Width;
        public float Bottom => Y + Height;
        public float Top => Y;
        public float Left => X;

        public RectF(float x = 0, float y = 0, float width = 0, float height = 0)
        {
            X = x; Y = y;
            Width = width; Height = height;
        }

        public PointF Location
        {
            get { return new PointF(X, Y); }
        }

        public SizeF Size
        {
            get { return new SizeF(Width, Height); }
        }

        public static RectF operator +(RectF a, PointF b)
        {
            return new RectF(a.X + b.X, a.Y + b.Y, a.Width, a.Height);
        }

        public static RectF operator -(RectF a, PointF b)
        {
            return new RectF(a.X - b.X, a.Y - b.Y, a.Width, a.Height);
        }

        public Rect ToRect()
        {
            return new Rect(X, Y, Width, Height);
        }
    }
}
