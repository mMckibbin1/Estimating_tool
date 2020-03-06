using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estimating_Tool.DAL;
using Estimating_Tool.Models;
using Microsoft.Ajax.Utilities;

namespace Estimating_Tool.Controllers
{
	public class ManagerController : Controller
    {
        private Estimatingcontext db = new Estimatingcontext();

        // GET: Manager
        public ActionResult Index()
        {
            return PartialView(db.Managers.ToList());
        }


        public ActionResult ToList()
        {
            Session["ActiveTab"] = "Tab2";
            return RedirectToAction(@"..\Admin\Index");
        }
        // GET: Manager/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // GET: Manager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Firstname,Lastname")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Managers.Add(manager);
                    db.SaveChanges();
                    ManagerPreferences pref = new ManagerPreferences();
                    pref.ManagerId = manager.Id;
                    db.Preferences.Add(pref);
                    db.SaveChanges();
                    TempData["ManagerMessage"] = "Manager created successfully";
                    Session["ActiveTab"] = "Tab2";
                }
                catch (Exception e)
                {
                    TempData["ManagerMessage"] = "Manager not created. If problem persists please contact your administrator. Error message: " + e.Message;
                }
                finally
                {

                }
                return RedirectToAction(@"..\Admin\Index");
            }

            return View(manager);
        }

        // GET: Manager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Manager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Firstname,Lastname")] Manager manager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manager).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ManagerMessage"] = "Manager updated successfully";
                Session["ActiveTab"] = "Tab2";
                return RedirectToAction(@"..\Admin\Index");
            }
            return View(manager);
        }

        // GET: Manager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manager manager = db.Managers.Find(id);
            if (manager == null)
            {
                return HttpNotFound();
            }
            return View(manager);
        }

        // POST: Manager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manager manager = db.Managers.Find(id);
            db.Managers.Remove(manager);
            db.SaveChanges();
            TempData["ManagerMessage"] = "Manager deleted successfully";
            Session["ActiveTab"] = "Tab2";
            return RedirectToAction(@"..\Admin\Index");
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
