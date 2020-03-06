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
	public class EstimateTypeController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		// GET: EstimateType
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
			//Variables
			var estimateTypes = from s in db.EstimateType
								where s.IsActive == true
								select s; //temp data stores

            //LINQ Query to provide a list of distinct ooptions for the dropdown search to be populated by
            var EstimateTypesDistinct = db.EstimateType.Where(x => x.IsActive == true).Select(x => x.EstimateTypeStr).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in EstimateTypesDistinct)//used to save data into the select list of search options
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
                estimateTypes = estimateTypes.Where(s => s.EstimateTypeStr.Contains(currentFilter));
            }
			ViewBag.CurrentFilter = searchString; //checking search


			//Sorting
			ViewBag.EstimateTypeIdSortParm = sortOrder == "EstimateTypeId_desc" ? "EstimateTypeId" : "EstimateTypeId_desc";
			ViewBag.EstimateTypeStrSortParm = sortOrder == "EstimateTypeStr_desc" ? "EstimateTypeStr" : "EstimateTypeStr_desc";
			ViewBag.CreatedDateSortParm = sortOrder == "CreatedDate_desc" ? "CreatedDate" : "CreatedDate_desc";
			ViewBag.CreatedBySortParm = sortOrder == "CreatedBy_desc" ? "CreatedBy" : "CreatedBy_desc";
			ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate_desc" ? "ModifiedDate" : "ModifiedDate_desc";
			ViewBag.ModifiedBySortParm = sortOrder == "ModifiedBy_desc" ? "ModifiedBy" : "ModifiedBy_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller


			switch (sortOrder) //Sort method
			{
				case "EstimateTypeId":
					estimateTypes = estimateTypes.OrderBy(s => s.EstimateTypeId);
					break;

				case "CommercialTypeId_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.EstimateTypeId);
					break;

				case "CommercialTypeStr":
					estimateTypes = estimateTypes.OrderBy(s => s.EstimateTypeStr);
					break;

				case "CommercialTypeStr_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.EstimateTypeStr);
					break;

				case "CreatedDate":
					estimateTypes = estimateTypes.OrderBy(s => s.CreatedDate);
					break;

				case "CreatedDate_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.CreatedDate);
					break;

				case "CreatedBy":
					estimateTypes = estimateTypes.OrderBy(s => s.CreatedBy);
					break;

				case "CreatedBy_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.CreatedBy);
					break;

				case "ModifiedDate":
					estimateTypes = estimateTypes.OrderBy(s => s.ModifiedDate);
					break;

				case "ModifiedDate_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.ModifiedDate);
					break;

				case "ModifiedBy":
					estimateTypes = estimateTypes.OrderBy(s => s.ModifiedBy);
					break;

				case "ModifiedBy_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.ModifiedBy);
					break;

				case "IsActive":
					estimateTypes = estimateTypes.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.IsActive);
					break;

				default:
					estimateTypes = estimateTypes.OrderBy(s => s.EstimateTypeId);
					break;
			}


			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(estimateTypes.ToPagedList(pageNumber, pageSize));

		}

		// GET: EstimateType/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			EstimateType estimateType = db.EstimateType.Where(x=> x.IsActive == true).Where(x=> x.EstimateTypeId == id).FirstOrDefault();
			if (estimateType == null)
			{
				return HttpNotFound();
			}
			return View(estimateType);
		}

		// GET: EstimateType/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: EstimateType/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "EstimateTypeId,EstimateTypeStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] EstimateType estimateType)
		{
			estimateType.CreatedDate = DateTime.Now;
			estimateType.ModifiedDate = DateTime.Now;
			estimateType.CreatedBy = User.Identity.Name;
			estimateType.ModifiedBy = User.Identity.Name;
			estimateType.EstimateTypeId = estimateType.EstimateTypeId;
			estimateType.EstimateTypeStr = estimateType.EstimateTypeStr;
			estimateType.IsActive = true;

			if (ModelState.IsValid)
			{
				db.EstimateType.Add(estimateType);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(estimateType);
		}

		// GET: EstimateType/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            EstimateType estimateType = db.EstimateType.Where(x => x.IsActive == true).Where(x => x.EstimateTypeId == id).FirstOrDefault();
            if (estimateType == null)
			{
				return HttpNotFound();
			}
			return View(estimateType);
		}

		// POST: EstimateType/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "EstimateTypeId,EstimateTypeStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] EstimateType estimateType, int? id)
		{
            var EstimateType = db.EstimateType.AsNoTracking().Where(x => x.EstimateTypeId == id).FirstOrDefault();

            estimateType.CreatedDate = EstimateType.CreatedDate;
			estimateType.ModifiedDate = DateTime.Now;
            estimateType.CreatedBy = EstimateType.CreatedBy;
			estimateType.ModifiedBy = User.Identity.Name;
            estimateType.IsActive = true;

			if (ModelState.IsValid)
			{
				db.Entry(estimateType).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(estimateType);
		}

		// GET: EstimateType/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            EstimateType estimateType = db.EstimateType.Where(x => x.IsActive == true).Where(x => x.EstimateTypeId == id).FirstOrDefault();
            if (estimateType == null)
			{
				return HttpNotFound();
			}
			return View(estimateType);
		}

		// POST: EstimateType/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			EstimateType estimateType = db.EstimateType.Find(id);
			estimateType.IsActive = false;
			estimateType.ModifiedBy = User.Identity.Name;

			db.Entry(estimateType).State = EntityState.Modified;
			db.SaveChanges();
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
