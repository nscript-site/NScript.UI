using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI
{
    using NScript.UI.Media;
    using NScript.UI.Controls;
    using NScript.UI.Input;

    public class WindowImpl
    {
        public Window Owner { get; internal set; }

        protected internal Action OnClosed { get; set; }
        protected internal Action OnCreated { get; set; }
        protected bool _created = false;

        public virtual void ShowDialog()
        {
        }

        public virtual void Show()
        {
        }

        public virtual void Close()
        {
        }

        public bool IsDestory { get; protected set; }

        /// <summary>
        /// 是否正在绘制
        /// </summary>
        public bool IsDrawing { get; protected set; }

        public virtual void Invalidate(bool forcePaint = false)
        {
        }

        protected void OnMouseOut()
        {
            Owner.OnMouseOut();
        }

        protected void OnMouseMove(PointF p)
        {
            Owner.OnMouseMove(p);
        }

        protected void OnMouseUp(MouseButtons mouseButtons, PointF p)
        {
            Owner.OnMouseUp(mouseButtons, p);
        }

        protected void OnMouseDown(MouseButtons mouseButtons, PointF p)
        {
            Owner.OnMouseDown(mouseButtons, p);
        }

        protected void OnMouseClick(PointF p)
        {
            Owner.OnMouseClick(p);
        }

        protected void OnMouseDoubleClick(MouseButtons mouseButtons, PointF p)
        {
            Owner.OnMouseDoubleClick(mouseButtons, p);
        }

        protected void OnMouseWheel(PointF p, float delta)
        {
            Owner.OnMouseWheel(p, delta);
        }

        protected void OnKeyDown(Key key, InputModifiers modifiers)
        {
            Owner.OnKeyDown(key, modifiers);
        }

        protected void OnKeyUp(Key key, InputModifiers modifiers)
        {
            Owner.OnKeyUp(key, modifiers);
        }

        protected void OnTextInput(String txt)
        {
            Owner.OnTextInput(txt);
        }

        protected virtual IDrawContext GetDrawContext()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 计算每 dp 对应的像素数量
        /// </summary>
        /// <returns></returns>
        public virtual float GetPixelsPerDip()
        {
            return 1.0f;
        }

        /// <summary>
        /// 计算舞台尺寸
        /// </summary>
        /// <returns></returns>
        protected Size GetStageSize()
        {
            float ppDip = GetPixelsPerDip();
            return new Size((int)(ppDip * Owner.Size.Width), (int)(ppDip * Owner.Size.Height));
        }

        protected Point GetStartPosition(Size screenSize, Size stageSize)
        {
            Point pos = new Point(Owner.Location.X, Owner.Location.Y);
            switch (Owner.StartPosition)
            {
                case Window.WindowStartAlign.CenterScreen:
                    pos.X = (screenSize.Width - stageSize.Width)/2;
                    pos.Y = Math.Max(0, (screenSize.Height - stageSize.Height) / 2);
                    break;
                case Window.WindowStartAlign.CenterParent:
                //TODO: GetStartPosition of CenterParent
                default:
                    float scale = GetPixelsPerDip();
                    pos = new Point(pos.X, pos.Y, scale);
                    break;
            }
            
            return pos;
        }

        protected internal virtual void SetCursor(Cursors cursor)
        {
        }
    }
}
