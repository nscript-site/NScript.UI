using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NScript.UI.D2D
{
    using NScript.UI.Input;
    using NScript.UI.D2D.Win32;

    internal class D2DClipboard : IClipboard
    {
        private async Task OpenClipboard()
        {
            while (!Win32Api.OpenClipboard(IntPtr.Zero))
            {
                await Task.Delay(100);
            }
        }

        public async Task<string> GetTextAsync()
        {
            await OpenClipboard();
            try
            {
                IntPtr hText = Win32Api.GetClipboardData(ClipboardFormat.CF_UNICODETEXT);
                if (hText == IntPtr.Zero)
                {
                    return null;
                }

                var pText = Win32Api.GlobalLock(hText);
                if (pText == IntPtr.Zero)
                {
                    return null;
                }

                var rv = Marshal.PtrToStringUni(pText);
                Win32Api.GlobalUnlock(hText);
                return rv;
            }
            finally
            {
                Win32Api.CloseClipboard();
            }
        }

        public async Task SetTextAsync(string text)
        {
            if (text == null) return;

            await OpenClipboard();

            Win32Api.EmptyClipboard();

            try
            {
                var hGlobal = Marshal.StringToHGlobalUni(text);
                Win32Api.SetClipboardData(ClipboardFormat.CF_UNICODETEXT, hGlobal);
            }
            finally
            {
                Win32Api.CloseClipboard();
            }
        }

        public async Task ClearAsync()
        {
            await OpenClipboard();
            try
            {
                Win32Api.EmptyClipboard();
            }
            finally
            {
                Win32Api.CloseClipboard();
            }
        }
    }
}
