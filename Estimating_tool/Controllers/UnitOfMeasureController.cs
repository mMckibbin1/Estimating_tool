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
	public class UnitOfMeasureController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		// GET: UnitOfMeasure
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
			//Variables
			var unitOfMeasures = from s in db.UnitOfMeasure
								 where s.IsActive == true
								 select s; //temp data stores

            //LINQ Query to provide a list of distinct ooptions for the dropdown search to be populated by
            var UnitOFMesuresStrsDistinct = db.UnitOfMeasure.Where(x => x.IsActive == true).Select(x => x.UnitOfMeasureStr).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in UnitOFMesuresStrsDistinct)//used to save data into the select list of search options
            {
                searchoptions.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.DropdownOptions = searchoptions;//viewbag used to send search dropdown data

            //Searching
            //string search method
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
                unitOfMeasures = unitOfMeasures.Where(s => s.UnitOfMeasureStr.Contains(currentFilter));
            }
			ViewBag.CurrentFilter = searchString; //checking search


			//Sorting
			ViewBag.UnitOfMeasureIdSortParm = sortOrder == "UnitOfMeasureId_desc" ? "UnitOdMeasureId" : "UnitOfMeasureId_desc";
			ViewBag.UnitOfMeasureStrSortParm = sortOrder == "UnitOfMeasureStr_desc" ? "UnitOfMeasureStr" : "UnitOfMeasureStr_desc";
			ViewBag.CreatedDateSortParm = sortOrder == "CreatedDate_desc" ? "CreatedDate" : "CreatedDate_desc";
			ViewBag.CreatedBySortParm = sortOrder == "CreatedBy_desc" ? "CreatedBy" : "CreatedBy_desc";
			ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate_desc" ? "ModifiedDate" : "ModifiedDate_desc";
			ViewBag.ModifiedBySortParm = sortOrder == "ModifiedBy_desc" ? "ModifiedBy" : "ModifiedBy_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller

			switch (sortOrder) //Sort method
			{
				case "UnitOfMeasureId":
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.UnitOfMeasureId);
					break;

				case "UnitOfMeasureId_desc":
					unitOfMeasures = unitOfMeasures.OrderByDescending(s => s.UnitOfMeasureId);
					break;

				case "UnitOfMeasureStr":
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.UnitOfMeasureStr);
					break;

				case "UnitOfMeasureStr_desc":
					unitOfMeasures = unitOfMeasures.OrderByDescending(s => s.UnitOfMeasureStr);
					break;

				case "CreatedDate":
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.CreatedDate);
					break;

				case "CreatedDate_desc":
					unitOfMeasures = unitOfMeasures.OrderByDescending(s => s.CreatedDate);
					break;

				case "CreatedBy":
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.CreatedBy);
					break;

				case "CreatedBy_desc":
					unitOfMeasures = unitOfMeasures.OrderByDescending(s => s.CreatedBy);
					break;

				case "ModifiedDate":
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.ModifiedDate);
					break;

				case "ModifiedDate_desc":
					unitOfMeasures = unitOfMeasures.OrderByDescending(s => s.ModifiedDate);
					break;

				case "ModifiedBy":
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.ModifiedBy);
					break;

				case "ModifiedBy_desc":
					unitOfMeasures = unitOfMeasures.OrderByDescending(s => s.ModifiedBy);
					break;

				case "IsActive":
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					unitOfMeasures = unitOfMeasures.OrderByDescending(s => s.IsActive);
					break;

				default:
					unitOfMeasures = unitOfMeasures.OrderBy(s => s.UnitOfMeasureId);
					break;
			}

			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(unitOfMeasures.ToPagedList(pageNumber, pageSize));

		}

		// GET: UnitOfMeasure/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			UnitOfMeasure unitOfMeasure = db.UnitOfMeasure.Where(x=> x.IsActive == true).Where(x=> x.UnitOfMeasureId == id).FirstOrDefault();
			if (unitOfMeasure == null)
			{
				return HttpNotFound();
			}

			return View(unitOfMeasure);
		}

		// GET: UnitOfMeasure/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UnitOfMeasure/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "UnitOfMeasureId,UnitOfMeasureStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] UnitOfMeasure unitOfMeasure)
		{
			unitOfMeasure.CreatedDate = DateTime.Now;
			unitOfMeasure.ModifiedDate = DateTime.Now;
			unitOfMeasure.CreatedBy = User.Identity.Name;
			unitOfMeasure.ModifiedBy = User.Identity.Name;
			unitOfMeasure.UnitOfMeasureId = unitOfMeasure.UnitOfMeasureId;
			unitOfMeasure.UnitOfMeasureStr = unitOfMeasure.UnitOfMeasureStr;
			unitOfMeasure.IsActive = true;
			if (ModelState.IsValid)
			{
				db.UnitOfMeasure.Add(unitOfMeasure);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}

			return View(unitOfMeasure);
		}

		// GET: UnitOfMeasure/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            UnitOfMeasure unitOfMeasure = db.UnitOfMeasure.Where(x => x.IsActive == true).Where(x => x.UnitOfMeasureId == id).FirstOrDefault();
            if (unitOfMeasure == null)
			{
				return HttpNotFound();
			}
			return View(unitOfMeasure);
		}

		// POST: UnitOfMeasure/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "UnitOfMeasureId,UnitOfMeasureStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] UnitOfMeasure unitOfMeasure)
		{
			unitOfMeasure.CreatedDate = DateTime.Now;
			unitOfMeasure.ModifiedDate = DateTime.Now;
			unitOfMeasure.CreatedBy = unitOfMeasure.CreatedBy;
			unitOfMeasure.ModifiedBy = User.Identity.Name;
			unitOfMeasure.UnitOfMeasureId = unitOfMeasure.UnitOfMeasureId;
			unitOfMeasure.UnitOfMeasureStr = unitOfMeasure.UnitOfMeasureStr;
			unitOfMeasure.IsActive = true;
			if (ModelState.IsValid)
			{
				db.Entry(unitOfMeasure).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index", "UnitOfMeasure");
			}
			return View(unitOfMeasure);
		}

		// GET: UnitOfMeasure/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            UnitOfMeasure unitOfMeasure = db.UnitOfMeasure.Where(x => x.IsActive == true).Where(x => x.UnitOfMeasureId == id).FirstOrDefault();
            if (unitOfMeasure == null)
			{
				return HttpNotFound();
			}

			return View(unitOfMeasure);
		}

		// POST: UnitOfMeasure/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			UnitOfMeasure unitOfMeasure = db.UnitOfMeasure.Find(id);
			unitOfMeasure.IsActive = false;
			unitOfMeasure.ModifiedBy = User.Identity.Name;
			db.Entry(unitOfMeasure).State = EntityState.Modified;
			db.SaveChanges();
			TempData["RecordDeleted"] = " Record Has Been Deleted Successfully.";
			return RedirectToAction("Index", "UnitOfMeasure");
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
