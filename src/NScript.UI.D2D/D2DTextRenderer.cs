using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    using SharpDX;
    using SharpDX.Direct2D1;
    using SharpDX.DirectWrite;
    using SharpDX.Mathematics.Interop;

    internal class BrushWrapper : ComObject
    {
        public BrushWrapper(NScript.UI.Media.Brush brush)
        {
            Brush = brush;
        }

        public NScript.UI.Media.Brush Brush { get; private set; }
    }

    internal class D2DTextRenderer : TextRendererBase
    {
        private readonly RenderTarget _renderTarget;

        private readonly Brush _foreground;
        private D2DDrawContext _context;

        public D2DTextRenderer(
            D2DDrawContext context,
            RenderTarget target,
            Brush foreground)
        {
            _context = context;
            _renderTarget = target;
            _foreground = foreground;
        }

        public override Result DrawGlyphRun(
            object clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            MeasuringMode measuringMode,
            GlyphRun glyphRun,
            GlyphRunDescription glyphRunDescription,
            ComObject clientDrawingEffect)
        {
            var wrapper = clientDrawingEffect as BrushWrapper;

            var brush = _foreground;
            if(brush.IsDisposed == false)
            {
                _renderTarget.DrawGlyphRun(
                    new RawVector2 { X = baselineOriginX, Y = baselineOriginY },
                    glyphRun,
                    brush,
                    measuringMode);
            }

            if (wrapper != null)
            {
                brush.Dispose();
            }

            return Result.Ok;
        }

        public override RawMatrix3x2 GetCurrentTransform(object clientDrawingContext)
        {
            return _renderTarget.Transform;
        }

        public override float GetPixelsPerDip(object clientDrawingContext)
        {
            return _renderTarget.DotsPerInch.Width / 96;
        }
    }
}
