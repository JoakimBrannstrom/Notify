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
		}
	}
}
