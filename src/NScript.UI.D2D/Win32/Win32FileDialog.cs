using System;
using System.Runtime.InteropServices;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    public class Win32FileDialog
    {
        public const int MEMBERID_NIL = (-1),
        MAX_PATH = 260,
        MAX_UNICODESTRING_LEN = short.MaxValue, // maximum unicode string length 
        ERROR_INSUFFICIENT_BUFFER = 122, //https://msdn.microsoft.com/en-us/library/windows/desktop/ms681382(v=vs.85).aspx
        MA_ACTIVATE = 0x0001,
        MA_ACTIVATEANDEAT = 0x0002,
        MA_NOACTIVATE = 0x0003,
        MA_NOACTIVATEANDEAT = 0x0004,
        MM_TEXT = 1,
        MM_ANISOTROPIC = 8,
        MK_LBUTTON = 0x0001,
        MK_RBUTTON = 0x0002,
        MK_SHIFT = 0x0004,
        MK_CONTROL = 0x0008,
        MK_MBUTTON = 0x0010,
        MNC_EXECUTE = 2,
        MNC_SELECT = 3,
        MIIM_STATE = 0x00000001,
        MIIM_ID = 0x00000002,
        MIIM_SUBMENU = 0x00000004,
        MIIM_TYPE = 0x00000010,
        MIIM_DATA = 0x00000020,
        MIIM_STRING = 0x00000040,
        MIIM_BITMAP = 0x00000080,
        MIIM_FTYPE = 0x00000100,
        MB_OK = 0x00000000,
        MF_BYCOMMAND = 0x00000000,
        MF_BYPOSITION = 0x00000400,
        MF_ENABLED = 0x00000000,
        MF_GRAYED = 0x00000001,
        MF_POPUP = 0x00000010,
        MF_SYSMENU = 0x00002000,
        MFS_DISABLED = 0x00000003,
        MFT_MENUBREAK = 0x00000040,
        MFT_SEPARATOR = 0x00000800,
        MFT_RIGHTORDER = 0x00002000,
        MFT_RIGHTJUSTIFY = 0x00004000,
        MDIS_ALLCHILDSTYLES = 0x0001,
        MDITILE_VERTICAL = 0x0000,
        MDITILE_HORIZONTAL = 0x0001,
        MDITILE_SKIPDISABLED = 0x0002,
        MCM_SETMAXSELCOUNT = (0x1000 + 4),
        MCM_SETSELRANGE = (0x1000 + 6),
        MCM_GETMONTHRANGE = (0x1000 + 7),
        MCM_GETMINREQRECT = (0x1000 + 9),
        MCM_SETCOLOR = (0x1000 + 10),
        MCM_SETTODAY = (0x1000 + 12),
        MCM_GETTODAY = (0x1000 + 13),
        MCM_HITTEST = (0x1000 + 14),
        MCM_SETFIRSTDAYOFWEEK = (0x1000 + 15),
        MCM_SETRANGE = (0x1000 + 18),
        MCM_SETMONTHDELTA = (0x1000 + 20),
        MCM_GETMAXTODAYWIDTH = (0x1000 + 21),
        MCHT_TITLE = 0x00010000,
        MCHT_CALENDAR = 0x00020000,
        MCHT_TODAYLINK = 0x00030000,
        MCHT_TITLEBK = (0x00010000),
        MCHT_TITLEMONTH = (0x00010000 | 0x0001),
        MCHT_TITLEYEAR = (0x00010000 | 0x0002),
        MCHT_TITLEBTNNEXT = (0x00010000 | 0x01000000 | 0x0003),
        MCHT_TITLEBTNPREV = (0x00010000 | 0x02000000 | 0x0003),
        MCHT_CALENDARBK = (0x00020000),
        MCHT_CALENDARDATE = (0x00020000 | 0x0001),
        MCHT_CALENDARDATENEXT = ((0x00020000 | 0x0001) | 0x01000000),
        MCHT_CALENDARDATEPREV = ((0x00020000 | 0x0001) | 0x02000000),
        MCHT_CALENDARDAY = (0x00020000 | 0x0002),
        MCHT_CALENDARWEEKNUM = (0x00020000 | 0x0003),
        MCSC_TEXT = 1,
        MCSC_TITLEBK = 2,
        MCSC_TITLETEXT = 3,
        MCSC_MONTHBK = 4,
        MCSC_TRAILINGTEXT = 5,
        MCN_VIEWCHANGE = (0 - 750), // MCN_SELECT -4  - give state of calendar view
        MCN_SELCHANGE = ((0 - 750) + 1),
        MCN_GETDAYSTATE = ((0 - 750) + 3),
        MCN_SELECT = ((0 - 750) + 4),
        MCS_DAYSTATE = 0x0001,
        MCS_MULTISELECT = 0x0002,
        MCS_WEEKNUMBERS = 0x0004,
        MCS_NOTODAYCIRCLE = 0x0008,
        MCS_NOTODAY = 0x0010,
        MSAA_MENU_SIG = (unchecked((int)0xAA0DF00D));

        public const int OFN_READONLY = 0x00000001,
        OFN_OVERWRITEPROMPT = 0x00000002,
        OFN_HIDEREADONLY = 0x00000004,
        OFN_NOCHANGEDIR = 0x00000008,
        OFN_SHOWHELP = 0x00000010,
        OFN_ENABLEHOOK = 0x00000020,
        OFN_NOVALIDATE = 0x00000100,
        OFN_ALLOWMULTISELECT = 0x00000200,
        OFN_PATHMUSTEXIST = 0x00000800,
        OFN_FILEMUSTEXIST = 0x00001000,
        OFN_CREATEPROMPT = 0x00002000,
        OFN_EXPLORER = 0x00080000,
        OFN_NODEREFERENCELINKS = 0x00100000,
        OFN_ENABLESIZING = 0x00800000,
        OFN_USESHELLITEM = 0x01000000,
        OLEIVERB_PRIMARY = 0,
        OLEIVERB_SHOW = -1,
        OLEIVERB_HIDE = -3,
        OLEIVERB_UIACTIVATE = -4,
        OLEIVERB_INPLACEACTIVATE = -5,
        OLEIVERB_DISCARDUNDOSTATE = -6,
        OLEIVERB_PROPERTIES = -7,
        OLE_E_INVALIDRECT = unchecked((int)0x8004000D),
        OLE_E_NOCONNECTION = unchecked((int)0x80040004),
        OLE_E_PROMPTSAVECANCELLED = unchecked((int)0x8004000C),
        OLEMISC_RECOMPOSEONRESIZE = 0x00000001,
        OLEMISC_INSIDEOUT = 0x00000080,
        OLEMISC_ACTIVATEWHENVISIBLE = 0x0000100,
        OLEMISC_ACTSLIKEBUTTON = 0x00001000,
        OLEMISC_SETCLIENTSITEFIRST = 0x00020000,
        OBJ_PEN = 1,
        OBJ_BRUSH = 2,
        OBJ_DC = 3,
        OBJ_METADC = 4,
        OBJ_PAL = 5,
        OBJ_FONT = 6,
        OBJ_BITMAP = 7,
        OBJ_REGION = 8,
        OBJ_METAFILE = 9,
        OBJ_MEMDC = 10,
        OBJ_EXTPEN = 11,
        OBJ_ENHMETADC = 12,
        ODS_CHECKED = 0x0008,
        ODS_COMBOBOXEDIT = 0x1000,
        ODS_DEFAULT = 0x0020,
        ODS_DISABLED = 0x0004,
        ODS_FOCUS = 0x0010,
        ODS_GRAYED = 0x0002,
        ODS_HOTLIGHT = 0x0040,
        ODS_INACTIVE = 0x0080,
        ODS_NOACCEL = 0x0100,
        ODS_NOFOCUSRECT = 0x0200,
        ODS_SELECTED = 0x0001,
        OLECLOSE_SAVEIFDIRTY = 0,
        OLECLOSE_PROMPTSAVE = 2;

        public delegate IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct OPENFILENAME_I
        {
            public int lStructSize; //ndirect.DllLib.sizeOf(this);
            public IntPtr hwndOwner;
            public IntPtr hInstance;
            public IntPtr lpstrFilter;   // use embedded nulls to separate filters
            public IntPtr lpstrCustomFilter;
            public int nMaxCustFilter;
            public int nFilterIndex;
            public IntPtr lpstrFile;
            public int nMaxFile;
            public IntPtr lpstrFileTitle;
            public int nMaxFileTitle;
            public IntPtr lpstrInitialDir;
            public IntPtr lpstrTitle;
            public int Flags;
            public short nFileOffset;
            public short nFileExtension;
            public IntPtr lpstrDefExt;
            public IntPtr lCustData;
            public IntPtr lpfnHook;
            public IntPtr lpTemplateName;
            public IntPtr pvReserved;
            public int dwReserved;
            public int FlagsEx;

            public static OPENFILENAME_I Create()
            {
                OPENFILENAME_I item = new OPENFILENAME_I();
                item.lStructSize = 152;
                item.nMaxFile = MAX_PATH;
                item.nMaxFileTitle = MAX_PATH;
                return item;
            }
        }

        public abstract class CharBuffer
        {

            public static CharBuffer CreateBuffer(int size)
            {
                return new UnicodeCharBuffer(size);
            }

            public abstract IntPtr AllocCoTaskMem();
            public abstract string GetString();
            public abstract void PutCoTaskMem(IntPtr ptr);
            public abstract void PutString(string s);
        }

        public class UnicodeCharBuffer : CharBuffer
        {

            internal char[] buffer;
            internal int offset;

            public UnicodeCharBuffer(int size)
            {
                buffer = new char[size];
            }

            public override IntPtr AllocCoTaskMem()
            {
                IntPtr result = Marshal.AllocCoTaskMem(buffer.Length * 2);
                Marshal.Copy(buffer, 0, result, buffer.Length);
                return result;
            }

            public override string GetString()
            {
                int i = offset;
                while (i < buffer.Length && buffer[i] != 0) i++;
                string result = new string(buffer, offset, i - offset);
                if (i < buffer.Length) i++;
                offset = i;
                return result;
            }

            public override void PutCoTaskMem(IntPtr ptr)
            {
                Marshal.Copy(ptr, buffer, 0, buffer.Length);
                offset = 0;
            }

            public override void PutString(string s)
            {
                int count = Math.Min(s.Length, buffer.Length - offset);
                s.CopyTo(0, buffer, offset, count);
                offset += count;
                if (offset < buffer.Length) buffer[offset++] = (char)0;
            }
        }
        internal const int OPTION_ADDEXTENSION = unchecked(unchecked((int)0x80000000));

        internal int options;

        private const int FILEBUFSIZE = 8192;

        private string title;
        private string initialDir;
        private string defaultExt;
        private string[] fileNames;
        private string filter;
        private int filterIndex;
        private bool supportMultiDottedExtensions;
        private bool ignoreSecondFileOkNotification;
        private int okNotificationCount;
        private CharBuffer charBuffer;
        private IntPtr dialogHWnd;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string modName);

        [DllImport("comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OPENFILENAME_I ofn);

        protected virtual IntPtr Instance
        {
            get { return GetModuleHandle(null); }
        }

        public bool RunDialog()
        {
            return RunDialog(IntPtr.Zero);
        }

        public bool RunDialog(IntPtr hWndOwner)
        {
            WndProc hookProcPtr = new WndProc(this.HookProc);
            OPENFILENAME_I ofn = OPENFILENAME_I.Create();
            try
            {
                charBuffer = CharBuffer.CreateBuffer(FILEBUFSIZE);
                if (fileNames != null)
                {
                    charBuffer.PutString(fileNames[0]);
                }
                ofn.lStructSize = Marshal.SizeOf(typeof(OPENFILENAME_I));
                // Degrade to the older style dialog if we're not on Win2K.
                // We do this by setting the struct size to a different value
                //
                if (Environment.OSVersion.Platform != System.PlatformID.Win32NT ||
                    Environment.OSVersion.Version.Major < 5)
                {
                    ofn.lStructSize = 0x4C;
                }
                ofn.hwndOwner = hWndOwner;
                ofn.hInstance = Instance;
                // ofn.lpstrFilter = MakeFilterString(filter, this.DereferenceLinks);
                ofn.nFilterIndex = filterIndex;
                ofn.lpstrFile = charBuffer.AllocCoTaskMem();
                ofn.nMaxFile = FILEBUFSIZE;
                // ofn.lpstrInitialDir = initialDir;
                // ofn.lpstrTitle = title;
                ofn.Flags = (OFN_EXPLORER | OFN_ENABLEHOOK | OFN_ENABLESIZING);
                // ofn.lpfnHook = hookProcPtr;
                ofn.FlagsEx = OFN_USESHELLITEM;
                // if (defaultExt != null && AddExtension) {
                //     ofn.lpstrDefExt = defaultExt;
                // }
                //Security checks happen here
                return RunFileDialog(ofn);
            }
            finally
            {
                charBuffer = null;
                if (ofn.lpstrFile != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(ofn.lpstrFile);
                }
            }
        }

        internal bool RunFileDialog(OPENFILENAME_I ofn)
        {
            bool result = GetOpenFileName(ofn);
            if (!result)
            {
                // Something may have gone wrong - check for error condition
                //
                // int errorCode = SafeNativeMethods.CommDlgExtendedError();
                // switch (errorCode)
                // {
                //     case NativeMethods.FNERR_INVALIDFILENAME:
                //         throw new InvalidOperationException(string.Format(SR.FileDialogInvalidFileName, FileName));

                //     case NativeMethods.FNERR_SUBCLASSFAILURE:
                //         throw new InvalidOperationException(SR.FileDialogSubLassFailure);

                //     case NativeMethods.FNERR_BUFFERTOOSMALL:
                //         throw new InvalidOperationException(SR.FileDialogBufferTooSmall);
                // }
            }
            return result;
        }

        protected IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            // if (msg == NativeMethods.WM_NOTIFY) {
            //     dialogHWnd = UnsafeNativeMethods.GetParent(new HandleRef(null, hWnd));
            //     try {
            //         UnsafeNativeMethods.OFNOTIFY notify = (UnsafeNativeMethods.OFNOTIFY)UnsafeNativeMethods.PtrToStructure(lparam, typeof(UnsafeNativeMethods.OFNOTIFY));

            //         switch (notify.hdr_code) {
            //             case -601: /* CDN_INITDONE */
            //                 MoveToScreenCenter(dialogHWnd);
            //                 break;
            //             case -602: /* CDN_SELCHANGE */
            //                 NativeMethods.OPENFILENAME_I ofn = (NativeMethods.OPENFILENAME_I)UnsafeNativeMethods.PtrToStructure(notify.lpOFN, typeof(NativeMethods.OPENFILENAME_I));
            //                 // Get the buffer size required to store the selected file names.
            //                 int sizeNeeded = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, dialogHWnd), 1124 /*CDM_GETSPEC*/, System.IntPtr.Zero, System.IntPtr.Zero);
            //                 if (sizeNeeded > ofn.nMaxFile) {
            //                     // A bigger buffer is required.
            //                     try {
            //                         int newBufferSize = sizeNeeded + (FILEBUFSIZE / 4);
            //                         // Allocate new buffer
            //                         CharBuffer charBufferTmp = CharBuffer.CreateBuffer(newBufferSize);
            //                         IntPtr newBuffer = charBufferTmp.AllocCoTaskMem();
            //                         // Free old buffer
            //                         Marshal.FreeCoTaskMem(ofn.lpstrFile);
            //                         // Substitute buffer
            //                         ofn.lpstrFile = newBuffer;
            //                         ofn.nMaxFile = newBufferSize;
            //                         this.charBuffer = charBufferTmp;
            //                         Marshal.StructureToPtr(ofn, notify.lpOFN, true);
            //                         Marshal.StructureToPtr(notify, lparam, true);
            //                     }
            //                     catch {
            //                         // intentionaly not throwing here.
            //                     }
            //                 }
            //                 this.ignoreSecondFileOkNotification = false;
            //                 break;
            //             case -604: /* CDN_SHAREVIOLATION */
            //                 // When the selected file is locked for writing,
            //                 // we get this notification followed by *two* CDN_FILEOK notifications.                            
            //                 this.ignoreSecondFileOkNotification = true;  // We want to ignore the second CDN_FILEOK
            //                 this.okNotificationCount = 0;                // to avoid a second prompt by PromptFileOverwrite.
            //                 break;
            //             case -606: /* CDN_FILEOK */
            //                 if (this.ignoreSecondFileOkNotification)
            //                 {
            //                     // We got a CDN_SHAREVIOLATION notification and want to ignore the second CDN_FILEOK notification
            //                     if (this.okNotificationCount == 0)
            //                     {
            //                         this.okNotificationCount = 1;   // This one is the first and is all right.
            //                     }
            //                     else
            //                     {
            //                         // This is the second CDN_FILEOK, so we want to ignore it.
            //                         this.ignoreSecondFileOkNotification = false;
            //                         UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, NativeMethods.InvalidIntPtr));
            //                         return NativeMethods.InvalidIntPtr;
            //                     }
            //                 }
            //                 if (!DoFileOk(notify.lpOFN)) {
            //                     UnsafeNativeMethods.SetWindowLong(new HandleRef(null, hWnd), 0, new HandleRef(null, NativeMethods.InvalidIntPtr));
            //                     return NativeMethods.InvalidIntPtr;
            //                 }
            //                 break;
            //         }
            //     }
            //     catch {
            //         if (dialogHWnd != IntPtr.Zero) {
            //             UnsafeNativeMethods.EndDialog(new HandleRef(this, dialogHWnd), IntPtr.Zero);
            //         }
            //         throw;
            //     }
            // }
            return IntPtr.Zero;
        }
    }
}
