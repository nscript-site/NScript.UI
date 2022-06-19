using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    /// <summary>
    /// Extension methods for brush classes.
    /// </summary>
    public static class BrushExtensions
    {
        /// <summary>
        /// Converts a brush to an immutable brush.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <returns>
        /// The result of calling <see cref="IMutableBrush.ToImmutable"/> if the brush is mutable,
        /// otherwise <paramref name="brush"/>.
        /// </returns>
        public static Brush ToImmutable(this Brush brush)
        {
            return brush;
        }

        /// <summary>
        /// Converts a pen to a pen with an immutable brush
        /// </summary>
        /// <param name="pen">The pen.</param>
        /// <returns>
        /// A copy of the pen with an immutable brush, or <paramref name="pen"/> if the pen's brush
        /// is already immutable or null.
        /// </returns>
        public static Pen ToImmutable(this Pen pen)
        {
            var brush = pen?.Brush?.ToImmutable();
            return pen == null || ReferenceEquals(pen?.Brush, brush) ?
                pen :
                new Pen(
                    brush,
                    thickness: pen.Thickness,
                    dashStyle: pen.DashStyle,
                    dashCap: pen.DashCap,
                    startLineCap: pen.StartLineCap,
                    endLineCap: pen.EndLineCap,
                    lineJoin: pen.LineJoin,
                    miterLimit: pen.MiterLimit);
        }
    }
}
