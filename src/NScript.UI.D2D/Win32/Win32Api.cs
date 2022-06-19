using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    using NScript.UI.Media;
    using NScript.UI.Input;
    using SharpDX;

    public sealed partial class Win32Api
    {
        public static ushort LOWORD(uint value) { return (ushort)(value & 0xFFFF); }
        public static ushort HIWORD(uint value) { return (ushort)(value >> 16); }
        public static byte LOWBYTE(ushort value) { return (byte)(value & 0xFF); }
        public static byte HIGHBYTE(ushort value) { return (byte)(value >> 8); }

        public static int ToInt32(IntPtr ptr)
        {
            if (IntPtr.Size == 4) return ptr.ToInt32();
            return (int)(ptr.ToInt64() & 0xffffffff);
        }

        public static Size ParseParamToSize(IntPtr lParam)
        {
            uint lParamVal = (uint)lParam.ToInt32();
            return new Size(Win32Api.LOWORD(lParamVal), Win32Api.HIWORD(lParamVal));
        }

        public static SizeF ParseParamToSizeF(IntPtr lParam)
        {
            uint lParamVal = (uint)lParam.ToInt32();
            return new SizeF(Win32Api.LOWORD(lParamVal), Win32Api.HIWORD(lParamVal));
        }

        public static PointF ParseParamToPointF(IntPtr lParam)
        {
            uint lParamVal = (uint)lParam.ToInt32();
            return new PointF(Win32Api.LOWORD(lParamVal), Win32Api.HIWORD(lParamVal));
        }

        /// <summary>
        /// 获取鼠标位置的屏幕坐标
        /// </summary>
        /// <returns></returns>
        public static PointF GetCursorPos()
        {
            POINT p;
            GetCursorPos(out p);
            PointF pos = new PointF(p.X, p.Y);
            return pos;
        }

        public static short GetWheelDelta(IntPtr wParam)
        {
            unchecked
            {
                return ((short)(wParam.ToInt64() >> 16));
            }
        }

        /// <summary>
        /// 获取主显示器的屏幕尺寸
        /// </summary>
        /// <returns></returns>
        public static Size GetPrimaryScreenSize()
        {
            int width = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
            int height = GetSystemMetrics(SystemMetric.SM_CYSCREEN);
            return new Size(width, height);
        }

        public static void SetCursor(IntPtr hwnd, Cursors cursor)
        {
            var hCursor = LoadCursor(IntPtr.Zero, cursor.ToSystemCursor());

            Win32Api.SetClassLong(hwnd, ClassLongIndex.GCL_HCURSOR, hCursor);
            SetCursor(hCursor);
            DestroyCursor(hCursor);
        }

        public static PointF ScreenToClient(IntPtr hwnd, PointF p)
        {
            POINT pTmp = new POINT((int)p.X, (int)p.Y);
            ScreenToClient(hwnd, ref pTmp);
            return new PointF(pTmp.X, pTmp.Y);
        }

        public static PointF ClientToScreen(IntPtr hwnd, PointF p)
        {
            POINT pTmp = new POINT((int)p.X, (int)p.Y);
            ClientToScreen(hwnd, ref pTmp);
            return new PointF(pTmp.X, pTmp.Y);
        }

        public static IntPtr CreateComInstance(Guid clsid, Guid riid)
        {
            IntPtr pointer;
            var result = CoCreateInstance(clsid, IntPtr.Zero, Win32.CLSCTX.ClsctxInprocServer, riid, out pointer);
            result.CheckError();
            return pointer;
        }
    }
}
