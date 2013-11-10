using Notify.Web.Models;
using XSockets.Core.Common.Globals;
using XSockets.Core.Common.Socket;
using XSockets.Core.XSocket;
using XSockets.Plugin.Framework.Core;

namespace Notify.Web.Controllers
{
	/// <summary>
	/// An XSockets longrunning controller.
	/// A longrunning controller cant be connected to.
	/// It will work inside the server as a longrunning process...
	/// Perfect for collecting data or similar and then occationally send info over to other public controllers
	/// </summary>
	[XBaseSocketMetadata("StockTickerController", Constants.GenericTextBufferSize, PluginRange.Internal)]
	public class EventGeneratorController : XSocketController
	{
		// The controller to send data to when the OnTick event fires
		private static readonly NotificationController NotificationController = new NotificationController();

		static EventGeneratorController()
		{
			// New ticker... with the action for tick
			new NotificatonsTicker(notification => NotificationController.Notify(notification));
		}
	}
}
