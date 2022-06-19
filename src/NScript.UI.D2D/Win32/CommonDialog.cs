// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    using NScript.UI.Controls;

    public abstract class CommonDialog
    {
        private static int helpMsg;
        private IntPtr defOwnerWndProc;
        private IntPtr hookedWndProc;
        private IntPtr defaultControlHwnd;

        public Object Tag { get; set; }

        protected virtual IntPtr HookProc(IntPtr hWnd, WM msg, IntPtr wparam, IntPtr lparam)
        {
            if (msg == WM.INITDIALOG)
            {
                MoveToScreenCenter(hWnd);

                // Under some circumstances, the dialog
                // does not initially focus on any control. We fix that by explicitly
                // setting focus ourselves.
                //
                this.defaultControlHwnd = wparam;
                Win32Api.SetFocus(wparam);
            }
            else if (msg == WM.SETFOCUS)
            {
                Win32Api.PostMessage(hWnd, (uint)WM.CDM_SETDEFAULTFOCUS, IntPtr.Zero, IntPtr.Zero);
            }
            else if (msg == WM.CDM_SETDEFAULTFOCUS)
            {
                // If the dialog box gets focus, bounce it to the default control.
                // so we post a message back to ourselves to wait for the focus change then push it to the default
                // control.
                //
                Win32Api.SetFocus(defaultControlHwnd);
            }
            return IntPtr.Zero;
        }

        internal static void MoveToScreenCenter(IntPtr hWnd)
        {
            //RECT r = new RECT();
            //Win32Api.GetWindowRect(hWnd, ref r);
            //Rectangle screen = Screen.GetWorkingArea(Control.MousePosition);
            //int x = screen.X + (screen.Width - r.right + r.left) / 2;
            //int y = screen.Y + (screen.Height - r.bottom + r.top) / 3;
            //SafeNativeMethods.SetWindowPos(new HandleRef(null, hWnd), NativeMethods.NullHandleRef, x, y, 0, 0, NativeMethods.SWP_NOSIZE |
            //                     NativeMethods.SWP_NOZORDER | NativeMethods.SWP_NOACTIVATE);
        }

        protected virtual IntPtr OwnerWndProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            return NativeMethods.CallWindowProc(defOwnerWndProc, hWnd, msg, wparam, lparam);
        }

        public abstract void Reset();

        protected abstract bool RunDialog(IntPtr hwndOwner);

        public DialogResult ShowDialog()
        {
            return ShowDialog(IntPtr.Zero);
        }

        public DialogResult ShowDialog(IntPtr owner)
        {
            throw new NotImplementedException();

            //NativeWindow native = null;//This will be used if there is no owner or active window (declared here so it can be kept alive)

            //IntPtr hwndOwner = IntPtr.Zero;
            //DialogResult result = DialogResult.Cancel;
            //try
            //{
            //    if (owner != null)
            //    {
            //        hwndOwner = Control.GetSafeHandle(owner);
            //    }

            //    if (hwndOwner == IntPtr.Zero)
            //    {
            //        hwndOwner = UnsafeNativeMethods.GetActiveWindow();
            //    }

            //    if (hwndOwner == IntPtr.Zero)
            //    {
            //        //We will have to create our own Window
            //        native = new NativeWindow();
            //        native.CreateHandle(new CreateParams());
            //        hwndOwner = native.Handle;
            //    }

            //    if (helpMsg == 0)
            //    {
            //        helpMsg = SafeNativeMethods.RegisterWindowMessage("commdlg_help");
            //    }

            //    NativeMethods.WndProc ownerProc = new NativeMethods.WndProc(this.OwnerWndProc);
            //    hookedWndProc = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(ownerProc);
            //    System.Diagnostics.Debug.Assert(IntPtr.Zero == defOwnerWndProc, "The previous subclass wasn't properly cleaned up");

            //    IntPtr userCookie = IntPtr.Zero;
            //    try
            //    {
            //        //UnsafeNativeMethods.[Get|Set]WindowLong is smart enough to call SetWindowLongPtr on 64-bit OS
            //        defOwnerWndProc = UnsafeNativeMethods.SetWindowLong(new HandleRef(this, hwndOwner), NativeMethods.GWL_WNDPROC, ownerProc);

            //        if (Application.UseVisualStyles)
            //        {
            //            userCookie = UnsafeNativeMethods.ThemingScope.Activate();
            //        }

            //        Application.BeginModalMessageLoop();
            //        try
            //        {
            //            result = RunDialog(hwndOwner) ? DialogResult.OK : DialogResult.Cancel;
            //        }
            //        finally
            //        {
            //            Application.EndModalMessageLoop();
            //        }
            //    }
            //    finally
            //    {
            //        IntPtr currentSubClass = UnsafeNativeMethods.GetWindowLong(new HandleRef(this, hwndOwner), NativeMethods.GWL_WNDPROC);
            //        if (IntPtr.Zero != defOwnerWndProc || currentSubClass != hookedWndProc)
            //        {
            //            UnsafeNativeMethods.SetWindowLong(new HandleRef(this, hwndOwner), NativeMethods.GWL_WNDPROC, new HandleRef(this, defOwnerWndProc));
            //        }
            //        UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);

            //        defOwnerWndProc = IntPtr.Zero;
            //        hookedWndProc = IntPtr.Zero;
            //        //Ensure that the subclass delegate will not be GC collected until after it has been subclassed
            //        GC.KeepAlive(ownerProc);
            //    }
            //}
            //finally
            //{
            //    if (null != native)
            //    {
            //        native.DestroyHandle();
            //    }
            //}

            //return result;
        }
    }
}
