using System;
using System.Diagnostics;
using System.Linq;
using Notify.Web.Models;
using XSockets.Core.Common.Socket.Event.Arguments;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;

namespace Notify.Web.Controllers
{
	public class NotificationController : XSocketController
	{
		// We have state :) Each user can listen to specific types of choice
		public bool Listening { get; set; }

		public string[] SelectedTypes { get; set; }

		public NotificationController()
		{
			// By default, listen to all types
			Listening = true;

			OnOpen += Client_OnOpen;
			OnClose += Client_OnClose;
			OnReopen += OnOnReopen;
		}

		void Client_OnOpen(object sender, OnClientConnectArgs e)
		{
			SelectedTypes = Notification.AllTypes;

			// Send all available notification types to the client when s/he connects. No need to ask for them.
			this.Send(SelectedTypes, "allTypes");
		}

		private void Client_OnClose(object sender, OnClientDisconnectArgs e)
		{
			Debug.WriteLine("Client closed connection.");
			Close();
		}

		private void OnOnReopen(object sender, OnClientConnectArgs onClientConnectArgs)
		{
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

		public void MarkAsRead(Guid id, bool value)
		{
			// Since the notifications aren't persisted yet, we have nothing to do... let's just broadcast the update to everyone
			this.SendToAll(id, "markedAsRead");
		}
	}
}
