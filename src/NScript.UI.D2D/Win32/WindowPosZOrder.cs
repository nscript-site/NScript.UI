using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    public static class WindowPosZOrder
    {
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
    }
}
