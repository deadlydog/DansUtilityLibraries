using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DansCSharpLibrary.Extensions
{
	public static class TimeSpanExtensions
	{
		public static string HumanFriendlyString(this TimeSpan timeSpan)
		{
			string timeSpanString = string.Empty;
			if (timeSpan.Days > 0)
				timeSpanString += string.Format("{0} day(s), ", timeSpan.Days.ToString());
			if (timeSpan.Hours > 0)
				timeSpanString += string.Format("{0} hour(s), ", timeSpan.Hours.ToString());
			if (timeSpan.Minutes > 0)
				timeSpanString += string.Format("{0} minute(s) and ", timeSpan.Minutes.ToString());
			if (timeSpan.TotalSeconds > 10)
				timeSpanString += string.Format("{0} seconds", timeSpan.Seconds.ToString());
			else if (timeSpan.TotalSeconds > 1)
				timeSpanString += string.Format("{0} seconds", (timeSpan.TotalSeconds - (timeSpan.Minutes * 60)).ToString("0.#"));
			// Make sure we display the time properly, even for super fast operations that took a fraction of a second to complete.
			else
				timeSpanString += string.Format("{0} seconds", (timeSpan.TotalSeconds - (timeSpan.Minutes * 60)).ToString("0.###"));

			return timeSpanString;
		}
	}
}
