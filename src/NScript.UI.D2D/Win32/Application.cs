using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    public class Application
    {
        internal static void RunDialog(D2DWindow window)
        {
            LocalModalMessageLoop(window);
        }

        private static bool LocalModalMessageLoop(D2DWindow window)
        {
            try
            {
                // Execute the message loop until the active component tells us to stop.
                //
                NativeMethods.MSG msg = new NativeMethods.MSG();
                bool unicodeWindow = false;
                bool continueLoop = true;

                while (continueLoop)
                {
                    bool peeked = NativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, NativeMethods.PM_NOREMOVE);
                    if (peeked)
                    {
                        // If the component wants us to process the message, do it.
                        // The component manager hosts windows from many places.  We must be sensitive
                        // to ansi / Unicode windows here.
                        //
                        if (msg.hwnd != IntPtr.Zero && SafeNativeMethods.IsWindowUnicode(new HandleRef(null, msg.hwnd)))
                        {
                            unicodeWindow = true;
                            if (!NativeMethods.GetMessageW(ref msg, NativeMethods.NullHandleRef, 0, 0))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            unicodeWindow = false;
                            if (!NativeMethods.GetMessageA(ref msg, NativeMethods.NullHandleRef, 0, 0))
                            {
                                continue;
                            }
                        }

                        NativeMethods.TranslateMessage(ref msg);
                        if (unicodeWindow)
                        {
                            NativeMethods.DispatchMessageW(ref msg);
                        }
                        else
                        {
                            NativeMethods.DispatchMessageA(ref msg);
                        }

                        //if (form != null)
                        //{
                        //    continueLoop = !form.CheckCloseDialog(false);
                        //}
                    }
                    else if (window == null)
                    {
                        break;
                    }
                    else if (!NativeMethods.PeekMessage(ref msg, NativeMethods.NullHandleRef, 0, 0, NativeMethods.PM_NOREMOVE))
                    {
                        if (window.IsDestory == true)
                        {
                            continueLoop = false;
                            break;
                        }

                        NativeMethods.WaitMessage();
                    }
                }
                return continueLoop;
            }
            catch
            {
                return false;
            }
        }
    }
}
