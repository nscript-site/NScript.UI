using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    using NScript.UI.Media;

    public struct MSG
    {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public POINT pt;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(Rect rect)
        {
            left = (int)rect.X;
            top = (int)rect.Y;
            right = (int)(rect.X + rect.Width);
            bottom = (int)(rect.Y + rect.Height);
        }
    }
}
