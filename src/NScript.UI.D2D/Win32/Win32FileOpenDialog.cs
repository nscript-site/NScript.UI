using System;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    using NScript.UI.Controls;

    public class Win32FileOpenDialog
    {
        public static Object syncroot = new object();

        public string DirectoryPath { get; set; }

        public DialogResult ShowDialog(IntPtr? owner = null)
        {
            IntPtr hwndOwner = owner ?? Win32Api.GetActiveWindow();

            //ComFileOpenDialog fileDialog = ComFileOpenDialog.Create();
            //fileDialog.Show(hwndOwner);

            //Win32.Win32Api.EnableWindow(new HandleRef(null, hwndOwner), false);

            new Win32FileDialog().RunDialog();

            //Win32.Win32Api.EnableWindow(new HandleRef(null, hwndOwner), true);
            //Console.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
            //Console.WriteLine(System.Threading.Thread.CurrentThread.GetApartmentState());

            //System.Threading.Thread thread = new System.Threading.Thread(ShowDialogCore);
            //thread.SetApartmentState(System.Threading.ApartmentState.STA);
            //thread.IsBackground = true;
            //thread.Start();

            //FileOpenDialog dialog = FileOpenDialog.Create();
            //dialog.Show(hwndOwner);
            //dialog.Dispose();
            return DialogResult.OK;

            //TestApi.commonItemDialog(hwndOwner, ComIds.CLSID_FileOpenDialog, ComIds.IID_IFileOpenDialog);

            //FileOpenDialog dialog = FileOpenDialog.Create();

            DialogResult dr = DialogResult.OK;


            return dr;

            //using (var dialog = FileOpenDialog.Create())
            //{
            //    if (!string.IsNullOrEmpty(DirectoryPath))
            //    {
            //        IntPtr idl;
            //        uint atts = 0;
            //        if (Win32Api.SHILCreateFromPath(DirectoryPath, out idl, ref atts) == 0)
            //        {
            //            using (var item = new ShellItem())
            //            {
            //                if (Win32Api.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item.Pointer) == 0)
            //                {
            //                    dialog.SetFolder(item);
            //                }
            //            }
            //            Marshal.FreeCoTaskMem(idl);
            //        }
            //    }

            //    //dialog.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM);
            //    uint hr = dialog.Show(hwndOwner);
            //    if (hr == Win32Api.ERROR_CANCELLED)
            //        return DialogResult.Cancel;

            //    if (hr != 0)
            //        return DialogResult.Abort;
            //    Platform.Log("before dialog.GetResult");
            //    using (var item = dialog.GetResult())
            //    {
            //        DirectoryPath = item.FullName;
            //    }
            //    return DialogResult.OK;
            //}
        }
    }
}
