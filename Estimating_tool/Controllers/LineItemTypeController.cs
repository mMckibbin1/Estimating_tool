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
	public class LineItemTypeController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		// GET: LineItemType
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
			//Variables
			var lineItemTypes = from s in db.LineItemType
								where s.IsActive == true
								select s; //temp data stores

            //LINQ Query to provide a list of distinct ooptions for the dropdown search to be populated by
            var LineItemTypeStrsDistinct = db.LineItemType.Where(x => x.IsActive == true).Select(x => x.LineItemTypeStr).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in LineItemTypeStrsDistinct)//used to save data into the select list of search options
            {
                if(item != null)
                {
                    searchoptions.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
                }
               
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
                lineItemTypes = lineItemTypes.Where(s => s.LineItemTypeStr.Contains(currentFilter));
            }
			ViewBag.CurrentFilter = searchString; //checking search


			//Sorting
			ViewBag.LineItemIdSortParm = sortOrder == "LineItemTypeId_desc" ? "LineItemTypeId" : "LineItemTypeId_desc";
			ViewBag.LineItemStrSortParm = sortOrder == "LineItemTypeStr_desc" ? "LineItemTypeStr" : "LineItemTypeStr_desc";
			ViewBag.CreatedDateSortParm = sortOrder == "CreatedDate_desc" ? "CreatedDate" : "CreatedDate_desc";
			ViewBag.CreatedBySortParm = sortOrder == "CreatedBy_desc" ? "CreatedBy" : "CreatedBy_desc";
			ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate_desc" ? "ModifiedDate" : "ModifiedDate_desc";
			ViewBag.ModifiedBySortParm = sortOrder == "ModifiedBy_desc" ? "ModifiedBy" : "ModifiedBy_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller

			switch (sortOrder) //Sort method
			{
				case "LineItemTypeId":
					lineItemTypes = lineItemTypes.OrderBy(s => s.LineItemTypeId);
					break;

				case "LineItemTypeId_desc":
					lineItemTypes = lineItemTypes.OrderByDescending(s => s.LineItemTypeId);
					break;

				case "LineItemTypeStr":
					lineItemTypes = lineItemTypes.OrderBy(s => s.LineItemTypeStr);
					break;

				case "LineItemStr_desc":
					lineItemTypes = lineItemTypes.OrderByDescending(s => s.LineItemTypeStr);
					break;

				case "CreatedDate":
					lineItemTypes = lineItemTypes.OrderBy(s => s.CreatedDate);
					break;

				case "CreatedDate_desc":
					lineItemTypes = lineItemTypes.OrderByDescending(s => s.CreatedDate);
					break;

				case "CreatedBy":
					lineItemTypes = lineItemTypes.OrderBy(s => s.CreatedBy);
					break;

				case "CreatedBy_desc":
					lineItemTypes = lineItemTypes.OrderByDescending(s => s.CreatedBy);
					break;

				case "ModifiedDate":
					lineItemTypes = lineItemTypes.OrderBy(s => s.ModifiedDate);
					break;

				case "ModifiedDate_desc":
					lineItemTypes = lineItemTypes.OrderByDescending(s => s.ModifiedDate);
					break;

				case "ModifiedBy":
					lineItemTypes = lineItemTypes.OrderBy(s => s.ModifiedBy);
					break;

				case "ModifiedBy_desc":
					lineItemTypes = lineItemTypes.OrderByDescending(s => s.ModifiedBy);
					break;

				case "IsActive":
					lineItemTypes = lineItemTypes.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					lineItemTypes = lineItemTypes.OrderByDescending(s => s.IsActive);
					break;

				default:
					lineItemTypes = lineItemTypes.OrderBy(s => s.LineItemTypeId);
					break;
			}

			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(lineItemTypes.ToPagedList(pageNumber, pageSize));
		}

		// GET: LineItemType/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            LineItemType lineItemType = db.LineItemType.Where(x => x.IsActive == true).Where(x => x.LineItemTypeId == id).FirstOrDefault();
            if (lineItemType == null)
			{
				return HttpNotFound();
			}
			return View(lineItemType);
		}

		// GET: LineItemType/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: LineItemType/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "LineItemTypeId,LineItemTypeStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] LineItemType lineItemType)
		{
			lineItemType.CreatedDate = DateTime.Now;
			lineItemType.ModifiedDate = DateTime.Now;
			lineItemType.CreatedBy = User.Identity.Name;
			lineItemType.ModifiedBy = User.Identity.Name;
			lineItemType.LineItemTypeStr = lineItemType.LineItemTypeStr;
			lineItemType.IsActive = true;

			if (ModelState.IsValid)
			{
				db.LineItemType.Add(lineItemType);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}

			return View(lineItemType);
		}

		// GET: LineItemType/Edit/5
		public ActionResult Edit(int? id)
		{
			LineItemType Li = db.LineItemType.Where(x => x.LineItemTypeId == id).Where(x=> x.IsActive == true).FirstOrDefault();
			
			if (id == null || Li == null )
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			LineItemType lineItemType = db.LineItemType.Find(id);
			lineItemType.LineItemTypeStr= Li.LineItemTypeStr;


			if (lineItemType == null)
			{
				return HttpNotFound();
			}
			return View(lineItemType);
		}

		// POST: LineItemType/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "LineItemTypeId,LineItemTypeStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] LineItemType lineItemType)
		{
			lineItemType.CreatedDate = DateTime.Now;
			lineItemType.ModifiedDate = DateTime.Now;
			lineItemType.CreatedBy = lineItemType.CreatedBy;
			lineItemType.ModifiedBy = User.Identity.Name;
			lineItemType.LineItemTypeStr = lineItemType.LineItemTypeStr;
			lineItemType.IsActive = true;

			if (ModelState.IsValid)
			{
				db.Entry(lineItemType).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index", "LineItemType");
			}
			return View(lineItemType);
		}

		// GET: LineItemType/Delete/5
		public ActionResult Delete(int? id)
		{
            LineItemType lineItemType = db.LineItemType.Where(x => x.IsActive == true).Where(x => x.LineItemTypeId == id).FirstOrDefault();
            if (id == null || lineItemType == null )
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (lineItemType == null)
			{
				return HttpNotFound();
			}
			return View(lineItemType);
		}

		// POST: LineItemType/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			LineItemType lineItemType = db.LineItemType.Find(id);
			lineItemType.IsActive = false;
			lineItemType.ModifiedBy = User.Identity.Name;
			db.Entry(lineItemType).State = EntityState.Modified;
			db.SaveChanges();
			TempData["RecordDeleted"] = " Record Has Been Deleted Successfully.";
			return RedirectToAction("Index", "LineItemType");
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
