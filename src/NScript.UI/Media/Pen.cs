using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    /// <summary>
    /// Describes how a stroke is drawn.
    /// </summary>
    public class Pen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pen"/> class.
        /// </summary>
        /// <param name="brush">The brush used to draw.</param>
        /// <param name="thickness">The stroke thickness.</param>
        /// <param name="dashStyle">The dash style.</param>
        /// <param name="dashCap">The dash cap.</param>
        /// <param name="startLineCap">The start line cap.</param>
        /// <param name="endLineCap">The end line cap.</param>
        /// <param name="lineJoin">The line join.</param>
        /// <param name="miterLimit">The miter limit.</param>
        public Pen(
            Brush brush,
            float thickness = 1.0f,
            DashStyle dashStyle = null,
            PenLineCap dashCap = PenLineCap.Flat,
            PenLineCap startLineCap = PenLineCap.Flat,
            PenLineCap endLineCap = PenLineCap.Flat,
            PenLineJoin lineJoin = PenLineJoin.Miter,
            double miterLimit = 10.0)
        {
            Brush = brush;
            Thickness = thickness;
            DashCap = dashCap;
            StartLineCap = startLineCap;
            EndLineCap = endLineCap;
            LineJoin = lineJoin;
            MiterLimit = miterLimit;
            DashStyle = dashStyle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pen"/> class.
        /// </summary>
        /// <param name="color">The stroke color.</param>
        /// <param name="thickness">The stroke thickness.</param>
        /// <param name="dashStyle">The dash style.</param>
        /// <param name="dashCap">The dash cap.</param>
        /// <param name="startLineCap">The start line cap.</param>
        /// <param name="endLineCap">The end line cap.</param>
        /// <param name="lineJoin">The line join.</param>
        /// <param name="miterLimit">The miter limit.</param>
        public Pen(
            uint color,
            float thickness = 1.0f,
            DashStyle dashStyle = null,
            PenLineCap dashCap = PenLineCap.Flat,
            PenLineCap startLineCap = PenLineCap.Flat,
            PenLineCap endLineCap = PenLineCap.Flat,
            PenLineJoin lineJoin = PenLineJoin.Miter,
            double miterLimit = 10.0)
        {
            Brush = new SolidColorBrush(color);
            Thickness = thickness;
            StartLineCap = startLineCap;
            EndLineCap = endLineCap;
            LineJoin = lineJoin;
            MiterLimit = miterLimit;
            DashStyle = dashStyle;
            DashCap = dashCap;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pen"/> class.
        /// </summary>
        /// <param name="color">The stroke color.</param>
        /// <param name="thickness">The stroke thickness.</param>
        /// <param name="dashStyle">The dash style.</param>
        /// <param name="dashCap">The dash cap.</param>
        /// <param name="startLineCap">The start line cap.</param>
        /// <param name="endLineCap">The end line cap.</param>
        /// <param name="lineJoin">The line join.</param>
        /// <param name="miterLimit">The miter limit.</param>
        public Pen(
            Color color,
            float thickness = 1.0f,
            DashStyle dashStyle = null,
            PenLineCap dashCap = PenLineCap.Flat,
            PenLineCap startLineCap = PenLineCap.Flat,
            PenLineCap endLineCap = PenLineCap.Flat,
            PenLineJoin lineJoin = PenLineJoin.Miter,
            double miterLimit = 10.0)
        {
            Brush = new SolidColorBrush(color);
            Thickness = thickness;
            StartLineCap = startLineCap;
            EndLineCap = endLineCap;
            LineJoin = lineJoin;
            MiterLimit = miterLimit;
            DashStyle = dashStyle;
            DashCap = dashCap;
        }

        /// <summary>
        /// Gets the brush used to draw the stroke.
        /// </summary>
        public Brush Brush { get; }

        /// <summary>
        /// Gets the stroke thickness.
        /// </summary>
        public float Thickness { get; } = 1.0f;

        public DashStyle DashStyle { get; }

        public PenLineCap DashCap { get; }

        public PenLineCap StartLineCap { get; } = PenLineCap.Flat;

        public PenLineCap EndLineCap { get; } = PenLineCap.Flat;

        public PenLineJoin LineJoin { get; } = PenLineJoin.Miter;

        public double MiterLimit { get; } = 10.0;
    }
}
