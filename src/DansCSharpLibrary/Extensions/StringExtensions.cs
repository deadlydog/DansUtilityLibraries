using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DansCSharpLibrary.Extensions
{
	/// <summary>
	/// Extensions to the string class.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Converts the string to the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="stringValue">The string value.</param>
		/// <returns></returns>
		public static T ConvertTo<T>(this string stringValue)
		{
			T value;

			var typeOfT = typeof(T);
			if (typeOfT.IsPrimitive)
			{
				value = (T)Convert.ChangeType(stringValue, typeOfT);
			}
			else if (typeOfT.IsEnum)
			{
				value = (T)System.Enum.Parse(typeOfT, stringValue);
			}
			else
			{
				var typeOfString = typeof(string);
				var converter = TypeDescriptor.GetConverter(typeOfT);
				if (converter.CanConvertFrom(typeOfString))
				{
					value = (T)(converter.ConvertFromInvariantString(stringValue));
				}
				else
				{
					var methodInfo = typeOfT.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeOfString }, null);
					if (methodInfo != null)
					{
						value = (T)methodInfo.Invoke(null, new object[] { stringValue });
					}
					else
					{
						value = (T)Convert.ChangeType(stringValue, typeOfT);
					}
				}
			}

			return value;
		}

		/// <summary>
		/// Converts the string to the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="stringValue">The string value.</param>
		/// <returns></returns>
		public static T ConvertToOrDefault<T>(this string stringValue) => stringValue.ConvertToOrDefault<T>(default(T));

		/// <summary>
		/// Converts the string to the type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="stringValue">The string value.</param>
		/// <param name="defaultValue">The default value to return if unable to convert the string to the type.</param>
		/// <returns></returns>
		public static T ConvertToOrDefault<T>(this string stringValue, T defaultValue)
		{
			var value = defaultValue;

			try
			{
				value = stringValue.ConvertTo<T>();
			}
			catch
			{ }

			return value;
		}

		/// <summary>
		/// Determines whether [contains] [the specified value].
		/// </summary>
		/// <param name="source">The source string.</param>
		/// <param name="value">The value to look for in the string.</param>
		/// <param name="stringComparison">The string comparison method to use.</param>
		public static bool Contains(this string source, string value, StringComparison stringComparison) => source.IndexOf(value, stringComparison) >= 0;
	}
}
