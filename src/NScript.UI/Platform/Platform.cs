using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace NScript.UI
{
    using NScript.UI.Input;
    public class Platform
    {
        private static Platform _defaultPlatform;

        public static Platform Instance { get { return _defaultPlatform; } }

        private static Object _syncLogger = new object();

        /// <summary>
        /// 注册 UIManager
        /// </summary>
        /// <param name="platform"></param>
        protected static void Regist(Platform platform)
        {
            _defaultPlatform = platform;
        }

        public virtual IClipboard GetClipboard()
        {
            throw new NotImplementedException();
        }

        public virtual ISystemDialog GetSystemDialog()
        {
            throw new NotImplementedException();
        }

        public virtual WindowImpl CreateWindow()
        {
            throw new NotImplementedException();
        }

        public virtual IDrawContext3D CreateDrawContext3D()
        {
            throw new NotImplementedException();
        }

        public static bool IsLoggerEnable = false;

        public static void Log(String msg, String logFile = "log_ui.txt")
        {
            if (String.IsNullOrEmpty(msg)) return;

            lock(_syncLogger)
            {
                System.IO.File.AppendAllText(logFile, AddTime(msg + Environment.NewLine));
            }
        }

        public static void HandleException(Exception ex)
        {
            Log(ex.Message, "log_error.txt");
            throw ex;
        }

        protected static String AddTime(String msg)
        {
            var now = DateTime.Now;
            return now.ToString() + ":" + now.Millisecond.ToString().PadLeft(3, '0') + "  " + msg;
        }

        /// <summary>
        /// We need a way to override the default assembly selected by the host platform
        /// because right now it is selecting the wrong one for PCL based Apps. The 
        /// AssetLoader needs a refactor cause right now it lives in 3+ platforms which 
        /// can all be loaded on Windows. 
        /// </summary>
        /// <param name="assembly"></param>
        public void SetDefaultAssembly(Assembly assembly) { }

        /// <summary>
        /// Checks if an asset with the specified URI exists.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="baseUri">
        /// A base URI to use if <paramref name="uri"/> is relative.
        /// </param>
        /// <returns>True if the asset could be found; otherwise false.</returns>
        public bool Exists(Uri uri, Uri baseUri = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens the asset with the requested URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="baseUri">
        /// A base URI to use if <paramref name="uri"/> is relative.
        /// </param>
        /// <returns>A stream containing the asset contents.</returns>
        /// <exception cref="FileNotFoundException">
        /// The asset could not be found.
        /// </exception>
        public Stream Open(Uri uri, Uri baseUri = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Opens the asset with the requested URI and returns the asset stream and the
        /// assembly containing the asset.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="baseUri">
        /// A base URI to use if <paramref name="uri"/> is relative.
        /// </param>
        /// <returns>
        /// The stream containing the asset contents together with the assembly.
        /// </returns>
        /// <exception cref="FileNotFoundException">
        /// The asset could not be found.
        /// </exception>
        public (Stream stream, Assembly assembly) OpenAndGetAssembly(Uri uri, Uri baseUri = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all assets of a folder and subfolders that match specified uri.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>All matching assets as a tuple of the absolute path to the asset and the assembly containing the asset</returns>
        public IEnumerable<(string absolutePath, Assembly assembly)> GetAssets(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
