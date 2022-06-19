using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace NScript.UI.Media
{
    public static class FontFamilyLoader
    {
        public static IEnumerable<Uri> LoadFontAssets(FontFamilyKey fontFamilyKey)
        {
            return fontFamilyKey.FileName != null
                ? GetFontAssetsByFileName(fontFamilyKey.Location, fontFamilyKey.FileName)
                : GetFontAssetsByLocation(fontFamilyKey.Location);
        }

        /// <summary>
        /// Searches for font assets at a given location and returns a quantity of found assets
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private static IEnumerable<Uri> GetFontAssetsByLocation(Uri location)
        {
            var availableAssets = Platform.Instance.GetAssets(location);

            var matchingAssets = availableAssets.Where(x => x.absolutePath.EndsWith(".ttf"));

            return matchingAssets.Select(x => GetAssetUri(x.absolutePath, x.assembly));
        }

        /// <summary>
        /// Searches for font assets at a given location and only accepts assets that fit to a given filename expression.
        /// <para>File names can target multiple files with * wildcard. For example "FontFile*.ttf"</para>
        /// </summary>
        /// <param name="location"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static IEnumerable<Uri> GetFontAssetsByFileName(Uri location, string fileName)
        {
            var availableResources = Platform.Instance.GetAssets(location);

            var compareTo = location.AbsolutePath + "." + fileName.Split('*').First();

            var matchingResources =
                availableResources.Where(x => x.absolutePath.Contains(compareTo) && x.absolutePath.EndsWith(".ttf"));

            return matchingResources.Select(x => GetAssetUri(x.absolutePath, x.assembly));
        }

        /// <summary>
        /// Returns a <see cref="Uri"/> for a font asset that follows the resm scheme
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static Uri GetAssetUri(string absolutePath, Assembly assembly)
        {
            return new Uri("resm:" + absolutePath + "?assembly=" + assembly.GetName().Name, UriKind.RelativeOrAbsolute);
        }
    }
}
