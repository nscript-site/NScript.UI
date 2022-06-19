using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    [Flags]
    public enum CLSCTX : uint
    {
        ClsctxInprocServer = 0x1,
        ClsctxInprocHandler = 0x2,
        ClsctxLocalServer = 0x4,
        ClsctxInprocServer16 = 0x8,
        ClsctxRemoteServer = 0x10,
        ClsctxInprocHandler16 = 0x20,
        ClsctxReserved1 = 0x40,
        ClsctxReserved2 = 0x80,
        ClsctxReserved3 = 0x100,
        ClsctxReserved4 = 0x200,
        ClsctxNoCodeDownload = 0x400,
        ClsctxReserved5 = 0x800,
        ClsctxNoCustomMarshal = 0x1000,
        ClsctxEnableCodeDownload = 0x2000,
        ClsctxNoFailureLog = 0x4000,
        ClsctxDisableAaa = 0x8000,
        ClsctxEnableAaa = 0x10000,
        ClsctxFromDefaultContext = 0x20000,
        ClsctxInproc = ClsctxInprocServer | ClsctxInprocHandler,
        ClsctxServer = ClsctxInprocServer | ClsctxLocalServer | ClsctxRemoteServer,
        ClsctxAll = ClsctxServer | ClsctxInprocHandler
    }
}
