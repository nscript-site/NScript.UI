using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct COMDLG_FILTERSPEC
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pszSpec;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PROPERTYKEY
    {
        public Guid fmtid;
        public uint pid;
    }

    public enum SIATTRIBFLAGS
    {
        SIATTRIBFLAGS_AND = 1,
        SIATTRIBFLAGS_APPCOMPAT = 3,
        SIATTRIBFLAGS_OR = 2
    }

    public unsafe class ShellItem : ComObject
    {
        delegate int IShellItem_GetDisplayName(IntPtr thisPtr, SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);

        public String GetDisplayName(SIGDN sigdnName)
        {
            string ppszName;
            Marshal.ThrowExceptionForHR(
                Marshal.GetDelegateForFunctionPointer<IShellItem_GetDisplayName>(*((*(IntPtr**)Pointer) + 5))(Pointer, sigdnName, out ppszName),
                new IntPtr(-1));
            return ppszName;
        }

        public String FullName => GetDisplayName(SIGDN.SIGDN_FILESYSPATH);
    }
}
