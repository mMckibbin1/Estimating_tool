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
	public class CustomersController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();
		// GET: Customers
		public ActionResult Index(string CustomerName, string AtlasID, string sortOrder, int? page, string CurrentFilterCustomerName, string CurrentFilterAtlasID)
		{//setting sorting
			var customer = from c in db.Customer
						   where c.IsActive == true
						   select c;

            List<SelectListItem> CustomerNamesDistinct = new List<SelectListItem>();//list to hold data to be used to populate search dropdown
            List<SelectListItem> CustomerAtlasIDsDistinct = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            var CustomerNameDistinct = db.Customer.Where(x => x.IsActive == true).Select(x => x.CustomerName).ToList().Distinct();
            var CustomerAtlasIdDistinct = db.Customer.Where(x => x.IsActive == true).Select(x => x.AtlasID).ToList().Distinct();
            foreach (var item in CustomerNameDistinct)//used to save data into the select list of search options
            {
                CustomerNamesDistinct.Add(new SelectListItem { Text = item, Value = item });
            }
            foreach (var item in CustomerAtlasIdDistinct)//used to save data into the select list of search options
            {
                CustomerAtlasIDsDistinct.Add(new SelectListItem { Text = item, Value = item });
            }

            ViewBag.CustomerNameDistinct = CustomerNamesDistinct;//viewbag used to send search dropdown data
            ViewBag.CustomerAtlasIdDistinct = CustomerAtlasIDsDistinct;//viewbag used to send search dropdown data

            ViewBag.CurrentSort = sortOrder;
			ViewBag.Rate = sortOrder == "Rate_desc" ? "Rate" : "Rate_desc";
			ViewBag.NameSortParm = sortOrder == "CustomerName_desc" ? "CustomerName_asce" : "CustomerName_desc";
			ViewBag.AtlasIDSort = sortOrder == "AtlasID_desc" ? "AtlasID_asce" : "AtlasID_desc";

			//checking for filter 
			if (CustomerName != null || AtlasID != null)
			{
				page = 1;
			}
			else
			{
				//setting filter if no new filter as been given
				CustomerName = CurrentFilterCustomerName;
				AtlasID = CurrentFilterAtlasID;
			}

			ViewBag.CurrentFilterCustomerName = CustomerName;
			ViewBag.CurrentFilterAtlasID = AtlasID;


			if (!string.IsNullOrEmpty(CustomerName))
			{
				customer = customer.Where(s => s.CustomerName.Contains(CustomerName));
			}

			if (!string.IsNullOrEmpty(AtlasID))
			{
				customer = customer.Where(s => s.AtlasID.Contains(AtlasID));
			}
			switch (sortOrder)
			{//sorting view 
				case "CustomerName_desc": customer = customer.OrderByDescending(c => c.CustomerName); break;
				case "AtlasID_desc": customer = customer.OrderByDescending(c => c.AtlasID); break;
				case "CustomerName_asce": customer = customer.OrderBy(c => c.CustomerName); break;
				case "AtlasID_asce": customer = customer.OrderBy(c => c.AtlasID); break;
				default: customer = customer.OrderByDescending(c => c.CustomerName); break;
			}

			int pageSize = 8;//amount to items displayed per page
			int PageNumber = (page ?? 1);

			return View(customer.ToPagedList(PageNumber, pageSize));
		}


		// GET: Customers/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Customers/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "CustomerID,AtlasID,CustomerName,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] Customer customer)
		{//setting date to current system and is active to true when new customer is created 

			customer.CreatedBy = User.Identity.Name;
			customer.ModifiedBy = User.Identity.Name;
			customer.CreatedDate = DateTime.Now;
			customer.ModifiedDate = DateTime.Now;
            customer.IsActive = true;

			if (ModelState.IsValid)
			{
				db.Customer.Add(customer);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}

			return View(customer);
		}

		// GET: Customers/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Customer customer = db.Customer.Where(x=> x.IsActive == true).Where(x=> x.CustomerID == id).FirstOrDefault();
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		// POST: Customers/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "CustomerID,AtlasID,CustomerName,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] Customer customer)
		{//changing modified date to current system time and modified by to current user
			customer.ModifiedDate = DateTime.Now;
			customer.ModifiedBy = User.Identity.Name;
			customer.IsActive = true;

			if (ModelState.IsValid)
			{
				db.Entry(customer).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index");
			}
			return View(customer);
		}

		// GET: Customers/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Customer customer = db.Customer.Where(x => x.IsActive == true).Where(x => x.CustomerID == id).FirstOrDefault();
            if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		// POST: Customers/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Customer customer = db.Customer.Find(id);
			customer.IsActive = false;
			customer.ModifiedBy = User.Identity.Name;
			db.Entry(customer).State = EntityState.Modified;
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
