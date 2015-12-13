using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DansCSharpLibrary.Threading
{
	public static class Tasks
	{
		/// <summary>
		/// Runs the given tasks and waits for them to complete.
		/// </summary>
		/// <param name="tasksToRun">The tasks to run.</param>
		/// <param name="maxTasksToRunInParallel">The maximum tasks to run in parallel.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		public static void RunThrottled(IEnumerable<Task> tasksToRun, int maxTasksToRunInParallel, CancellationToken cancellationToken = new CancellationToken())
		{
			RunThrottled(tasksToRun, maxTasksToRunInParallel, TimeSpan.FromMilliseconds(1), cancellationToken);
		}

		/// <summary>
		/// Runs the given tasks and waits for them to complete.
		/// </summary>
		/// <param name="tasksToRun">The tasks to run.</param>
		/// <param name="maxTasksToRunInParallel">The maximum tasks to run in parallel.</param>
		/// <param name="timeout">The maximum time we should allow the max tasks to run in parallel before allowing another task to start. Specify 1 millisecond to wait indefinitely.</param>
		/// <param name="cancellationToken">The cancellation token.</param>
		public static void RunThrottled(IEnumerable<Task> tasksToRun, int maxTasksToRunInParallel, TimeSpan timeout, CancellationToken cancellationToken = new CancellationToken())
		{
			// Convert to a list of tasks so that we don't enumerate over it multiple times needlessly.
			var tasks = tasksToRun.ToList();

			using (var throttler = new SemaphoreSlim(maxTasksToRunInParallel))
			{
				// Have each task notify the throttler when it completes so that it decrements the number of tasks currently running.
				tasks.ForEach(t => t.ContinueWith(tsk => throttler.Release()));

				// Start running each task.
				foreach (var task in tasks)
				{
					// Increment the number of tasks currently running and wait if too many are running.
					throttler.Wait(timeout, cancellationToken);

					cancellationToken.ThrowIfCancellationRequested();
					task.Start();
				}

				// Wait for all of the provided tasks to complete.
				Task.WaitAll(tasks.ToArray(), cancellationToken);
			}
		}
	}
}
