# NUI 介绍

这里放了一些 UI 方面小工具。主要包含3个小东西：

- NScript.Plot : 基于 Avalonia 和 SkiaSharp 开发的 plot 工具及 GUI 库，主要应用于两个场景：
  - 方便在脚本中使用，弹出可视化窗体或交互窗体；
  - 方便直接使用脚本编写 UI 程序。

- minigui.tsx : 用 typescript 写的基于 canvas 的迷你 gui 库的原型，方便做一些复杂的基于canvas 的 ui 交互功能；

- NScript.UI : 基于 Avalonia 进行裁剪，剔除了很多不必要的东西，保留一个小规模的库，方便写小型工具程序。

## NScript.Plot

对 Avalonia 和 SkiaSharp 进行封装，方便通过脚本进行可视化展示。代码示例：

```csharp
#r "nuget: NScript.Plot, 6.0.1"

using SkiaSharp;
using NScript.Plot;

// 显示一张图像再显示一张蓝色图像
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

```

暂时先支持显示图像和序列图像，我这研究图形、图像和视频的基本够用，后面再慢慢更新。

Plan:

1. - [x] 基础库集成
   1. - [x] 基于 Avalonia 实现;
   2. - [x] 集成 Skia；
   3. - [ ] 集成 OpenCV;
   4. - [ ] 集成第三方 2D 可视化库；
   5. - [ ] 集成 3D 库；
2. - [ ] 基本功能
   1. - [x] 图像显示；
   2. - [x] 序列图像显示；
3. - [ ] 提供轻量级编写 Avalonia 程序的方法。

## minigui

演示了基于 canvas api 构建一个最简单的控件体系的代码，用在一些 web 项目里，编写复杂的图形图像交互逻辑。

## NScript.UI

这个库的目的是想要实现一个对 AOT 友好的 GUI 库，可以编译成小尺寸的程序，是个玩票性质的项目。

Avalonia 大量应用反射，对 AOT 不友好，NScript.UI 对 Avalonia 进行裁剪, 里面包含了勉强可用的编辑控件，这个版本是基于 Direct2D 来实现的，用 ILCompiler 1.0 禁用反射可以 AOT 编译通过正常运行，编译后程序尺寸为 4 M左右 .Net 6 和 .Net 7 下的 ILCompiler 禁用反射AOT编译后无法正常运行，先放着凉一会。
