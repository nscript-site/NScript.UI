#load "common.csx"

using Geb.UI;
using Geb.UI.Media;
using Geb.UI.Controls;
using Geb.UI.Input;
using Geb.Image;

// App.Name = "测试";
Geb.UI.D2D.D2DPlatform.IsLoggerEnable = true;
Geb.UI.D2D.D2DPlatform.Init();
var image = new Geb.Image.ImageBgra32(300,300);
image.Fill(Geb.Image.Bgra32.GREEN);
image.ShowDialog("image");