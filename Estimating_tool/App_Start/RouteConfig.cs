using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Estimating_Tool
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
			 "Default", // Route name
			"{controller}/{action}/{id}", // URL with parameters
			new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
			new string[] { "Estimating_Tool.Controllers" }
			);

			routes.MapRoute(
                name: "Default1",
                url: "{controller}/{action}/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );

			routes.MapRoute(
				name:"Customer",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Customer", action = "Index", id = UrlParameter.Optional }
				);
		}
	}
}
