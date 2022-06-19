#load "common.csx"

using Geb.Image;
using NScript.Plot;

// App.Name = "测试";
NScript.UI.D2D.D2DPlatform.IsLoggerEnable = true;
NScript.UI.D2D.D2DPlatform.Init();
var image = new Geb.Image.ImageBgra32(300,300);
image.Fill(Geb.Image.Bgra32.GREEN);
image.ShowDialog("image");