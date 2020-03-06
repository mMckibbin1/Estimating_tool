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
	public class CommercialTypeController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		// GET: CommercialType
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
			//Variables          
			var commercialTypes = from s in db.CommercialType
								  where s.IsActive == true
								  select s; //temp data stores

            //LINQ Query to provide a list of distinct ooptions for the dropdown search to be populated by
            var CommercialTypesstrDistinct = db.CommercialType.Where(x => x.IsActive == true).Select(x => x.CommercialTypeStr).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in CommercialTypesstrDistinct)//used to save data into the select list of search options
            {
                searchoptions.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.DropdownOptions = searchoptions;//viewbag used to send search dropdown data


            //Searching
            //string search 
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
                commercialTypes = commercialTypes.Where(s => s.CommercialTypeStr.Contains(currentFilter));
            }
			ViewBag.CurrentFilter = searchString; //checking search 


			//Sorting
			ViewBag.CommercialTypeIdSortParm = sortOrder == "CommercialTypeId_desc" ? "CommercialTypeId" : "CommercialTypeId_desc";
			ViewBag.CommercialTypeStrSortParm = sortOrder == "CommercialTypeStr_desc" ? "CommercialTypeStr" : "CommercialTypeStr_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller

			switch (sortOrder) //Sort method
			{
				case "CommercialTypeId":
					commercialTypes = commercialTypes.OrderBy(s => s.CommercialTypeId);
					break;

				case "CommercialTypeId_desc":
					commercialTypes = commercialTypes.OrderByDescending(s => s.CommercialTypeId);
					break;

				case "CommercialTypeStr":
					commercialTypes = commercialTypes.OrderBy(s => s.CommercialTypeStr);
					break;

				case "CommercialTypeStr_desc":
					commercialTypes = commercialTypes.OrderByDescending(s => s.CommercialTypeStr);
					break;

				case "IsActive":
					commercialTypes = commercialTypes.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					commercialTypes = commercialTypes.OrderByDescending(s => s.IsActive);
					break;

				default:
					commercialTypes = commercialTypes.OrderBy(s => s.CommercialTypeId);
					break;
			}

			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(commercialTypes.ToPagedList(pageNumber, pageSize));


		}

		// GET: CommercialType/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			CommercialType commercialType = db.CommercialType.Where(x=> x.IsActive == true).Where(x=> x.CommercialTypeId == id).FirstOrDefault();

			if (commercialType == null)
			{
				return HttpNotFound();
			}
			return View(commercialType);
		}

		// GET: CommercialType/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CommercialType/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CommercialTypeId,CommercialTypeStr,IsActive")] CommercialType commercialType)
		{

			commercialType.IsActive = true;
			if (ModelState.IsValid)
			{
                commercialType.CreatedBy = User.Identity.Name;
                commercialType.ModifiedBy = User.Identity.Name;
                commercialType.CreatedDate = DateTime.Now;
                commercialType.ModifiedDate = DateTime.Now;
				db.CommercialType.Add(commercialType);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}

			return View(commercialType);
		}

		// GET: CommercialType/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            CommercialType commercialType = db.CommercialType.Where(x => x.IsActive == true).Where(x => x.CommercialTypeId == id).FirstOrDefault();
            if (commercialType == null)
			{
				return HttpNotFound();
			}
			return View(commercialType);
		}

		// POST: CommercialType/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CommercialTypeId,CommercialTypeStr,IsActive,CreatedBy,CreatedDate")] CommercialType commercialType)
		{

			commercialType.IsActive = true;
			if (ModelState.IsValid)
			{

                commercialType.ModifiedBy = User.Identity.Name;
                commercialType.ModifiedDate = DateTime.Now;
				db.Entry(commercialType).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index");
			}
			return View(commercialType);
		}

		// GET: CommercialType/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            CommercialType commercialType = db.CommercialType.Where(x => x.IsActive == true).Where(x => x.CommercialTypeId == id).FirstOrDefault();
            if (commercialType == null)
			{
				return HttpNotFound();
			}
			return View(commercialType);
		}

		// POST: CommercialType/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			CommercialType commercialType = db.CommercialType.Find(id);
			commercialType.IsActive = false;
			db.Entry(commercialType).State = EntityState.Modified;
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
