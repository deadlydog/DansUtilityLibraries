using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DansCSharpLibrary.Extensions;

namespace DansCSharpLibrary.Timers
{
	public class Measure
	{
		/// <summary>
		/// Returns how long it takes to perform the given action.
		/// </summary>
		/// <param name="action">The action.</param>
		public static TimeSpan ElapsedTime(Action action)
		{
			// Time how long it takes to perform the action and return the result.
			var timer = System.Diagnostics.Stopwatch.StartNew();
			action();
			timer.Stop();
			return timer.Elapsed;
		}

		/// <summary>
		/// Elapseds the time asynchronous.
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static async Task<TimeSpan> ElapsedTimeAsync(Func<Task> action)
		{
			// Time how long it takes to perform the action and return the result.
			var timer = System.Diagnostics.Stopwatch.StartNew();
			await action();
			timer.Stop();
			return timer.Elapsed;
		}
	}
}
