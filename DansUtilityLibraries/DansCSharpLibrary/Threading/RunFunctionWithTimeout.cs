//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace DansCSharpLibrary.Threading
//{
//	public static class RunFunction
//	{
//		// http://kossovsky.net/index.php/2009/07/csharp-how-to-limit-method-execution-time/
//		// http://stackoverflow.com/questions/299198/implement-c-sharp-generic-timeout


//		public static T Invoke<T>(Func<CancelEventArgs, T> function, TimeSpan timeout)
//		{
//			if (timeout.TotalMilliseconds <= 0)
//				throw new ArgumentOutOfRangeException("timeout");

//			CancelEventArgs args = new CancelEventArgs(false);
//			IAsyncResult functionResult = function.BeginInvoke(args, null, null);
//			WaitHandle waitHandle = functionResult.AsyncWaitHandle;
//			if (!waitHandle.WaitOne(timeout))
//			{
//				args.Cancel = true; // flag to worker that it should cancel!
//				/* •————————————————————————————————————————————————————————————————————————•
//				   | IMPORTANT: Always call EndInvoke to complete your asynchronous call.   |
//				   | http://msdn.microsoft.com/en-us/library/2e08f6yc(VS.80).aspx           |
//				   | (even though we arn't interested in the result)                        |
//				   •————————————————————————————————————————————————————————————————————————• */
//				ThreadPool.UnsafeRegisterWaitForSingleObject(waitHandle,
//					(state, timedOut) => function.EndInvoke(functionResult),
//					null, -1, true);
//				throw new TimeoutException();
//			}
//			else
//				return function.EndInvoke(functionResult);
//		}

//		public static T Invoke<T>(Func<T> function, TimeSpan timeout)
//		{
//			return Invoke(args => function(), timeout); // ignore CancelEventArgs
//		}

//		public static void Invoke(Action<CancelEventArgs> action, TimeSpan timeout)
//		{
//			Invoke<int>(args =>
//			{ // pass a function that returns 0 & ignore result
//				action(args);
//				return 0;
//			}, timeout);
//		}




//		/// <summary>
//		/// Executes the given function, allowing it to run for the Maximum Execution Time specified.
//		/// If the function completes within the specified MaxExecutionTime, the result will be returned and Completed will be true.
//		/// If the function does not complete within the specified MaxExecutionTime, the result will be null/default and Completed will be false.
//		/// </summary>
//		/// <param name="function">The function to call.</param>
//		/// <param name="maxExecutionTime">The maximum amount of time to allow the function to run for before terminating it and returning.</param>
//		/// <param name="completed">Will be set to true if the function was able to complete in time, false if not.</param>
//		public static T WithTimeout<T>(Func<T> function, TimeSpan maxExecutionTime, out bool completed)
//		{
//			// Start executing the function.
//			var ayncResult = function.BeginInvoke(null, new object());

//			// If it completes before the Max Execution Time.
//			if (ayncResult.AsyncWaitHandle.WaitOne(maxExecutionTime))
//			{
//				// Mark that it completed and return the result.
//				completed = true;
//				return function.EndInvoke(ayncResult);
//			}

//			// Else mark that it did not complete and return default/null.
//			completed = false;
//			return default(T); 

//			//// Set result to the default value or null in case the function does not complete on time.
//			//T result = default(T);

//			//// Execute the function on a new thread to have it run in the background.
//			//var thread = new Thread(() => result = function());
//			//thread.Start();

//			//// Wait until the function completes or the Max Execution Time elapses.
//			//completed = thread.Join((int)maxExecutionTime.TotalMilliseconds);

//			//// If the function did not complete on time, kill the thread it is running on.
//			//if (!completed) thread.Abort();

//			//// Return the result returned by the function if it completed, default/null if it didn't.
//			//return result; 
//		}

//		/// <summary>
//		/// Executes the given function, allowing it to run for the Maximum Execution Time specified.
//		/// If the function completes within the specified MaxExecutionTime, the result will be returned.
//		/// If the function does not complete within the specified MaxExecutionTime, the result will be null/default.
//		/// </summary>
//		/// <param name="function">The function to call.</param>
//		/// <param name="maxExecutionTime">The maximum amount of time to allow the function to run for before terminating it and returning.</param>
//		public static T WithTimeout<T>(Func<T> function, TimeSpan maxExecutionTime)
//		{
//			bool completed;
//			return WithTimeout(function, maxExecutionTime, out completed);
//		}

//		public static T WithTimeout<T>(Action action, TimeSpan maxExecutionTime)
//		{
//			throw new NotImplementedException();
//			//return WithTimeout(args =>
//			//	{
//			//		action();
//			//		return 0;
//			//	});
//		}
//	}
//}
