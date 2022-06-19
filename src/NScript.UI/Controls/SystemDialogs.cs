using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NScript.UI.Controls
{
    public enum DialogResult
    {
        Cancel, Abort, OK
    }

    public abstract class FileDialog : FileSystemDialog
    {
        public List<FileDialogFilter> Filters { get; set; } = new List<FileDialogFilter>();
        public string InitialFileName { get; set; }
    }

    public abstract class FileSystemDialog : SystemDialog
    {
        public string InitialDirectory { get; set; }
    }

    public class SaveFileDialog : FileDialog
    {
        public string DefaultExtension { get; set; }

        public async Task<string> ShowAsync(Window window)
            =>
                ((await Platform.Instance.GetSystemDialog().ShowFileDialogAsync(this, window?.Impl)) ??
                 new string[0]).FirstOrDefault();
    }

    public class OpenFileDialog : FileDialog
    {
        public bool AllowMultiple { get; set; }

        public Task<string[]> ShowAsync(Window window = null)
            => Platform.Instance.GetSystemDialog().ShowFileDialogAsync(this, window?.Impl);
    }

    public class OpenFolderDialog : FileSystemDialog
    {
        public string DefaultDirectory { get; set; }

        public Task<string> ShowAsync(Window window = null)
               => Platform.Instance.GetSystemDialog().ShowFolderDialogAsync(this, window?.Impl);

        public DialogResult Show(Window window = null)
            => Platform.Instance.GetSystemDialog().ShowFolderDialog(this, window?.Impl);
    }

    public abstract class SystemDialog
    {
        public string Title { get; set; }
    }

    public class FileDialogFilter
    {
        public string Name { get; set; }
        public List<string> Extensions { get; set; } = new List<string>();
    }
}
