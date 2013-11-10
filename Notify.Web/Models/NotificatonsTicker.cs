using System;
using System.Linq;
using System.Timers;

namespace Notify.Web.Models
{
	public class NotificatonsTicker
	{
		private static int _counter;
		private readonly object _updateNotificationsLock = new object();

		private readonly Action<Notification> _onTick;

		public NotificatonsTicker(Action<Notification> onTick, int updateInterval = 2500)
		{
			_onTick = onTick;

			// Initialize ticker...
			var stockTimer = new Timer(updateInterval);
			stockTimer.Elapsed += TimerElapsed;
			stockTimer.Start();
		}

		private void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			lock (_updateNotificationsLock)
			{
				_counter++;

				var r = new Random();
				var typeIndex = r.Next(0, Notification.AllTypes.Count() - 1);

				var notification = new Notification
				{
					Title = "Some title " + _counter,
					Message = "Some message " + _counter,
					Type = Notification.AllTypes[typeIndex],
					UtcTimestamp = DateTime.UtcNow
				};

				_onTick.Invoke(notification);
			}
		}
	}
}
