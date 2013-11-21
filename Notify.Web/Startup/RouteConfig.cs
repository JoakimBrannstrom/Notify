using System.Web.Mvc;
using System.Web.Routing;

namespace Notify.Web.Startup
{
	public static class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes
			.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes
			.MapRoute("Default",
						"{controller}/{action}/{id}",
						new { controller = "Home", action = "Index", id = UrlParameter.Optional });

			routes
			.MapRoute("Stocks",
						"stock/{controller}/{action}/{id}",
						new { area = "StockTicker", controller = "Stock", action = "Index", id = UrlParameter.Optional });

			routes
			.MapRoute("Fallback",
						"{controller}/{action}",
						new { controller = "Fallback", action = "Init" },
						new[] { "XSockets.Longpolling" });
		}
	}
}
