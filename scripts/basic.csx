// #load "common.csx"

// using NScript.UI;
// using NScript.UI.Media;
// using NScript.UI.Controls;
// using NScript.UI.Input;

// App.Name = "测试";
// NScript.UI.D2D.D2DPlatform.IsLoggerEnable = true;
// NScript.UI.D2D.D2DPlatform.Init();
// Window window = new Window(600,400,App.Name);

// UIElement ui = new UIElement
// {
//     Size = new SizeF(50, 20),
//     Location = new PointF(20, 20),
//     BackColor = Color.GREEN
// };

// var image = new Geb.Image.ImageBgra32(300,300);
// image.Fill(Geb.Image.Bgra32.GREEN);

// ImageView img = new ImageView
// {
//     Size = new SizeF(300,200),
//     Location = new PointF(100,100),
//     Image = image
// };

// window.Add(ui);
// window.Add(img);

// int val = 0;
// bool RenderLoop(Window wd)
// {
//     val = (val + 30)%255;
//     image.Fill(new Geb.Image.Bgra32(val,0,255));
//     // System.Threading.Thread.Sleep(1000);
//     return true;
// }

// window.ShowDialog(0, RenderLoop);