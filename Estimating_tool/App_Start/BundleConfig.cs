using System.Web;
using System.Web.Optimization;

namespace Estimating_Tool
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js"));
            
			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css",
					  "~/Content/site.css",
                      "~/Content/PagedList.css"));

            bundles.Add(new ScriptBundle("~/Content/bootstrap-Select").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootstrap-select.min.js",
                "~/Scripts/select-dropdown.js"
                ));

            bundles.Add(new ScriptBundle("~/Content/bootstrap-SelectPartailViews").Include(
           "~/Scripts/bootstrap-select.min.js",
           "~/Scripts/select-dropdown.js"
           ));

            bundles.Add(new StyleBundle("~/Content/bootstrap-SelectStyle").Include(
                "~/Content/bootstrap-select.min.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/validate").Include(
                "~/Scripts/jquery.validate.min.js",
               "~/Scripts/jquery.validate.unobtrusive.min.js"
    ));
        }
	}
}
