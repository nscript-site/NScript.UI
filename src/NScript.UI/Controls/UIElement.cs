using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Controls
{
    using NScript.UI.Media;
    using NScript.UI.Input;

    public class UIElement : Visual, IComparable<UIElement>
    {
        internal long GlobalIndex { get; set; }

        private static long _globalIndex = 0;
        internal static long NextGlobalIndex
        {
            get
            {
                _globalIndex++;
                return _globalIndex;
            }
        }

        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public bool Visible { get; set; } = true;

        public bool ClipContent { get; set; } = true;

        /// <summary>
        /// 是否是装饰器。如果是装饰器，则采用 Parent 得 Capture
        /// </summary>
        public bool IsDecorator { get; protected set; } = false;

        public Cursors Cursor { get; set; } = Cursors.Arrow;

        public UIElement Parent { get; set; }

        public int ZIndex { get; set; }

        private bool _capture;
        public Boolean Capture {
            get
            {
                return IsDecorator ? Parent._capture : _capture;
            }
            set
            {
                if(IsDecorator == false)
                {
                    if (_capture == value) return;
                    _capture = value;
                    if (_capture == true) OnGotFocus(EventArgs.Empty);
                    else OnLostFocus(EventArgs.Empty);
                }
                else
                {
                    if (Parent._capture == value) return;
                    Parent._capture = value;
                    if (Parent._capture == true) Parent.OnGotFocus(EventArgs.Empty);
                    else Parent.OnLostFocus(EventArgs.Empty);
                }
            }
        }

        public bool IsDecoratorOf(UIElement parent)
        {
            return this.IsDecorator == true && Parent == parent;
        }

        public Boolean IsMouseDisable { get; set; }

        public Color BackColor = Color.TRANSPARENT;

        public Color ForeColor = Color.BLACK;  

        public Matrix Transform { get; set; } = new Matrix(1, 0, 0, 1, 0, 0);

        public RectF Bound
        {
            get { return new RectF(0, 0, Size.Width, Size.Height); }
        }

        protected bool _painted = false;
        public virtual void SetInvalidated(Boolean value)
        {
            this._painted = value;
        }

        public virtual void Invalidate(bool forcePaint = true)
        {
            if (_painted == true || forcePaint == true)
            {
                _painted = false;
                if (Parent != null) Parent.Invalidate();
            }
        }

        public override void Draw(IDrawContext cxt)
        {
            if (this.Visible == false) return;

            Measure(cxt);

            Matrix oldMatrix = cxt.Transform;
            cxt.Transform = oldMatrix * Matrix.CreateTranslation(Location.X, Location.Y);
            DrawBackground(cxt);

            Nullable<RectF> clip = GetContentClip();
            if (clip.HasValue) cxt.PushClip(clip.Value);
            DrawContent(cxt);
            if (clip.HasValue) cxt.PopClip();

            FinishedDrawContent(cxt);

            cxt.Transform = oldMatrix;
        }

        protected virtual Nullable<RectF> GetContentClip()
        {
            return null;
        }

        protected virtual void DrawContent(IDrawContext cxt)
        {
        }

        protected virtual void DrawBackground(IDrawContext cxt)
        {
            if (cxt == null || BackColor.Alpha == 0) return;
            cxt.Fill(BackColor, Bound);
        }

        protected virtual void FinishedDrawContent(IDrawContext cxt)
        {
        }

        public int CompareTo(UIElement other)
        {
            return this.ZIndex == other.ZIndex ? this.GlobalIndex.CompareTo(other.GlobalIndex) : this.ZIndex.CompareTo(other.ZIndex);
        }

        protected internal virtual void OnCreate()
        {
        }

        public virtual void Measure(IDrawContext cxt)
        {
        }

        public delegate void MouseEventHandler(UIElement sender, MouseEventArgs e);
        public delegate void KeyEventHandler(UIElement sender, KeyEventArgs e);
        public delegate void UIElementEventHandler(UIElement sender, EventArgs e);
        public delegate void TextInputEventHandler(UIElement sender, TextInputEventArgs e);

        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseWheel;
        public event UIElementEventHandler MouseEnter;
        public event UIElementEventHandler MouseOut;
        public event MouseEventHandler MouseClick;
        public event MouseEventHandler MouseDoubleClick;
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event TextInputEventHandler TextInput;

        protected internal virtual void OnMouseDown(MouseEventArgs e)
        {
            Capture = true;
            if (MouseDown != null) MouseDown(this, e);
        }

        protected internal virtual void OnMouseUp(MouseEventArgs e)
        {
            if (MouseUp != null) MouseUp(this, e);
        }

        protected internal virtual void OnMouseMove(MouseEventArgs e)
        {
            if (MouseMove != null) MouseMove(this, e);
        }

        protected internal virtual void OnMouseWheel(MouseEventArgs e)
        {
            if (MouseWheel != null) MouseWheel(this, e);
        }

        protected internal virtual void OnMouseEnter()
        {
            if (MouseEnter != null) MouseEnter(this, EventArgs.Empty);
        }

        protected internal virtual void OnMouseOut()
        {
            if (MouseOut != null) MouseOut(this, EventArgs.Empty);
        }

        protected internal virtual void OnMouseClick(MouseEventArgs e)
        {
            if (MouseClick != null) MouseClick(this, e);
        }

        protected internal virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (MouseDoubleClick != null) MouseDoubleClick(this, e);
        }

        protected internal virtual void OnGotFocus(EventArgs e)
        {
        }

        protected internal virtual void OnLostFocus(EventArgs e)
        {
        }

        protected internal virtual void OnKeyDown(KeyEventArgs e)
        {
            if (KeyDown != null) KeyDown(this, e);
            if (Parent != null) Parent.OnKeyDown(e);
        }

        protected internal virtual void OnKeyUp(KeyEventArgs e)
        {
            if (KeyUp != null) KeyUp(this, e);
            if (Parent != null) Parent.OnKeyUp(e);
        }

        protected internal virtual void OnTextInput(TextInputEventArgs e)
        {
            if (TextInput != null) TextInput(this, e);
            if (Parent != null) Parent.OnTextInput(e);
        }

        public virtual UIElement HitTest(float x, float y)
        {
            if (Visible == false || IsMouseDisable == true || x < 0 || y < 0 || x > Size.Width || y > Size.Height) return null;
            else return this;
        }

        public PointF LocalToGlobal(PointF pos)
        {
            if (this is Window || Parent == null) return pos + Location;
            return Parent.LocalToGlobal(pos + Location);
        }

        public PointF GlobalToLocal(PointF pos)
        {
            if (this is Window || Parent == null) return pos - Location;
            return Parent.GlobalToLocal(pos - Location);
        }
    }
}
