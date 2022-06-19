using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Input
{
    public class GotFocusEventArgs : BaseEventArgs
    {
        /// <summary>
        /// Gets or sets a value indicating how the change in focus occurred.
        /// </summary>
        public NavigationMethod NavigationMethod { get; set; }

        /// <summary>
        /// Gets or sets any input modifiers active at the time of focus.
        /// </summary>
        public InputModifiers InputModifiers { get; set; }
    }
}
