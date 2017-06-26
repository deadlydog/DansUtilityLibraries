using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DansCSharpLibrary.Extensions
{
	public static class TimeSpanExtensions
	{
		/// <summary>
		/// Gets the TimeSpan as a human-friendly readable string.
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		public static string AsHumanFriendlyString(this TimeSpan timeSpan)
		{
			string timeSpanString = string.Empty;
			if (timeSpan.Days > 0)
				timeSpanString += $"{timeSpan.Days.ToString()} day(s), ";
			if (timeSpan.Hours > 0)
				timeSpanString += $"{timeSpan.Hours.ToString()} hour(s), ";
			if (timeSpan.Minutes > 0)
				timeSpanString += $"{timeSpan.Minutes.ToString()} minute(s) and ";
			if (timeSpan.TotalSeconds > 10)
				timeSpanString += $"{timeSpan.Seconds.ToString()} seconds";
			else if (timeSpan.TotalSeconds > 1)
				timeSpanString += $"{(timeSpan.TotalSeconds - (timeSpan.Minutes * 60)).ToString("0.#")} seconds";
			// Make sure we display the time properly, even for super fast operations that took a fraction of a second to complete.
			else
				timeSpanString += $"{(timeSpan.TotalSeconds - (timeSpan.Minutes * 60)).ToString("0.###")} seconds";
			return timeSpanString;
		}
	}
}
