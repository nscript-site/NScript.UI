using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    using NScript.UI.Media;
    using SharpDX.Direct2D1;
    using SharpDX.Direct3D11;
    using SharpDX.Mathematics.Interop;

    public class D2DDrawContext : IDrawContext
    {
        private RenderTarget _renderTarget;

        public Media.Size StageSize { get; protected set; }

        public D2DDrawContext(RenderTarget renderTarget, Media.Size stageSize)
        {
            _renderTarget = renderTarget;
            StageSize = stageSize;
        }

        public void Fill(Color color)
        {
            _renderTarget.Clear(color.ToDirect2D());
        }

        public void Fill(Color color, Media.RectF rect)
        {
            _renderTarget.FillRectangle(rect.ToDirect2D(), 
                new SharpDX.Direct2D1.SolidColorBrush(_renderTarget, color.ToDirect2D()));
        }

        public IFormattedTextImpl CreateFormattedText(
            string text,
            Typeface typeface,
            TextAlignment textAlignment,
            TextWrapping wrapping,
            SizeF constraint,
            IReadOnlyList<FormattedTextStyleSpan> spans)
        {
            return new D2DFormattedText(
                text,
                typeface,
                textAlignment,
                wrapping,
                constraint,
                spans);
        }

        /// <summary>
        /// Draws text.
        /// </summary>
        /// <param name="foreground">The foreground brush.</param>
        /// <param name="origin">The upper-left corner of the text.</param>
        /// <param name="text">The text.</param>
        public void DrawText(NScript.UI.Media.Brush foreground, Media.PointF origin, IFormattedTextImpl text)
        {
            if (!string.IsNullOrEmpty(text.Text))
            {
                var impl = (D2DFormattedText)text;

                using (var brush = CreateBrush(foreground, impl.Size))
                using (var renderer = new D2DTextRenderer(this, _renderTarget, brush.PlatformBrush))
                {
                    if (brush.PlatformBrush != null)
                    {
                        impl.TextLayout.Draw(renderer, (float)origin.X, (float)origin.Y);
                    }
                }
            }
        }

        public void PushClip(Media.RectF clip)
        {
            _renderTarget.PushAxisAlignedClip(clip.ToDirect2D(), AntialiasMode.PerPrimitive);
        }

        public void PopClip()
        {
            _renderTarget.PopAxisAlignedClip();
        }

        /// <summary>
        /// Creates a Direct2D brush wrapper for a Avalonia brush.
        /// </summary>
        /// <param name="brush">The avalonia brush.</param>
        /// <param name="destinationSize">The size of the brush's target area.</param>
        /// <returns>The Direct2D brush wrapper.</returns>
        public D2DBrush CreateBrush(NScript.UI.Media.Brush brush, SizeF destinationSize)
        {
            //return new D2DSolidColorBrush(new NScript.UI.Media.SolidColorBrush(Color.BLACK), _renderTarget);

            var solidColorBrush = brush as Media.SolidColorBrush;

            //var linearGradientBrush = brush as ILinearGradientBrush;
            //var radialGradientBrush = brush as IRadialGradientBrush;
            //var imageBrush = brush as IImageBrush;
            //var visualBrush = brush as IVisualBrush;

            if (solidColorBrush != null)
            {
                return new D2DSolidColorBrush(new NScript.UI.Media.SolidColorBrush(solidColorBrush.Color), _renderTarget);
            }

            //else if (linearGradientBrush != null)
            //{
            //    return new LinearGradientBrushImpl(linearGradientBrush, _deviceContext, destinationSize);
            //}
            //else if (radialGradientBrush != null)
            //{
            //    return new RadialGradientBrushImpl(radialGradientBrush, _deviceContext, destinationSize);
            //}
            //else if (imageBrush?.Source != null)
            //{
            //    return new ImageBrushImpl(
            //        imageBrush,
            //        _deviceContext,
            //        (BitmapImpl)imageBrush.Source.PlatformImpl.Item,
            //        destinationSize);
            //}
            //else if (visualBrush != null)
            //{
            //    if (_visualBrushRenderer != null)
            //    {
            //        var intermediateSize = _visualBrushRenderer.GetRenderTargetSize(visualBrush);

            //        if (intermediateSize.Width >= 1 && intermediateSize.Height >= 1)
            //        {
            //            using (var intermediate = new BitmapRenderTarget(
            //                _deviceContext,
            //                CompatibleRenderTargetOptions.None,
            //                intermediateSize.ToSharpDX()))
            //            {
            //                using (var ctx = new RenderTarget(intermediate).CreateDrawingContext(_visualBrushRenderer))
            //                {
            //                    intermediate.Clear(null);
            //                    _visualBrushRenderer.RenderVisualBrush(ctx, visualBrush);
            //                }

            //                return new ImageBrushImpl(
            //                    visualBrush,
            //                    _deviceContext,
            //                    new D2DBitmapImpl(intermediate.Bitmap),
            //                    destinationSize);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        throw new NotSupportedException("No IVisualBrushRenderer was supplied to DrawingContextImpl.");
            //    }
            return new D2DSolidColorBrush(null, _renderTarget);
        }

        public Media.Matrix Transform
        {
            get { return _renderTarget.Transform.ToMatrix(); }
            set { _renderTarget.Transform = value.ToDirect2D();}
        }

        public void DrawLine(Pen pen, Media.PointF p0, Media.PointF p1)
        {
            if (pen != null)
            {
                float xMin = Math.Min(p0.X, p1.X);
                float xMax = Math.Max(p0.X, p1.X);
                float yMin = Math.Min(p0.Y, p1.Y);
                float yMax = Math.Max(p0.Y, p1.Y);
                SizeF size = new SizeF(xMax - xMin, yMax - yMin);

                using (var d2dBrush = CreateBrush(pen.Brush, size))
                using (var d2dStroke = pen.ToDirect2DStrokeStyle(this._renderTarget))
                {
                    if (d2dBrush.PlatformBrush != null)
                    {
                        _renderTarget.DrawLine(
                            p0.ToDirect2D(),
                            p1.ToDirect2D(),
                            d2dBrush.PlatformBrush,
                            (float)pen.Thickness,
                            d2dStroke);
                    }
                }
            }
        }

        public void DrawRect(Pen pen, Media.RectF rect)
        {
            if (pen != null)
            {
                using (var d2dBrush = CreateBrush(pen.Brush, rect.Size))
                {
                    if (d2dBrush.PlatformBrush != null)
                    {
                        _renderTarget.DrawRectangle(rect.ToDirect2D(), d2dBrush.PlatformBrush, pen.Thickness);
                    }
                }
            }
        }

        public void Draw(IDrawContext3D cxt3d)
        {
            if (cxt3d == null) return;
            D3DDrawContext d3dCxt = cxt3d as D3DDrawContext;
            d3dCxt.Draw();
            Console.WriteLine(d3dCxt.Data);
        }

        public void DrawImage(Geb.Image.ImageBgra32 image, Media.RectF rect, float opacity)
        {
            if (image == null || rect.Width < 1 || rect.Height < 1) return;

            BitmapProperties bp = new BitmapProperties(new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied), 72, 72);
            Bitmap bmp = new Bitmap(_renderTarget, new SharpDX.Size2(image.Width, image.Height), bp);
            bmp.CopyFromMemory(image.StartIntPtr, image.Stride);
            _renderTarget.DrawBitmap(bmp, new RawRectangleF(rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height), 1.0f, BitmapInterpolationMode.Linear);
            bmp.Dispose();
        }
    }
}
