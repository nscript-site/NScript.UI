using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    public class D2DSolidColorBrush : D2DBrush
    {
        public D2DSolidColorBrush(NScript.UI.Media.SolidColorBrush brush, SharpDX.Direct2D1.RenderTarget target)
        {
            PlatformBrush = new SharpDX.Direct2D1.SolidColorBrush(
                target,
                brush?.Color.ToDirect2D() ?? new SharpDX.Mathematics.Interop.RawColor4(),
                new SharpDX.Direct2D1.BrushProperties
                {
                    Opacity = brush != null ? (float)brush.Opacity : 1.0f,
                    Transform = target.Transform
                }
            );
        }
    }
}
