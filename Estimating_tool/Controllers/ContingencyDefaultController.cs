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
using PagedList;

namespace Estimating_Tool.Controllers
{
	public class ContingencyDefaultController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		// GET: ContingencyDefault
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			//Variables
			var ContingencyDefaults = from s in db.ContingencyDefault
									  where s.IsActive == true
									  select s; //temp data stores
            
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown
            var ContingencyDefaultsIntDisDistinct = db.ContingencyDefault.Where(x => x.IsActive == true).Select(x => x.ContingencyDefaultInt).ToList().Distinct();
            foreach (var item in ContingencyDefaultsIntDisDistinct)//used to save data into the select list of search options
            {
                searchoptions.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString()}); 
            }

            ViewBag.DropdownOptions = searchoptions;//viewbag used to send search dropdown data

            //Searching
			if (searchString != null)
			{
				page = 1;
                currentFilter = searchString;
            }
			else
			{
				searchString = currentFilter;
			}
            if(currentFilter != null)
            {
                try
                {
                    int ContingencySearch = Convert.ToInt32(currentFilter);
                    ContingencyDefaults = ContingencyDefaults.Where(s => s.ContingencyDefaultInt.Equals(ContingencySearch));
                }
                catch (FormatException)
                {
                   //handles formate exception by not filtering the data from the database
                }
              
            }
			ViewBag.CurrentFilter = searchString; //checking search

           

            //Sorting
            ViewBag.ContingencyDefaultIdSortParm = sortOrder == "ContingencyDefaultId_desc" ? "ContingencyDefaultInt" : "ContingencyDefaultId_desc";
			ViewBag.ContingencyDefaultIntSortParm = sortOrder == "ContingencyDefaultInt_desc" ? "ContingencyDefaultInt" : "ContingencyDefaultInt_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller

			switch (sortOrder) //Sort method
			{
				case "ContingencyDefaultId":
					ContingencyDefaults = ContingencyDefaults.OrderBy(s => s.ContingencyDefaultId);
					break;

				case "ContingencyDefaultId_desc":
					ContingencyDefaults = ContingencyDefaults.OrderByDescending(s => s.ContingencyDefaultId);
					break;

				case "ContingencyDefaultInt":
					ContingencyDefaults = ContingencyDefaults.OrderBy(s => s.ContingencyDefaultInt);
					break;

				case "ContingencyDefaultInt_desc":
					ContingencyDefaults = ContingencyDefaults.OrderByDescending(s => s.ContingencyDefaultInt);
					break;

				case "IsActive":
					ContingencyDefaults = ContingencyDefaults.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					ContingencyDefaults = ContingencyDefaults.OrderByDescending(s => s.IsActive);
					break;

				default:
					ContingencyDefaults = ContingencyDefaults.OrderBy(s => s.ContingencyDefaultId);
					break;
			}


			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(ContingencyDefaults.ToPagedList(pageNumber, pageSize));

		}

		// GET: ContingencyDefault/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			ContingencyDefault ContingencyDefault = db.ContingencyDefault.Where(x=> x.IsActive == true).Where(x=> x.ContingencyDefaultId == id).FirstOrDefault();
			if (ContingencyDefault == null)
			{
				return HttpNotFound();
			}
			return View(ContingencyDefault);
		}

		// GET: ContingencyDefault/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ContingencyDefault/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "ContingencyDefaultId,ContingencyDefaultInt,IsActive")] ContingencyDefault contingencyDefault)
		{
			contingencyDefault.IsActive = true;
			if (ModelState.IsValid)
			{
                contingencyDefault.CreatedBy = User.Identity.Name;
                contingencyDefault.CreatedDate = DateTime.Now;
                contingencyDefault.ModifiedBy = User.Identity.Name;
                contingencyDefault.ModifiedDate = DateTime.Now;
				db.ContingencyDefault.Add(contingencyDefault);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}

			return View(contingencyDefault);
		}

		// GET: ContingencyDefault/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            ContingencyDefault ContingencyDefault = db.ContingencyDefault.Where(x => x.IsActive == true).Where(x => x.ContingencyDefaultId == id).FirstOrDefault();
            if (ContingencyDefault == null)
			{
				return HttpNotFound();
			}
			return View(ContingencyDefault);
		}

		// POST: ContingencyDefault/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ContingencyDefaultId,ContingencyDefaultInt,IsActive,CreatedBy,CreatedDate")] ContingencyDefault contingencyDefault)
		{
			contingencyDefault.IsActive = true;

			if (ModelState.IsValid)
			{
                contingencyDefault.ModifiedDate = DateTime.Now;
                contingencyDefault.ModifiedBy = User.Identity.Name;
				db.Entry(contingencyDefault).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index");
			}

			return View(contingencyDefault);
		}

		// GET: ContingencyDefault/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            ContingencyDefault ContingencyDefault = db.ContingencyDefault.Where(x => x.IsActive == true).Where(x => x.ContingencyDefaultId == id).FirstOrDefault();
            if (ContingencyDefault == null)
			{
				return HttpNotFound();
			}
			return View(ContingencyDefault);
		}

		// POST: ContingencyDefault/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			ContingencyDefault ContingencyDefault = db.ContingencyDefault.Find(id);
			ContingencyDefault.IsActive = false;
			db.Entry(ContingencyDefault).State = EntityState.Modified;
			db.SaveChanges();
			TempData["RecordDeleted"] = " Record Has Been Deleted Successfully.";

			return RedirectToAction("Index");
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
