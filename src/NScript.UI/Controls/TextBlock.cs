using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Controls
{
    using NScript.UI.Media;

    public class TextBlock : Container
    {
        private string _text;
        private FormattedText _formattedText;
        private SizeF _constraint;

        public override void BuildDefaultStyleNames(List<String> names)
        {
            base.BuildDefaultStyleNames(names);
            names.Add(".textblock_default");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        public TextBlock()
        {
            _text = string.Empty;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text {
            get => _text;
            set
            {
                OnSetText(value);
            }
        }

        protected virtual void OnSetText(String newValue)
        {
            _text = newValue;
            InvalidateFormattedText();
        }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        public FontFamily FontFamily { get; set; } = FontFamily.Default;

        /// <summary>
        /// Gets or sets the font size.
        /// </summary>
        public double FontSize { get; set; } = 12;

        /// <summary>
        /// Gets or sets the font style.
        /// </summary>
        public FontStyle FontStyle { get; set; } = FontStyle.Normal;

        /// <summary>
        /// Gets or sets the font weight.
        /// </summary>
        public FontWeight FontWeight { get; set; } = FontWeight.Normal;

        /// <summary>
        /// Gets the <see cref="FormattedText"/> used to render the text.
        /// </summary>
        public FormattedText FormattedText
        {
            get
            {
                if (_formattedText == null)
                {
                    _formattedText = CreateFormattedText(_constraint, Text);
                }

                return _formattedText;
            }
        }

        /// <summary>
        /// Gets or sets the control's text wrapping mode.
        /// </summary>
        public TextWrapping TextWrapping { get; set; } = TextWrapping.Wrap;

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        public TextAlignment TextAlignment { get; set; } = TextAlignment.Left;

        protected override void DrawContent(IDrawContext cxt)
        {
            base.DrawContent(cxt);

            Matrix oldMatrix = cxt.Transform;
            cxt.Transform = oldMatrix * Matrix.CreateTranslation(ContentBound.X, ContentBound.Y);
            
            FormattedText.Constraint = ContentBound.Size;
            FormattedText.Context = cxt;
            SolidColorBrush brush = new SolidColorBrush(ForeColor);
            cxt.DrawText(brush, new PointF(), FormattedText.PlatformImpl);
            cxt.Transform = oldMatrix;
        }

        /// <summary>
        /// Creates the <see cref="FormattedText"/> used to render the text.
        /// </summary>
        /// <param name="constraint">The constraint of the text.</param>
        /// <param name="text">The text to format.</param>
        /// <returns>A <see cref="FormattedText"/> object.</returns>
        protected virtual FormattedText CreateFormattedText(SizeF constraint, string text)
        {
            return new FormattedText
            {
                Constraint = constraint,
                Typeface = new Typeface(FontFamily, FontSize, FontStyle, FontWeight),
                Text = text ?? string.Empty,
                TextAlignment = TextAlignment,
                Wrapping = TextWrapping,
            };
        }

        /// <summary>
        /// Invalidates <see cref="FormattedText"/>.
        /// </summary>
        protected void InvalidateFormattedText()
        {
            if (_formattedText != null)
            {
                _constraint = _formattedText.Constraint;
                _formattedText = null;
            }
        }

        /// <summary>
        /// Measures the control.
        /// </summary>
        /// <param name="availableSize">The available size for the control.</param>
        /// <returns>The desired size.</returns>
        protected virtual SizeF MeasureOverride(SizeF availableSize)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                if (TextWrapping == TextWrapping.Wrap)
                {
                    FormattedText.Constraint = new SizeF(availableSize.Width, float.MaxValue);
                }
                else
                {
                    FormattedText.Constraint = SizeF.Infinity;
                }

                return FormattedText.Measure();
            }

            return new SizeF();
        }
    }
}
