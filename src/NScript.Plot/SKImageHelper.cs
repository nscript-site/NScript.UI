using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NScript.Plot
{
    using NScript.Windows;
    using SkiaSharp;

    public static class SKImageHelper
    {
        public static SKBitmap UseCanvas(this SKBitmap bmp, Action<SKCanvas> onCanvas)
        {
            using (SKCanvas cvs = new SKCanvas(bmp))
            {
                onCanvas?.Invoke(cvs);
            }
            return bmp;
        }

        public static SKBitmap Show(this SKBitmap bmp, String title = "")
        {
            App.Run(() =>
            {
                if (String.IsNullOrEmpty(title)) title = "ImageWindow";
                ImageWindow window = new ImageWindow();
                window.Title = title;
                window.Bitmap = bmp;
                return window;
            });
            return bmp;
        }

        public static void Show(this Func<SKBitmap> gen, String title = "", int stepByMiniSeconds = 100)
        {
            App.Run(() =>
            {
                if (String.IsNullOrEmpty(title)) title = "ImageWindow";
                ImageWindow window = new ImageWindow();
                window.Title = title;
                window.BitmapGenerator = gen;
                window.GeneratorStepByMiniSeconds = stepByMiniSeconds;
                return window;
            });
        }
    }
}
