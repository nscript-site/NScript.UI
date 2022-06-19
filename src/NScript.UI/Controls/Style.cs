using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NScript.UI.Controls
{
    using ExCSS;

    public enum SizeMode
    {
        None,
        Pixel,
        Weight,
        Percent
    }

    public struct SizeSetting
    {
        public SizeMode Mode;
        public float Value;
        public float Pixels
        {
            get {
                if (Mode == SizeMode.Pixel) return Value;
                else return 0.0f;
            }
        }
    }

    /// <summary>
    /// 类似 CSS 的 Style 系统
    /// </summary>
    public class Style
    {
        public static List<String> StyleNames = new List<string>();

        public Nullable<Border> Border { get; set; }

        public Padding Padding;

        public Nullable<Margin> Margin { get; set; }
        public Nullable<SizeSetting> Width { get; set; }
        public Nullable<SizeSetting> Height { get; set; }
        public Nullable<Media.Color> BackgroundColor { get; set; }

        public Style Append(Style newStyle)
        {
            if (newStyle == null) return this;

            if (newStyle.BackgroundColor.HasValue) this.BackgroundColor = newStyle.BackgroundColor;
            if (newStyle.Padding.Left.Mode != SizeMode.None) this.Padding.Left = newStyle.Padding.Left;
            if (newStyle.Padding.Right.Mode != SizeMode.None) this.Padding.Right = newStyle.Padding.Right;
            if (newStyle.Padding.Bottom.Mode != SizeMode.None) this.Padding.Bottom = newStyle.Padding.Bottom;
            if (newStyle.Padding.Top.Mode != SizeMode.None) this.Padding.Top = newStyle.Padding.Top;
            if (newStyle.Border.HasValue) this.Border = newStyle.Border;

            return this;
        }
    }

    public class StyleFileLoader
    {
        private StylesheetParser _parser = new StylesheetParser();
        private Stylesheet stylesheet;

        public StyleFileLoader Load(String filePath)
        {
            if(File.Exists(filePath))
            {
                String txt = File.ReadAllText(filePath);
                stylesheet = _parser.Parse(txt);
            }
            return this;
        }

        public Style this[String selector]
        {
            get
            {
                if (stylesheet == null) return null;
                foreach(var item in stylesheet.Children)
                {
                    IStyleRule styleRule = item as IStyleRule;
                    if(styleRule != null && styleRule.SelectorText == selector)
                    {
                        return Create(styleRule);
                    }
                }
                return null;
            }
        }

        protected Style Create(IStyleRule node)
        {
            Style style = new Style();

            ParserBackground(node.Style, style);
            ParserPadding(node.Style, style);
            ParserBorder(node.Style, style);
            return style;
        }

        protected void ParserBackground(StyleDeclaration cssStyle, Style style)
        {
            if (String.IsNullOrEmpty(cssStyle.BackgroundColor) == false)
            {
                Media.Color bgColor = new Media.Color();
                if (TryParserColor(cssStyle.BackgroundColor, ref bgColor))
                {
                    style.BackgroundColor = bgColor;
                }
            }
        }

        protected void ParserBorder(StyleDeclaration cssStyle, Style style)
        {
            if (String.IsNullOrEmpty(cssStyle.BorderColor) == false  && String.IsNullOrEmpty(cssStyle.BorderWidth) == false)
            {
                Media.Color color = new Media.Color();
                Border border = new Border { Thickness = 0 };
                if (TryParserColor(cssStyle.BorderColor, ref color))
                {
                    border.Color = color;
                }

                SizeSetting size = new SizeSetting();
                if (TryParserSize(cssStyle.BorderWidth, ref size)) border.Thickness = size.Value;
                style.Border = border;
            }
        }

        protected void ParserPadding(StyleDeclaration cssStyle, Style style)
        {
            SizeSetting size = new SizeSetting();
            if (TryParserSize(cssStyle.PaddingTop, ref size)) style.Padding.Top = size;
            if (TryParserSize(cssStyle.PaddingBottom, ref size)) style.Padding.Bottom = size;
            if (TryParserSize(cssStyle.PaddingLeft, ref size)) style.Padding.Left = size;
            if (TryParserSize(cssStyle.PaddingRight, ref size)) style.Padding.Right = size;
        }

        protected bool TryParserSize(String txt, ref SizeSetting size)
        {
            if (String.IsNullOrEmpty(txt)) return false;
            if (txt.EndsWith("px")) txt = txt.Substring(0, txt.Length - 2);
            float val = 0;
            bool result = float.TryParse(txt, out val);
            if(result == true)
            {
                size.Mode = SizeMode.Pixel;
                size.Value = val;
            }
            return result;
        }

        protected bool TryParserColor(String color, ref Media.Color val)
        {
            if(color.StartsWith("rgb(") && color.EndsWith(")"))
            {
                color = color.Substring(4, color.Length - 5);
                String[] terms = color.Split(',');
                int r = 0, g = 0, b = 0;
                if (terms.Length == 3)
                {
                    int.TryParse(terms[0], out r);
                    int.TryParse(terms[1], out g);
                    int.TryParse(terms[2], out b);
                }
                val.Red = r / 255.0f;
                val.Green = g / 255.0f;
                val.Blue = b / 255.0f;
                val.Alpha = 1.0f;
                return true;
            }
            return false;
        }
    }
}
