using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI.D2D
{
    using NScript.UI.Media;
    internal static class D2DFontCollectionCache
    {
        private static readonly ConcurrentDictionary<FontFamilyKey, SharpDX.DirectWrite.FontCollection> s_cachedCollections;
        private static readonly SharpDX.DirectWrite.FontCollection s_installedFontCollection;

        static D2DFontCollectionCache()
        {
            s_cachedCollections = new ConcurrentDictionary<FontFamilyKey, SharpDX.DirectWrite.FontCollection>();

            s_installedFontCollection = D2DPlatform.DirectWriteFactory.GetSystemFontCollection(false);
        }

        public static SharpDX.DirectWrite.TextFormat GetTextFormat(Typeface typeface)
        {
            var fontFamily = typeface.FontFamily;
            var fontCollection = GetOrAddFontCollection(fontFamily);
            var fontFamilyName = FontFamily.Default.Name;

            // Should this be cached?
            foreach (var familyName in fontFamily.FamilyNames)
            {
                if (!fontCollection.FindFamilyName(familyName, out _))
                {
                    continue;
                }

                fontFamilyName = familyName;

                break;
            }

            return new SharpDX.DirectWrite.TextFormat(
                D2DPlatform.DirectWriteFactory,
                fontFamilyName,
                fontCollection,
                (SharpDX.DirectWrite.FontWeight)typeface.Weight,
                (SharpDX.DirectWrite.FontStyle)typeface.Style,
                SharpDX.DirectWrite.FontStretch.Normal,
                (float)typeface.FontSize);
        }

        private static SharpDX.DirectWrite.FontCollection GetOrAddFontCollection(FontFamily fontFamily)
        {
            return fontFamily.Key == null ? s_installedFontCollection : s_cachedCollections.GetOrAdd(fontFamily.Key, CreateFontCollection);
        }

        private static SharpDX.DirectWrite.FontCollection CreateFontCollection(FontFamilyKey key)
        {
            var assets = FontFamilyLoader.LoadFontAssets(key);

            var fontLoader = new D2DWriteResourceFontLoader(D2DPlatform.DirectWriteFactory, assets);

            return new SharpDX.DirectWrite.FontCollection(D2DPlatform.DirectWriteFactory, fontLoader, fontLoader.Key);
        }
    }
}
