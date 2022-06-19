using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    using SharpDX;
    using SharpDX.DXGI;
    using SharpDX.Direct3D;
    using SharpDX.Direct3D11;

    public class D3DDrawContext : IDrawContext3D
    {
        private SharpDX.Direct3D11.Device _device;
        private Texture2D _backBuffer;
        private RenderTargetView _backBufferView;
        private float _width;
        private float _height;

        public float Width => _width;
        public float Height => _height;

        public Texture2D Texture
        {
            get { return _backBuffer; }
        }

        /// <summary>
        /// Returns the device
        /// </summary>
        public SharpDX.Direct3D11.Device Device
        {
            get
            {
                return _device;
            }
        }

        /// <summary>
        /// Returns the backbuffer used by the SwapChain
        /// </summary>
        public Texture2D BackBuffer
        {
            get
            {
                return _backBuffer;
            }
        }

        public DataBox Data { get; private set; }

        /// <summary>
        /// Returns the render target view on the backbuffer used by the SwapChain.
        /// </summary>
        public RenderTargetView BackBufferView
        {
            get
            {
                return _backBufferView;
            }
        }

        public D3DDrawContext() { }

        public void Measure(Media.SizeF size)
        {
            if (Width != size.Width && Height != size.Height) Resize(size.Width, size.Height);
        }

        protected void Resize(float width, float height)
        {
            if (width != _width && height != _height && width > 0 && height > 0)
                Initialize(width, height);
        }

        protected void Initialize(float width, float height)
        {
            _width = width;
            _height = height;
            _device = new SharpDX.Direct3D11.Device(DriverType.Hardware, DeviceCreationFlags.Debug
                | DeviceCreationFlags.BgraSupport);

            Texture2DDescription desc = new Texture2DDescription
            {
                Width = (int)_width,
                Height = (int)_height,
                Usage = ResourceUsage.Default,
                Format = Format.R8G8B8A8_UNorm,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget, CpuAccessFlags = CpuAccessFlags.Write, MipLevels = 1,
                ArraySize = 1, OptionFlags = ResourceOptionFlags.None, SampleDescription = new SampleDescription(1, 0)
            };
            _backBuffer = new Texture2D(_device, desc);
            _backBufferView = new RenderTargetView(_device, _backBuffer);
        }

        protected virtual void BeginDraw()
        {
            Device.ImmediateContext.Rasterizer.SetViewport(0,0,_width,_height);
            Device.ImmediateContext.OutputMerger.SetRenderTargets(_backBufferView);
            Device.ImmediateContext.ClearRenderTargetView(_backBufferView, new SharpDX.Mathematics.Interop.RawColor4(1, 0, 0, 1));
        }

        protected virtual void EndDraw()
        {
        }

        public void Draw()
        {
            BeginDraw();
            DrawContent();
            EndDraw();
        }

        public virtual void DrawContent()
        {
            //var surface = _backBuffer.QueryInterface<Surface>();
            DataStream dataStream;
            //surface.Map(SharpDX.DXGI.MapFlags.Read, out dataStream);
            Device.ImmediateContext.MapSubresource(_backBuffer, 0, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out dataStream);
        }
    }
}
