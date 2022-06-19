using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    public class Win32Exception : Exception
    {
        private int _errCode;

        public Win32Exception(int err = -1, String msg = ""):base(msg)
        {
            _errCode = err;
        }
    }
}
