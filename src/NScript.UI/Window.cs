using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI
{
    using NScript.UI.Controls;
    using NScript.UI.Media;
    using NScript.UI.Input;

    /// <summary>
    /// Defines the minimized/maximized state of a <see cref="Window"/>.
    /// </summary>
    public enum WindowState
    {
        /// <summary>
        /// The window is neither minimized or maximized.
        /// </summary>
        Normal,

        /// <summary>
        /// The window is minimized.
        /// </summary>
        Minimized,

        /// <summary>
        /// The window is maximized.
        /// </summary>
        Maximized,
    }

    public class Window : Container
    {
        public enum WindowStartAlign
        {
            CenterScreen,
            CenterParent,
            Default
        }

        private UIElement _captureObj;
        private UIElement _lastMouseHoverObject;
        private UIElement _lastMouseDownObject;
        private WindowImpl _impl;

        /// <summary>
        /// 是否正在绘制
        /// </summary>
        public bool IsDrawing { get { return _impl.IsDrawing; } }

        public WindowImpl Impl { get { return _impl; } }

        public String Title { get; set; }

        public WindowStartAlign StartPosition { get; set; } = WindowStartAlign.CenterScreen;

        private System.Threading.Thread _renderLoopThread;

        public Window(int width, int height, String title)
        {
            this.Size = new SizeF(width, height);
            this.Title = title;
            this.BackColor = Color.WHITE;
            _impl = Platform.Instance.CreateWindow();
            _impl.Owner = this;
            _impl.OnClosed = this.OnClosed;
            _impl.OnCreated = this.OnCreated;
        }

        public void Show()
        {
            _impl.Show();
        }

        private double _displaySeconds = 0;

        protected virtual void OnCreated()
        {
            if (_renderLoop != null)
            {
                _renderLoopThread = new System.Threading.Thread(new System.Threading.ThreadStart(RenderLoop));
                _renderLoopThread.Start();
            }

            if (_displaySeconds > 0)
            {
                System.Threading.Tasks.Task.Delay((int)(_displaySeconds * 1000)).ContinueWith(task => {
                    this._impl.Close();
                });
            }
        }

        protected virtual void OnClosed()
        {
            _renderLoop = null;
            if (_renderLoopThread != null)
            {
                _renderLoopThread = null;
            }
        }

        private Func<Window, bool> _renderLoop;

        private void RenderLoop()
        {
            while(_renderLoop != null)
            {
                bool result = _renderLoop(this);
                if (result == true)
                {
                    this.Invalidate(true);
                }
                else
                    break;
            }
        }

        public void ShowDialog(Func<Window, bool> renderLoop)
        {
            ShowDialog(0, renderLoop);
        }

        public void ShowDialog(double displaySeconds = 0, Func<Window, bool> renderLoop = null)
        {
            _renderLoop = renderLoop;
            _displaySeconds = displaySeconds;
            _impl.ShowDialog();
        }

        public override void Invalidate(bool forcePaint = false)
        {
            base.Invalidate(forcePaint);
            _impl.Invalidate(forcePaint);
        }

        internal void OnMouseMove(PointF p)
        {
            if (_captureObj != null && _captureObj.Capture == true && _isMouseDowning == true)
            {
                MouseEventArgs me = MouseEventArgs.Create(p, _captureObj);
                _captureObj.OnMouseMove(me);
                UpdateCursor(_captureObj);
                return;
            }

            UIElement find = HitTest(p.X, p.Y);
            if (find != this && _lastMouseHoverObject != find)
            {
                if (_lastMouseHoverObject != null) _lastMouseHoverObject.OnMouseOut();
                _lastMouseHoverObject = find;
                if (find != null && find != this)
                {
                    find.OnMouseEnter();
                    UpdateCursor(_captureObj);
                }
                return;
            }

            if(find != null)
            {
                MouseEventArgs me = MouseEventArgs.Create(p, find);
                find.OnMouseMove(me);
                UpdateCursor(find);
            }
        }

        private bool _isMouseDowning;
        internal void OnMouseUp(MouseButtons mouseButtons, PointF p)
        {
            _isMouseDowning = false;
            if (_captureObj != null && _captureObj.Capture == true)
            {
                MouseEventArgs me = MouseEventArgs.Create(p, _captureObj, mouseButtons);
                _captureObj.OnMouseUp(me);
                return;
            }

            UIElement find = HitTest(p);
            if (find != null)
            {
                MouseEventArgs me = MouseEventArgs.Create(p, find, mouseButtons);
                find.OnMouseUp(me);
            }
        }

        internal void OnMouseDown(MouseButtons mouseButtons, PointF p)
        {
            _isMouseDowning = true;

            UIElement find = HitTest(p);
            if(find == null)
            {
                if (_captureObj != null && _captureObj.Capture == true) _captureObj.Capture = false;
            }
            else
            {
                _lastMouseDownObject = find;
                MouseEventArgs me = MouseEventArgs.Create(p, find, mouseButtons);
                find.OnMouseDown(me);

                if(find.Capture == true && find != _captureObj)
                {
                    if (_captureObj != null && _captureObj.Capture == true)
                    {
                        // 如果当前capture的对象和命中的对象之间存在装饰关系，则不释放capture对象的Capture
                        if((!find.IsDecoratorOf(_captureObj)) && (!_captureObj.IsDecoratorOf(find)))
                        {
                            _captureObj.Capture = false;
                        }
                    }

                    _captureObj = find;
                }
            }
        }

        internal void OnMouseClick(PointF p)
        {
            UIElement find = HitTest(p);
            if (find != null && find == _lastMouseDownObject)
            {
                MouseEventArgs me = MouseEventArgs.Create(p, find);
                find.OnMouseClick(me);
            }

            if (_lastMouseDownObject != null) _lastMouseDownObject = null;
        }

        internal void OnMouseDoubleClick(MouseButtons mouseButtons, PointF p)
        {
            UIElement find = HitTest(p.X,p.Y);
            if (find != null)
            {
                MouseEventArgs me = MouseEventArgs.Create(p, find, mouseButtons);
                find.OnMouseDoubleClick(me);
                if (find.Capture == true) _captureObj = find;
            }
        }

        internal void OnMouseWheel(PointF p, float delta)
        {
            UIElement find = HitTest(p.X, p.Y);
            if (find != null)
            {
                MouseEventArgs me = MouseEventArgs.Create(p, find);
                me.Delta = delta;
                find.OnMouseWheel(me);
            }
        }

        protected internal override void OnMouseOut()
        {
            if (_lastMouseDownObject != null) _lastMouseDownObject = null;

            if (_lastMouseHoverObject != null)
            {
                _lastMouseHoverObject.OnMouseOut();
                _lastMouseHoverObject = null;
            }
            else
            {
                base.OnMouseOut();
            }
        }

        protected internal void OnKeyDown(Key key, InputModifiers modifiers)
        {
            KeyEventArgs e = new KeyEventArgs { Key = key, Modifiers = modifiers };
            if (_captureObj != null) _captureObj.OnKeyDown(e);
            this.OnKeyDown(e);
        }

        protected internal void OnKeyUp(Key key, InputModifiers modifiers)
        {
            KeyEventArgs e = new KeyEventArgs { Key = key, Modifiers = modifiers };
            if (_captureObj != null) _captureObj.OnKeyUp(e);
            this.OnKeyUp(e);
        }

        protected internal void OnTextInput(String txt)
        {
            TextInputEventArgs e = new TextInputEventArgs { Text = txt };
            if (_captureObj != null) _captureObj.OnTextInput(e);
            this.OnTextInput(e);
        }

        private Cursors _displayCursor = Cursors.Default;
        private void UpdateCursor(UIElement ui = null)
        {
            if (ui == null || _displayCursor == ui.Cursor) return;
            _displayCursor = ui.Cursor;
            _impl.SetCursor(ui.Cursor);
        }
    }
}
