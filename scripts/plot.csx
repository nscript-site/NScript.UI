#r "nuget: NScript.Plot, 6.0.1"

using SkiaSharp;
using NScript.Plot;

// 显示单张图像
var bmp = new SKBitmap(400, 600);
bmp.UseCanvas(c => c.Clear(SKColors.Red)).Show("红色图像")
    .UseCanvas(c => c.Clear(SKColors.Blue)).Show("蓝色图像");

// 显示序列图像
int idx = 0;
Func<SKBitmap> g = () =>
{
    idx += 5;
    Byte val = (Byte)(idx % 255);
    return new SKBitmap(400, 600).UseCanvas(c => c.Clear(new SKColor(val, val, val, 0xFF)));
};
g.Show("序列图像");
