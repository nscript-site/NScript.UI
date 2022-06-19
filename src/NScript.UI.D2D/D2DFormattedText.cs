using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NScript.UI.D2D
{
    using NScript.UI.Media;
    using DWrite = SharpDX.DirectWrite;
    
    public class D2DFormattedText : IFormattedTextImpl
    {
        public D2DFormattedText(
            string text,
            Typeface typeface,
            TextAlignment textAlignment,
            TextWrapping wrapping,
            SizeF constraint,
            IReadOnlyList<FormattedTextStyleSpan> spans)
        {
            Text = text;

            using (var textFormat = D2DFontCollectionCache.GetTextFormat(typeface))
            {
                textFormat.WordWrapping =
                    wrapping == TextWrapping.Wrap ? (DWrite.WordWrapping.Wrap) : DWrite.WordWrapping.NoWrap;

                TextLayout = new DWrite.TextLayout(
                                 D2DPlatform.DirectWriteFactory,
                                 Text ?? string.Empty,
                                 textFormat,
                                 (float)constraint.Width,
                                 (float)constraint.Height);
                TextLayout.TextAlignment = textAlignment.ToDirect2D();
                var lines = TextLayout.GetLineMetrics();
                Console.WriteLine(lines);
            }

            if (spans != null)
            {
                foreach (var span in spans)
                {
                    ApplySpan(span);
                }
            }

            Size = Measure();
        }

        public SizeF Constraint => new SizeF(TextLayout.MaxWidth, TextLayout.MaxHeight);

        public SizeF Size { get; }

        public string Text { get; }

        public DWrite.TextLayout TextLayout { get; }

        public IEnumerable<FormattedTextLine> GetLines()
        {
            var result = TextLayout.GetLineMetrics();
            return from line in result select new FormattedTextLine(line.Length, line.Height);
        }

        public TextHitTestResult HitTestPoint(PointF point)
        {
            var result = TextLayout.HitTestPoint(
                point.X,
                point.Y,
                out var isTrailingHit,
                out var isInside);

            return new TextHitTestResult
            {
                IsInside = isInside,
                TextPosition = result.TextPosition,
                IsTrailing = isTrailingHit,
            };
        }

        public Rect HitTestTextPosition(int index)
        {
            var result = TextLayout.HitTestTextPosition(index, false, out _, out _);

            return new Rect(result.Left, result.Top, result.Width, result.Height);
        }

        public IEnumerable<Rect> HitTestTextRange(int index, int length)
        {
            var result = TextLayout.HitTestTextRange(index, length, 0, 0);
            return result.Select(x => new Rect(x.Left, x.Top, x.Width, x.Height));
        }

        private void ApplySpan(FormattedTextStyleSpan span)
        {
            if (span.Length > 0)
            {
                if (span.ForegroundBrush != null)
                {
                    TextLayout.SetDrawingEffect(
                        new BrushWrapper(span.ForegroundBrush.ToImmutable()),
                        new DWrite.TextRange(span.StartIndex, span.Length));
                }
            }
        }

        private SizeF Measure()
        {
            var metrics = TextLayout.Metrics;
            var width = metrics.Width;
            return new SizeF(width, TextLayout.Metrics.Height);
        }
    }
}
