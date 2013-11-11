using System.Collections.Generic;
using System.Linq;
using Notify.Web.Models;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;

namespace Notify.Web.Controllers
{
	public class NotificationController : XSocketController
	{
		private bool _listening;
		private List<string> _notificationTypes;

		// We have state :) Each user can listen to specific types of choice
		public List<string> NotificationTypes
		{
			get { return _notificationTypes; }
			set { _notificationTypes = value; }
		}

		public bool Listening
		{
			get { return _listening; }
			set { _listening = value; }
		}

		public NotificationController()
		{
			// By default, listen to all types
			NotificationTypes = Notification.AllTypes.ToList();
			Listening = true;

			OnOpen += Client_OnOpen;
		}

		void Client_OnOpen(object sender, XSockets.Core.Common.Socket.Event.Arguments.OnClientConnectArgs e)
		{
			// Send all available notification types to the client when s/he connects. No need to ask for them.
			this.Send(Notification.AllTypes, "allTypes");
		}

		/// <summary>
		/// Do a conditional send to only clients listening for the actual notification type
		/// </summary>
		/// <param name="notification"></param>
		public void Notify(Notification notification)
		{
			// Send only to clients that is listening to this type
			this.SendTo(client => client.Listening && client.NotificationTypes.Contains(notification.Type), notification, "notify");
		}
	}
}
