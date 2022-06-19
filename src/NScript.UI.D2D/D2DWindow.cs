using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace NScript.UI.D2D
{
    using Win32;
    using SharpDX.Direct2D1;
    using SharpDX.DXGI;
    using SharpDX;
    using Factory2D = SharpDX.Direct2D1.Factory;
    using AlphaMode = SharpDX.Direct2D1.AlphaMode;
    using NScript.UI.Media;
    using NScript.UI.Input;

    public class D2DWindow : WindowImpl, IDisposable
    {
        class D2DNativeWindow:NativeWindow
        {
            private D2DWindow _window;
            public D2DNativeWindow(D2DWindow window)
            {
                this._window = window;
            }

            protected override void WndProc(ref Message m)
            {
                _window.WndProc(ref m);
            }
        }

        private D2DDrawContext _drawContext;
        private Thread _renderThread;
        private D2DNativeWindow _nativeWindow;

        public IntPtr Handle => _nativeWindow.Handle;

        protected virtual CreateParams CreateParams()
        {
            WindowStylesEx exStyle = WindowStylesEx.WS_EX_APPWINDOW | WindowStylesEx.WS_EX_CONTROLPARENT;
            WindowStyles style = WindowStyles.WS_BORDER | WindowStyles.WS_CAPTION | WindowStyles.WS_SIZEBOX
                | WindowStyles.WS_SYSMENU | WindowStyles.WS_POPUP | WindowStyles.WS_CLIPSIBLINGS
                | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_MAXIMIZEBOX | WindowStyles.WS_MINIMIZEBOX;


            Size stageSize = GetStageSize();
            Size screenSize = Win32Api.GetPrimaryScreenSize();
            Point pos = this.GetStartPosition(screenSize, stageSize);

            CreateParams cp = new CreateParams();
            cp.Parent = IntPtr.Zero;
            cp.ClassStyle = ClassStyles.HorizontalRedraw | ClassStyles.VerticalRedraw | ClassStyles.DoubleClicks;
            cp.Style = (int)style;
            cp.ExStyle = (int)exStyle;
            cp.X = pos.X;
            cp.Y = pos.Y;
            cp.Width = stageSize.Width;
            cp.Height = stageSize.Height;
            return cp;
        }

        public D2DWindow():base()
        {
            _nativeWindow = new D2DNativeWindow(this);
        }

        public override void ShowDialog()
        {
            Show();
            Application.RunDialog(this);
        }

        public override void Show()
        {
            _nativeWindow.CreateHandle(this.CreateParams());
            UpdateD2DDrawContext();
            _nativeWindow.Show();
        }

        private Factory2D _factory2D;
        private RenderTarget _renderTarget2D;

        /// <summary>
        /// 计算每 dp 对应的像素数量
        /// </summary>
        /// <returns></returns>
        public override float GetPixelsPerDip()
        {
            return Win32.Win32Api.GetDpiForSystem() / 96.0f;
        }

        protected void UpdateD2DDrawContext()
        {
            UpdateD2DDrawContextWithWindowRenderTarget();
        }

        protected void UpdateD2DDrawContextWithWindowRenderTarget()
        {
            if (Handle == IntPtr.Zero) return;

            Size stageSize = GetStageSize();

            if (stageSize.Width < 5 && stageSize.Height < 5) return;    // 窗体太小也不创建了
            if (_drawContext != null && _drawContext.StageSize == stageSize) return;  // 如果 StageSize 一样就不需要创建了

            RenderTarget oldTarget = _renderTarget2D;

            if (_factory2D == null) _factory2D = new Factory2D();

            HwndRenderTargetProperties hwndRenderTargetProperties = new HwndRenderTargetProperties();
            hwndRenderTargetProperties.Hwnd = Handle;
            hwndRenderTargetProperties.PixelSize = new Size2(stageSize.Width, stageSize.Height);

            hwndRenderTargetProperties.PresentOptions = PresentOptions.None;
            SharpDX.Direct2D1.PixelFormat pixelFormat = new SharpDX.Direct2D1.PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied);
            RenderTargetProperties renderTargetProperties = new RenderTargetProperties(RenderTargetType.Hardware,
                pixelFormat, 0, 0, RenderTargetUsage.None, FeatureLevel.Level_DEFAULT);

            _renderTarget2D = new WindowRenderTarget(_factory2D, renderTargetProperties, hwndRenderTargetProperties);
            _renderTarget2D.AntialiasMode = AntialiasMode.Aliased;
            _renderTarget2D.TextAntialiasMode = TextAntialiasMode.Cleartype;
            _drawContext = new D2DDrawContext(_renderTarget2D, stageSize);

            // 释放旧资源
            if (oldTarget != null) oldTarget.Dispose();
        }

        private bool _needRender = true;
        private bool _disposed = false;

        protected virtual void WndProc(ref Message m)
        {
            m.Result = WindowProc(m.HWnd, (uint)m.Msg, m.WParam, m.LParam);
        }

        protected virtual IntPtr WindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            bool skip = BeforeHandle(hWnd, msg, wParam, lParam);
            if (skip == true) return IntPtr.Zero;

            bool transferFocusEvent = false;
            bool nonClient = false;

            switch ((WM)msg)
            {
                case WM.LBUTTONDOWN:
                case WM.LBUTTONUP:
                case WM.LBUTTONDBLCLK:
                case WM.RBUTTONDOWN:
                case WM.RBUTTONUP:
                case WM.RBUTTONDBLCLK:
                case WM.MBUTTONDOWN:
                case WM.MBUTTONUP:
                case WM.MBUTTONDBLCLK:
                    transferFocusEvent = true;
                    break;
            }

            switch ((WM)msg)
            {
                case WM.PAINT:
                    OnPaint(true);
                    break;
                case WM.SIZE:
                    {
                        Size size = Win32Api.ParseParamToSize(lParam);
                        Owner.Size = new SizeF(size.Width, size.Height, 1.0f / GetPixelsPerDip());
                    }
                    OnPaint(true);
                    break;
                case WM.CLOSE:
                    return Win32Api.DefWindowProc(hWnd, (WM)msg, wParam, lParam);
                case WM.DESTROY:
                    if(OnClosed != null) OnClosed();
                    IsDestory = true;
                    Win32Api.DefWindowProc(hWnd, (WM)msg, wParam, lParam);
                    return IntPtr.Zero;
                default:
                    if (HandleKeyMessage((WM)msg, wParam, lParam) == true) return IntPtr.Zero;
                    if (HandleMouseMessage((WM)msg, wParam, lParam) == true) return IntPtr.Zero;
                    //return IntPtr.Zero;
                    return Win32Api.DefWindowProc(hWnd, (WM)msg, wParam, lParam);
            }

            return Win32Api.DefWindowProc(hWnd, (WM)msg, wParam, lParam);
        }

        protected PointF GetMousePosition(IntPtr lParam)
        {
            PointF p = Win32Api.ParseParamToPointF(lParam);
            return p * (1.0f / GetPixelsPerDip());
        }

        protected PointF GetMousePositionFromScreen(IntPtr lParam)
        {
            PointF p = Win32Api.ParseParamToPointF(lParam);
            p = Win32Api.ScreenToClient(this.Handle, p);
            return p * (1.0f / GetPixelsPerDip());
        }

        protected bool HandleMouseMessage(WM msg, IntPtr wParam, IntPtr lParam)
        {
            bool result = true;
            switch (msg)
            {
                case WM.MOUSEMOVE:
                    OnMouseMove(GetMousePosition(lParam));
                    break;
                case WM.MOUSELEAVE:
                case WM.NCMOUSELEAVE:
                    OnMouseOut();
                    break;
                case WM.LBUTTONDOWN:
                    OnMouseDown(Input.MouseButtons.Left, GetMousePosition(lParam));
                    break;
                case WM.LBUTTONUP:
                    OnMouseUp(Input.MouseButtons.Left, GetMousePosition(lParam));
                    break;
                case WM.LBUTTONDBLCLK:
                    OnMouseDoubleClick(Input.MouseButtons.Left, GetMousePosition(lParam));
                    break;
                case WM.RBUTTONDOWN:
                    OnMouseDown(Input.MouseButtons.Right, GetMousePosition(lParam));
                    break;
                case WM.RBUTTONUP:
                    OnMouseUp(Input.MouseButtons.Right, GetMousePosition(lParam));
                    break;
                case WM.RBUTTONDBLCLK:
                    OnMouseDoubleClick(Input.MouseButtons.Right, GetMousePosition(lParam));
                    break;
                case WM.MBUTTONDOWN:
                    OnMouseDown(Input.MouseButtons.Middle, GetMousePosition(lParam));
                    break;
                case WM.MBUTTONUP:
                    OnMouseUp(Input.MouseButtons.Middle, GetMousePosition(lParam));
                    break;
                case WM.MBUTTONDBLCLK:
                    OnMouseDoubleClick(Input.MouseButtons.Middle, GetMousePosition(lParam));
                    break;
                case WM.MOUSEWHEEL:
                    {
                        var delta = Win32Api.GetWheelDelta(wParam);
                        // lParam为鼠标的屏幕位置
                        OnMouseWheel(GetMousePositionFromScreen(lParam), delta);
                    }
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        protected bool HandleKeyMessage(WM msg, IntPtr wParam, IntPtr lParam)
        {
            switch(msg)
            {
                case WM.SYSKEYDOWN:
                case WM.KEYDOWN:
                    this.OnKeyDown(KeyInterop.KeyFromVirtualKey(Win32Api.ToInt32(wParam)), WindowsKeyboardDevice.Instance.Modifiers);
                    return true;
                    break;
                case WM.KEYUP:
                case WM.SYSKEYUP:
                    this.OnKeyUp(KeyInterop.KeyFromVirtualKey(Win32Api.ToInt32(wParam)), WindowsKeyboardDevice.Instance.Modifiers);
                    return true;
                    break;
                case WM.CHAR:
                    // Ignore control chars
                    if (Win32Api.ToInt32(wParam) >= 32)
                    {
                        String txt = new string((Char)Win32Api.ToInt32(wParam), 1);
                        OnTextInput(txt);
                    }
                    return true;
                    break;
                default:
                    return false;
            }
        }

        protected virtual bool BeforeHandle(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return false;
        }

        public override void Invalidate(bool forcePaint = false)
        {
            base.Invalidate(forcePaint);
            if(forcePaint == true && IsDrawing == false)
            {
                if (_renderThread == null || _renderThread == Thread.CurrentThread){
                    OnPaint(forcePaint);
                }
                else
                {
                    _needRender = true;
                    Win32Api.InvalidateRect(this.Handle, IntPtr.Zero, false);
                }
            }
        }

        private Object _paintSyncObject = new object();
        protected void OnPaint(bool forcePaint = false)
        {
            if (_disposed == true || Handle == IntPtr.Zero) return;
            if (_needRender == false && forcePaint == false) return;
            if (_renderTarget2D == null) UpdateD2DDrawContext();
            if (_renderTarget2D == null) return;
            if (_created == false)
            {
                _created = true;
                if (OnCreated != null) OnCreated();
            }

            lock(_paintSyncObject)
            {
                IsDrawing = true;

                UpdateD2DDrawContext();

                _renderTarget2D.BeginDraw();

                Owner.Draw(GetDrawContext());

                _renderTarget2D.EndDraw();

                _needRender = false;

                IsDrawing = false;

                if (_renderThread == null) _renderThread = System.Threading.Thread.CurrentThread;
            }
        }

        protected override IDrawContext GetDrawContext()
        {
            return _drawContext;
        }

        protected override void SetCursor(Cursors cursor)
        {
            Win32Api.SetCursor(Handle, cursor);
        }

        public override void Close()
        {
            if (IsDestory == true) return;

            NativeMethods.SendCallbackMessage(new HandleRef(null,Handle), (int)WM.CLOSE, IntPtr.Zero, IntPtr.Zero);
            IsDestory = true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                _nativeWindow.ReleaseHandle();
                disposedValue = true;
            }
        }

        ~D2DWindow()
        {
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
