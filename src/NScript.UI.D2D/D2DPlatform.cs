using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    using NScript.UI.Input;

    using FactoryWrite = SharpDX.DirectWrite.Factory;

    public class D2DPlatform : Platform
    {
        public static SharpDX.DirectWrite.Factory1 DirectWriteFactory { get; private set; }

        private readonly Lazy<IClipboard> _clipboard = new Lazy<IClipboard>(() => new D2DClipboard());

        private readonly Lazy<ISystemDialog> _systemDialog = new Lazy<ISystemDialog>(() => new D2DSystemDialog());

        internal static void InitializeDirect2D()
        {
            if (Platform.IsLoggerEnable == true)
                Platform.Log("Start InitializeDirect2D");

            DirectWriteFactory = new SharpDX.DirectWrite.Factory1();

            if (Platform.IsLoggerEnable == true)
                Platform.Log("InitializeDirect2D OK");
        }

        public static void Init()
        {
            if (Platform.IsLoggerEnable == true)
                Platform.Log("Start SetProcessDPIAware");

            Win32.Win32Api.CoInitialize(IntPtr.Zero);

            Win32.Win32Api.SetProcessDPIAware();

            if (Platform.IsLoggerEnable == true)
                Platform.Log("SetProcessDPIAware OK");

            Platform.Regist(new D2DPlatform());
            InitializeDirect2D();
        }

        public override WindowImpl CreateWindow()
        {
            return new D2DWindow();
        }

        public override IClipboard GetClipboard()
        {
            return _clipboard.Value;
        }

        public override ISystemDialog GetSystemDialog()
        {
            return _systemDialog.Value;
        }

        public override IDrawContext3D CreateDrawContext3D()
        {
            D3DDrawContext context = new D3DDrawContext();
            return context;
        }
    }
}
