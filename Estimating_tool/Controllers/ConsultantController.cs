using Estimating_Tool.DAL;
using Estimating_Tool.Models;
using Estimating_Tool.View_Model;
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
	public class ConsultantController : Controller
    {
        private Estimatingcontext db = new Estimatingcontext();

        // GET: Consultant
        public ActionResult Index()
        {
            //Creates a list of objects using the ConlultantIndex view model. This model is required as the data 
            //for Consultants does not hold the manager name, only their ID, so the manager name must be sent to the view also  
            List<ConsultantIndex> consultants = new List<ConsultantIndex>();
            List<Manager> managers = db.Managers.ToList();

            foreach (Consultant con in db.Consultants.ToList())
            {
                ConsultantIndex tempCon = new ConsultantIndex();
                tempCon.Consultant = con;
                List<Manager> mans = managers.Where(x => x.Id == tempCon.Consultant.ManagerId).Distinct().ToList();
                if (mans.Count() == 0)
                {
                    tempCon.Manager = "";
                }
                else
                {
                    tempCon.Manager = mans[0].Firstname + " " + mans[0].Lastname;
                }
                consultants.Add(tempCon);
            }
            return PartialView(consultants);
        }

        public ActionResult ToList()
        {
            Session["ActiveTab"] = "Tab4";
            return RedirectToAction(@"..\Admin\Index");
        }

        // GET: Consultant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consultant consultant = db.Consultants.Find(id);
            if (consultant == null)
            {
                return HttpNotFound();
            }
            return View(consultant);
        }

        // GET: Consultant/Create
        public ActionResult Create()
        {
            ConsultantEdit consultantToEdit = new ConsultantEdit();
            consultantToEdit.Managers = new Dictionary<string, int>();
            foreach (Manager manager in db.Managers)
            {
                string managerName = manager.Firstname + " " + manager.Lastname;
                consultantToEdit.Managers.Add(managerName.ToString(), (int)manager.Id);
            }
            return View(consultantToEdit);
        }

        // POST: Consultant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Firstname,Lastname,ManagerId")] Consultant consultant)
        {
            if (ModelState.IsValid)
            {
                db.Consultants.Add(consultant);
                db.SaveChanges();
                TempData["ConsultantMessage"] = "Consultant created successfully";
                return RedirectToAction(@"..\Admin\Index");
            }
            return RedirectToAction(@"..\Admin\Index");
        }

        // GET: Consultant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConsultantEdit consultantToEdit = new ConsultantEdit();
            consultantToEdit.Consultant = db.Consultants.Find(id);
            consultantToEdit.Managers = new Dictionary<string, int>();
            foreach (Manager manager in db.Managers)
            {
                string managerName = manager.Firstname + " " + manager.Lastname;

                consultantToEdit.Managers.Add(managerName.ToString(), (int)manager.Id);
            }

            List<Manager> man = db.Managers.Where(m => m.Username.ToLower().ToString() == User.Identity.Name.ToLower().ToString()).ToList();
            consultantToEdit.Manager = man[0].Id.ToString();

            return View(consultantToEdit);
        }

        // POST: Consultant/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Firstname,Lastname,ManagerId")] Consultant consultant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultant).State = EntityState.Modified;
                db.SaveChanges();
                TempData["ConsultantMessage"] = "Consultant updated successfully";
                Session["ActiveTab"] = "Tab4";
                return RedirectToAction(@"..\Admin\Index");
            }
            return View(consultant);
        }

        // GET: Consultant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consultant = db.Consultants.Find(id);
            if (consultant == null)
            {
                //need to include feedback here to user if an error occurrs 
                return HttpNotFound();
            }
            var man = db.Managers.Where(x => x.Id == consultant.ManagerId).ToList();
            ConsultantEdit consultantToDelete = new ConsultantEdit();
            consultantToDelete.Consultant = consultant;
            consultantToDelete.Managers = new Dictionary<string, int>();
            consultantToDelete.Manager = man[0].Firstname.ToString() + " " + man[0].Lastname.ToString();
            return View(consultantToDelete);
        }

        // POST: Consultant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Consultant consultant = db.Consultants.Find(id);
            db.Consultants.Remove(consultant);
            db.SaveChanges();
            TempData["ConsultantMessage"] = "Consultant deleted successfully";
            return RedirectToAction(@"..\Admin\Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
