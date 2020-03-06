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
    public class CurrencyController : Controller
    {
        private Estimatingcontext db = new Estimatingcontext();

        // GET: Currency
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //Variables
            var currencies = from s in db.Currency
                        where s.IsActive == true
                        select s; //temp data stores

            //LINQ Query to provide a list of distinct options for the dropdown search to be populated by
            var CurrencyStrDistinct = db.Currency.Where(x => x.IsActive == true).Select(x => x.CurrencyName).ToList().Distinct();
            List<SelectListItem> searchoptions = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in CurrencyStrDistinct)//used to save data into the select list of search options
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
            if (currentFilter != null)
            {
                currencies = currencies.Where(s => s.CurrencyName.Contains(searchString));
            }
            ViewBag.CurrentFilter = searchString; //checking search

            //Sorting
            ViewBag.CurrencyIdSortParm = sortOrder == "CurrencyId_desc" ? "CurrencyId" : "CurrencyId_desc";
            ViewBag.CurrencyNameSortParm = sortOrder == "CurrencyName_desc" ? "CurrencyName" : "CurrencyName_desc";


            switch (sortOrder) //Sort method
            {
                case "CurrencyId":
                    currencies = currencies.OrderBy(s => s.CurrencyId);
                    break;

                case "CurrencyId_desc":
                    currencies = currencies.OrderByDescending(s => s.CurrencyId);
                    break;

                case "CurrencyName":
                    currencies = currencies.OrderBy(s => s.CurrencyName);
                    break;

                case "CurrencyName_desc":
                    currencies = currencies.OrderByDescending(s => s.CurrencyName);
                    break;

                default:
                    currencies = currencies.OrderBy(s => s.CurrencyId);
                    break;
            }

            //Paging
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(currencies.ToPagedList(pageNumber, pageSize));
        }

        // GET: Currency/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = db.Currency.Where(x=> x.IsActive == true).Where(x=> x.CurrencyId == id).FirstOrDefault();
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // GET: Currency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Currency/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CurrencyId,CurrencyName,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] Currency currency)
        {
            if (db.Currency.Any(x => x.CurrencyName == currency.CurrencyName))
            {
                ModelState.AddModelError("CurrencyName", "Currency name must be unique");
                return View(currency);
            }
            currency.IsActive = true; //making sure all new records are active

            if (ModelState.IsValid)
            {
                var currentCurrencyName = db.Currency.Where(x => x.IsActive == true).Select(x => x.CurrencyName).ToList(); //LINQ query for searching

                currency.CreatedBy = User.Identity.Name;
                currency.ModifiedBy = User.Identity.Name;
                currency.CreatedDate = DateTime.Now;
                currency.ModifiedDate = DateTime.Now;
                db.Currency.Add(currency);
                db.SaveChanges();
                TempData["RecordAdded"] = "Record has been added successfully";
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        // GET: Currency/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = db.Currency.Find(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Currency/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CurrencyId,CurrencyName,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,IsActive")] Currency currency)
        {
            currency.IsActive = true;
            if (ModelState.IsValid)
            {
                currency.ModifiedDate = DateTime.Now;
                currency.ModifiedBy = User.Identity.Name;
                db.Entry(currency).State = EntityState.Modified;
                db.SaveChanges();
                TempData["RecordEdited"] = "Record has been Edited Successfully.";
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        // GET: Currency/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Currency currency = db.Currency.Find(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        // POST: Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Currency currency = db.Currency.Find(id);
            currency.IsActive = false;
            db.SaveChanges();
            TempData["RecordDeleted"] = "Record has been Deleted Successfully.";
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
