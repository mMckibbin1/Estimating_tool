using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estimating_Tool.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        //		public ActionResult Login(LoginViewModel model, string returnUrl)
        //		{
        //			if (!ModelState.IsValid)
        //			{
        //				return View(model);
        //			}

        //			User user = new User() { Email = model.Email, Password = model.Password };

        //			user = Repository.GetUserDetails(user);

        //			if (user != null)
        //			{
        //				FormsAuthentication.SetAuthCookie(model.Email, false);

        //				var authTicket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddMinutes(20), false, user.Roles);
        //				string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
        //				var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
        //				HttpContext.Response.Cookies.Add(authCookie);
        //				return RedirectToAction("Index", "Home");
        //			}

        //			else
        //			{
        //				ModelState.AddModelError("", "Invalid login attempt.");
        //				return View(model);
        //			}
        //		}


    }
}