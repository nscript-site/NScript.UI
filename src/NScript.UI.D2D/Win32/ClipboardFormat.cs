using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D.Win32
{
    public enum ClipboardFormat
    {
        /// <summary>
        /// Text format. Each line ends with a carriage return/linefeed (CR-LF) combination. A null character signals the end of the data. Use this format for ANSI text.
        /// </summary>
        CF_TEXT = 1,
        /// <summary>
        /// A handle to a bitmap
        /// </summary>
        CF_BITMAP = 2,
        /// <summary>
        /// A memory object containing a BITMAPINFO structure followed by the bitmap bits.
        /// </summary>
        CF_DIB = 3,
        /// <summary>
        /// Unicode text format. Each line ends with a carriage return/linefeed (CR-LF) combination. A null character signals the end of the data.
        /// </summary>
        CF_UNICODETEXT = 13,
        /// <summary>
        /// A handle to type HDROP that identifies a list of files. 
        /// </summary>
        CF_HDROP = 15,
    }
}
