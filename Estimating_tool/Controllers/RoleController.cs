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
	public class RoleController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

		// GET: Role
		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
			//Variables
			var roles = from s in db.Role
						where s.IsActive == true
						select s; //temp data stores

            //LINQ Query to provide a list of distinct options for the dropdown search to be populated by
            var RolesStrDistinct = db.Role.Where(x => x.IsActive == true).Select(x => x.RoleName).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in RolesStrDistinct)//used to save data into the select list of search options
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
                roles = roles.Where(s => s.RoleName.Contains(currentFilter));
            }
			ViewBag.CurrentFilter = searchString; //checking search


			//Sorting
			ViewBag.RoleIdSortParm = sortOrder == "RoleId_desc" ? "RoleId" : "RoleId_desc";
			ViewBag.RoleNameSortParm = sortOrder == "RoleName_desc" ? "RoleName" : "RoleName_desc";
			ViewBag.IsActiveSortParm = sortOrder == "IsActive_desc" ? "IsActive" : "IsActive_desc"; //runs sort methods in controller


			switch (sortOrder) //Sort method
			{
				case "RoleId":
					roles = roles.OrderBy(s => s.Id);
					break;

				case "RoleId_desc":
					roles = roles.OrderByDescending(s => s.Id);
					break;

				case "RoleName":
					roles = roles.OrderBy(s => s.RoleName);
					break;

				case "RoleName_desc":
					roles = roles.OrderByDescending(s => s.RoleName);
					break;

				case "IsActive":
					roles = roles.OrderBy(s => s.IsActive);
					break;

				case "IsActive_desc":
					roles = roles.OrderByDescending(s => s.IsActive);
					break;

				default:
					roles = roles.OrderBy(s => s.Id);
					break;
			}


			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(roles.ToPagedList(pageNumber, pageSize));

		}

		// GET: Role/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Role role = db.Role.Where(x=> x.IsActive == true).Where(x=> x.Id == id).FirstOrDefault();
			if (role == null)
			{
				return HttpNotFound();
			}
			return View(role);
		}

		// GET: Role/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Role/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,RoleName,IsActive")] Role role)
		{
			role.IsActive = true;
			if (ModelState.IsValid)
			{
                role.CreatedBy = User.Identity.Name;
                role.ModifiedBy = User.Identity.Name;
                role.CreatedDate = DateTime.Now;
                role.ModifiedDate = DateTime.Now;
				db.Role.Add(role);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}

			return View(role);
		}

		// GET: Role/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Role role = db.Role.Where(x => x.IsActive == true).Where(x => x.Id == id).FirstOrDefault();
            if (role == null)
			{
				return HttpNotFound();
			}
			return View(role);
		}

		// POST: Role/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,RoleName,IsActive,CreatedBy,CreatedDate")] Role role)
		{
			role.IsActive = true;
			if (ModelState.IsValid)
			{
                role.ModifiedDate = DateTime.Now;
                role.ModifiedBy = User.Identity.Name;
				db.Entry(role).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index");
			}
			return View(role);
		}

		// GET: Role/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Role role = db.Role.Where(x => x.IsActive == true).Where(x => x.Id == id).FirstOrDefault();
            if (role == null)
			{
				return HttpNotFound();
			}
			return View(role);
		}

		// POST: Role/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Role role = db.Role.Find(id);
			role.IsActive = false;
			db.Entry(role).State = EntityState.Modified;
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
