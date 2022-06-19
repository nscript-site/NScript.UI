using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.Input
{
    using NScript.UI.Media;
    using NScript.UI.Controls;

    public class MouseEventArgs : BaseEventArgs
    {
        public float StageX, StageY;
        public MouseButtons Button;
        public UIElement Owner;
        public float X, Y, Delta;

        public PointF Location { get { return new PointF(X, Y); } }
        public PointF StageLocation { get { return new PointF(StageX, StageY); } }

        public static MouseEventArgs Create(PointF p, UIElement sender, MouseButtons mouseButtons = MouseButtons.None)
        {
            MouseEventArgs me = new MouseEventArgs { StageX = p.X, StageY = p.Y, Owner = sender, Button = MouseButtons.None };
            PointF pLoc = sender.GlobalToLocal(p);
            me.X = pLoc.X;
            me.Y = pLoc.Y;
            me.Button = mouseButtons;
            return me;
        }
    }
}
