using System;
using System.Collections.Generic;
using System.Text;

namespace NScript.UI
{
    public abstract class BaseDisposable
    {
        private bool _disposed;

        /// <summary>        
        /// Gets whether the object has been disposed of</summary>
        public bool IsDisposed
        {
            get { return _disposed; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or 
        /// resetting unmanaged resources</summary>
        public void Dispose()
        {
            if (_disposed) return;
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Sets dispose flag</summary>
        /// <param name="disposing">Value to set dispose flag to</param>
        protected virtual void Dispose(bool disposing)
        {
            _disposed = true;
        }

        /// <summary>
        /// Destructor</summary>
        ~BaseDisposable()
        {
            Dispose(false);
        }
    }
}
