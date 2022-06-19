using System.Threading.Tasks;

namespace NScript.UI
{
    using NScript.UI.Controls;
    /// <summary>
    /// Defines a platform-specific system dialog implementation.
    /// </summary>
    public interface ISystemDialog
    {
        /// <summary>
        /// Shows a file dialog.
        /// </summary>
        /// <param name="dialog">The details of the file dialog to show.</param>
        /// <param name="parent">The parent window.</param>
        /// <returns>A task returning the selected filenames.</returns>
        Task<string[]> ShowFileDialogAsync(FileDialog dialog, WindowImpl parent);

        Task<string> ShowFolderDialogAsync(OpenFolderDialog dialog, WindowImpl parent);

        DialogResult ShowFolderDialog(OpenFolderDialog dialog, WindowImpl parent);
    }
}
