using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.Markup.Xaml;
using System;
using System.Threading.Tasks;

namespace NScript.Windows
{
    using SkiaSharp;
    using Avalonia.Skia;

    public partial class ImageWindow : Window
    {
        public SKBitmap Bitmap { get; set; }

        public int GeneratorStepByMiniSeconds { get; set; }

        public Func<SKBitmap> BitmapGenerator { get; set; }

        public bool Exited { get; set; }

        public ImageWindow()
        {
            InitializeComponent();
            this.Opened += ImageWindow_Opened;
            this.Closing += ImageWindow_Closing;
            this.img = this.FindControl<NScript.Controls.SkiaImage>("img");
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void ImageWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Exited = true;
        }

        private void ImageWindow_Opened(object? sender, System.EventArgs e)
        {
            if (this.img != null)
                this.img.Source = Bitmap;

            if(BitmapGenerator != null)
            {
                Task.Run(RunUpdateLoop);
            }
        }

        private void RunUpdateLoop()
        {
            while(Exited == false)
            {
                if (GeneratorStepByMiniSeconds > 0)
                    System.Threading.Thread.Sleep(GeneratorStepByMiniSeconds);

                SKBitmap oldBmp = Bitmap;
                SKBitmap newBmp = BitmapGenerator();
                try
                {
                    Dispatcher.UIThread.Post(() => {
                        if (newBmp == oldBmp && newBmp != null)
                        {
                            this.img.Render();
                        }
                        else
                        {
                            this.img.Source = newBmp;
                        }
                        if (oldBmp != null && oldBmp != newBmp) oldBmp.Dispose();
                    });
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
