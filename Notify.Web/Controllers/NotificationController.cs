using System;
using System.Collections.Generic;
using System.Linq;
using Notify.Web.Models;
using XSockets.Core.Common.Socket.Event.Attributes;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;

namespace Notify.Web.Controllers
{
	public class NotificationController : XSocketController
	{
		// We have state :) Each user can listen to specific types of choice
		public bool Listening { get; set; }

		public List<string> SelectedTypes { get; set; }

		public NotificationController()
		{
			// By default, listen to all types
			Listening = true;
			SelectedTypes = Notification.AllTypes.ToList();

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
			this.SendTo(client => client.Listening && client.SelectedTypes.Contains(notification.Type), notification, "notify");
		}

		[HandlerEvent("MarkAsRead")]
		public void MarkAsRead(Guid id, bool value)
		{
			// Since the notifications aren't persisted yet, we have nothing to do...
		}
	}
}
