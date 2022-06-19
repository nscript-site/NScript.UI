using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI
{
    public class App
    {
        public static String Name { get; set; } = "App";
        public static Controls.StyleFileLoader DefaultStyle = new Controls.StyleFileLoader().Load("./Resource/global.css");

        public static void Init() { }
    }
}
