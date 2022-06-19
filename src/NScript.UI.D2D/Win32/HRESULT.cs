using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    public enum HRESULT : uint
    {
        S_FALSE = 0x0001,
        S_OK = 0x0000,
        E_INVALIDARG = 0x80070057,
        E_OUTOFMEMORY = 0x8007000E,
        E_NOTIMPL = 0x80004001,
        E_UNEXPECTED = 0x8000FFFF
    }
}
