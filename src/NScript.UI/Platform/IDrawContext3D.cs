using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI
{
    using NScript.UI.Media;

    public interface IDrawContext3D
    {
        void Measure(SizeF size);
        float Width { get; }
        float Height { get; }
    }
}
