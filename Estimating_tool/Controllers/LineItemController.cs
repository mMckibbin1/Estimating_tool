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
using Estimating_Tool.View_Model;
using PagedList;

namespace Estimating_Tool.Controllers
{
	public class LineItemController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();
        /// <summary>
        /// populates selectlist to provied data to the lineItemTypeGroup dropdown on view.
        /// </summary>
        /// <returns>returns Text as the string for lineitemtypegroup and the vaule of its id</returns>
		private List<SelectListItem> GetLineItemTypeGroupId()
		{
			using (var context = new Estimatingcontext())
			{
				return (from p in context.LineItemTypeGroup
						where p.IsActive == true
						orderby p.LineItemTypeGroupId
						select new SelectListItem { Text = p.LineItemTypeGroupStr, Value = p.LineItemTypeGroupId.ToString() }).Distinct().ToList();
			}
		}
        /// <summary>
        /// populates selectlist to provied data to the LineItemType dropdown on view.
        /// </summary>
        /// <returns>returns Text as the string for LineItemTypeStr and the vaule of its id</returns>
		private List<SelectListItem> GetLineItemTypeId()
		{
			using (var context = new Estimatingcontext())
			{
				return (from p in context.LineItemType
						where p.IsActive == true
						orderby p.LineItemTypeId
						select new SelectListItem { Text = p.LineItemTypeStr, Value = p.LineItemTypeId.ToString() }).Distinct().ToList();
			}
		}

		private List<SelectListItem> GetLineItemId()
		{
			using (var context = new Estimatingcontext())
			{
				return (from p in context.LineItem
						where p.IsActive == true
						orderby p.LineItemId
						select new SelectListItem { Text = p.LineItemStr, Value = p.LineItemId.ToString() }).Distinct().ToList();
			}
		}

		// GET: LineItem
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
			//LINQ Query to make LineItemGroupID available to display name instead of ID (looked up)
			var lineItems = db.LineItem.Include(c => c.LineItemTypeGroup).Where(c => c.IsActive == true);
            var lineItemsstrDistinct = db.LineItem.Include(c => c.LineItemTypeGroup).Where(c => c.IsActive == true).Select(x=> x.LineItemStr).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in lineItemsstrDistinct)//used to save data into the select list of search options
            {
                searchoptions.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString()});
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
                lineItems = lineItems.Where(s => s.LineItemStr.Contains(currentFilter));
            }
			ViewBag.CurrentFilter = searchString; //checking search


			//Sorting
			ViewBag.LineItemIdSortParm = sortOrder == "LineItemId_desc" ? "LineItemId" : "LineItemId_desc";
			ViewBag.LineItemGroupIdSortParm = sortOrder == "LineItemGroupId_desc" ? "LineItemGroupId" : "LineItemGroupId_desc";
			ViewBag.LineItemStrSortParm = sortOrder == "LineItemStr_desc" ? "LineItemStr" : "LineItemStr_desc";
			ViewBag.CreatedDateSortParm = sortOrder == "CreatedDate_desc" ? "CreatedDate" : "CreatedDate_desc";
			ViewBag.CreatedBySortParm = sortOrder == "CreatedBy_desc" ? "CreatedBy" : "CreatedBy_desc";
			ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate_desc" ? "ModifiedDate" : "ModifiedDate_desc";
			ViewBag.ModifiedBySortParm = sortOrder == "ModifiedBy_desc" ? "ModifiedBy" : "ModifiedBy_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller

			switch (sortOrder) //Sort method
			{
				case "LineItemId":
					lineItems = lineItems.OrderBy(s => s.LineItemId);
					break;

				case "LineItemId_desc":
					lineItems = lineItems.OrderByDescending(s => s.LineItemId);
					break;

				case "LineItemGroupId":
					lineItems = lineItems.OrderBy(s => s.LineItemGroupId);
					break;

				case "LineItemGroupId_desc":
					lineItems = lineItems.OrderByDescending(s => s.LineItemGroupId);
					break;

				case "LineItemStr":
					lineItems = lineItems.OrderBy(s => s.LineItemStr);
					break;

				case "LineItemStr_desc":
					lineItems = lineItems.OrderByDescending(s => s.LineItemStr);
					break;

				case "IsActive":
					lineItems = lineItems.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					lineItems = lineItems.OrderByDescending(s => s.IsActive);
					break;

				default:
					lineItems = lineItems.OrderBy(s => s.LineItemId);
					break;
			}


			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(lineItems.ToPagedList(pageNumber, pageSize));
			//Paging
		}

		// GET: LineItem/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			LineItem lineItem = db.LineItem.Include(c => c.LineItemTypeGroup).Where(c => c.LineItemId == id).Where(c => c.IsActive == true).FirstOrDefault();
			if (lineItem == null)
			{
				return HttpNotFound();
			}
			return View(lineItem);
		}

		// GET: LineItem/Create
		public ActionResult Create()
		{
			var VM = new LineItemVM();
			VM.LineItemGroupIdList = GetLineItemTypeGroupId();
			VM.LineItemTypeId = GetLineItemTypeId();
			VM.LineItemIds = GetLineItemId();


			return View(VM);
		}

		// POST: LineItem/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "LineItemId,LineItemGroupId,LineItemStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] LineItem lineItem)
		{
			//adding addtion data required in model but provieded in view model
			lineItem.CreatedDate = DateTime.Now;
			lineItem.ModifiedDate = DateTime.Now;
			lineItem.CreatedBy = User.Identity.Name;
			lineItem.ModifiedBy = User.Identity.Name;
			lineItem.IsActive = true;

			var VM = new LineItemVM();

            //if (db.LineItem.Any(c => c.LineItemStr == lineItem.LineItemStr))
            //{
            //    ModelState.AddModelError("LineItemStr", "Line Item Type Name must be unique");
            //    return View(VM);
            //}

            if (ModelState.IsValid)
			{
				db.LineItem.Add(lineItem);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}



			return View(VM);
		}

		// GET: LineItem/Edit/5
		public ActionResult Edit(int? id)
		{
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LineItem Li = db.LineItem.AsNoTracking().Where(x => x.LineItemId == id).Where(x=> x.IsActive==true).FirstOrDefault();
            if (Li == null)
            {
                return HttpNotFound();
            }
            var VM = new LineItemVM();
            VM.LineItemGroupId = Li.LineItemGroupId;
            VM.LineItemStr = Li.LineItemStr;
            VM.IsActive = Li.IsActive;
            VM.LineItemId = Li.LineItemId;
			return View(VM);
		}

		// POST: LineItem/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "LineItemId,LineItemGroupId,LineItemStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] LineItem lineItem, int?id)
		{
            LineItem Li = db.LineItem.AsNoTracking().Where(x => x.LineItemId == id).Where(x=> x.IsActive == true).FirstOrDefault();
            lineItem.LineItemTypeGroup =Li.LineItemTypeGroup;
            lineItem.CreatedDate = Li.CreatedDate;
            lineItem.CreatedBy = Li.CreatedBy;
            lineItem.ModifiedDate = DateTime.Now;
            lineItem.ModifiedBy = User.Identity.Name;
			lineItem.IsActive = Li.IsActive;

            var VM = new LineItemVM();

			if (ModelState.IsValid)
			{
                lineItem.LineItemId = Convert.ToInt32(id);
				lineItem.ModifiedBy = User.Identity.Name;
				db.Entry(lineItem).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index");
			}

            VM.LineItemGroupId = lineItem.LineItemGroupId;
            VM.LineItemStr = lineItem.LineItemStr;
            VM.IsActive = lineItem.IsActive;
            VM.LineItemId = lineItem.LineItemId;
            return View(VM);
		}

		// GET: LineItem/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			LineItem lineItem = db.LineItem.Include(c => c.LineItemTypeGroup).Where(c => c.LineItemId == id).Where(c => c.IsActive == true).FirstOrDefault();
			if (lineItem == null)
			{
				return HttpNotFound();
			}
			return View(lineItem);
		}

		// POST: LineItem/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			LineItem lineItem = db.LineItem.Find(id);
			lineItem.IsActive = false;
			lineItem.ModifiedBy = User.Identity.Name; 
			db.Entry(lineItem).State = EntityState.Modified;
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

		//public JsonResult updateLineItemGroupList(string LineItemTypeId)//used to update project dropdown on estimate header create page for customer selected
		//{
		//	var updatedList = db.LineItem.Include(x => x.LineItemTypeGroup).Where(x => x.IsActive == true).Where(x => x.LineItemTypeGroup.GetLineItemType.LineItemTypeId.ToString() == LineItemTypeId).OrderBy(x => x.LineItemTypeGroup.LineItemTypeGroupStr)
		//		.Select(x => new { Text = x.LineItemTypeGroup.LineItemTypeGroupStr, Value = x.LineItemTypeGroup.LineItemTypeGroupId.ToString() }).ToList().Distinct();
		//	return Json(updatedList, JsonRequestBehavior.AllowGet);

		//}

		//public JsonResult updateLineItemList(string LineItemGroupId)//used to update project dropdown on estimate header create page for customer selected
		//{
		//	var updatedList = db.LineItem.Where(x => x.IsActive == true).Where(x => x.LineItemTypeGroup.LineItemTypeGroupId.ToString() == LineItemGroupId).OrderBy(x => x.LineItemStr)
		//		.Select(x => new { Text = x.LineItemStr, Value = x.LineItemId.ToString() }).ToList();
		//	return Json(updatedList, JsonRequestBehavior.AllowGet);

		//}




	}
}
