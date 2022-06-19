using System;
using System.Collections.Generic;
using System.Text;
using NScript.UI.Media;

namespace NScript.UI.Controls
{
    public class UIElement3D : UIElement
    {
        private IDrawContext3D _drawContext;
        protected internal override void OnCreate()
        {
            base.OnCreate();
            _drawContext = Platform.Instance.CreateDrawContext3D();
        }

        public override void Measure(IDrawContext cxt)
        {
            base.Measure(cxt);
            if (_drawContext != null) _drawContext.Measure(Size);
        }

        protected override void DrawContent(IDrawContext cxt)
        {
            if (_drawContext != null) cxt.Draw(_drawContext);
        }
    }
}
