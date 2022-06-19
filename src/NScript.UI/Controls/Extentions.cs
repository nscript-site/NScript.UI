using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Controls
{
    public static class Extentions
    {
        public static float GetBorderThickness(this Container container)
        {
            if (container == null || container.Style == null || container.Style.Border.HasValue == false) return 1.0f;
            return Math.Max(0, container.Style.Border.Value.Thickness);
        }
    }
}
