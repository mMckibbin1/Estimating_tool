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
	public class EstimateHeaderController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();
		// GET: EstimateHeader
		public ActionResult Index(string currentFilterCustomer, string currentFilterProject, string currentFilterEstimateName, string CustomerNameSearch, string ProjectName, string EstimateName,string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
		{
            string username = User.Identity.Name;
            int userId = db.Consultants.Where(x => x.Username == username).Select(x => x.Id).FirstOrDefault();
            var joins = db.consultant_Projects.Where(x => x.ConsultantUserName == username).FirstOrDefault();
            //Variables
            var estimateHeaders = db.EstimateHeader.Where(x => x.IsActive == true).Include(c => c.commercialType).Where(c => c.commercialType.IsActive == true).Include(c => c.contingencyDefault)
                .Where(c => c.contingencyDefault.IsActive == true).Include(c => c.estimateStatus).Where(c => c.estimateStatus.IsActive == true).Include(c => c.estimateType)
                .Where(c => c.estimateType.IsActive == true).Include(c => c.project).Where(c => c.project.IsActive == true).Include(c => c.project.customer)
                .Where(c => c.project.customer.IsActive == true).Include(c => c.unitOfMeasure).Where(c => c.unitOfMeasure.IsActive == true);


            List<SelectListItem> DropDownOptionsCustomerNames = new List<SelectListItem>();//select List used to populate customer name dropdown search on view
            //LIQN query used to populate dropdown list with customer names
            var searchoptionsCustomerNames = db.EstimateHeader.Where(x => x.IsActive == true).Include(x => x.project.customer.CustomerName).Where(x => x.project.customer.IsActive == true).Select(x=> x.project.customer.CustomerName).ToList().Distinct();

            foreach(var item in searchoptionsCustomerNames)//used to add customer names from query to select list
            {
                DropDownOptionsCustomerNames.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.DropdownOptionsCustomerName = DropDownOptionsCustomerNames;//viewbag used to populate dropdown lists on view


            List<SelectListItem> DropDownOptionsProjectNames = new List<SelectListItem>();//select List used to populate project name dropdown search on view
            //LIQN query used to populate dropdown list with project names
            var searchoptionsProjectNames = db.EstimateHeader.Where(x => x.IsActive == true).Include(x => x.project.ProjectName).Where(x => x.project.IsActive == true).Select(x => x.project.ProjectName).ToList().Distinct();

            foreach(var item in searchoptionsProjectNames)//used to add project names from query to select list
            {
                DropDownOptionsProjectNames.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.DropdownOptionsProjectName = DropDownOptionsProjectNames;//viewbag used to populate dropdown lists on view

            List<SelectListItem> DropDownOptionsEstimateNames = new List<SelectListItem>();//select List used to populate project name dropdown search on view
            //LIQN query used to populate dropdown list with estimate names
            var searchoptionsEstimateNames = db.EstimateHeader.Where(x => x.IsActive == true).Select(x => x.EstimateName).ToList().Distinct();

            foreach(var item in searchoptionsEstimateNames)//used to add project names from query to select list
            {
                DropDownOptionsEstimateNames.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }

            ViewBag.DropDownOptionsEstimateNames = DropDownOptionsEstimateNames;//viewbag used to populate dropdown lists on view

            //Searching
            if (CustomerNameSearch != null || ProjectName != null || EstimateName !=null)
			{
				page = 1;

                //ifs used to filter data if a filter has been set by user
                if(CustomerNameSearch != null)
                {
                    estimateHeaders = estimateHeaders.Where(x => x.project.customer.CustomerName.Contains(CustomerNameSearch));
                }
                if(ProjectName != null)
                {
                    estimateHeaders = estimateHeaders.Where(x => x.project.ProjectName.Contains(ProjectName));
                }
                if(EstimateName != null)
                {
                    estimateHeaders = estimateHeaders.Where(x => x.EstimateName.Contains(EstimateName));
                }
			}
			else
			{
				CustomerNameSearch = currentFilterCustomer;
				ProjectName = currentFilterProject;
                EstimateName = currentFilterEstimateName;
			}

			ViewBag.currentFilterCustomer = CustomerNameSearch;
			ViewBag.currentFilterProject = ProjectName;
            ViewBag.currentFilterEstimateName = EstimateName;

			//Sorting
			//ViewBag.EstimateHeaderIdSortParm = sortOrder == "EstimateHeaderId_desc" ? "EstimateHeaderId" : "EstimateHeaderId_desc";
			ViewBag.CustomerIdSortParm = sortOrder == "CustomerId_desc" ? "CustomerId" : "CustomerId_desc";
			ViewBag.ProjectIdSortParm = sortOrder == "ProjectId_desc" ? "ProjectId" : "ProjectId_desc";
			ViewBag.EstimateNameSortParm = sortOrder == "EstimateName_desc" ? "EstimateName" : "EstimateName_desc";
			ViewBag.ValueSortParm = sortOrder == "Value_desc" ? "Value" : "Value_desc";
			ViewBag.UnitOfMeasureIdSortParm = sortOrder == "UnitOfMeasureId_desc" ? "UnitOfMeasureId" : "UnitOfMeasureId_desc";
			ViewBag.EstimateTypeIdSortParm = sortOrder == "EstimateTypeId_desc" ? "EstimateTypeId" : "EstimateTypeId_desc";
			ViewBag.CommercialTypeIdSortParm = sortOrder == "CommercialTypeId_desc" ? "CommercialTypeId" : "CommercialTypeId_desc";
			ViewBag.ContingencyDefaultIdSortParm = sortOrder == "ContingencyDefaultId_desc" ? "ContingencyDefaultId" : "ContingencyDefaultId_desc";
			ViewBag.EstimateStatusIdSortParm = sortOrder == "EstimateStatusId_desc" ? "EstimateStatusId" : "EstimateStatusId_desc";
			ViewBag.CreatedDateSortParm = sortOrder == "CreatedDate_desc" ? "CreatedDate" : "CreatedDate_desc";
			ViewBag.CreatedBySortParm = sortOrder == "CreatedBy_desc" ? "CreatedBy" : "CreatedBy_desc";
			ViewBag.ModifiedDateSortParm = sortOrder == "ModifiedDate_desc" ? "ModifiedDate" : "ModifiedDate_desc";
			ViewBag.ModifiedBySortParm = sortOrder == "ModifiedBy_desc" ? "ModifiedBy" : "ModifiedBy_desc"; //runs sort methods in controller

			switch (sortOrder) //Sort method
			{
				//case "EstimateHeaderId":
				//	estimateHeaders = estimateHeaders.OrderBy(s => s.EstimateHeaderId);
				//	break;

				case "EstimateHeaderId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.EstimateHeaderId);
					break;

				case "CustomerId":
					estimateHeaders = estimateHeaders.OrderBy(s => s.CustomerId);
					break;

				case "CustomerId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.CustomerId);
					break;

				case "ProjectId":
					estimateHeaders = estimateHeaders.OrderBy(s => s.ProjectId);
					break;

				case "ProjectId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.ProjectId);
					break;

				case "EstimateName":
					estimateHeaders = estimateHeaders.OrderBy(s => s.EstimateName);
					break;

				case "EstimateName_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.EstimateName);
					break;

				case "UnitOfMeasureId":
					estimateHeaders = estimateHeaders.OrderBy(s => s.UnitOfMeasureId);
					break;

				case "UnitOfMeasureId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.UnitOfMeasureId);
					break;

				case "EstimateTypeId":
					estimateHeaders = estimateHeaders.OrderBy(s => s.EstimateTypeId);
					break;

				case "EstimateTypeId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.EstimateTypeId);
					break;

				case "CommercialTypeId":
					estimateHeaders = estimateHeaders.OrderBy(s => s.CommercialTypeId);
					break;

				case "CommercialTypeId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.CommercialTypeId);
					break;

				case "ContingencyDefaultId":
					estimateHeaders = estimateHeaders.OrderBy(s => s.ContingencyDefaultId);
					break;

				case "ContingencyDefaultId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.ContingencyDefaultId);
					break;

				case "EstimateStatusId":
					estimateHeaders = estimateHeaders.OrderBy(s => s.EstimateStatusId);
					break;

				case "EstimateStatusId_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.EstimateStatusId);
					break;

				case "CreatedDate":
					estimateHeaders = estimateHeaders.OrderBy(s => s.CreatedDate);
					break;

				case "CreatedDate_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.CreatedDate);
					break;

				case "CreatedBy":
					estimateHeaders = estimateHeaders.OrderBy(s => s.CreatedBy);
					break;

				case "CreatedBy_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.CreatedBy);
					break;

				case "ModifiedDate":
					estimateHeaders = estimateHeaders.OrderBy(s => s.ModifiedDate);
					break;

				case "ModifiedDate_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.ModifiedDate);
					break;

				case "ModifiedBy":
					estimateHeaders = estimateHeaders.OrderBy(s => s.ModifiedBy);
					break;

				case "ModifiedBy_desc":
					estimateHeaders = estimateHeaders.OrderByDescending(s => s.ModifiedBy);
					break;

				default:
					estimateHeaders = estimateHeaders.OrderBy(s => s.EstimateHeaderId);
					break;
			}


			//Paging
			int pageSize = 8;
			int pageNumber = (page ?? 1);
			return View(estimateHeaders.ToPagedList(pageNumber, pageSize));
		}

		// GET: EstimateHeader/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			EstimateHeader estimateHeader = db.EstimateHeader.Find(id);
			var estimateHeaders = db.EstimateHeader.Where(x=> x.IsActive == true).Where(c => c.EstimateHeaderId == id).Include(c => c.commercialType).Where(c => c.commercialType.IsActive == true).Include(c => c.contingencyDefault)
				.Where(c => c.contingencyDefault.IsActive == true).Include(c => c.estimateStatus).Where(c => c.estimateStatus.IsActive == true).Include(c => c.estimateType)
				.Where(c => c.estimateType.IsActive == true).Include(c => c.project).Where(c => c.project.IsActive == true).Include(c => c.project.customer)
				.Where(c => c.project.customer.IsActive == true).Include(c => c.unitOfMeasure).Where(c => c.unitOfMeasure.IsActive == true).FirstOrDefault();
			if (estimateHeaders == null)
			{
				return HttpNotFound();
			}
			return View(estimateHeaders);
		}

		// GET: EstimateHeader/Create
		public ActionResult Create()
		{
            var VM = new View_Model.EstimateHeaderVM();
			return View(VM);
		}

		// POST: EstimateHeader/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "EstimateHeaderId,CustomerId,ProjectId,EstimateName,Value,UnitOfMeasureId,EstimateTypeId,CommercialTypeId,ContingencyDefaultId,EstimateStatusId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy,ROM")] EstimateHeader estimateHeader)
		{
            estimateHeader.IsActive = true;
			estimateHeader.CreatedDate = DateTime.Now;
			estimateHeader.ModifiedDate = DateTime.Now;
			estimateHeader.CreatedBy = User.Identity.Name;
			estimateHeader.ModifiedBy = User.Identity.Name;

			if (ModelState.IsValid)
			{
				db.EstimateHeader.Add(estimateHeader);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

            var VM = new EstimateHeaderVM();

			return View(VM);
		}

		// GET: EstimateHeader/Edit/5
		public ActionResult Edit(int? id)
		{
			var VM = new EstimateHeaderVM();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            EstimateHeader es = db.EstimateHeader.Where(x => x.EstimateHeaderId == id).Where(x=> x.IsActive == true).FirstOrDefault();

            if (es == null)
            {
                return HttpNotFound();
            }

            VM.EstimateName = es.EstimateName;
            VM.EstimateTypeId = es.EstimateTypeId;
            VM.ProjectId = es.ProjectId;
            VM.EstimateStatusId = es.EstimateStatusId;
            VM.UnitofMeasureId = es.UnitOfMeasureId;
            VM.CommercialTypeId = es.CommercialTypeId;
            VM.ContingencyDefaultId = es.ContingencyDefaultId;
            VM.CustomerId = es.CustomerId;

			EstimateHeader estimateHeader = db.EstimateHeader.Find(id);

			return View(VM);
		}

		// POST: EstimateHeader/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "EstimateHeaderId,CustomerId,ProjectId,EstimateName,Value,UnitOfMeasureId,EstimateTypeId,CommercialTypeId,ContingencyDefaultId,EstimateStatusId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] EstimateHeader estimateHeader, int? id)
		{

			if (ModelState.IsValid)
			{
				estimateHeader.ModifiedBy = User.Identity.Name;
				estimateHeader.EstimateHeaderId = Convert.ToInt32(id);
				db.Entry(estimateHeader).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				var VM = new View_Model.EstimateHeaderVM();
				VM.CommercialTypeId = estimateHeader.CommercialTypeId;
				VM.ContingencyDefaultId = estimateHeader.ContingencyDefaultId;
				VM.CustomerId = estimateHeader.CustomerId;
				VM.EstimateStatusId = estimateHeader.EstimateStatusId;
				VM.EstimateTypeId = estimateHeader.EstimateTypeId;
				VM.ProjectId = estimateHeader.ProjectId;
				VM.UnitofMeasureId = estimateHeader.UnitOfMeasureId;
				return View(VM);
			}

		}

		// GET: EstimateHeader/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			EstimateHeader estimateHeader = db.EstimateHeader.Find(id);
			var estimateHeaders = db.EstimateHeader.Where(x=> x.IsActive == true).Where(c => c.EstimateHeaderId == id).Include(c => c.commercialType).Where(c => c.commercialType.IsActive == true).Include(c => c.contingencyDefault)
				.Where(c => c.contingencyDefault.IsActive == true).Include(c => c.estimateStatus).Where(c => c.estimateStatus.IsActive == true).Include(c => c.estimateType)
				.Where(c => c.estimateType.IsActive == true).Include(c => c.project).Where(c => c.project.IsActive == true).Include(c => c.project.customer)
				.Where(c => c.project.customer.IsActive == true).Include(c => c.unitOfMeasure).Where(c => c.unitOfMeasure.IsActive == true).FirstOrDefault();
			if (estimateHeaders == null)
			{
				return HttpNotFound();
			}
			return View(estimateHeaders);
		}

		// POST: EstimateHeader/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			EstimateHeader estimateHeader = db.EstimateHeader.Find(id);
			estimateHeader.ModifiedBy = User.Identity.Name;
            estimateHeader.IsActive = false;
            db.Entry(estimateHeader).State = EntityState.Modified;
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

		public JsonResult updateProjectList(string CustomerID)//used to update project dropdown on estimate header create page for customer selected
		{
			var updatedList = db.Project.Include(x => x.customer).Where(x => x.IsActive == true).Where(x => x.customer.CustomerID.ToString() == CustomerID).OrderBy(x => x.ProjectName).Select(x => new { Text = x.ProjectName, Value = x.ProjectId.ToString() }).ToList();
			return Json(updatedList, JsonRequestBehavior.AllowGet);

		}
	}
}
