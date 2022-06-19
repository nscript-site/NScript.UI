using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    /// <summary>
    /// Represents a piece of text with formatting.
    /// </summary>
    public class FormattedText
    {
        private SizeF _constraint = new SizeF(30000,30000);
        private IReadOnlyList<FormattedTextStyleSpan> _spans;
        private Typeface _typeface;
        private string _text;
        private TextAlignment _textAlignment;
        private TextWrapping _wrapping;
        private IDrawContext _context;
        private IFormattedTextImpl _platformImpl;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedText"/> class.
        /// </summary>
        public FormattedText()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedText"/> class.
        /// </summary>
        /// <param name="platform">The platform render interface.</param>
        public FormattedText(IDrawContext context)
        {
            _context = context;
        }

        public IDrawContext Context
        {
            get => _context;
            set => _context = value;
        }

        /// <summary>
        /// Gets or sets the constraint of the text.
        /// </summary>
        public SizeF Constraint
        {
            get => _constraint;
            set => Set(ref _constraint, value);
        }

        /// <summary>
        /// Gets or sets the base typeface.
        /// </summary>
        public Typeface Typeface
        {
            get => _typeface;
            set => Set(ref _typeface, value);
        }

        /// <summary>
        /// Gets or sets a collection of spans that describe the formatting of subsections of the
        /// text.
        /// </summary>
        public IReadOnlyList<FormattedTextStyleSpan> Spans
        {
            get => _spans;
            set => Set(ref _spans, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get => _text;
            set => Set(ref _text, value);
        }

        /// <summary>
        /// Gets or sets the alignment of the text.
        /// </summary>
        public TextAlignment TextAlignment
        {
            get => _textAlignment;
            set => Set(ref _textAlignment, value);
        }

        /// <summary>
        /// Gets or sets the text wrapping.
        /// </summary>
        public TextWrapping Wrapping
        {
            get => _wrapping;
            set => Set(ref _wrapping, value);
        }

        /// <summary>
        /// Gets platform-specific platform implementation.
        /// </summary>
        public IFormattedTextImpl PlatformImpl
        {
            get
            {
                if (_platformImpl == null)
                {
                    _platformImpl = _context.CreateFormattedText(
                        _text,
                        _typeface,
                        _textAlignment,
                        _wrapping,
                        _constraint,
                        _spans);
                }

                return _platformImpl;
            }
        }

        /// <summary>
        /// Gets the lines in the text.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="FormattedTextLine"/> objects.
        /// </returns>
        public IEnumerable<FormattedTextLine> GetLines()
        {
            return PlatformImpl.GetLines();
        }

        /// <summary>
        /// Hit tests a point in the text.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// A <see cref="TextHitTestResult"/> describing the result of the hit test.
        /// </returns>
        public TextHitTestResult HitTestPoint(PointF point)
        {
            return PlatformImpl.HitTestPoint(point);
        }

        /// <summary>
        /// Gets the bounds rectangle that the specified character occupies.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character bounds.</returns>
        public Rect HitTestTextPosition(int index)
        {
            return PlatformImpl.HitTestTextPosition(index);
        }

        /// <summary>
        /// Gets the bounds rectangles that the specified text range occupies.
        /// </summary>
        /// <param name="index">The index of the first character.</param>
        /// <param name="length">The number of characters in the text range.</param>
        /// <returns>The character bounds.</returns>
        public IEnumerable<Rect> HitTestTextRange(int index, int length)
        {
            return PlatformImpl.HitTestTextRange(index, length);
        }

        /// <summary>
        /// Gets the size of the text, taking <see cref="Constraint"/> into account.
        /// </summary>
        /// <returns>The bounds box of the text.</returns>
        public SizeF Measure()
        {
            return PlatformImpl.Size;
        }

        private void Set<T>(ref T field, T value)
        {
            field = value;
            _platformImpl = null;
        }
    }
}
