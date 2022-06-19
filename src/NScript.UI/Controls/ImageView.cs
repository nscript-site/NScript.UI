using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Controls
{
    using Geb.Image;
    using NScript.UI.Media;

    public class ImageView : Container
    {
        public ImageBgra32 Image { get; set; }

        protected override void DrawContent(IDrawContext cxt)
        {
            base.DrawContent(cxt);
            cxt.DrawImage(Image, this.ClientBound, 1.0f);
        }
    }
}
