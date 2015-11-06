using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DansCSharpLibrary.Extensions
{
	public static class SecureStringExtensions
	{
		// Code taken from: http://stackoverflow.com/a/31491863/602585

		/// <summary>
		/// Converts a SecureString to a regular string.
		/// </summary>
		/// <param name="secureString">The secure string.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">secureString</exception>
		public static string ToUnsecureString(this SecureString secureString)
		{
			if (secureString == null) throw new ArgumentNullException("secureString");

			var unmanagedString = IntPtr.Zero;
			try
			{
				unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
				return Marshal.PtrToStringUni(unmanagedString);
			}
			finally
			{
				Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
			}
		}

		/// <summary>
		/// Converts the string to a SecureString.
		/// </summary>
		/// <param name="unsecureString">The unsecure string.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">unsecureString</exception>
		public static SecureString ToSecureString(this string unsecureString)
		{
			if (unsecureString == null) throw new ArgumentNullException("unsecureString");

			return unsecureString.Aggregate(new SecureString(), AppendChar, MakeReadOnly);
		}

		private static SecureString MakeReadOnly(SecureString ss)
		{
			ss.MakeReadOnly();
			return ss;
		}

		private static SecureString AppendChar(SecureString ss, char c)
		{
			ss.AppendChar(c);
			return ss;
		}
	}
}
