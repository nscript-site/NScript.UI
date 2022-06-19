using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    using Geb.Image;

    public interface IDrawContext
    {
        void Fill(Color color);
        void Fill(Color color, RectF rect);
        void DrawLine(Pen pen, PointF p0, PointF p1);
        void DrawRect(Pen pen, RectF rect);

        void DrawImage(ImageBgra32 image, RectF rect, float opacity);

        IFormattedTextImpl CreateFormattedText(
            string text,
            Typeface typeface,
            TextAlignment textAlignment,
            TextWrapping wrapping,
            SizeF constraint,
            IReadOnlyList<FormattedTextStyleSpan> spans);

        void DrawText(Brush foreground, PointF origin, IFormattedTextImpl text);

        void PushClip(RectF clip);

        void PopClip();

        Matrix Transform { get; set; }

        void Draw(IDrawContext3D cxt3d);
    }
}
