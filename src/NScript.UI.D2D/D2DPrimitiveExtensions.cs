using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NScript.UI.D2D
{
    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.Mathematics.Interop;
    using DWrite = SharpDX.DirectWrite;
    using NScript.UI.Media;
    using NScript.UI.Input;
    using Win32;

    public static class D2DPrimitiveExtensions
    {
        /// <summary>
        /// The value for which all absolute numbers smaller than are considered equal to zero.
        /// </summary>
        public const float ZeroTolerance = 1e-6f; // Value a 8x higher than 1.19209290E-07F

        public static readonly RawRectangleF RectangleInfinite;

        /// <summary>
        /// Gets the identity matrix.
        /// </summary>
        /// <value>The identity matrix.</value>
        public readonly static RawMatrix3x2 Matrix3x2Identity = new RawMatrix3x2 { M11 = 1, M12 = 0, M21 = 0, M22 = 1, M31 = 0, M32 = 0 };

        static D2DPrimitiveExtensions()
        {
            RectangleInfinite = new RawRectangleF
            {
                Left = float.NegativeInfinity,
                Top = float.NegativeInfinity,
                Right = float.PositiveInfinity,
                Bottom = float.PositiveInfinity
            };
        }

        public static RectF ToAvalonia(this RawRectangleF r)
        {
            return new RectF(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
        }

        public static RawRectangleF ToDirect2D(this Rect r)
        {
            return new RawRectangleF((float)r.X, (float)r.Y, (float)r.X + r.Width, (float)r.Y + r.Height);
        }

        public static RawVector2 ToDirect2D(this Point p)
        {
            return new RawVector2 { X = (float)p.X, Y = (float)p.Y };
        }

        public static Size2F ToDirect2D(this Size p)
        {
            return new Size2F((float)p.Width, (float)p.Height);
        }

        public static RawRectangleF ToDirect2D(this RectF r)
        {
            return new RawRectangleF(r.X, r.Y, r.Right, r.Bottom);
        }

        public static ExtendMode ToDirect2D(this GradientSpreadMethod spreadMethod)
        {
            if (spreadMethod == GradientSpreadMethod.Pad)
                return ExtendMode.Clamp;
            else if (spreadMethod == GradientSpreadMethod.Reflect)
                return ExtendMode.Mirror;
            else
                return ExtendMode.Wrap;
        }

        public static SharpDX.Direct2D1.LineJoin ToDirect2D(this PenLineJoin lineJoin)
        {
            if (lineJoin == PenLineJoin.Round)
                return LineJoin.Round;
            else if (lineJoin == PenLineJoin.Miter)
                return LineJoin.Miter;
            else
                return LineJoin.Bevel;
        }

        public static SharpDX.Direct2D1.CapStyle ToDirect2D(this PenLineCap lineCap)
        {
            if (lineCap == PenLineCap.Flat)
                return CapStyle.Flat;
            else if (lineCap == PenLineCap.Round)
                return CapStyle.Round;
            else if (lineCap == PenLineCap.Square)
                return CapStyle.Square;
            else
                return CapStyle.Triangle;
        }

        public static Guid ToWic(this NScript.UI.Media.PixelFormat format)
        {
            if (format == NScript.UI.Media.PixelFormat.Rgb565)
                return SharpDX.WIC.PixelFormat.Format16bppBGR565;
            if (format == NScript.UI.Media.PixelFormat.Bgra8888)
                return SharpDX.WIC.PixelFormat.Format32bppPBGRA;
            if (format == NScript.UI.Media.PixelFormat.Rgba8888)
                return SharpDX.WIC.PixelFormat.Format32bppPRGBA;
            throw new ArgumentException("Unknown pixel format");
        }

        /// <summary>
        /// Converts a pen to a Direct2D stroke style.
        /// </summary>
        /// <param name="pen">The pen to convert.</param>
        /// <param name="renderTarget">The render target.</param>
        /// <returns>The Direct2D brush.</returns>
        public static StrokeStyle ToDirect2DStrokeStyle(this Pen pen, SharpDX.Direct2D1.RenderTarget renderTarget)
        {
            return pen.ToDirect2DStrokeStyle(renderTarget.Factory);
        }

        /// <summary>
        /// Converts a pen to a Direct2D stroke style.
        /// </summary>
        /// <param name="pen">The pen to convert.</param>
        /// <param name="factory">The factory associated with this resource.</param>
        /// <returns>The Direct2D brush.</returns>
        public static StrokeStyle ToDirect2DStrokeStyle(this Pen pen, Factory factory)
        {
            var properties = new StrokeStyleProperties
            {
                DashStyle = SharpDX.Direct2D1.DashStyle.Solid,
                MiterLimit = (float)pen.MiterLimit,
                LineJoin = pen.LineJoin.ToDirect2D(),
                StartCap = pen.StartLineCap.ToDirect2D(),
                EndCap = pen.EndLineCap.ToDirect2D(),
                DashCap = pen.DashCap.ToDirect2D()
            };
            float[] dashes = null;
            if (pen.DashStyle?.Dashes != null && pen.DashStyle.Dashes.Count > 0)
            {
                properties.DashStyle = SharpDX.Direct2D1.DashStyle.Custom;
                properties.DashOffset = (float)pen.DashStyle.Offset;
                dashes = pen.DashStyle.Dashes.Select(x => (float)x).ToArray();
            }

            dashes = dashes ?? Array.Empty<float>();

            return new StrokeStyle(factory, properties, dashes);
        }

        /// <summary>
        /// Converts a Avalonia <see cref="Avalonia.Media.Color"/> to Direct2D.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>The Direct2D color.</returns>
        public static RawColor4 ToDirect2D(this Color color)
        {
            return new RawColor4(color.Red, color.Green, color.Blue, color.Alpha);
        }

        /// <summary>
        /// Converts a Avalonia <see cref="Avalonia.Matrix"/> to a Direct2D <see cref="RawMatrix3x2"/>
        /// </summary>
        /// <param name="matrix">The <see cref="Matrix"/>.</param>
        /// <returns>The <see cref="RawMatrix3x2"/>.</returns>
        public static RawMatrix3x2 ToDirect2D(this Matrix matrix)
        {
            return new RawMatrix3x2
            {
                M11 = (float)matrix.M11,
                M12 = (float)matrix.M12,
                M21 = (float)matrix.M21,
                M22 = (float)matrix.M22,
                M31 = (float)matrix.M31,
                M32 = (float)matrix.M32
            };
        }

        public static RawVector2 ToDirect2D(this PointF p)
        {
            return new RawVector2(p.X, p.Y);
        }

        /// <summary>
        /// Converts a Direct2D <see cref="RawMatrix3x2"/> to a Avalonia <see cref="Avalonia.Matrix"/>.
        /// </summary>
        /// <param name="matrix">The matrix</param>
        /// <returns>a <see cref="Matrix"/>.</returns>
        public static Matrix ToMatrix(this RawMatrix3x2 matrix)
        {
            return new Matrix(
                matrix.M11,
                matrix.M12,
                matrix.M21,
                matrix.M22,
                matrix.M31,
                matrix.M32);
        }

        ///// <summary>
        ///// Converts a Avalonia <see cref="Rect"/> to a Direct2D <see cref="RawRectangleF"/>
        ///// </summary>
        ///// <param name="rect">The <see cref="Rect"/>.</param>
        ///// <returns>The <see cref="RawRectangleF"/>.</returns>
        //public static RawRectangleF ToDirect2D(this Rect rect)
        //{
        //    return new RawRectangleF(
        //        (float)rect.X,
        //        (float)rect.Y,
        //        (float)rect.X + rect.Width,
        //        (float)rect.Y + rect.Height);
        //}

        public static DWrite.TextAlignment ToDirect2D(this TextAlignment alignment)
        {
            switch (alignment)
            {
                case TextAlignment.Left:
                    return DWrite.TextAlignment.Leading;
                case TextAlignment.Center:
                    return DWrite.TextAlignment.Center;
                case TextAlignment.Right:
                    return DWrite.TextAlignment.Trailing;
                case TextAlignment.Justified:
                default:
                    return DWrite.TextAlignment.Justified;
            }
        }

        public static SystemCursors ToSystemCursor(this Cursors cursor)
        {
            SystemCursors sc = SystemCursors.IDC_ARROW;
            switch (cursor)
            {
                case Cursors.Hand:
                    sc = SystemCursors.IDC_HAND;
                    break;
                case Cursors.AppStarting:
                    sc = SystemCursors.IDC_APPSTARTING;
                    break;
                case Cursors.Cross:
                    sc = SystemCursors.IDC_CROSS;
                    break;
                case Cursors.Help:
                    sc = SystemCursors.IDC_HELP;
                    break;
                case Cursors.IBeam:
                    sc = SystemCursors.IDC_IBEAM;
                    break;
                case Cursors.No:
                case Cursors.NoMove2D:
                case Cursors.NoMoveHoriz:
                case Cursors.NoMoveVert:
                    sc = SystemCursors.IDC_NO;
                    break;
                case Cursors.PanEast:
                case Cursors.PanNE:
                case Cursors.PanNorth:
                case Cursors.PanNW:
                case Cursors.PanSE:
                case Cursors.PanSouth:
                case Cursors.PanSW:
                case Cursors.PanWest:
                    break;
                case Cursors.SizeAll:
                    sc = SystemCursors.IDC_SIZEALL;
                    break;
                case Cursors.SizeNESW:
                    sc = SystemCursors.IDC_SIZENESW;
                    break;
                case Cursors.SizeNS:
                    sc = SystemCursors.IDC_SIZENS;
                    break;
                case Cursors.SizeNWSE:
                    sc = SystemCursors.IDC_SIZENWSE;
                    break;
                case Cursors.SizeWE:
                    sc = SystemCursors.IDC_SIZEWE;
                    break;
                case Cursors.UpArrow:
                    sc = SystemCursors.IDC_UPARROW;
                    break;
                case Cursors.WaitCursor:
                    sc = SystemCursors.IDC_WAIT;
                    break;
                case Cursors.VSplit:
                case Cursors.HSplit:
                case Cursors.Default:
                case Cursors.Arrow:
                default:
                    sc = SystemCursors.IDC_ARROW;
                    break;
            }
            return sc;
        }
    }
}
