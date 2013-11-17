using System.Web.Optimization;

namespace Notify.Web.Startup
{
	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles
			.Add(new StyleBundle("~/content/css")
						.Include("~/content/site.css",
								"~/content/menu.css"));

			bundles
			.Add(new ScriptBundle("~/scripts/notifications")
						.Include("~/scripts/XSockets.fallback.latest.js",
								"~/scripts/XSockets.latest.js",
								"~/scripts/knockout-2.2.0.js",
								"~/scripts/knockout.extensions.js",
								"~/scripts/notifications.js",
								"~/scripts/contact.js"));

			// BundleTable.EnableOptimizations = true;
		}
	}
}
