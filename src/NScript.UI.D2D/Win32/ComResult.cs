using System;
using System.Runtime.InteropServices;

namespace NScript.UI.D2D.Win32
{
    /// <summary>
    /// Result structure for COM methods.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ComResult : IEquatable<ComResult>
    {
        private int _code;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComResult"/> struct.
        /// </summary>
        /// <param name="code">The HRESULT error code.</param>
        public ComResult(int code)
        {
            _code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComResult"/> struct.
        /// </summary>
        /// <param name="code">The HRESULT error code.</param>
        public ComResult(uint code)
        {
            _code = unchecked((int)code);
        }

        /// <summary>
        /// Gets the HRESULT error code.
        /// </summary>
        /// <value>The HRESULT error code.</value>
        public int Code
        {
            get { return _code; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ComResult"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success
        {
            get { return Code >= 0; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ComResult"/> is failure.
        /// </summary>
        /// <value><c>true</c> if failure; otherwise, <c>false</c>.</value>
        public bool Failure
        {
            get { return Code < 0; }
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SharpDX.Result"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator int(ComResult result)
        {
            return result.Code;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="SharpDX.Result"/> to <see cref="System.UInt32"/>.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The result of the conversion.</returns>
        public static explicit operator uint(ComResult result)
        {
            return unchecked((uint)result.Code);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="SharpDX.Result"/>.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ComResult(int result)
        {
            return new ComResult(result);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.UInt32"/> to <see cref="SharpDX.Result"/>.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator ComResult(uint result)
        {
            return new ComResult(result);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        public bool Equals(ComResult other)
        {
            return this.Code == other.Code;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ComResult))
                return false;
            return Equals((ComResult)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Code;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(ComResult left, ComResult right)
        {
            return left.Code == right.Code;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(ComResult left, ComResult right)
        {
            return left.Code != right.Code;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "HRESULT = 0x{0:X}", _code);
        }

        /// <summary>
        /// Checks the error.
        /// </summary>
        public void CheckError()
        {
            if (_code < 0)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Gets a <see cref="ComResult"/> from an <see cref="Exception"/>.
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>The associated result code</returns>
        public static ComResult GetResultFromException(Exception ex)
        {
            return new ComResult(Marshal.GetHRForException(ex));
        }

        /// <summary>
        /// Gets the result from win32 error.
        /// </summary>
        /// <param name="win32Error">The win32Error.</param>
        /// <returns>A HRESULT.</returns>
        public static ComResult GetResultFromWin32Error(int win32Error)
        {
            const int FACILITY_WIN32 = 7;
            return win32Error <= 0 ? win32Error : (int)((win32Error & 0x0000FFFF) | (FACILITY_WIN32 << 16) | 0x80000000);
        }

        /// <summary>
        /// Result code Ok
        /// </summary>
        /// <unmanaged>S_OK</unmanaged>
        public readonly static ComResult Ok = new ComResult(unchecked((int)0x00000000));

        /// <summary>
        /// Result code False
        /// </summary>
        /// <unmanaged>S_FALSE</unmanaged>
        public readonly static ComResult False = new ComResult(unchecked((int)0x00000001));
    }
}
