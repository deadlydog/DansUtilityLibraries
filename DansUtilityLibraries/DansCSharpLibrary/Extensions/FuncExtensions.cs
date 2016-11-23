using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DansCSharpLibrary.Extensions
{
	public static class FuncExtensions
	{
		/// <summary>
		/// Executes the function with retries asynchronously.
		/// </summary>
		/// <param name="asyncFunc">The asynchronous function to execute.</param>
		/// <param name="maxNumberOfRetries">The maximum number of retries.</param>
		/// <param name="timeToWaitBetweenRetries">The time to wait between retries.</param>
		/// <param name="doNotRetryAgainIfThisMuchTimeHasElapsed">Do not retry again if this much time has elapsed.</param>
		/// <param name="funcShouldBeRetried">Predicate that returns if the function should be retried or not based on the exception that was thrown.</param>
		/// <returns></returns>
		public static async Task ExecuteWithRetriesAsync(this Func<Task> asyncFunc, int maxNumberOfRetries, TimeSpan timeToWaitBetweenRetries = new TimeSpan(), TimeSpan doNotRetryAgainIfThisMuchTimeHasElapsed = new TimeSpan(), Predicate<Exception> funcShouldBeRetried = null)
		{
			// If we don't care how much time has passed since we started trying, don't let it stop us.
			if (doNotRetryAgainIfThisMuchTimeHasElapsed <= TimeSpan.Zero)
				doNotRetryAgainIfThisMuchTimeHasElapsed = TimeSpan.MaxValue;

			// If no predicate was specified assume we always want to retry.
			funcShouldBeRetried = funcShouldBeRetried ?? ((ex) => true);

			var callSucceeded = false;
			var numberOfAttempts = 0;
			var executionTime = Stopwatch.StartNew();

			do
			{
				try
				{
					await asyncFunc();
					callSucceeded = true;
				}
				catch (Exception ex)
				{
					numberOfAttempts++;
					if (numberOfAttempts < maxNumberOfRetries && executionTime.Elapsed < doNotRetryAgainIfThisMuchTimeHasElapsed && funcShouldBeRetried(ex))
					{
						// Wait before trying again if specified.
						if (timeToWaitBetweenRetries > TimeSpan.Zero)
							Thread.Sleep(timeToWaitBetweenRetries);
					}
					else
					{
						throw;
					}
				}
			} while (!callSucceeded && numberOfAttempts < maxNumberOfRetries && executionTime.Elapsed < doNotRetryAgainIfThisMuchTimeHasElapsed);
		}
	}
}
