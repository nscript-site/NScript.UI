using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x = 0, int y = 0)
        {
            X = x; Y = y;
        }
        public Point(float x = 0, float y = 0, float scale = 1.0f)
        {
            X = (int)(x * scale); Y = (int)(y * scale);
        }
    }

    public struct PointF
    {
        public float X;
        public float Y;

        public PointF(float x = 0, float y = 0)
        {
            X = x;Y = y;
        }

        public static PointF operator +(PointF a, PointF b)
        {
            return new PointF(a.X + b.X, a.Y + b.Y);
        }

        public static PointF operator -(PointF a, PointF b)
        {
            return new PointF(a.X - b.X, a.Y - b.Y);
        }

        public static PointF operator *(PointF a, float b)
        {
            return new PointF(a.X *b, a.Y * b);
        }
    }
}
