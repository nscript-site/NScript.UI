using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    public enum COINIT
    {
        /// <summary>
        /// Initializes the thread for multi-threaded object concurrency.
        /// </summary>
        COINIT_MULTITHREADED = 0x0,
        /// <summary>
        /// Initializes the thread for apartment-threaded object concurrency. 
        /// </summary>
        COINIT_APARTMENTTHREADED = 0x2,
        /// <summary>
        /// Disables DDE for Ole1 support.
        /// </summary>
        COINIT_DISABLE_OLE1DDE = 0x4,
        /// <summary>
        /// Trades memory for speed.
        /// </summary>
        COINIT_SPEED_OVER_MEMORY = 0x8
    }


    public sealed partial class Win32Api
    {
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOWNOACTIVATE = 4;
        public const int SW_SHOW = 5;
        public const int SW_SHOWMINNOACTIVE = 7;
        public const int SW_SHOWNA = 8;
        public const int SW_RESTORE = 9;

        public const int CW_USEDEFAULT = unchecked((int)0x80000000);

        public delegate void TimerProc(IntPtr hWnd, uint uMsg, IntPtr nIDEvent, uint dwTime);

        public delegate void TimeCallback(uint uTimerID, uint uMsg, UIntPtr dwUser, UIntPtr dw1, UIntPtr dw2);

        public delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public const int ERROR_INVALID_HANDLE = 6;
        public const int ERROR_CLASS_ALREADY_EXISTS = 1410;

        [DllImport("user32.dll", SetLastError = true, EntryPoint = "RegisterClassExW")]
        public static extern short RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        [DllImport("user32.dll")]
        public static extern ushort RegisterClass([In] ref WNDCLASS lpWndClass);

        [DllImport("user32.dll")]
        public static extern bool UnregisterClass(string className, HandleRef hInstance);

        [DllImport("user32.dll")]
        public static extern bool IsWindowEnabled(HandleRef hWnd);

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(HandleRef hWnd, bool enable);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode, EntryPoint = "CreateWindowExW")]
        public static extern IntPtr CreateWindowEx(
           WindowStylesEx dwExStyle,
           IntPtr lpClassName,
           IntPtr lpWindowName,
           WindowStyles dwStyle,
           int x,
           int y,
           int nWidth,
           int nHeight,
           IntPtr hWndParent,
           IntPtr hMenu,
           IntPtr hInstance,
           IntPtr lpParam);

        [DllImport("user32.dll")]
        public static extern uint GetCaretBlinkTime();

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        public static extern uint GetDoubleClickTime();

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, [In, Out] ref RECT rect);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow = SW_SHOW);

        [DllImport("user32.dll")]
        public static extern bool UpdateWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool GetMessage(ref MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin = 0, uint wMsgFilterMax = 0);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        [DllImport("user32.dll")]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool OpenClipboard(IntPtr hWndOwner);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        public static extern bool EmptyClipboard();

        [DllImport("user32.dll")]
        public static extern IntPtr GetClipboardData(ClipboardFormat uFormat);

        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardData(ClipboardFormat uFormat, IntPtr hMem);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GlobalLock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern bool GlobalUnlock(IntPtr handle);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GlobalAlloc(int uFlags, int dwBytes);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GlobalFree(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string fileName);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string lpFileName);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, SystemCursors cursor = SystemCursors.IDC_ARROW);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr hCursor);

        [DllImport("user32.dll")]
        public static extern bool DestroyCursor(IntPtr hCursor);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetTimer(IntPtr hWnd, IntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, SetWindowPosFlags uFlags);
        [DllImport("user32.dll")]
        public static extern bool SetFocus(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern bool SetParent(IntPtr hWnd, IntPtr hWndNewParent);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommand nCmdShow);

        [DllImport("user32.dll")]
        public static extern int ToUnicode(
            uint virtualKeyCode,
            uint scanCode,
            byte[] keyboardState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
                    StringBuilder receivingBuffer,
            int bufferSize,
            uint flags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetProcessDPIAware();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetDpiForSystem();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName = null);

        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric smIndex);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "DefWindowProcW")]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SetClassLongPtr")]
        private static extern IntPtr SetClassLong64(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetClassLong")]
        private static extern IntPtr SetClassLong32(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong);

        //[DllImport("ole32.dll", PreserveSig = false)]
        //internal static extern int CoCreateInstance(ref Guid clsid,
        //    IntPtr ignore1, int ignore2, ref Guid iid, [MarshalAs(UnmanagedType.IUnknown), Out] out object pUnkOuter);

        [DllImport("ole32.dll", ExactSpelling = true, EntryPoint = "CoCreateInstance", PreserveSig = true)]
        private static extern ComResult CoCreateInstance([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid,
            IntPtr pUnkOuter, CLSCTX dwClsContext, 
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid, out IntPtr comObject);

        public static IntPtr SetClassLong(IntPtr hWnd, ClassLongIndex nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetClassLong32(hWnd, nIndex, dwNewLong);
            }

            return SetClassLong64(hWnd, nIndex, dwNewLong);
        }

        [DllImport("ole32.dll")]
        public static extern int CoInitializeEx(IntPtr pvReserved, COINIT dwCoInit);

        [DllImport("ole32.dll")]
        public static extern int CoInitialize(IntPtr pvReserved);

        [DllImport("shell32.dll")]
        public static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

        [DllImport("shell32.dll")]
        public static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IntPtr ppsi);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        public const uint ERROR_CANCELLED = 0x800704C7;
    }
}
