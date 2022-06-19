using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WNDCLASSEX
    {
        [MarshalAs(UnmanagedType.U4)]
        public int cbSize;
        [MarshalAs(UnmanagedType.U4)]
        public ClassStyles style;
        public IntPtr lpfnWndProc; // not WndProc
        public int cbClsExtra;
        public int cbWndExtra;
        public IntPtr hInstance;
        public IntPtr hIcon;
        public IntPtr hCursor;
        public IntPtr hbrBackground;
        public IntPtr lpszMenuName;
        public IntPtr lpszClassName;
        public IntPtr hIconSm;

        //Use this function to make a new one with cbSize already filled in.
        //For example:
        //var WndClss = WNDCLASSEX.Build()
        public static WNDCLASSEX Build()
        {
            var nw = new WNDCLASSEX();
            nw.cbSize = Marshal.SizeOf(typeof(WNDCLASSEX));
            return nw;
        }
    }
}
