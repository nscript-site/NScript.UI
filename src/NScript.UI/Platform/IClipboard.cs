using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NScript.UI
{
    public interface IClipboard
    {
        Task<string> GetTextAsync();

        Task SetTextAsync(string text);

        Task ClearAsync();
    }
}
