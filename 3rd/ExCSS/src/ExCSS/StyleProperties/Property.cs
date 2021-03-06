using System.IO;

namespace ExCSS
{
    public abstract class Property : StylesheetNode, IProperty
    {
        private readonly PropertyFlags _flags;

        internal Property(string name, PropertyFlags flags = PropertyFlags.None)
        {
            Name = name;
            _flags = flags;
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Declaration(Name, Value, IsImportant));
        }


        internal bool TrySetValue(TokenValue newTokenValue)
        {
            var value = Converter.Convert(newTokenValue ?? ExCSS.TokenValue.Initial);

            if (value != null)
            {
                DeclaredValue = value;
                return true;
            }

            return false;
        }

        public string Value => DeclaredValue != null ? DeclaredValue.CssText : Keywords.Initial;

        public bool IsInherited => (((_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited) && IsInitial) ||
                                   ((DeclaredValue != null) && DeclaredValue.CssText.Is(Keywords.Inherit));

        public bool IsAnimatable => (_flags & PropertyFlags.Animatable) == PropertyFlags.Animatable;

        public bool IsInitial => (DeclaredValue == null) || DeclaredValue.CssText.Is(Keywords.Initial);

        internal bool HasValue => DeclaredValue != null;

        internal bool CanBeHashless => (_flags & PropertyFlags.Hashless) == PropertyFlags.Hashless;

        internal bool CanBeUnitless => (_flags & PropertyFlags.Unitless) == PropertyFlags.Unitless;

        internal bool CanBeInherited => (_flags & PropertyFlags.Inherited) == PropertyFlags.Inherited;

        internal bool IsShorthand => (_flags & PropertyFlags.Shorthand) == PropertyFlags.Shorthand;

        public string Name { get; }

        public bool IsImportant { get; set; }

        public string CssText => this.ToCss();

        internal abstract IValueConverter Converter { get; }

        internal IPropertyValue DeclaredValue { get; set; }

    }
}