using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    /**
     * corert 不支持 com Interop
     * 变通方式见: https://github.com/dotnet/corert/issues/6252
     */
    public unsafe class ComFileOpenDialog : ComObject
    {
        public static ComFileOpenDialog Create()
        {
            return new ComFileOpenDialog { Pointer = Win32Api.CreateComInstance(ComIds.CLSID_FileOpenDialog, ComIds.IID_IFileOpenDialog) };
        }

        delegate uint IFileOpenDialog_Show(IntPtr thisPtr, IntPtr parent);
        public uint Show([In] IntPtr parent)
        {
            return Marshal.GetDelegateForFunctionPointer<IFileOpenDialog_Show>(*((*(IntPtr**)Pointer) + 3))(Pointer, parent);
        }

        delegate int IFileOpenDialog_SetOptions(IntPtr thisPtr, FOS fos);
        public void SetOptions(FOS fos)
        {
            Marshal.ThrowExceptionForHR(Marshal.GetDelegateForFunctionPointer<IFileOpenDialog_SetOptions>(*((*(IntPtr**)Pointer) + 9))(Pointer, fos), new IntPtr(-1));
        }

        delegate int IFileOpenDialog_SetFolder(IntPtr thisPtr, IntPtr psi);
        public void SetFolder(ShellItem psi)
        {
            Marshal.ThrowExceptionForHR(Marshal.GetDelegateForFunctionPointer<IFileOpenDialog_SetFolder>(*((*(IntPtr**)Pointer) + 12))(Pointer, psi.Pointer), new IntPtr(-1));
        }

        delegate int IFileOpenDialog_GetResult(IntPtr thisPtr, out IntPtr ppsi);
        public ShellItem GetResult()
        {
            var result = new ShellItem();
            Marshal.ThrowExceptionForHR(Marshal.GetDelegateForFunctionPointer<IFileOpenDialog_GetResult>(*((*(IntPtr**)Pointer) + 20))(Pointer, out result.Pointer), new IntPtr(-1));
            return result;
        }
    }
}
