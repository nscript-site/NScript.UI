using System;
using System.Collections.Generic;
using System.Text;
using NScript.UI.Media;

namespace NScript.UI.Controls
{
    using NScript.UI.Input;
    using NScript.UI.Media;

    public class ScrollerBar : UIElement
    {
        public static float GetContentOffset(float clientSize, float contentSize, float scrollVal)
        {
            return -scrollVal * (contentSize - clientSize);
        }

        public static Range GetValueRangeIfContentDisplay(float clientSize, float contentSize, float contentPosition)
        {
            return new Range(
                Math.Max(0, (contentPosition - clientSize) / (contentSize - clientSize)),
                Math.Min(1, contentPosition / (contentSize - clientSize))
                );
        }

        protected internal Container Owner { get; set; }

        public ScrollDirection Direction { get; set; }

        public float BarWidth { get; set; } = 16.0f;
        public float ThumbPading { get; set; } = 0.0f;
        public Color ThumbMouseOverColor { get; set; }
        public Color ThumbMouseDownColor { get; set; }

        public float ThumbMinPixels { get; set; } = 10;

        private float thumbPos;

        /// <summary>
        /// Thumb 的长度
        /// </summary>
        private float thumbLength;
        private RectF thumbBound;
        private float deltaWeight;

        protected bool _isThumbVisible;
        protected float _thumbLengthWeight;

        public ScrollerBar():base()
        {
            IsDecorator = true;
        }

        protected internal override void OnCreate()
        {
            base.OnCreate();
            BackColor = new Color(0xFFF1F1F1);
            ForeColor = new Color(0xFFC1C1C1);
        }

        /// <summary>
        /// Value 再 0-1 之间
        /// </summary>
        public float Value { get; protected set; }

        public void ScrollTo(float val)
        {
            val = Math.Max(0, val);
            val = Math.Min(1, val);
            if (_isThumbVisible == false) val = 0;

            if (Value != val)
            {
                Value = val;
                if (Owner != null) Owner.ScrollTo(Direction, val);
            }
        }

        public void ScrollDelta(float delta)
        {
            if (delta > 0) this.ScrollTo(this.Value + deltaWeight * 0.5f);
            else if (delta < 0) this.ScrollTo(this.Value - deltaWeight * 0.5f);
        }

        protected float BarLength
        {
            get => Direction == ScrollDirection.Vertical ? Size.Height : Size.Width;
        }

        protected override void DrawContent(IDrawContext cxt)
        {
            base.DrawContent(cxt);

            if (Owner == null || Visible == false || _isThumbVisible == false) return;

            float barLength = BarLength;
            thumbLength = _thumbLengthWeight * barLength + ThumbMinPixels;

            float absLength = barLength - thumbLength;
            float thumbWidth = BarWidth - ThumbPading * 2;

            thumbPos = absLength * Value;

            if (absLength < 2) return;
            if(Direction == ScrollDirection.Vertical)
            {
                thumbBound = new RectF(ThumbPading, thumbPos, thumbWidth, thumbLength);
                cxt.Fill(ForeColor, thumbBound);
            }
            else
            {
                thumbBound = new RectF(thumbPos, ThumbPading, thumbLength, thumbWidth);
                cxt.Fill(ForeColor, thumbBound);
            }
        }

        protected int GetDelta(PointF pointer)
        {
            if (Direction == ScrollDirection.Vertical)
            {
                if (pointer.Y >= thumbBound.Bottom) return 1;
                else if (pointer.Y <= thumbBound.Top) return -1;
                else return 0;
            }
            else
            {
                if (pointer.X >= thumbBound.Right) return 1;
                else if (pointer.X <= thumbBound.Left) return -1;
                else return 0;
            }
        }

        private bool _dragging;
        private PointF _lastMousePoint;

        protected internal override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                int delta = GetDelta(e.Location);
                if(delta != 0)
                {
                    float newVal = Math.Min(1, Math.Max(0, Value + delta * deltaWeight));
                    ScrollTo(newVal);
                }
                else
                {
                    _dragging = true;
                    _lastMousePoint = e.StageLocation;
                }
            }
        }

        protected internal override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if(_dragging == true)
            {
                PointF pos = e.StageLocation;
                float draggingDelta = Direction == ScrollDirection.Vertical ? pos.Y - _lastMousePoint.Y : pos.X - _lastMousePoint.X;
                float draggingMaxSize = BarLength - thumbLength;

                _lastMousePoint = pos;
                if (draggingDelta != 0 && draggingMaxSize > 0)
                {
                    float val = Value + draggingDelta / draggingMaxSize;
                    ScrollTo(val);
                }
            }
        }

        protected internal override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _dragging = false;
        }

        public override void Measure(IDrawContext cxt)
        {
            if (Owner == null) return;

            UpdateVisible();

            if(this.Visible == true)
            {
                float borderThickness = Owner.GetBorderThickness();

                if (Direction == ScrollDirection.Vertical)
                    this.Size = new SizeF(BarWidth, Owner.Size.Height - borderThickness * 2);
                else
                    this.Size = new SizeF(Owner.Size.Width - borderThickness * 2, BarWidth);

                if (Direction == ScrollDirection.Vertical)
                    this.Location = new PointF(Owner.Size.Width - this.Size.Width - borderThickness, borderThickness);
                else
                    this.Location = new PointF(borderThickness, Owner.Size.Height - this.Size.Height - borderThickness);

                UpdateParams(Owner.ClientBound, Owner.ContentBound);
            }
        }

        protected void UpdateParams(RectF rectClient, RectF rectContent)
        {
            if(Direction == ScrollDirection.Vertical)
                UpdateParams(rectClient.Height, rectContent.Height, rectContent.Y - rectClient.Y);
            else
                UpdateParams(rectClient.Width, rectContent.Width, rectContent.X - rectClient.X);
        }

        protected void UpdateParams(float clientSize, float contentSize, float contentOffset)
        {
            if (clientSize <= 10 || contentSize <= clientSize)
            {
                _isThumbVisible = false;
                Value = 0;
                return;
            }

            contentOffset = Math.Min(0, contentOffset);
            contentOffset = Math.Max(clientSize - contentSize, contentOffset);

            _isThumbVisible = true;
            _thumbLengthWeight = clientSize / contentSize;
            Value = -contentOffset / (contentSize - clientSize);
            deltaWeight = 0.8f * clientSize / contentSize;
        }

        protected internal void UpdateVisible()
        {
            if (Owner == null) this.Visible = false;
            else if (IsVisible(Owner.ScrollerVisibleSetting) == true) this.Visible = true;
            else if (this.Direction == ScrollDirection.Vertical
                && (Owner.ScrollerVisibleSetting == ScrollerBarVisible.VerticalAuto
                || Owner.ScrollerVisibleSetting == ScrollerBarVisible.BothAuto))
            {
                this.Visible = Owner.ContentBound.Height > Owner.ClientBound.Height;
            }
            else if (this.Direction == ScrollDirection.Horizontal
                && (Owner.ScrollerVisibleSetting == ScrollerBarVisible.HorizontalAuto
                || Owner.ScrollerVisibleSetting == ScrollerBarVisible.BothAuto))
            {
                this.Visible = Owner.ContentBound.Width > Owner.ClientBound.Width;
            }
            else
                this.Visible = false;
        }

        protected bool IsVisible(ScrollerBarVisible setting)
        {
            switch(setting)
            {
                case ScrollerBarVisible.Both:
                    return true;
                case ScrollerBarVisible.Horizontal:
                    return this.Direction == ScrollDirection.Horizontal;
                case ScrollerBarVisible.Vertical:
                    return this.Direction == ScrollDirection.Vertical;
                default:
                    return false;
            }
        }

        protected internal override void OnMouseWheel(MouseEventArgs e)
        {
            ScrollDelta(-e.Delta);
        }
    }
}
