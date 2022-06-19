using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Media
{
    /// <summary>
    /// Defines the platform-specific interface for <see cref="FormattedText"/>.
    /// </summary>
    public interface IFormattedTextImpl
    {
        /// <summary>
        /// Gets the constraint of the text.
        /// </summary>
        SizeF Constraint { get; }

        /// <summary>
        /// The measured size of the text.
        /// </summary>
        SizeF Size { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        string Text { get; }

        /// <summary>
        /// Gets the lines in the text.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="FormattedTextLine"/> objects.
        /// </returns>
        IEnumerable<FormattedTextLine> GetLines();

        /// <summary>
        /// Hit tests a point in the text.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns>
        /// A <see cref="TextHitTestResult"/> describing the result of the hit test.
        /// </returns>
        TextHitTestResult HitTestPoint(PointF point);

        /// <summary>
        /// Gets the bounds rectangle that the specified character occupies.
        /// </summary>
        /// <param name="index">The index of the character.</param>
        /// <returns>The character bounds.</returns>
        Rect HitTestTextPosition(int index);

        /// <summary>
        /// Gets the bounds rectangles that the specified text range occupies.
        /// </summary>
        /// <param name="index">The index of the first character.</param>
        /// <param name="length">The number of characters in the text range.</param>
        /// <returns>The character bounds.</returns>
        IEnumerable<Rect> HitTestTextRange(int index, int length);
    }
}
