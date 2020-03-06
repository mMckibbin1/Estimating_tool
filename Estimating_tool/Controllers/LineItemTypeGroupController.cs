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
using Estimating_Tool.View_Model;

namespace Estimating_Tool.Controllers
{
	public class LineItemTypeGroupController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

        private List<SelectListItem> GetLineItemTypeList()
        {
            using (var context = new Estimatingcontext())
            {
                return (from p in context.LineItemType
                        where p.IsActive == true
                        orderby p.LineItemTypeId
                        select new SelectListItem { Text = p.LineItemTypeStr, Value = p.LineItemTypeId.ToString() }).Distinct().ToList();
                //query to get LineItems & order by Id but display by name
            }
        }


		// GET: LineItemTypeGroup
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
			//LINQ Query to make LineItemGroupID available to display name instead of ID (looked up)
			var lineItemTypeGroups = db.LineItemTypeGroup.Include(c => c.GetLineItemType).Where(c => c.IsActive == true);

            //LINQ Query to provide a list of distinct ooptions for the dropdown search to be populated by
            var LineItemTypeGroupstrDistinct = db.LineItemTypeGroup.Where(x => x.IsActive == true).Select(x => x.LineItemTypeGroupStr).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in LineItemTypeGroupstrDistinct)//used to save data into the select list of search options
            {
                if (item != null)
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
                lineItemTypeGroups = lineItemTypeGroups.Where(s => s.LineItemTypeGroupStr.Contains(currentFilter));
            }
			ViewBag.CurrentFilter = searchString; //checking search


			//Sorting
			ViewBag.LineItemTypeGroupIdSortParm = sortOrder == "LineItemTypeGroupId_desc" ? "LineItemTypeGroupId" : "LineItemTypeGroupId_desc";
			ViewBag.LineItemTypeSortParm = sortOrder == "LineItemType_desc" ? "LineItemType" : "LineItemType_desc";
			ViewBag.LineItemTypeGroupStrSortParm = sortOrder == "LineItemTypeGroupStr_desc" ? "LineItemTypeGroupStr" : "LineItemTypeGroupStr_desc";
			ViewBag.CreatedDateSortParm = sortOrder == "CreatedDate_desc" ? "CreatedDate" : "CreatedDate_desc";
			ViewBag.CreatedBySortParm = sortOrder == "CreatedBy_desc" ? "CreatedBy" : "CreatedBy_desc";
			ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate_desc" ? "ModifiedDate" : "ModifiedDate_desc";
			ViewBag.ModifiedBySortParm = sortOrder == "ModifiedBy_desc" ? "ModifiedBy" : "ModifiedBy_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller

			switch (sortOrder) //Sort method
			{
				case "LineItemTypeGroupId":
					lineItemTypeGroups = lineItemTypeGroups.OrderBy(s => s.LineItemTypeGroupId);
					break;

				case "LineItemTypeGroupId_desc":
					lineItemTypeGroups = lineItemTypeGroups.OrderByDescending(s => s.LineItemTypeGroupId);
					break;

				case "LineItemType":
					lineItemTypeGroups = lineItemTypeGroups.OrderBy(s => s.LineItemType);
					break;

				case "LineItemType_desc":
					lineItemTypeGroups = lineItemTypeGroups.OrderByDescending(s => s.LineItemType);
					break;

				case "LineItemTypeGroupStr":
					lineItemTypeGroups = lineItemTypeGroups.OrderBy(s => s.LineItemTypeGroupStr);
					break;

				case "LineItemTypeGroupStr_desc":
					lineItemTypeGroups = lineItemTypeGroups.OrderByDescending(s => s.LineItemTypeGroupStr);
					break;

				case "IsActive":
					lineItemTypeGroups = lineItemTypeGroups.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					lineItemTypeGroups = lineItemTypeGroups.OrderByDescending(s => s.IsActive);
					break;

				default:
					lineItemTypeGroups = lineItemTypeGroups.OrderBy(s => s.LineItemTypeGroupId);
					break;
			}

			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(lineItemTypeGroups.ToPagedList(pageNumber, pageSize));
		}

		// GET: LineItemTypeGroup/Details/5
		public ActionResult Details(int? id)
		{

			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var lineItemTypeGroup = db.LineItemTypeGroup.Include(c => c.GetLineItemType).Where(c => c.IsActive == true).Where(c => c.LineItemTypeGroupId == id).FirstOrDefault();
			if (lineItemTypeGroup == null)
			{
				return HttpNotFound();
			}
			return View(lineItemTypeGroup);
		}

		// GET: LineItemTypeGroup/Create
		public ActionResult Create()
		{
            LineItemTypeGroupVM vm = new LineItemTypeGroupVM();

            vm.LineItemTypeList = GetLineItemTypeList();
			return View(vm);
		}

		// POST: LineItemTypeGroup/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "LineItemTypeGroupId,LineItemType,LineItemTypeGroupStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] LineItemTypeGroup lineItemTypeGroup)
		{
			lineItemTypeGroup.CreatedDate = DateTime.Now;
			lineItemTypeGroup.ModifiedDate = DateTime.Now;
			lineItemTypeGroup.CreatedBy = User.Identity.Name;
			lineItemTypeGroup.ModifiedBy = User.Identity.Name;
			lineItemTypeGroup.LineItemTypeGroupId = lineItemTypeGroup.LineItemTypeGroupId;
			lineItemTypeGroup.LineItemTypeGroupStr = lineItemTypeGroup.LineItemTypeGroupStr;
			lineItemTypeGroup.LineItemType = lineItemTypeGroup.LineItemType;
			lineItemTypeGroup.IsActive = true;


			if (ModelState.IsValid)
			{
				db.LineItemTypeGroup.Add(lineItemTypeGroup);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}

            var VM = new LineItemTypeGroupVM();
            VM.LineItemTypeList = GetLineItemTypeList();

			return View(VM);
		}

		// GET: LineItemTypeGroup/Edit/5
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LineItemTypeGroup LITG = db.LineItemTypeGroup.AsNoTracking().Where(x => x.LineItemTypeGroupId == id).Where(x=> x.IsActive == true).FirstOrDefault();//getting record from database that was request by the user for edit

            if (LITG == null)
            {
                return HttpNotFound();
            }
        
            //populating selectlistItem uses to populate the line item type dropdown
            var LineItemStrings = db.LineItemType.Where(x => x.IsActive == true).Select(x=> new SelectListItem {Text = x.LineItemTypeStr, Value = x.LineItemTypeId.ToString() }).ToList().Distinct();
            //getting id of the line item so it will be selected on the drop down when the page loads
            var LineItemSelected = db.LineItemTypeGroup.Where(x => x.LineItemTypeGroupId == id).Select(x => x.LineItemType.ToString()).FirstOrDefault();
            //viewbags used to populate the view with the data needed by the dropdowns 
            ViewBag.DropDownOptions = LineItemStrings;
            ViewBag.DropDownSelected = LineItemSelected;
 
			LineItemTypeGroup lineItemTypeGroup = db.LineItemTypeGroup.Find(id);

			return View(LITG);
		}

		// POST: LineItemTypeGroup/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "LineItemTypeGroupId,LineItemType,LineItemTypeGroupStr,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] LineItemTypeGroup lineItemTypeGroup, int?id)
		{
            LineItemTypeGroup LITG = db.LineItemTypeGroup.AsNoTracking().Where(x => x.LineItemTypeGroupId == id).FirstOrDefault();

			if (ModelState.IsValid)
			{
                lineItemTypeGroup.LineItemTypeGroupId = Convert.ToInt32(id);
                lineItemTypeGroup.IsActive = true;
                lineItemTypeGroup.CreatedBy = User.Identity.Name;
                lineItemTypeGroup.ModifiedBy = User.Identity.Name;
                lineItemTypeGroup.ModifiedDate = DateTime.Now;
                lineItemTypeGroup.CreatedDate = LITG.CreatedDate;
				db.Entry(lineItemTypeGroup).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index");
			}
			return View(lineItemTypeGroup);
		}

		// GET: LineItemTypeGroup/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var lineItemTypeGroup = db.LineItemTypeGroup.Include(c => c.GetLineItemType).Where(c => c.IsActive == true).Where(c => c.LineItemTypeGroupId == id).FirstOrDefault();
			if (lineItemTypeGroup == null)
			{
				return HttpNotFound();
			}
			return View(lineItemTypeGroup);
		}

		// POST: LineItemTypeGroup/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			LineItemTypeGroup lineItemTypeGroup = db.LineItemTypeGroup.Find(id);
			lineItemTypeGroup.IsActive = false;
			lineItemTypeGroup.ModifiedBy = User.Identity.Name;
			db.Entry(lineItemTypeGroup).State = EntityState.Modified;
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
