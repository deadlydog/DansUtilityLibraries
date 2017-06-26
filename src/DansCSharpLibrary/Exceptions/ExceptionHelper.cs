using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DansCSharpLibrary.Exceptions
{
	public static class ExceptionHelper
	{
		/// <summary>
		/// The default separator character(s) used to separate inner exception messages.
		/// </summary>
		public const string DEFAULT_INNER_EXCEPTION_MESSAGE_SEPARATOR = "\n";

		/// <summary>
		/// Gets the exception error to return based on if a Debugger is attached or not.
		/// <para>If a Debugger is attached, ex.ToString() is returned.
		/// This typically contains more information that can be useful to tracking down where in code the exception was thrown from.</para>
		/// <para>If a Debugger is not attached, GetExceptionMessages(ex, innerExceptionMessageSeparator) is returned. This is a more user-friendly error message.</para>
		/// </summary>
		/// <param name="ex">The exception to process.</param>
		/// <param name="removeSeeInnerExceptionForDetailsStringsFromMessage">If true all occurrences of "See the inner exception for details." will be removed from the returned string.</param>
		/// <param name="innerExceptionMessageSeparator">The separator to use to separate each inner exception's message in the returned string.</param>
		/// <returns></returns>
		public static string GetExceptionMessagesBasedOnDebugging(Exception ex, bool removeSeeInnerExceptionForDetailsStringsFromMessage = true, string innerExceptionMessageSeparator = DEFAULT_INNER_EXCEPTION_MESSAGE_SEPARATOR)
		{
			if (Debugger.IsAttached)
				return ex.ToString();
			else
				return GetExceptionMessages(ex, removeSeeInnerExceptionForDetailsStringsFromMessage, innerExceptionMessageSeparator);
		}

		/// <summary>
		/// Gets the exception's message, along with all inner exception messages. This is typically a more user-friendly error message than ex.ToString().
		/// </summary>
		/// <param name="ex">The exception to process.</param>
		/// <param name="removeSeeInnerExceptionForDetailsStringsFromMessage">If true all occurrences of "See the inner exception for details." will be removed from the returned string.</param>
		/// <param name="innerExceptionMessageSeparator">The separator to use to separate each inner exception's message in the returned string.</param>
		/// <returns></returns>
		public static string GetExceptionMessages(Exception ex, bool removeSeeInnerExceptionForDetailsStringsFromMessage = true, string innerExceptionMessageSeparator = DEFAULT_INNER_EXCEPTION_MESSAGE_SEPARATOR)
		{
			var messages = new StringBuilder();
			messages.Append(ex.Message);
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
				messages.Append(innerExceptionMessageSeparator + ex.Message);
			}

			// Erase all references to checking the inner exception for details, as that's not very user-friendly.
			if (removeSeeInnerExceptionForDetailsStringsFromMessage)
				messages = messages.Replace(" See the inner exception for details.", string.Empty);

			return messages.ToString();
		}
	}
}
