using System;
using System.Collections.Generic;
using System.Text;

namespace Examples.Common
{
    using NScript.UI;
    using NScript.UI.Controls;
    using NScript.UI.Media;
    using NScript.UI.Input;

    public class DemoWindow : Window
    {
        public DemoWindow():base(600,400,App.Name)
        {
            BackColor = new Color(0xFFEEEEEE);
            CreateChilds();
        }

        private void CreateChilds()
        {
            UIElement ui = new UIElement
            {
                Size = new SizeF(50, 20),
                Location = new PointF(20, 20),
                Cursor = Cursors.Hand,
                BackColor = Color.GREEN
            };
            ui.MouseClick += Ui_MouseClick;
            ui.MouseDown += Ui_MouseDown;

            TextBlock tb = new TextBlock
            {
                FontSize = 20,
                Location = new PointF(20, 50),
                FontFamily = "Microsoft YaHei",
                Size = new SizeF(100, 100),
                Text = "Hello World!",
                Cursor = Cursors.Hand
            };

            TextBox textBox = new TextBox
            {
                FontSize = 20,
                BackColor = Color.WHITE,
                Location = new PointF(200, 40),
                FontFamily = "Microsoft YaHei",
                Size = new SizeF(300, 200),
                ScrollerVisibleSetting = ScrollerBarVisible.Vertical,
                Text = "Hello TextBox! 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... 这是一段很长很长的文字... "
            };

            tb.MouseDown += Tb_MouseDown;
            tb.MouseUp += Tb_MouseUp;
            tb.MouseMove += Tb_MouseMove;
            tb.MouseUp += Tb_MouseUp1;

            this.Add(ui);
            this.Add(tb);
            this.Add(textBox);
        }

        private void StartCore()
        {
            Window window = new Examples.Common.DemoWindow();
            window.ShowDialog();
        }

        private System.Timers.Timer _timer;

        private void Ui_MouseDown(UIElement sender, MouseEventArgs e)
        {
            //System.Threading.Thread thread = new System.Threading.Thread(StartCore);
            //thread.SetApartmentState(System.Threading.ApartmentState.STA);
            //thread.Start();

            //for (int i = 0; i < 5; i++)
            {
                OpenFolderDialog openFolderDialog = new OpenFolderDialog();
                openFolderDialog.Show(this);
                //System.Threading.Thread.Sleep(5000);
            }
        }

        private void Ui_MouseClick(UIElement sender, MouseEventArgs e)
        {
        }

        private void Tb_MouseUp1(UIElement sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private PointF _lastPos;
        private bool _dragging;
        private void Tb_MouseMove(UIElement sender, MouseEventArgs e)
        {
            if (_dragging == false) return;

            float dx = e.StageX - _lastPos.X;
            float dy = e.StageY - _lastPos.Y;
            _lastPos = new PointF(e.StageX, e.StageY);
            sender.Location += new PointF(dx, dy);
            sender.Invalidate();
        }

        private void Tb_MouseUp(UIElement sender, MouseEventArgs e)
        {
            sender.BackColor = new Color(0x00000000);
            sender.Invalidate();
        }

        private void Tb_MouseDown(UIElement sender, MouseEventArgs e)
        {
            _lastPos = new PointF(e.StageX, e.StageY);
            _dragging = true;
            sender.BackColor = new Color(0xFF880000);
            sender.Invalidate();
        }
    }
}
