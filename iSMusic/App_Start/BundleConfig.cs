using System.Web;
using System.Web.Optimization;

namespace iSMusic
{
	public class BundleConfig
	{
		// 如需統合的詳細資訊，請瀏覽 https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new Bundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new Bundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new Bundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new Bundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.min.js",
					  "~/Scripts/fontawesome-free-6.2.1-web/js/all.js"
					  ));

			bundles.Add(new Bundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/bootstrap.min.css",
					  "~/Content/lux.css",
					  "~/Content/fontawesome-free-6.2.1-web/css/all.css",
					  "~/Content/site.css"
					  ));
		}
	}
}
