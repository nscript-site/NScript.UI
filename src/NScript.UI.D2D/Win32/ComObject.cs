using System;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    public unsafe class ComObject : IDisposable
    {
        public IntPtr Pointer;

        delegate int IUnknown_Release(IntPtr thisPtr);
        public void Dispose()
        {
            //TestApi.InvokeFunc1(*((*(IntPtr**)Pointer) + 2),Pointer);
            Marshal.GetDelegateForFunctionPointer<IUnknown_Release>(*((*(IntPtr**)Pointer) + 2))(Pointer);
            Pointer = IntPtr.Zero;
        }
    }
}
