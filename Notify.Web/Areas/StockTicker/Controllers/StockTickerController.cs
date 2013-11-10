using XSockets.Core.Common.Globals;
using XSockets.Core.Common.Socket;
using XSockets.Core.XSocket;
using XSockets.Plugin.Framework.Core;

namespace Notify.Web.Areas.StockTicker.Controllers
{
	/// <summary>
	/// An XSockets longrunning controller.
	/// A longrunning controller cant be connected to.
	/// It will work inside the server as a longrunning process...
	/// Perfect for collecting data or similar and then occationally send info over to other public controllers
	/// </summary>
	[XBaseSocketMetadata("StockTickerController", Constants.GenericTextBufferSize, PluginRange.Internal)]
	public class StockTickerController : XSocketController
	{
		// The controller to send data to when the OnTick event fires
		private static readonly StockController StockController = new StockController();

		static StockTickerController()
		{
			//New ticker... with the action for tick
			new Model.StockTicker(stock => StockController.Tick(stock));
		}
	}
}
