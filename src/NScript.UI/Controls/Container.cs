using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NScript.UI.Media;

namespace NScript.UI.Controls
{
    using NScript.UI.Input;

    public class Container : UIElement
    {
        public Boolean IsChildrenMouseEnable = true;

        protected List<UIElement> _childrens = new List<UIElement>();
        protected List<UIElement> _decorations = new List<UIElement>();

        public ScrollerBarVisible ScrollerVisibleSetting { get; set; } = ScrollerBarVisible.None;

        public ScrollerBar VerticalBar { get; private set; }
        public ScrollerBar HorizontalBar { get; private set; }

        public delegate void ScrollEventHandler(UIElement sender, ScrollEventArgs e);

        public event ScrollEventHandler Scroll;

        /// <summary>
        /// 内容实际显示区域的范围
        /// </summary>
        public RectF ClientBound { get; protected set; }

        /// <summary>
        /// 内容逻辑显示区域的范围
        /// </summary>
        public RectF ContentBound { get; protected set; }

        public Style Style { get; set; }

        /// <summary>
        /// 默认的 StyleNames
        /// </summary>
        /// <param name="names"></param>
        public virtual void BuildDefaultStyleNames(List<String> names)
        {
            names.Add(".container_default");
        }

        /// <summary>
        /// 用户自定义的 StyleNames，用户自定义的 StyleNames 属性会覆盖默认的 StyleNames
        /// </summary>
        public String StyleNames { get; set; }

        public void LoadStyle()
        {
            Style.StyleNames.Clear();
            BuildDefaultStyleNames(Style.StyleNames);

            if (Style == null) Style = new Style();

            foreach(var name in Style.StyleNames)
            {
                Style.Append(App.DefaultStyle[name]);
            }

            UseStyle();
        }

        protected void UseStyle()
        {
            if (Style == null) return;
            if (Style.BackgroundColor.HasValue) BackColor = Style.BackgroundColor.Value;
        }

        protected void MeasureClient(IDrawContext cxt)
        {
            float dx = 0, dy = 0;
            float barw = VerticalBar == null ? 0.0f : (VerticalBar.Visible == true ? VerticalBar.BarWidth : 0.0f);
            float barh = HorizontalBar == null ? 0.0f : (HorizontalBar.Visible == true ? HorizontalBar.BarWidth : 0.0f);

            if (Style != null)
            {
                if (Style.Border.HasValue && Style.Border.Value.Thickness > 0)
                {
                    dx += Style.Border.Value.Thickness;
                    dy += Style.Border.Value.Thickness;
                }
                dx += Style.Padding.Left.Pixels;
                dy += Style.Padding.Top.Pixels;
            }

            float dw = 0, dh = 0;

            if(Style != null)
            {
                if (Style.Border.HasValue && Style.Border.Value.Thickness > 0)
                {
                    dw += 2 * Style.Border.Value.Thickness;
                    dh += 2 * Style.Border.Value.Thickness;
                }

                dw += Style.Padding.Left.Pixels + Style.Padding.Right.Pixels;
                dh += Style.Padding.Top.Pixels + Style.Padding.Bottom.Pixels;
            }

            float w = Math.Max(0, Size.Width - dw - barw);
            float h = Math.Max(0, Size.Height - dh - barh);
            ClientBound = new RectF(dx, dy, w, h);
        }

        public void Add(UIElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (_childrens == null) _childrens = new List<UIElement>();
            element.Parent = this;
            element.GlobalIndex = UIElement.NextGlobalIndex;
            element.OnCreate();
            _childrens.Add(element);
            _childrens.Sort();
        }

        protected override void DrawContent(IDrawContext cxt)
        {
            base.DrawContent(cxt);
            foreach (UIElement child in _childrens)
                child.Draw(cxt);
        }

        protected override void FinishedDrawContent(IDrawContext cxt)
        {
            base.FinishedDrawContent(cxt);
            foreach (UIElement item in _decorations)
                item.Draw(cxt);

            // 绘制 Border
            if (Style != null && Style.Border != null)
            {
                float thickness = Style.Border.Value.Thickness;
                Color color = Style.Border.Value.Color;

                if(thickness > 0 && color.Alpha > 0)
                {
                    RectF rect = new RectF(thickness * 0.5f, thickness * 0.5f, Size.Width - thickness, Size.Height - thickness);
                    cxt.DrawRect(new Pen(Style.Border.Value.Color, Style.Border.Value.Thickness), rect);
                }
            }
        }

        protected override Nullable<RectF> GetContentClip()
        {
            if (ClipContent == false) return null;
            return ClientBound;
        }

        internal struct ScrollerBarVisibleState
        {
            internal bool IsHorizontalBarVisible;
            internal bool IsVerticalBarVisible;

            public static bool operator == (ScrollerBarVisibleState a, ScrollerBarVisibleState b)
            {
                return a.IsHorizontalBarVisible == b.IsHorizontalBarVisible && a.IsVerticalBarVisible == b.IsVerticalBarVisible;
            }

            public static bool operator !=(ScrollerBarVisibleState a, ScrollerBarVisibleState b)
            {
                return a.IsHorizontalBarVisible != b.IsHorizontalBarVisible || a.IsVerticalBarVisible != b.IsVerticalBarVisible;
            }
        }

        private ScrollerBarVisibleState GetScrollerBarVisibleState()
        {
            return new ScrollerBarVisibleState
            {
                IsHorizontalBarVisible = HorizontalBar == null ? false : HorizontalBar.Visible,
                IsVerticalBarVisible = VerticalBar == null ? false : VerticalBar.Visible
            };
        }

        public override void Measure(IDrawContext cxt)
        {
            // 先计算 Client 尺寸
            MeasureClient(cxt);

            // 再 Measure 子元素
            foreach (UIElement child in _childrens)
                child.Measure(cxt);

            // 再Measure 内容部分
            MeasureContent(cxt);

            ScrollerBarVisibleState oldState = GetScrollerBarVisibleState();

            // 再 Measure Decoration 部分，这时可能需要 Client 重新计算
            MeasureDecorations(cxt);

            ScrollerBarVisibleState newState = GetScrollerBarVisibleState();
            if(oldState != newState)
            {
                // 再来一遍
                MeasureClient(cxt);
                foreach (UIElement child in _childrens)
                    child.Measure(cxt);
                MeasureContent(cxt);
                MeasureDecorations(cxt);
            }

            Layout(cxt);
        }

        public void ScrollTo(ScrollDirection direction, float val)
        {
            val = Math.Max(0, Math.Min(1, val));
            if (direction == ScrollDirection.Vertical)
            {
                if (this.VerticalBar != null) this.VerticalBar.ScrollTo(val);
            }
            else if (direction == ScrollDirection.Horizontal)
            {
                if (this.HorizontalBar != null) this.HorizontalBar.ScrollTo(val);
            }

            if (Scroll != null) Scroll(this, new ScrollEventArgs { Direction = direction, Value = val });
                   
            this.Invalidate();
        }

        protected virtual PointF GetContentBoundOffset()
        {
            float xOffset = 0;
            float yOffset = 0;
            
            if(this.VerticalBar != null && this.VerticalBar.Visible == true)
                yOffset = ScrollerBar.GetContentOffset(ClientBound.Height, ContentBound.Height, this.VerticalBar.Value);
            else if(this.HorizontalBar != null && this.HorizontalBar.Visible == true)
                xOffset = ScrollerBar.GetContentOffset(ClientBound.Width, ContentBound.Width, this.HorizontalBar.Value);
            return new PointF(xOffset,yOffset);
        }

        protected virtual void MeasureContent(IDrawContext cxt)
        {
            ContentBound = ClientBound + GetContentBoundOffset();
        }

        protected virtual void MeasureDecorations(IDrawContext cxt)
        {
            foreach (UIElement item in _decorations)
                item.Measure(cxt);
        }

        protected virtual void Layout(IDrawContext cxt)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UIElement HitTest(PointF p)
        {
            return HitTest(p.X, p.Y);
        }

        public override UIElement HitTest(float x, float y)
        {
            UIElement match = base.HitTest(x, y);
            if (match == null) return null;
            else
            {
                if (_childrens != null && IsChildrenMouseEnable == true)
                {
                    for (int i = _childrens.Count - 1; i >= 0; i--)
                    {
                        UIElement item = _childrens[i];
                        UIElement m = item.HitTest(x - item.Location.X, y - item.Location.Y);
                        if (m != null) return m;
                    }
                }

                if(_decorations!= null)
                {
                    foreach(UIElement item in _decorations)
                    {
                        UIElement m = item.HitTest(x - item.Location.X, y - item.Location.Y);
                        if (m != null) return m;
                    }
                }
            }
            return match;
        }

        public void Clear()
        {
            if (_childrens != null) _childrens.Clear();
        }

        protected internal override void OnCreate()
        {
            if(this.ScrollerVisibleSetting != ScrollerBarVisible.None)
            {
                VerticalBar = new ScrollerBar();
                VerticalBar.Direction = ScrollDirection.Vertical;
                HorizontalBar = new ScrollerBar();
                HorizontalBar.Direction = ScrollDirection.Horizontal;
                VerticalBar.Parent = VerticalBar.Owner = this;
                HorizontalBar.Parent = HorizontalBar.Owner = this;
                this._decorations.Add(VerticalBar);
                this._decorations.Add(HorizontalBar);
                VerticalBar.OnCreate();
                HorizontalBar.OnCreate();
            }

            LoadStyle();

            if (_childrens == null) return;
            foreach (UIElement item in _childrens)
            {
                item.OnCreate();
            }
        }

        public void Remove(UIElement element)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (_childrens != null && this._childrens.Contains(element) == true)
            {
                element.Parent = null;
                _childrens.Remove(element);
            }
        }

        public void RemoveAt(int idx)
        {
            if (_childrens != null)
            {
                _childrens.RemoveAt(idx);
            }
        }
    }
}
