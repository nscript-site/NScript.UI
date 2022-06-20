#r "nuget: NScript.Plot, 6.0.1"

using SkiaSharp;
using NScript.Plot;

//@main
SKBitmap bmp = new SKBitmap(400, 600);
bmp.UseCanvas(c => c.Clear(SKColors.Red)).Show();
bmp.UseCanvas(c => c.Clear(SKColors.Blue)).Show();

// 测试显示序列图像
int idx = 0;
Func<SKBitmap> g = ()=>{
    idx+=5;
    SKBitmap bmp2 = new SKBitmap(400, 600);
    Byte val = (Byte)(idx % 255);
    Console.WriteLine(val);
    bmp2.UseCanvas(c => c.Clear(new SKColor(val,val,val,0xFF)));
    return bmp2;
};

g.Show("序列图像");
