using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    public static class ComIds
    {
        public static readonly Guid SaveFileDialog = Guid.Parse("C0B4E2F3-BA21-4773-8DBA-335EC946EB8B");
        public static readonly Guid IShellItem = Guid.Parse("43826D1E-E718-42EE-BC55-A1E261C37BFE");

        public static readonly Guid CLSID_FileOpenDialog = new Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7");
        public static readonly Guid IID_IFileOpenDialog = new Guid("42f85136-db7e-439c-85f1-e4075d135fc8");
    }
}
