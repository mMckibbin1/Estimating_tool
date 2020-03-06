using Estimating_Tool.DAL;
using Estimating_Tool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Estimating_Tool
{
	public class MvcApplication : System.Web.HttpApplication
	{
		
        protected void Application_Start()
        {

			//Database.SetInitializer<Estimatingcontext>(null);
			AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
		//protected void Session_Start(Object sender, EventArgs e)
		//{
		//	Estimatingcontext context = new Estimatingcontext();
		//	List<Manager> managers = context.Managers.ToList();
		//	string admin;
		//	if (managers.Any(x => x.Username.ToLower() == User.Identity.Name.ToLower()))
		//	{
		//		admin = "true";
		//	}
		//	else
		//	{
		//		admin = "false";
		//	}

		//	HttpContext.Current.Session.Add("_SessionAdmin", admin);
		//}
	}
}
