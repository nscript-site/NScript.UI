using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    using IComDataObject = System.Runtime.InteropServices.ComTypes.IDataObject;

    internal static class SafeNativeMethods
    {

        [DllImport("shlwapi.dll")]
        public static extern int SHAutoComplete(HandleRef hwndEdit, int flags);

        /*
        #if DEBUG
                [DllImport(ExternDll.Shell32, EntryPoint="SHGetFileInfo")]
                private static extern IntPtr IntSHGetFileInfo([MarshalAs(UnmanagedType.LPWStr)]string pszPath, int dwFileAttributes, NativeMethods.SHFILEINFO info, int cbFileInfo, int flags);
                public static IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, NativeMethods.SHFILEINFO info, int cbFileInfo, int flags) {
                    IntPtr newHandle = IntSHGetFileInfo(pszPath, dwFileAttributes, info, cbFileInfo, flags);
                    validImageListHandles.Add(newHandle);
                    return newHandle;
                }
        #else
                [DllImport(ExternDll.Shell32, CharSet=CharSet.Auto)]
                public static extern IntPtr SHGetFileInfo(string pszPath, int dwFileAttributes, NativeMethods.SHFILEINFO info, int cbFileInfo, int flags);
        #endif
        */
        [DllImport(ExternDll.User32)]
        public static extern int OemKeyScan(short wAsciiVal);


        [DllImport(ExternDll.Gdi32)]
        public static extern int GetSystemPaletteEntries(HandleRef hdc, int iStartIndex, int nEntries, byte[] lppe);
        [DllImport(ExternDll.Gdi32)]
        public static extern int GetDIBits(HandleRef hdc, HandleRef hbm, int uStartScan, int cScanLines, byte[] lpvBits, ref NativeMethods.BITMAPINFO_FLAT bmi, int uUsage);
        [DllImport(ExternDll.Gdi32)]
        public static extern int StretchDIBits(HandleRef hdc, int XDest, int YDest, int nDestWidth, int nDestHeight, int XSrc, int YSrc, int nSrcWidth, int nSrcHeight, byte[] lpBits, ref NativeMethods.BITMAPINFO_FLAT lpBitsInfo, int iUsage, int dwRop);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateCompatibleBitmap", CharSet = CharSet.Auto)]
        public static extern IntPtr IntCreateCompatibleBitmap(HandleRef hDC, int width, int height);
        //public static IntPtr CreateCompatibleBitmap(HandleRef hDC, int width, int height)
        //{
        //    return System.Internal.HandleCollector.Add(IntCreateCompatibleBitmap(hDC, width, height), NativeMethods.CommonHandles.GDI);
        //}

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool GetScrollInfo(HandleRef hWnd, int fnBar, [In, Out] NativeMethods.SCROLLINFO si);

        [DllImport(ExternDll.Ole32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool IsAccelerator(HandleRef hAccel, int cAccelEntries, [In] ref NativeMethods.MSG lpMsg, short[] lpwCmd);
        [DllImport(ExternDll.Comdlg32, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ChooseFont([In, Out] NativeMethods.CHOOSEFONT cf);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetBitmapBits(HandleRef hbmp, int cbBuffer, byte[] lpvBits);
        [DllImport(ExternDll.Comdlg32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int CommDlgExtendedError();
        [DllImport(ExternDll.Oleaut32, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern void SysFreeString(HandleRef bstr);

        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)]string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);
        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)]string caption, int objects, [MarshalAs(UnmanagedType.Interface)] ref object pobjs, int pages, Guid[] pClsid, int locale, int reserved1, IntPtr reserved2);
        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        public static extern void OleCreatePropertyFrame(HandleRef hwndOwner, int x, int y, [MarshalAs(UnmanagedType.LPWStr)]string caption, int objects, HandleRef lplpobjs, int pages, HandleRef pClsid, int locale, int reserved1, IntPtr reserved2);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)]string pszFile, int uCommand, int dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)]string pszFile, int uCommand, string dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)]string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)]NativeMethods.HH_POPUP dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)]string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)]NativeMethods.HH_FTS_QUERY dwData);
        [DllImport(ExternDll.Hhctrl, CharSet = CharSet.Auto)]
        public static extern int HtmlHelp(HandleRef hwndCaller, [MarshalAs(UnmanagedType.LPTStr)]string pszFile, int uCommand, [MarshalAs(UnmanagedType.LPStruct)]NativeMethods.HH_AKLINK dwData);
        [DllImport(ExternDll.Oleaut32)]
        public static extern void VariantInit(HandleRef pObject);
        [DllImport(ExternDll.Oleaut32, PreserveSig = false)]
        public static extern void VariantClear(HandleRef pObject);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool LineTo(HandleRef hdc, int x, int y);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool MoveToEx(HandleRef hdc, int x, int y, NativeMethods.POINT pt);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool Rectangle(
                                           HandleRef hdc, int left, int top, int right, int bottom);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool PatBlt(HandleRef hdc, int left, int top, int width, int height, int rop);

        [DllImport(ExternDll.Kernel32, EntryPoint = "GetThreadLocale", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetThreadLCID();

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetMessagePos();

        [DllImport(ExternDll.User32, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int RegisterClipboardFormat(string format);
        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetClipboardFormatName(int format, StringBuilder lpString, int cchMax);

        [DllImport(ExternDll.Comdlg32, SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ChooseColor([In, Out] NativeMethods.CHOOSECOLOR cc);
        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int RegisterWindowMessage(string msg);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "DeleteObject", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ExternalDeleteObject(HandleRef hObject);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "DeleteObject", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        internal static extern bool IntDeleteObject(HandleRef hObject);
        //public static bool DeleteObject(HandleRef hObject)
        //{
        //    System.Internal.HandleCollector.Remove((IntPtr)hObject, NativeMethods.CommonHandles.GDI);
        //    return IntDeleteObject(hObject);
        //}

        [DllImport(ExternDll.Oleaut32, EntryPoint = "OleCreateFontIndirect", ExactSpelling = true, PreserveSig = false)]
        public static extern SafeNativeMethods.IFontDisp OleCreateIFontDispIndirect(NativeMethods.FONTDESC fd, ref Guid iid);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, EntryPoint = "CreateSolidBrush", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr IntCreateSolidBrush(int crColor);
        //public static IntPtr CreateSolidBrush(int crColor)
        //{
        //    return System.Internal.HandleCollector.Add(IntCreateSolidBrush(crColor), NativeMethods.CommonHandles.GDI);
        //}
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SetWindowExtEx(HandleRef hDC, int x, int y, [In, Out] NativeMethods.SIZE size);

        [DllImport(ExternDll.Kernel32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int FormatMessage(int dwFlags, HandleRef lpSource, int dwMessageId,
                                               int dwLanguageId, StringBuilder lpBuffer, int nSize, HandleRef arguments);


        [DllImport(ExternDll.Comctl32)]
        public static extern void InitCommonControls();

        [DllImport(ExternDll.Comctl32)]
        public static extern bool InitCommonControlsEx(NativeMethods.INITCOMMONCONTROLSEX icc);

#if DEBUG
        private static System.Collections.ArrayList validImageListHandles = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList());
#endif

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool TrackPopupMenuEx(HandleRef hmenu, int fuFlags, int x, int y, HandleRef hwnd, NativeMethods.TPMPARAMS tpm);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetKeyboardLayout(int dwLayout);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr ActivateKeyboardLayout(HandleRef hkl, int uFlags);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetKeyboardLayoutList(int size, [Out, MarshalAs(UnmanagedType.LPArray)] IntPtr[] hkls);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref NativeMethods.DEVMODE lpDevMode);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out]NativeMethods.MONITORINFOEX info);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern IntPtr MonitorFromPoint(NativeMethods.POINTSTRUCT pt, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern IntPtr MonitorFromRect(ref NativeMethods.RECT rect, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern IntPtr MonitorFromWindow(HandleRef handle, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true)]
        public static extern bool EnumDisplayMonitors(HandleRef hdc, NativeMethods.COMRECT rcClip, NativeMethods.MonitorEnumProc lpfnEnum, IntPtr dwData);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool AdjustWindowRectEx(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle);

        // This API is available only starting Windows 10 RS1 
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool AdjustWindowRectExForDpi(ref NativeMethods.RECT lpRect, int dwStyle, bool bMenu, int dwExStyle, uint dpi);

        //[DllImport(ExternDll.Ole32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //public static extern int DoDragDrop(IComDataObject dataObject, UnsafeNativeMethods.IOleDropSource dropSource, int allowedEffects, int[] finalEffect);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr GetSysColorBrush(int nIndex);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool EnableWindow(HandleRef hWnd, bool enable);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool GetClientRect(HandleRef hWnd, [In, Out] ref NativeMethods.RECT rect);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetDoubleClickTime();
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetUpdateRgn(HandleRef hwnd, HandleRef hrgn, bool fErase);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ValidateRect(HandleRef hWnd, [In, Out] ref NativeMethods.RECT rect);

        //
        // WARNING: Don't uncomment this code unless you absolutelly need it.  Use instead Marshal.GetLastWin32Error
        // and mark your PInvoke [DllImport(..., SetLastError=true)]
        // From MSDN:
        // GetLastWin32Error exposes the Win32 GetLastError API method from Kernel32.DLL. This method exists because 
        // it is not safe to make a direct platform invoke call to GetLastError to obtain this information. If you 
        // want to access this error code, you must call GetLastWin32Error rather than writing your own platform invoke 
        // definition for GetLastError and calling it. The common language runtime can make internal calls to APIs that 
        // overwrite the operating system maintained GetLastError.
        //
        //[DllImport(ExternDll.Kernel32, ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        //public extern static int GetLastError();

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int FillRect(HandleRef hdc, [In] ref NativeMethods.RECT rect, HandleRef hbrush);
        
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool IsWindowEnabled(HandleRef hWnd);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool IsWindowVisible(HandleRef hWnd);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ReleaseCapture();
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetCurrentThreadId();

        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);
        internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool GetExitCodeThread(HandleRef hWnd, out int lpdwExitCode);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ShowWindow(HandleRef hWnd, int nCmdShow);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool SetWindowPos(HandleRef hWnd, HandleRef hWndInsertAfter,
                                               int x, int y, int cx, int cy, int flags);

        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetWindowTextLength(HandleRef hWnd);
        // this is a wrapper that comctl exposes for the NT function since it doesn't exist natively on 95.
        [DllImport(ExternDll.Comctl32, ExactSpelling = true), CLSCompliantAttribute(false)]
        private static extern bool _TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme);
        public static bool TrackMouseEvent(NativeMethods.TRACKMOUSEEVENT tme)
        {
            // only on NT - not on 95 - comctl32 has a wrapper for 95 and NT.
            return _TrackMouseEvent(tme);
        }
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool RedrawWindow(HandleRef hwnd, ref NativeMethods.RECT rcUpdate, HandleRef hrgnUpdate, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool RedrawWindow(HandleRef hwnd, NativeMethods.COMRECT rcUpdate, HandleRef hrgnUpdate, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool InvalidateRect(HandleRef hWnd, ref NativeMethods.RECT rect, bool erase);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool InvalidateRect(HandleRef hWnd, NativeMethods.COMRECT rect, bool erase);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool InvalidateRgn(HandleRef hWnd, HandleRef hrgn, bool erase);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool UpdateWindow(HandleRef hWnd);
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetCurrentProcessId();
        [DllImport(ExternDll.User32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int ScrollWindowEx(HandleRef hWnd, int nXAmount, int nYAmount, NativeMethods.COMRECT rectScrollRegion, ref NativeMethods.RECT rectClip, HandleRef hrgnUpdate, ref NativeMethods.RECT prcUpdate, int flags);
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetThreadLocale();
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool MessageBeep(int type);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool DrawMenuBar(HandleRef hWnd);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public extern static bool IsChild(HandleRef parent, HandleRef child);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr SetTimer(HandleRef hWnd, int nIDEvent, int uElapse, IntPtr lpTimerFunc);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool KillTimer(HandleRef hwnd, int idEvent);
        [DllImport(ExternDll.User32, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int MessageBox(HandleRef hWnd, string text, string caption, int type);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr SelectObject(HandleRef hDC, HandleRef hObject);
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetTickCount();
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ScrollWindow(HandleRef hWnd, int nXAmount, int nYAmount, ref NativeMethods.RECT rectScrollRegion, ref NativeMethods.RECT rectClip);
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr GetCurrentProcess();
        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr GetCurrentThread();

        [DllImport(ExternDll.Kernel32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public extern static bool SetThreadLocale(int Locale);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool IsWindowUnicode(HandleRef hWnd);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool DrawEdge(HandleRef hDC, ref NativeMethods.RECT rect, int edge, int flags);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool DrawFrameControl(HandleRef hDC, ref NativeMethods.RECT rect, int type, int state);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetClipRgn(HandleRef hDC, HandleRef hRgn);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetRgnBox(HandleRef hRegion, ref NativeMethods.RECT clipRect);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int SelectClipRgn(HandleRef hDC, HandleRef hRgn);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int SetROP2(HandleRef hDC, int nDrawMode);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool DrawIcon(HandleRef hDC, int x, int y, HandleRef hIcon);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool DrawIconEx(HandleRef hDC, int x, int y, HandleRef hIcon, int width, int height, int iStepIfAniCursor, HandleRef hBrushFlickerFree, int diFlags);
        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int SetBkMode(HandleRef hDC, int nBkMode);

        [DllImport(ExternDll.Gdi32, SetLastError = true, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight,
                                         HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool ShowCaret(HandleRef hWnd);
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern bool HideCaret(HandleRef hWnd);
        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern uint GetCaretBlinkTime();
        
        [DllImportAttribute(ExternDll.User32)]
        public static extern IntPtr OpenInputDesktop(int dwFlags, [MarshalAs(UnmanagedType.Bool)] bool fInherit, int dwDesiredAccess);

        [DllImportAttribute(ExternDll.User32)]
        public static extern bool CloseDesktop(IntPtr hDesktop);

        // for Windows vista to windows 8.
        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool SetProcessDPIAware();

        // for windows 8.1 and above 
        [DllImport(ExternDll.ShCore, SetLastError = true)]
        public static extern int SetProcessDpiAwareness(NativeMethods.PROCESS_DPI_AWARENESS awareness);

        // for windows 8.1 and above 
        [DllImport(ExternDll.ShCore, SetLastError = true)]
        public static extern int GetProcessDpiAwareness(IntPtr processHandle, out NativeMethods.PROCESS_DPI_AWARENESS awareness);

        // for Windows 10 version RS2 and above
        [DllImport(ExternDll.User32, SetLastError = true)]
        public static extern bool SetProcessDpiAwarenessContext(int dpiFlag);

        // Available in Windows 10 version RS1 and above.
        [DllImport(ExternDll.User32)]
        public static extern int GetThreadDpiAwarenessContext();

        // Available in Windows 10 version RS1 and above.
        [DllImport(ExternDll.User32)]
        public static extern int GetWindowDpiAwarenessContext(IntPtr hWnd);

        // Available in Windows 10 version RS1 and above.
        [DllImport(ExternDll.User32)]
        public static extern bool AreDpiAwarenessContextsEqual(int dpiContextA, int dpiContextB);

        [DllImport(ExternDll.Kernel32, SetLastError = true)]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        internal const int PROCESS_QUERY_INFORMATION = 0x0400;
        internal const int PROCESS_VM_READ = 0x0010;

        // Color conversion
        //
        public static int RGBToCOLORREF(int rgbValue)
        {

            // clear the A value, swap R & B values
            int bValue = (rgbValue & 0xFF) << 16;

            rgbValue &= 0xFFFF00;
            rgbValue |= ((rgbValue >> 16) & 0xFF);
            rgbValue &= 0x00FFFF;
            rgbValue |= bValue;
            return rgbValue;
        }

        //public static Color ColorFromCOLORREF(int colorref)
        //{
        //    int r = colorref & 0xFF;
        //    int g = (colorref >> 8) & 0xFF;
        //    int b = (colorref >> 16) & 0xFF;
        //    return Color.FromArgb(r, g, b);
        //}

        //public static int ColorToCOLORREF(Color color)
        //{
        //    return (int)color.R | ((int)color.G << 8) | ((int)color.B << 16);
        //}

        [ComImport(), Guid("BEF6E003-A874-101A-8BBA-00AA00300CAB"), System.Runtime.InteropServices.InterfaceTypeAttribute(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch)]
        public interface IFontDisp
        {

            string Name { get; set; }

            long Size { get; set; }

            bool Bold { get; set; }

            bool Italic { get; set; }

            bool Underline { get; set; }

            bool Strikethrough { get; set; }

            short Weight { get; set; }

            short Charset { get; set; }
        }
    }
}
