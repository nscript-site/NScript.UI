using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace NScript.UI.D2D
{
    using NScript.UI.D2D.Win32;
    using NScript.UI.Controls;
    using SharpDX;

    class D2DSystemDialog : ISystemDialog
    {
        private const FOS DefaultDialogOptions = FOS.FOS_FORCEFILESYSTEM | FOS.FOS_NOVALIDATE |
            FOS.FOS_NOTESTFILECREATE | FOS.FOS_DONTADDTORECENT;

        public unsafe Task<string[]> ShowFileDialogAsync(FileDialog dialog, WindowImpl parent)
        {
            D2DWindow window = parent as D2DWindow;
            var hWnd = window.Handle;
            return Task.Factory.StartNew(() =>
            {
                return new string[0]; ;
                //var result = new string[0];
                //ComObject comObject = new ComObject(IntPtr.Zero);

                //Guid clsid = dialog is OpenFileDialog ? ComIds.OpenFileDialog : ComIds.SaveFileDialog;
                //Guid iid = ComIds.IFileDialog;
                //Platform.Log("Start coCreateInstanceResult");
                ////ComResult coCreateInstanceResult = Win32Api.CoCreateInstance(clsid, IntPtr.Zero,
                ////    CLSCTX.ClsctxInprocServer, iid, comObject);
                //Win32Api.CreateComInstance(clsid, CLSCTX.ClsctxInprocServer, iid, comObject);
                //Console.WriteLine("coCreateInstanceResult:" + comObject.NativePointer);
                //Platform.Log("coCreateInstanceResult:" + comObject.NativePointer);

                //var frm = (IFileDialog)unk;

                //var openDialog = dialog as OpenFileDialog;

                //uint options;
                //frm.GetOptions(out options);
                //options |= (uint)(DefaultDialogOptions);
                //if (openDialog?.AllowMultiple == true)
                //    options |= (uint)FOS.FOS_ALLOWMULTISELECT;
                //frm.SetOptions(options);

                //var defaultExtension = (dialog as SaveFileDialog)?.DefaultExtension ?? "";
                //frm.SetDefaultExtension(defaultExtension);
                //frm.SetFileName(dialog.InitialFileName ?? "");
                //frm.SetTitle(dialog.Title ?? "");

                //var filters = new List<COMDLG_FILTERSPEC>();
                //if (dialog.Filters != null)
                //{
                //    foreach (var filter in dialog.Filters)
                //    {
                //        var extMask = string.Join(";", filter.Extensions.Select(e => "*." + e));
                //        filters.Add(new COMDLG_FILTERSPEC { pszName = filter.Name, pszSpec = extMask });
                //    }
                //}
                //if (filters.Count == 0)
                //    filters.Add(new COMDLG_FILTERSPEC { pszName = "All files", pszSpec = "*.*" });

                //frm.SetFileTypes((uint)filters.Count, filters.ToArray());
                //frm.SetFileTypeIndex(0);

                //if (dialog.InitialDirectory != null)
                //{
                //    IShellItem directoryShellItem;
                //    Guid riid = ShellIds.IShellItem;
                //    if (Win32Api.SHCreateItemFromParsingName(dialog.InitialDirectory, IntPtr.Zero, ref riid, out directoryShellItem) == (uint)HRESULT.S_OK)
                //    {
                //        frm.SetFolder(directoryShellItem);
                //        frm.SetDefaultFolder(directoryShellItem);
                //    }
                //}

                //if (frm.Show(hWnd) == (uint)HRESULT.S_OK)
                //{
                //    if (openDialog?.AllowMultiple == true)
                //    {
                //        IShellItemArray shellItemArray;
                //        ((IFileOpenDialog)frm).GetResults(out shellItemArray);
                //        uint count;
                //        shellItemArray.GetCount(out count);
                //        result = new string[count];
                //        for (uint i = 0; i < count; i++)
                //        {
                //            IShellItem shellItem;
                //            shellItemArray.GetItemAt(i, out shellItem);
                //            result[i] = GetAbsoluteFilePath(shellItem);
                //        }
                //    }
                //    else
                //    {
                //        IShellItem shellItem;
                //        if (frm.GetResult(out shellItem) == (uint)HRESULT.S_OK)
                //        {
                //            result = new string[] { GetAbsoluteFilePath(shellItem) };
                //        }
                //    }
                //}

                //return result;
            });
        }

        public Task<string> ShowFolderDialogAsync(OpenFolderDialog dialog, WindowImpl parent)
        {
            return Task.Factory.StartNew(() =>
            {
                string result = string.Empty;

                //var hWnd = ((D2DWindow)parent).Handle;
                //Guid clsid = ShellIds.OpenFileDialog;
                //Guid iid = ShellIds.IFileDialog;

                //Win32Api.CoCreateInstance(ref clsid, IntPtr.Zero, 1, ref iid, out var unk);
                //var frm = (IFileDialog)unk;
                //uint options;
                //frm.GetOptions(out options);
                //options |= (uint)(FOS.FOS_PICKFOLDERS | DefaultDialogOptions);
                //frm.SetOptions(options);

                //if (dialog.InitialDirectory != null)
                //{
                //    IShellItem directoryShellItem;
                //    Guid riid = ShellIds.IShellItem;
                //    if (Win32Api.SHCreateItemFromParsingName(dialog.InitialDirectory, IntPtr.Zero, ref riid, out directoryShellItem) == (uint)HRESULT.S_OK)
                //    {
                //        frm.SetFolder(directoryShellItem);
                //    }
                //}

                //if (dialog.DefaultDirectory != null)
                //{
                //    IShellItem directoryShellItem;
                //    Guid riid = ShellIds.IShellItem;
                //    if (Win32Api.SHCreateItemFromParsingName(dialog.DefaultDirectory, IntPtr.Zero, ref riid, out directoryShellItem) == (uint)HRESULT.S_OK)
                //    {
                //        frm.SetDefaultFolder(directoryShellItem);
                //    }
                //}

                //if (frm.Show(hWnd) == (uint)HRESULT.S_OK)
                //{
                //    IShellItem shellItem;
                //    if (frm.GetResult(out shellItem) == (uint)HRESULT.S_OK)
                //    {
                //        result = GetAbsoluteFilePath(shellItem);
                //    }
                //}

                return result;
            });
        }

        public DialogResult ShowFolderDialog(OpenFolderDialog dialog, WindowImpl parent)
        {
            IntPtr? handle = null;
            if (parent != null)
                handle = ((D2DWindow)parent).Handle;
            Win32FileOpenDialog folderBrowser = new Win32FileOpenDialog();
            folderBrowser.DirectoryPath = dialog.DefaultDirectory;
            DialogResult result = folderBrowser.ShowDialog(handle);
            dialog.DefaultDirectory = folderBrowser.DirectoryPath;
            return result;
        }
    }
}
