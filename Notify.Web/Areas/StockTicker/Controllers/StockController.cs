using System.Collections.Generic;
using System.Linq;
using Notify.Web.Areas.StockTicker.Model;
using XSockets.Core.XSocket;
using XSockets.Core.XSocket.Helpers;

namespace Notify.Web.Areas.StockTicker.Controllers
{
	public class StockController : XSocketController
	{
		// We have state :) Each user can listen to stocks of choice
		public List<string> MyStocks { get; set; }

		public StockController()
		{
			// By default, listen to all stocks
			MyStocks = Model
						.StockTicker
						.Stocks
						.Values
						.Select(p => p.Symbol)
						.ToList();

			OnOpen += StockController_OnOpen;
		}

		void StockController_OnOpen(object sender, XSockets.Core.Common.Socket.Event.Arguments.OnClientConnectArgs e)
		{
			// Send the available stocks to the client when s/he connects. No need to ask for them.
			this.Send(Model.StockTicker.Stocks.Values, "allStocks");
		}

		/// <summary>
		/// Do a conditional send to only clients listening for the actual stock
		/// </summary>
		/// <param name="stock"></param>
		public void Tick(Stock stock)
		{
			// Send only to client having this stock in their list
			this.SendTo(p => p.MyStocks.Contains(stock.Symbol), stock, "tick");
		}
	}
}
