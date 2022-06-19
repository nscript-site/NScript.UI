using System;
using System.IO;

namespace SimpleTest
{
    using NScript.UI;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            App.Name = "测试";
            NScript.UI.D2D.D2DPlatform.IsLoggerEnable = true;
            NScript.UI.D2D.D2DPlatform.Init();
            Window window = new Examples.Common.DemoWindow();
            window.ShowDialog();
        }
    }
}
