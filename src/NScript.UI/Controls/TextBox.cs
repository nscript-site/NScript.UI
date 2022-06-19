//// Copyright (c) The Avalonia Project. All rights reserved.
//// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;

namespace NScript.UI.Controls
{
    using NScript.UI.Utils;
    using NScript.UI.Media;
    using NScript.UI.Input;

    public struct TextBoxUndoRedoState : IEquatable<TextBoxUndoRedoState>
    {
        public string Text { get; }
        public int CaretPosition { get; }

        public TextBoxUndoRedoState(string text, int caretPosition)
        {
            Text = text;
            CaretPosition = caretPosition;
        }

        public bool Equals(TextBoxUndoRedoState other) => ReferenceEquals(Text, other.Text) || Equals(Text, other.Text);
    }

    public class TextBox : TextPresenter, IUndoRedoHost<TextBoxUndoRedoState>
    {
        private UndoRedoHelper<TextBoxUndoRedoState> _undoRedoHelper;
        private bool _isUndoingRedoing;
        private string _newLine = Environment.NewLine;
        private static readonly string[] invalidCharacters = new String[1] { "\u007f" };

        public TextBox()
        {
            _undoRedoHelper = new UndoRedoHelper<TextBoxUndoRedoState>(this);
            Cursor = Cursors.IBeam;
        }

        public override void BuildDefaultStyleNames(List<String> names)
        {
            base.BuildDefaultStyleNames(names);
            names.Add(".textbox_default");
        }

        public bool AcceptsReturn { get; set; } = true;

        public bool AcceptsTab { get; set; } = true;

        protected override int CoerceCaretIndex(int value)
        {
            int result = base.CoerceCaretIndex(value);
            TextBoxUndoRedoState state;
            //if (_undoRedoHelper.TryGetLastState(out state) && state.Text == Text)
            //    _undoRedoHelper.UpdateLastState();
            return result;
        }

        /// <summary>
        /// 是否下一次绘制时滚动到光标处。绘制循环中如果发现 _isScrollToCaretNextRenderFlag 为 true，则应
        /// 让光标位置的内容尽量显示出来。当绘制完毕，设置 _isScrollToCaretNextRenderFlag 为 false。
        /// </summary>
        protected bool _isScrollToCaretNextRenderFlag = false;

        public bool IsReadOnly { get; set; }

        protected override void OnSetText(string newValue)
        {
            base.OnSetText(newValue);

            CaretIndex = CoerceCaretIndex(CaretIndex, newValue?.Length ?? 0);

            if (!_isUndoingRedoing)
            {
                //_undoRedoHelper.Clear();
            }
        }

        protected override void MeasureContent(IDrawContext cxt)
        {
            FormattedText.Constraint = ClientBound.Size;
            FormattedText.Context = cxt;
            SizeF size = FormattedText.Measure();

            ContentBound = new RectF(ClientBound.X, ClientBound.Y, size.Width, size.Height) + GetContentBoundOffset();
            if (_isScrollToCaretNextRenderFlag == true && ContentBound.Height > ClientBound.Height)
            {
                Rect rect = FormattedText.HitTestTextPosition(CaretIndex);
                Range r1 = ScrollerBar.GetValueRangeIfContentDisplay(ClientBound.Height, ContentBound.Height, rect.Top);
                Range r2 = ScrollerBar.GetValueRangeIfContentDisplay(ClientBound.Height, ContentBound.Height, rect.Bottom);
                
                if(VerticalBar != null)
                {
                    float scrollerBarVal = VerticalBar.Value;

                    float d1 = r1.GetDistance(scrollerBarVal);
                    float d2 = r2.GetDistance(scrollerBarVal);

                    if (d1 == 0 && d2 == 0) return;
                    if (d1 > d2) scrollerBarVal = r1.Min;
                    else scrollerBarVal = r2.Max;
                    
                    float offset = ScrollerBar.GetContentOffset(ClientBound.Height, ContentBound.Height, scrollerBarVal);
                    ContentBound = new RectF(ClientBound.X, ClientBound.Y + offset, size.Width, size.Height);
                }
            }
        }

        /// <summary>
        /// Gets or sets which characters are inserted when Enter is pressed. Default: <see cref="Environment.NewLine"/>
        /// </summary>
        public string NewLine { get; set; } = Environment.NewLine;

        protected  internal override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            DecideCaretVisibility();
        }

        private void DecideCaretVisibility()
        {
            if (!IsReadOnly)
                this.ShowCaret();
            else
                this.HideCaret();
        }

        protected override void DrawContent(IDrawContext cxt)
        {
            base.DrawContent(cxt);
            _isScrollToCaretNextRenderFlag = false;
        }

        protected internal override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            SelectionStart = 0;
            SelectionEnd = 0;
            this.HideCaret();
        }

        protected internal override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
            if (!e.Handled)
            {
                HandleTextInput(e.Text);
                e.Handled = true;
            }
        }

        private void HandleTextInput(string input)
        {
            if (!IsReadOnly)
            {
                input = RemoveInvalidCharacters(input);
                string text = Text ?? string.Empty;
                int caretIndex = CaretIndex;
                if (!string.IsNullOrEmpty(input))
                {
                    DeleteSelection();
                    caretIndex = CaretIndex;
                    text = Text ?? string.Empty;
                    SetTextInternal(text.Substring(0, caretIndex) + input + text.Substring(caretIndex));
                    CaretIndex += input.Length;
                    SelectionStart = SelectionEnd = CaretIndex;
                    //_undoRedoHelper.DiscardRedo();
                    this.Invalidate();
                }
            }
        }

        public string RemoveInvalidCharacters(string text)
        {
            for (var i = 0; i < invalidCharacters.Length; i++)
            {
                text = text.Replace(invalidCharacters[i], string.Empty);
            }

            return text;
        }

        private async void Copy()
        {
            await (Platform.Instance.GetClipboard().SetTextAsync(GetSelection()));
        }

        private async void Paste()
        {
            var text = await Platform.Instance.GetClipboard().GetTextAsync();
            if (text == null)
            {
                return;
            }
            //_undoRedoHelper.Snapshot();
            HandleTextInput(text);
        }

        protected internal override void OnKeyDown(KeyEventArgs e)
        {
            string text = Text ?? string.Empty;
            int caretIndex = CaretIndex;
            bool movement = false;
            bool handled = false;
            var modifiers = e.Modifiers;

            switch (e.Key)
            {
                case Key.A:
                    if (modifiers == InputModifiers.Control)
                    {
                        SelectAll();
                        handled = true;
                    }
                    break;
                case Key.C:
                    if (modifiers == InputModifiers.Control)
                    {
                        if (!IsPasswordBox)
                        {
                            Copy();
                        }
                        handled = true;
                    }
                    break;

                case Key.X:
                    if (modifiers == InputModifiers.Control)
                    {
                        if (!IsPasswordBox)
                        {
                            Copy();
                            DeleteSelection();
                        }
                        handled = true;
                    }
                    break;

                case Key.V:
                    if (modifiers == InputModifiers.Control)
                    {
                        Paste();
                        handled = true;
                    }

                    break;

                case Key.Z:
                    if (modifiers == InputModifiers.Control)
                    {
                        try
                        {
                            _isUndoingRedoing = true;
                            //_undoRedoHelper.Undo();
                        }
                        finally
                        {
                            _isUndoingRedoing = false;
                        }
                        handled = true;
                    }
                    break;
                case Key.Y:
                    if (modifiers == InputModifiers.Control)
                    {
                        try
                        {
                            _isUndoingRedoing = true;
                            //_undoRedoHelper.Redo();
                        }
                        finally
                        {
                            _isUndoingRedoing = false;
                        }
                        handled = true;
                    }
                    break;
                case Key.Left:
                    MoveHorizontal(-1, modifiers);
                    movement = true;
                    break;

                case Key.Right:
                    MoveHorizontal(1, modifiers);
                    movement = true;
                    break;

                case Key.Up:
                    movement = MoveVertical(-1, modifiers);
                    break;

                case Key.Down:
                    movement = MoveVertical(1, modifiers);
                    break;

                case Key.Home:
                    MoveHome(modifiers);
                    movement = true;
                    break;

                case Key.End:
                    MoveEnd(modifiers);
                    movement = true;
                    break;

                case Key.Back:
                    if (modifiers == InputModifiers.Control && SelectionStart == SelectionEnd)
                    {
                        SetSelectionForControlBackspace(modifiers);
                    }

                    if (!DeleteSelection() && CaretIndex > 0)
                    {
                        var removedCharacters = 1;
                        // handle deleting /r/n
                        // you don't ever want to leave a dangling /r around. So, if deleting /n, check to see if 
                        // a /r should also be deleted.
                        if (CaretIndex > 1 &&
                            text[CaretIndex - 1] == '\n' &&
                            text[CaretIndex - 2] == '\r')
                        {
                            removedCharacters = 2;
                        }

                        int newCaretIndex = CaretIndex - removedCharacters;
                        SetTextInternal(text.Substring(0, caretIndex - removedCharacters) + text.Substring(caretIndex));
                        SelectionStart = SelectionEnd = CaretIndex = newCaretIndex;
                    }
                    handled = true;
                    break;

                case Key.Delete:
                    if (modifiers == InputModifiers.Control && SelectionStart == SelectionEnd)
                    {
                        SetSelectionForControlDelete(modifiers);
                    }

                    if (!DeleteSelection() && caretIndex < text.Length)
                    {
                        var removedCharacters = 1;
                        // handle deleting /r/n
                        // you don't ever want to leave a dangling /r around. So, if deleting /n, check to see if 
                        // a /r should also be deleted.
                        if (CaretIndex < text.Length - 1 &&
                            text[caretIndex + 1] == '\n' &&
                            text[caretIndex] == '\r')
                        {
                            removedCharacters = 2;
                        }

                        SetTextInternal(text.Substring(0, caretIndex) + text.Substring(caretIndex + removedCharacters));
                    }
                    handled = true;
                    break;

                case Key.Enter:
                    if (AcceptsReturn)
                    {
                        HandleTextInput(NewLine);
                        handled = true;
                    }

                    break;

                case Key.Tab:
                    if (AcceptsTab)
                    {
                        HandleTextInput("\t");
                        handled = true;
                    }
                    else
                    {
                        base.OnKeyDown(e);
                    }

                    break;

                default:
                    handled = false;
                    break;
            }

            if (movement && ((modifiers & InputModifiers.Shift) != 0))
            {
                SelectionEnd = CaretIndex;
            }
            else if (movement)
            {
                SelectionStart = SelectionEnd = CaretIndex;
            }

            if (handled || movement)
            {
                e.Handled = true;
            }

            _isScrollToCaretNextRenderFlag = true;
            this.Invalidate();
        }

        protected internal override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            var point = new PointF(e.X, e.Y);
            var index = CaretIndex = GetCaretIndex(point);
            var text = Text;

            if (text != null && e.Button == MouseButtons.Left)
            {
                _isSelecting = true;
                SelectionStart = SelectionEnd = index;
            }

            this.Invalidate();

            e.Handled = true;
        }

        protected bool _isSelecting = false;
        protected internal override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            var point = new PointF(e.X, e.Y);
            var index = CaretIndex = GetCaretIndex(point);
            var text = Text;

            if (text != null && e.Button == MouseButtons.Left)
            {
                SelectionStart = SelectionEnd = index;

                if (!StringUtils.IsStartOfWord(text, index))
                {
                    SelectionStart = StringUtils.PreviousWord(text, index);
                }

                SelectionEnd = StringUtils.NextWord(text, index);
            }

            e.Handled = true;
            Invalidate();
        }

        protected internal override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if(_isSelecting == true)
            {
                var point = new PointF(e.X, e.Y);
                CaretIndex = SelectionEnd = GetCaretIndex(point);
                _isScrollToCaretNextRenderFlag = true;
                Invalidate();
            }
        }

        protected internal override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isSelecting = false;
        }

        protected internal override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (VerticalBar != null) VerticalBar.ScrollDelta(-e.Delta);
        }

        private int CoerceCaretIndex(int value, int length)
        {
            var text = Text;

            if (value < 0)
            {
                return 0;
            }
            else if (value > length)
            {
                return length;
            }
            else if (value > 0 && text[value - 1] == '\r' && text[value] == '\n')
            {
                return value + 1;
            }
            else
            {
                return value;
            }
        }

        private int DeleteCharacter(int index)
        {
            var start = index + 1;
            var text = Text;
            var c = text[index];
            var result = 1;

            if (c == '\n' && index > 0 && text[index - 1] == '\r')
            {
                --index;
                ++result;
            }
            else if (c == '\r' && index < text.Length - 1 && text[index + 1] == '\n')
            {
                ++start;
                ++result;
            }

            Text = text.Substring(0, index) + text.Substring(start);

            return result;
        }

        private void MoveHorizontal(int direction, InputModifiers modifiers)
        {
            var text = Text ?? string.Empty;
            var caretIndex = CaretIndex;

            if ((modifiers & InputModifiers.Control) == 0)
            {
                var index = caretIndex + direction;

                if (index < 0 || index > text.Length)
                {
                    return;
                }
                else if (index == text.Length)
                {
                    CaretIndex = index;
                    return;
                }

                var c = text[index];

                if (direction > 0)
                {
                    CaretIndex += (c == '\r' && index < text.Length - 1 && text[index + 1] == '\n') ? 2 : 1;
                }
                else
                {
                    CaretIndex -= (c == '\n' && index > 0 && text[index - 1] == '\r') ? 2 : 1;
                }
            }
            else
            {
                if (direction > 0)
                {
                    CaretIndex += StringUtils.NextWord(text, caretIndex) - caretIndex;
                }
                else
                {
                    CaretIndex += StringUtils.PreviousWord(text, caretIndex) - caretIndex;
                }
            }
        }

        private bool MoveVertical(int count, InputModifiers modifiers)
        {
            var formattedText = this.FormattedText;
            var lines = formattedText.GetLines().ToList();
            var caretIndex = CaretIndex;
            var lineIndex = GetLine(caretIndex, lines) + count;

            if (lineIndex >= 0 && lineIndex < lines.Count)
            {
                var line = lines[lineIndex];
                var rect = formattedText.HitTestTextPosition(caretIndex);
                var y = count < 0 ? rect.Y : rect.Bottom;
                var point = new PointF(rect.X, y + (count * (line.Height / 2.0f)));
                var hit = formattedText.HitTestPoint(point);
                CaretIndex = hit.TextPosition + (hit.IsTrailing ? 1 : 0);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void MoveHome(InputModifiers modifiers)
        {
            var text = Text ?? string.Empty;
            var caretIndex = CaretIndex;

            if ((modifiers & InputModifiers.Control) != 0)
            {
                caretIndex = 0;
            }
            else
            {
                var lines = this.FormattedText.GetLines();
                var pos = 0;

                foreach (var line in lines)
                {
                    if (pos + line.Length > caretIndex || pos + line.Length == text.Length)
                    {
                        break;
                    }

                    pos += line.Length;
                }

                caretIndex = pos;
            }

            CaretIndex = caretIndex;
        }

        private void MoveEnd(InputModifiers modifiers)
        {
            var text = Text ?? string.Empty;
            var caretIndex = CaretIndex;

            if ((modifiers & InputModifiers.Control) != 0)
            {
                caretIndex = text.Length;
            }
            else
            {
                var lines = this.FormattedText.GetLines();
                var pos = 0;

                foreach (var line in lines)
                {
                    pos += line.Length;

                    if (pos > caretIndex)
                    {
                        if (pos < text.Length)
                        {
                            --pos;
                            if (pos > 0 && text[pos - 1] == '\r' && text[pos] == '\n')
                            {
                                --pos;
                            }
                        }

                        break;
                    }
                }

                caretIndex = pos;
            }

            CaretIndex = caretIndex;
        }

        private void SelectAll()
        {
            SelectionStart = 0;
            SelectionEnd = Text?.Length ?? 0;
        }

        private bool DeleteSelection()
        {
            if (!IsReadOnly)
            {
                var selectionStart = SelectionStart;
                var selectionEnd = SelectionEnd;

                if (selectionStart != selectionEnd)
                {
                    var start = Math.Min(selectionStart, selectionEnd);
                    var end = Math.Max(selectionStart, selectionEnd);
                    var text = Text;
                    SetTextInternal(text.Substring(0, start) + text.Substring(end));
                    SelectionStart = SelectionEnd = CaretIndex = start;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private string GetSelection()
        {
            var text = Text;
            if (string.IsNullOrEmpty(text))
                return "";
            var selectionStart = SelectionStart;
            var selectionEnd = SelectionEnd;
            var start = Math.Min(selectionStart, selectionEnd);
            var end = Math.Max(selectionStart, selectionEnd);
            if (start == end || (Text?.Length ?? 0) < end)
            {
                return "";
            }
            return text.Substring(start, end - start);
        }

        private int GetLine(int caretIndex, IList<FormattedTextLine> lines)
        {
            int pos = 0;
            int i;

            for (i = 0; i < lines.Count - 1; ++i)
            {
                var line = lines[i];
                pos += line.Length;

                if (pos > caretIndex)
                {
                    break;
                }
            }

            return i;
        }

        private void SetTextInternal(string value)
        {
            Text = value;
        }

        private void SetSelectionForControlBackspace(InputModifiers modifiers)
        {
            SelectionStart = CaretIndex;
            MoveHorizontal(-1, modifiers);
            SelectionEnd = CaretIndex;
        }

        private void SetSelectionForControlDelete(InputModifiers modifiers)
        {
            SelectionStart = CaretIndex;
            MoveHorizontal(1, modifiers);
            SelectionEnd = CaretIndex;
        }

        private bool IsPasswordBox => PasswordChar != default(char);

        public TextBoxUndoRedoState UndoRedoState
        {
            get { return new TextBoxUndoRedoState(Text, CaretIndex); }
            set
            {
                Text = value.Text;
                SelectionStart = SelectionEnd = CaretIndex = value.CaretPosition;
            }
        }
    }
}
