using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    public abstract class D2DBrush : IDisposable
    {
        public SharpDX.Direct2D1.Brush PlatformBrush { get; set; }

        public virtual void Dispose()
        {
            if (PlatformBrush != null)
            {
                PlatformBrush.Dispose();
            }
        }
    }
}
