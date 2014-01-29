using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DansCSharpLibrary.Events
{
	/// <summary>
	/// Class used to prevent an event from firing multiple times within a specified timespan.
	/// If the event is told to fire multiple times in a given timespan, it will only fire once when the timespan has elapsed.
	/// </summary>
	public class DelayedEvent : IDisposable
	{
		/// <summary>
		/// How long to wait after FireEvent() is called before actually firing the event.
		/// <para>This value may be overridden directly when calling FireEvent().</para>
		/// <para>Use TimeSpan.Zero to have the event fire immediately.</para>
		/// </summary>
		public TimeSpan DefaultDelay = TimeSpan.Zero;

		/// <summary>
		/// The event triggered when the FireEvent() function is called (after the specified Delay has elapsed).
		/// </summary>
		public event EventHandler EventFired = delegate { };

		/// <summary>
		/// Timer used to fire the event after the specified Delay of time has elapsed.
		/// </summary>
		private Timer _timer = null;

		/// <summary>
		/// The last object that requested the event to fire.
		/// </summary>
		private object _sender = null;

		/// <summary>
		/// The arguments to pass the to the event handler.
		/// </summary>
		private EventArgs _eventArgs = EventArgs.Empty;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="defaultDelay">How long to wait before firing the event.</param>
		/// <param name="eventHandlers">The event handlers to add to the EventFired event.</param>
		public DelayedEvent(TimeSpan defaultDelay, params EventHandler[] eventHandlers)
		{
			DefaultDelay = defaultDelay;
			foreach (var eventHandler in eventHandlers)
				EventFired += eventHandler;
			_timer = new Timer(_timer_Elapsed);
		}

		/// <summary>
		/// The Timer callback that fires when the Timer's timespan has elapsed.
		/// </summary>
		/// <param name="state">I don't know what this does, but this is the Timer callback delegate signature that is required.</param>
		private void _timer_Elapsed(object state)
		{
			// Fire the event.
			EventFired(_sender, _eventArgs);

			// Reset the event data.
			_sender = null;
			_eventArgs = EventArgs.Empty;
		}

		/// <summary>
		/// Triggers the event to fire.
		/// <para>The event will not fire until the Delay has elapsed since the last time this function was called (i.e. each time this function is called, the delay timer is reset).
		/// Because of this, if FireEvent() is constantly called without the Delay timespan elapsing between calls, the event potentially may never fire.</para>
		/// <para>If this is called multiple times within the Delay timespan, it will only fire once.</para>
		/// <para>Only the Sender and EventArgs of the last call to this function will be passed to the event handlers.</para>
		/// </summary>
		/// <param name="sender">The object that triggered the event.</param>
		/// <param name="eventArgs">The <see cref="EventArgs" /> instance containing the event data to pass to the event handlers.</param>
		/// <param name="delayOverride">How long to wait before firing the event.
		/// <para>Leave this null to use the DefaultDelay.</para>
		/// <para>Use TimeSpan.Zero to have the event fire immediately.</para></param>
		public void FireEvent(object sender = null, EventArgs eventArgs = null, TimeSpan? delayOverride = null)
		{
			// If the Timer hasn't been initialized, just exit without doing anything.
			if (_timer == null)
				return;

			// Save the sender and event arguments to pass to the event handlers when we do actually fire the event.
			_sender = sender;
			_eventArgs = eventArgs ?? EventArgs.Empty;

			// Reset our timer to fire the event after the specified wait period is over.
			_timer.Change(delayOverride ?? DefaultDelay, TimeSpan.FromMilliseconds(Timeout.Infinite));
		}

		/// <summary>
		/// Release the resources used by this class.
		/// </summary>
		public void Dispose()
		{
			if (_timer != null)
				_timer.Dispose();

			_sender = null;
			_eventArgs = EventArgs.Empty;

			EventFired = null;
		}
	}
}
