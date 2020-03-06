using Estimating_Tool.DAL;
using Estimating_Tool.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Estimating_Tool.Controllers
{
	public class PreferencesController : Controller
    {
        private Estimatingcontext db = new Estimatingcontext();


        public ActionResult Preference()
        {
            string manager = User.Identity.Name.ToLower();
            List<Manager> managerRecord = db.Managers.Where(x => x.Username.ToLower() == manager).ToList().Distinct().ToList();
            int id = managerRecord[0].Id;
            List<ManagerPreferences> preferences = db.Preferences.Where(x => x.ManagerId == id).ToList();
            //Need to send out the Managers preferences
            //include possible error checking here for if the preference does not exist.
            return PartialView(preferences[0]);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PreferenceSave([Bind(Include = "Id, ManagerId, Status, Project, Customer")] ManagerPreferences pref)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pref).State = EntityState.Modified;
                db.SaveChanges();
                Session["ActiveTab"] = "Tab5";
                return RedirectToAction(@"..\Admin\Index");
            }
            return View(pref);
        }
        //// GET: api/Preferences
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET: api/Preferences/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Preferences


        //// DELETE: api/Preferences/5
        //public void Delete(int id)
        //{
        //}
    }
}
