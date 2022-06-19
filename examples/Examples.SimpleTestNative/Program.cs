using System;

namespace NativeSampleTest
{
    using NScript.UI;
    using NScript.UI.Controls;

    class Program
    {
        static void Main(string[] args)
        {
            App.Name = "测试";
            NScript.UI.D2D.D2DPlatform.IsLoggerEnable = true;
            NScript.UI.D2D.D2DPlatform.Init();

            // for(int i = 0; i < 5; i++)
            // {
            //     OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            //     openFolderDialog.Show(null);
            // }

            Window window = new Examples.Common.DemoWindow();
            window.ShowDialog();
        }
    }
}
