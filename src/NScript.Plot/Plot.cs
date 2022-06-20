using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NScript.Plot
{
    public class Window
    {
        public Window Show()
        {
            Plot.Show(this);
            return this;
        }

        internal Avalonia.Controls.Window Create()
        {
            var w = new Avalonia.Controls.Window();
            return w;
        }
    }

    public static class Plot
    {
        public static void Show(Window window)
        {
            App.Run(window.Create);
        }

        public static void Main(string[] args) { }
    }
}
