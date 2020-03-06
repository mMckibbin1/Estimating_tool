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
	public class EstimateStatusController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		// GET: EstimateStatus
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.EstimateStatusIdSortParm = sortOrder == "EstimateStatusId_desc" ? "EstimateStatusId" : "EstimateStatusId_desc";
            ViewBag.EstimateStatusStrSortParm = sortOrder == "EstimateStatusStr_desc" ? "EstimateStatusStr" : "EstimateStatusStr_desc";

			var estimateTypes = from s in db.EstimateStatus
								where s.IsActive == true
								select s;  //temp data stores

            //LINQ Query to provide a list of distinct ooptions for the dropdown search to be populated by
            var EstimateStatuseStrDistinct = db.EstimateStatus.Where(x => x.IsActive == true).Select(x => x.EstimateStatusStr).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in EstimateStatuseStrDistinct)//used to save data into the select list of search options
            {
                searchoptions.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.DropdownOptions = searchoptions;//viewbag used to send search dropdown data

            if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}
			ViewBag.CurrentFilter = searchString; //filtering method

			if (!String.IsNullOrEmpty(searchString))
			{
				estimateTypes = estimateTypes.Where(s => s.EstimateStatusStr.Contains(searchString));
			}//string search method

			switch (sortOrder) //Sort method
			{
				case "CommercialTypeId":
					estimateTypes = estimateTypes.OrderBy(s => s.EstimateStatusId);
					break;

				case "CommercialTypeId_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.EstimateStatusId);
					break;

				case "CommercialTypeStr":
					estimateTypes = estimateTypes.OrderBy(s => s.EstimateStatusStr);
					break;

				case "CommercialTypeStr_desc":
					estimateTypes = estimateTypes.OrderByDescending(s => s.EstimateStatusStr);
					break;
				default:
					estimateTypes = estimateTypes.OrderBy(s => s.EstimateStatusId);
					break;
			}

			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(estimateTypes.ToPagedList(pageNumber, pageSize));
			//Paging
		}

		// GET: EstimateStatus/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			EstimateStatus estimateStatus = db.EstimateStatus.Where(x=> x.IsActive == true).Where(x=> x.EstimateStatusId == id).FirstOrDefault();
			if (estimateStatus == null)
			{
				return HttpNotFound();
			}
			return View(estimateStatus);
		}

		// GET: EstimateStatus/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: EstimateStatus/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "EstimateStatusId,EstimateStatusStr,IsActive")] EstimateStatus estimateStatus)
		{
			estimateStatus.IsActive = true;

			if (ModelState.IsValid)
			{
                estimateStatus.CreatedBy = User.Identity.Name;
                estimateStatus.ModifiedBy = User.Identity.Name;
                estimateStatus.CreatedDate = DateTime.Now;
                estimateStatus.ModifiedDate = DateTime.Now;
				db.EstimateStatus.Add(estimateStatus);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(estimateStatus);
		}

		// GET: EstimateStatus/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            EstimateStatus estimateStatus = db.EstimateStatus.Where(x => x.IsActive == true).Where(x => x.EstimateStatusId == id).FirstOrDefault();
            if (estimateStatus == null)
			{
				return HttpNotFound();
			}
			return View(estimateStatus);
		}

		// POST: EstimateStatus/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "EstimateStatusId,EstimateStatusStr,IsActive,CreatedBy,CreatedDate")] EstimateStatus estimateStatus)
		{
			
			estimateStatus.IsActive = true;
			if (ModelState.IsValid)
			{
                estimateStatus.ModifiedDate = DateTime.Now;
                estimateStatus.ModifiedBy = User.Identity.Name;
				db.Entry(estimateStatus).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(estimateStatus);
		}

		// GET: EstimateStatus/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            EstimateStatus estimateStatus = db.EstimateStatus.Where(x => x.IsActive == true).Where(x => x.EstimateStatusId == id).FirstOrDefault();
            if (estimateStatus == null)
			{
				return HttpNotFound();
			}
			return View(estimateStatus);
		}

		// POST: EstimateStatus/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			EstimateStatus estimateStatus = db.EstimateStatus.Find(id);
			estimateStatus.IsActive = false;
			db.Entry(estimateStatus).State = EntityState.Modified;
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
