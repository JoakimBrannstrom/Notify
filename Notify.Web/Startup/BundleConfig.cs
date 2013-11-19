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
				.Add(new ScriptBundle("~/scripts/knockout/notifications")
							.Include("~/scripts/XSockets.fallback.latest.js",
									"~/scripts/XSockets.latest.js",
									"~/scripts/knockout/knockout-2.2.0.js",
									"~/scripts/knockout/knockout.extensions.js",
									"~/scripts/knockout/notifications.js",
									"~/scripts/knockout/contact.js"));

			bundles
				.Add(new ScriptBundle("~/scripts/angular/notifications")
							.Include("~/scripts/XSockets.fallback.latest.js",
									"~/scripts/XSockets.latest.js",
									"~/scripts/angular/angular.1.2.1.js",
									"~/scripts/angular/angular.xsockets.js",
									"~/scripts/angular/notifications.js",
									"~/scripts/angular/contact.js"));

			// BundleTable.EnableOptimizations = true;
		}
	}
}
