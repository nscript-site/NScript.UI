// Copyright (c) The Avalonia Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Timers;

namespace NScript.UI.Controls
{
    using NScript.UI.Media;

    public class TextPresenter : TextBlock
    {
        protected readonly System.Timers.Timer _caretTimer;
        protected int _caretIndex;
        protected int _selectionStart;
        protected int _selectionEnd;
        protected bool _caretBlink;

        public TextPresenter()
        {
            _caretTimer = new System.Timers.Timer(500);
            _caretTimer.Elapsed += CaretTimerTick;
        }

        public Color HighlightColor = Color.RED;

        public int CaretIndex
        {
            get
            {
                return _caretIndex;
            }

            set
            {
                _caretIndex = CoerceCaretIndex(value);
            }
        }

        public char PasswordChar { get; set; } = default(Char);

        public int SelectionStart
        {
            get
            {
                return _selectionStart;
            }

            set
            {
                _selectionStart = CoerceCaretIndex(value);
            }
        }

        public int SelectionEnd
        {
            get
            {
                return _selectionEnd;
            }

            set
            {
                _selectionEnd = CoerceCaretIndex(value);
            }
        }

        public int GetCaretIndex(PointF point)
        {
            var hit = FormattedText.HitTestPoint(point - ContentBound.Location);
            return hit.TextPosition + (hit.IsTrailing ? 1 : 0);
        }

        protected override void DrawBackground(IDrawContext cxt)
        {
            base.DrawBackground(cxt);

            var selectionStart = SelectionStart;
            var selectionEnd = SelectionEnd;

            if (selectionStart != selectionEnd)
            {
                cxt.PushClip(ClientBound);
                var start = Math.Min(selectionStart, selectionEnd);
                var length = Math.Max(selectionStart, selectionEnd) - start;

                FormattedText.Constraint = ContentBound.Size;

                var rects = FormattedText.HitTestTextRange(start, length);

                foreach (var rect in rects)
                {
                    cxt.Fill(HighlightColor, new RectF(rect.X + ContentBound.X, rect.Y + ContentBound.Y, rect.Width, rect.Height));
                }
                cxt.PopClip();
            }
        }

        protected override void DrawContent(IDrawContext cxt)
        {
            var selectionStart = SelectionStart;
            var selectionEnd = SelectionEnd;
            base.DrawContent(cxt);

            if (selectionStart == selectionEnd)
            {
                var backgroundColor = BackColor;
                var caretBrush = Color.BLACK;

                if (_caretBlink)
                {
                    var charPos = FormattedText.HitTestTextPosition(CaretIndex);
                    var x = Math.Floor((double)charPos.X) + 0.5 + ContentBound.X;
                    var y = Math.Floor((double)charPos.Y) + 0.5 + ContentBound.Y;
                    var b = Math.Floor((double)charPos.Bottom) + ContentBound.Y;

                    cxt.DrawLine(new Pen(caretBrush,1),new PointF((float)x, (float)y), new PointF((float)x, (float)b));
                }
            }
        }

        public void ShowCaret()
        {
            _caretBlink = true;
            _caretTimer.Start();
            CaretTimerTick(null, null);
        }

        public void HideCaret()
        {
            _caretBlink = false;
            _caretTimer.Stop();
        }

        internal void CaretIndexChanged(int caretIndex)
        {
            if (this.Parent == null) return;

            if (_caretTimer.Enabled)
            {
                _caretBlink = true;
                _caretTimer.Stop();
                _caretTimer.Start();
                Invalidate();
            }
            else
            {
                _caretTimer.Start();
                Invalidate();
                _caretTimer.Stop();
            }
        }

        /// <summary>
        /// Creates the <see cref="FormattedText"/> used to render the text.
        /// </summary>
        /// <param name="constraint">The constraint of the text.</param>
        /// <param name="text">The text to generated the <see cref="FormattedText"/> for.</param>
        /// <returns>A <see cref="FormattedText"/> object.</returns>
        protected override FormattedText CreateFormattedText(SizeF constraint, string text)
        {
            FormattedText result = null;

            if (PasswordChar != default(char))
            {
                result = base.CreateFormattedText(constraint, new string(PasswordChar, text?.Length ?? 0));
            }
            else
            {
                result = base.CreateFormattedText(constraint, text);
            }

            var selectionStart = SelectionStart;
            var selectionEnd = SelectionEnd;
            var start = Math.Min(selectionStart, selectionEnd);
            var length = Math.Max(selectionStart, selectionEnd) - start;

            if (length > 0)
            {
                result.Spans = new[]
                {
                    new FormattedTextStyleSpan(start, length, foregroundBrush: new SolidColorBrush(Color.WHITE)),
                };
            }

            return result;
        }

        protected override SizeF MeasureOverride(SizeF availableSize)
        {
            var text = Text;

            if (!string.IsNullOrEmpty(text))
            {
                return base.MeasureOverride(availableSize);
            }
            else
            {
                return new FormattedText
                {
                    Text = "X",
                    Typeface = new Typeface(FontFamily, FontSize, FontStyle, FontWeight),
                    TextAlignment = TextAlignment,
                    Constraint = availableSize,
                }.Measure();
            }
        }

        protected virtual int CoerceCaretIndex(int value)
        {
            var text = Text;
            var length = text?.Length ?? 0;
            return Math.Max(0, Math.Min(length, value));
        }

        private void CaretTimerTick(object sender, ElapsedEventArgs e)
        {
            _caretBlink = !_caretBlink;
            this.Invalidate();
        }
    }
}
