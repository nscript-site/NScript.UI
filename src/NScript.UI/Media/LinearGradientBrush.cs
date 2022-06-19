using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    /// <summary>
    /// A brush that draws with a linear gradient.
    /// </summary>
    public sealed class LinearGradientBrush : GradientBrush
    {
        /// <summary>
        /// Gets or sets the start point for the gradient.
        /// </summary>
        public PointF StartPoint { get; set; }

        /// <summary>
        /// Gets or sets the end point for the gradient.
        /// </summary>
        public PointF EndPoint { get; set; }
    }
}
