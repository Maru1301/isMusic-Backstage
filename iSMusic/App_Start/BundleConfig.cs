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

			// 使用開發版本的 Modernizr 進行開發並學習。然後，當您
			// 準備好可進行生產時，請使用 https://modernizr.com 的建置工具，只挑選您需要的測試。
			bundles.Add(new Bundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new Bundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js"));
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
