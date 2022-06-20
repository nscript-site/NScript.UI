using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace NScript.Plot
{
    public partial class App : Application
    {
        private static AppBuilder Builder;

        private static ClassicDesktopStyleApplicationLifetime Lifttime;

        public static void Run(Func<Avalonia.Controls.Window> createWindow)
        {
            if (Builder == null)
            {
                Builder = AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
                Lifttime = new ClassicDesktopStyleApplicationLifetime()
                {
                    ShutdownMode = ShutdownMode.OnMainWindowClose
                };

                Builder.SetupWithLifetime(Lifttime);
            }

            Lifttime.MainWindow = createWindow();
            Lifttime.Start(null);
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}