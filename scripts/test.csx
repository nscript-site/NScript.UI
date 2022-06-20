// #r "nuget: Geb.Image, 6.0.5"
// #r "nuget: Avalonia, 0.10.14"
// #r "nuget: Avalonia.Desktop, 0.10.14"
// #r "nuget: Avalonia.Diagnostics, 0.10.14"
// #r "nuget: XamlNameReferenceGenerator, 1.3.4"
// #r "nuget: SkiaSharp, 2.88.0"

// #r "../src/NScript.Plot/bin/Debug/net6.0/NScript.Plot.dll"

// using Geb.Image;
// using SkiaSharp;
// using NScript.Plot;

// //@main
// SKBitmap bmp = new SKBitmap(400, 600);
// bmp.UseCanvas(c => c.Clear(SKColors.Red)).Show();
// bmp.UseCanvas(c => c.Clear(SKColors.Blue)).Show();

// SKColor[] colors = new SKColor[]{ SKColors.Red, SKColors.Blue, SKColors.Green };
// int idx = 0;

// Func<SKBitmap> g = ()=>{
//     idx++;
//     SKBitmap bmp2 = new SKBitmap(400, 600);
//     Byte val = (Byte)(idx % 255);
//     Console.WriteLine(val);
//     bmp2.UseCanvas(c => c.Clear(new SKColor(val,val,val,0xFF)));
//     return bmp2;
// };

// g.Show("g");
