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
using System.Data.Entity.Migrations;
using PagedList;

namespace Estimating_tool.Controllers
{/// <summary>
 /// This is the controller for the project pages.  These include the project index, create, details, edit and delete pages.  This controller deals with the access
 /// between the view and the database.
 /// </summary>
    public class ProjectsController : Controller
	{
		private Estimatingcontext db = new Estimatingcontext();

        // GET: Projects
        /// <summary>
        /// This method popluates the project model to be sent to the project index page. The method handles the sorting and filter of data being sent to the view 
        /// using the paramters sent to it by the view.
        /// </summary>
        /// <param name="currentFilterCustomer">Current filter for customer name dropdown</param>
        /// <param name="currentFilterProject">Currrent filter for Project name dropdown</param>
        /// <param name="currentFilterAtlasID">Current filter for Atlas ID dropdown</param>
        /// <param name="AtlasIDs">for new filter with AtlasID</param>
        /// <param name="CustomerNameSearch">for new filter with CustomerName</param>
        /// <param name="ProjectName">for new filter with ProjectName</param>
        /// <param name="sortOrder">stores what storing is being done on index page</param>
        /// <param name="page">page number currently viewed</param>
        /// <returns>Iqueryable Project paged to index view</returns>
        public ActionResult Index(string currentFilterCustomer, string currentFilterProject, string currentFilterAtlasID, string AtlasIDs, string CustomerNameSearch, string ProjectName, string sortOrder, int? page)
		{
			var projects = db.Project.Include(p => p.customer).Include(p=>p.currency).Where(p => p.IsActive == true);//Getting projects with their realated customers

            var CustomerNameDistinct = db.Project.Include(p => p.customer).Where(p => p.IsActive == true).Select(x=> x.customer.CustomerName).ToList().Distinct();
            var projectsNameDistinct = db.Project.Include(p => p.customer).Where(p => p.IsActive == true).Select(x => x.ProjectName).ToList().Distinct();
            var AtlasIDistinct = db.Project.Include(p => p.customer).Where(p => p.IsActive == true).Select(x => x.AtlasID).ToList().Distinct();

            List<SelectListItem> searchOptionsProjectName = new List<SelectListItem>();//list to hold data to be used to populate search dropdown
            List<SelectListItem> searchOptionsCustomerName = new List<SelectListItem>();//list to hold data to be used to populate search dropdown
            List<SelectListItem> searchOptionsAtlasID = new List<SelectListItem>();//list to hold data to be used to populate search dropdown

            foreach (var item in CustomerNameDistinct)//used to save data into the select list of search options
            {
                searchOptionsCustomerName.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }
            foreach (var item in projectsNameDistinct)//used to save data into the select list of search options
            {
                searchOptionsProjectName.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
            }
            foreach (var item in AtlasIDistinct)//used to save data into the select list of search options
            {
                if(item != null)
                {
                    searchOptionsAtlasID.Add(new SelectListItem { Text = item.ToString(), Value = item.ToString() });
                }
            }
            ViewBag.searchOptionsCustomerName = searchOptionsCustomerName.Distinct();//viewbag used to send search dropdown data
            ViewBag.searchOptionsProjectName = searchOptionsProjectName.Distinct();//viewbag used to send search dropdown data
            ViewBag.searchOptionsAtlasID = searchOptionsAtlasID.Distinct();//viewbag used to send search dropdown data

            ViewBag.CurrentSort = sortOrder;
			ViewBag.projectNames = db.Project.Where(x => x.IsActive == true).Select(x => x.ProjectName).ToList();
			//paging

			if (CustomerNameSearch != null || ProjectName != null || AtlasIDs != null)// if new filter index will be set to page 1
			{
				page = 1;

			}
			else//currentFilter values saved into filter paramters
			{
				CustomerNameSearch = currentFilterCustomer;
				ProjectName = currentFilterProject;
				AtlasIDs = currentFilterAtlasID;
			}
            //setting currentfilters with filters being used in method
			ViewBag.currentFilterCustomer = CustomerNameSearch;
			ViewBag.currentFilterAtlasID = AtlasIDs;
			ViewBag.currentFilterProject = ProjectName;

			int pageSize = 8;//setting page size limit
			int pageNumber = (page ?? 1);

			//sorting 
			ViewBag.CustomerSortname = sortOrder == "name_desc" ? "name" : "name_desc";
			ViewBag.DateSort = sortOrder == "date_desc" ? "Date" : "date_desc";
			ViewBag.ProjectSortname = sortOrder == "proName_desc" ? "proName" : "proName_desc";
			ViewBag.ModDateSort = sortOrder == "modDate_desc" ? "ModDate" : "modDate_desc";
			ViewBag.AtlasID = sortOrder == "atlas_desc" ? "atlas" : "atlas_desc";
			ViewBag.CreatedBy = sortOrder == "CreateBy_desc" ? "CreatedBy" : "CreatedBy_desc";
			ViewBag.ModifiedBy = sortOrder == "ModifiedBy_desc" ? "ModifiedBy" : "ModifiedBy_desc";
			ViewBag.Rate = sortOrder == "Rate_desc" ? "Rate" : "Rate_desc";
            ViewBag.CurrencySort = sortOrder == "Currency_desc" ? "Currency" : "Currency_desc" ;



            switch (sortOrder)//sorting projects depending on sortorder pasted to method
			{
				case "name_desc":
					projects = projects.OrderByDescending(s => s.customer.CustomerName);
					break;

				case "name":
					projects = projects.OrderBy(s => s.customer.CustomerName);
					break;

				case "Date":
					projects = projects.OrderBy(s => s.CreatedDate);
					break;

				case "date_desc":
					projects = projects.OrderByDescending(s => s.CreatedDate);
					break;

				case "proName_desc":
					projects = projects.OrderByDescending(s => s.ProjectName);
					break;

				case "proName":
					projects = projects.OrderBy(s => s.ProjectName);
					break;

				case "modDate_desc":
					projects = projects.OrderByDescending(s => s.ModifiedDate);
					break;

				case "ModDate":
					projects = projects.OrderBy(s => s.ModifiedDate);
					break;

				case "atlas_desc":
					projects = projects.OrderByDescending(s => s.AtlasID);
					break;

				case "atlas":
					projects = projects.OrderBy(s => s.AtlasID);
					break;

				case "CreatedBy_desc":
					projects = projects.OrderByDescending(s => s.CreatedBy);
					break;

				case "CreatedBy":
					projects = projects.OrderBy(s => s.CreatedBy);
					break;

				case "ModifiedBy_desc":
					projects = projects.OrderByDescending(s => s.ModifiedBy);
					break;

				case "ModifiedBy":
					projects = projects.OrderBy(s => s.ModifiedBy);
					break;

				case "Rate_desc":
					projects = projects.OrderByDescending(s => s.Rate);
					break;

				case "Rate":
					projects = projects.OrderBy(s => s.Rate);
					break;

                case "Currency_desc":
                    projects = projects.OrderByDescending(s => s.CurrencyId);
                    break;

                case "Currency":
                    projects = projects.OrderBy(s => s.CurrencyId);
                    break;

				default:
					projects = projects.OrderBy(s => s.customer.CustomerName);
					break;
			}

			//filter projects using the filter paramiters on filter submited
			if (!string.IsNullOrEmpty(AtlasIDs))
			{
				projects = projects.Where(n => n.AtlasID.ToUpper().Contains(AtlasIDs.ToUpper()));
			}
			if (!string.IsNullOrEmpty(CustomerNameSearch))
			{
				projects = projects.Where(n => n.customer.CustomerName.ToUpper().Contains(CustomerNameSearch.ToUpper()));
			}
			if (!string.IsNullOrEmpty(ProjectName))
			{
				projects = projects.Where(n => n.ProjectName.ToUpper().Contains(ProjectName.ToUpper()));
			}

			return View(projects.ToPagedList(pageNumber, pageSize));//returing pageedList of projects to view
		}

		// GET: Projects/Details/5
        /// <summary>
        /// method populates a projet model to be sent to the project details view.
        /// </summary>
        /// <param name="id">ID of project that user slecte to be displayed in project details view</param>
        /// <param name="Return">ViewBag paramter used to determin what view the back hyperlink on the project details view should link to </param>
        /// <returns>Instance of the Project data model</returns>
		public ActionResult Details(int? id, string Return)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Project project = db.Project.Include(s => s.customer).Include(s => s.currency).Where(p => p.ProjectId == id).Where(x=> x.IsActive == true).FirstOrDefault();//getting project details with related customer using ID
			if (project == null)
			{
				return HttpNotFound();
			}
			ViewBag.Return = Return;
			return View(project);
		}

		private List<SelectListItem> GetCurrency()
		{
			using (var context = new Estimatingcontext())
			{
				return (from p in context.Currency
						where p.IsActive == true
						orderby p.CurrencyId
						select new SelectListItem { Text = p.CurrencyName, Value = p.CurrencyId.ToString() }).Distinct().ToList();
			}
		}

		// GET: Projects/Create
		/// <summary>
		/// Creating instance of Project View Model to be sent to the proejct create view.
		/// </summary>
		/// <returns>Instance of ProjectVM class</returns>
		public ActionResult Create()
		{
            ProjectVM vm = new ProjectVM();
			return View(vm);
		}

		// POST: Projects/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Method handles creating a new project to be saved into the databas by using the data from the Project view model sent from the project create view
        /// to complete an instance of the project data model that is then saved into the database.
        /// </summary>
        /// <param name="projectVM">instance of ProjectVM that has been set from the project create view to controller</param>
        /// <returns>If model is vald then it is saved and RedirectToAction is return.  If model is invalid then instance of ProjectVM sent to controller is return 
        /// to project create view</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "AtlasID,ProjectName, CurrencyId, Rate,CustomerID")] ProjectVM projectVM,int[] ConsultantIds)
		{
            if (ConsultantIds == null)
            {
                ModelState.AddModelError("ConsultantIds", "Consultants must be added");
                return View(projectVM);
            }
            //getting instance of customer that matches Customer ID from ProjectVM to save in customer for project.
            Customer customer = db.Customer.Where(x => x.CustomerID == projectVM.CustomerID).FirstOrDefault();

            Currency currency = db.Currency.Where(x => x.CurrencyId == projectVM.CurrencyId).FirstOrDefault();

            //setting values in instance of project data model to be saved in database
            Project project = new Project
			{
				ProjectName = projectVM.ProjectName,
				CreatedBy = User.Identity.Name,
				CurrencyId = projectVM.CurrencyId,
				Rate = projectVM.Rate,
				CreatedDate = DateTime.Now,
				ModifiedBy = User.Identity.Name,
				ModifiedDate = DateTime.Now,
				CustomerID = projectVM.CustomerID,
				IsActive = true,
				AtlasID = projectVM.AtlasID,
				customer = customer,
				currency = currency
			};

            List<Consultant_Project_join> joins = new List<Consultant_Project_join>();

            foreach (var item in ConsultantIds)
            {
                Consultant_Project_join join = new Consultant_Project_join
                {
                    ProjectId = projectVM.ProjectId,
                    ConsultantId = item,
                    ConsultantUserName = db.Consultants.Where(x => x.Id == item).Select(x => x.Username).FirstOrDefault()
                };
                joins.Add(join);
            }

            foreach (var item in joins)
            {
                db.consultant_Projects.Add(item);
            }



			if (ModelState.IsValid)
			{
                db.Project.Add(project);
				db.SaveChanges();
				TempData["RecordAdded"] = " Record Has Been Added Successfully.";
				return RedirectToAction("Index");
			}
			return View(projectVM);
		}

		// GET: Projects/Edit/5
        /// <summary>
        /// Creates and populates an instance of the ProjectVM class to be sent to the edit project view.
        /// </summary>
        /// <param name="id"> ID of project which is to be sent to edit view</param>
        /// <param name="Return">Used by viewbag on edit page to determin what view back hyperlink should return to</param>
        /// <returns>ProjectVM to edit view</returns>
		public ActionResult Edit(int? id, string Return)
		{

			ViewBag.Return = Return;//sending return value to edit view
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
            Project project = db.Project.Include(s => s.customer).Include(s => s.currency).Where(p => p.ProjectId == id).Where(x => x.IsActive == true).FirstOrDefault();//getting project with matching ID with customer included
            if (project == null)
            {
                return HttpNotFound();
            }
            ProjectVM vm = new ProjectVM { ProjectId = project.ProjectId, AtlasID = project.AtlasID, Rate = project.Rate, CustomerID = project.customer.CustomerID, ProjectName = project.ProjectName, CurrencyId = project.CurrencyId };

            //popluating instance of ProjectVM with details from Project instance.

            return View(vm);
		}

		// POST: Projects/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Method takes in ProjectMV from project edit view and populates an instance of the Project Class with those detials.  These details are saved into the database
        /// </summary>
        /// <param name="projectVM">Instance of ProjectVM class from project edit view</param>
        /// <param name="id">ID of project being edited in project edti view</param>
        /// <returns>If model valid RedirectToAction is return. If model is invalid instance of ProjectVM sent to controller is return to the project edit view</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "ProjectId, AtlasID, Rate,CustomerID,ProjectName,CurrencyId")] ProjectVM projectVM, int? id, int[] ConsultantIds)
		{
            if (ConsultantIds == null)
            {
                ModelState.AddModelError("ConsultantIds", "Consultants must be added");
                return View(projectVM);
            }
            //getting instance of Project where projectId matchs id
            Project project = db.Project.Where(x => x.IsActive == true).Where(x => x.ProjectId == id).FirstOrDefault();
           if(project == null)
            {
                return HttpNotFound();
            }
			//setting values edited values of the project.
			project.ModifiedDate = DateTime.Now;
			project.ModifiedBy = User.Identity.Name;
			project.CustomerID = projectVM.CustomerID;
            project.CurrencyId = projectVM.CurrencyId;
			project.Rate = projectVM.Rate;
			project.customer = db.Customer.Where(x => x.IsActive == true).Where(x => x.CustomerID == project.CustomerID).FirstOrDefault();
			project.ProjectName = projectVM.ProjectName;
			project.IsActive = true;

            //adding consultants selected to Consultant_Project_join table to relate them to the project

            List<Consultant_Project_join> joins = new List<Consultant_Project_join>();

            foreach (var item in ConsultantIds)
            {
                Consultant_Project_join join = new Consultant_Project_join
                {
                    ProjectId = projectVM.ProjectId,
                    ConsultantId = item,
                    ConsultantUserName = db.Consultants.Where(x => x.Id == item).Select(x => x.Username).FirstOrDefault()
                };
                joins.Add(join);
            }

            foreach (var item in joins)
            {
                db.Set<Consultant_Project_join>().AddOrUpdate(item);
            }

            if (ModelState.IsValid)
			{
				db.Entry(project).State = EntityState.Modified;
				db.SaveChanges();
				TempData["RecordEdited"] = " Record Has Been Edited Successfully.";
				return RedirectToAction("Index");
			}
			return View(projectVM);
		}

		// GET: Projects/Delete/5
        /// <summary>
        /// Method creates the instance of Project to be sent to the Project delete View 
        /// </summary>
        /// <param name="id">Project ID of project instance to be sent to the project delete View</param>
        /// <returns></returns>
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Project project = db.Project.Where(x=> x.IsActive == true).Where(x=> x.ProjectId == id).FirstOrDefault();
            if (project == null)
            {
                return HttpNotFound();
            }
            project.customer = db.Customer.Where(n => n.CustomerID == project.CustomerID).FirstOrDefault();//adding customer that related to project

			return View(project);
		}

		// POST: Projects/Delete/5
        /// <summary>
        /// Method handles project being soft deleted from the database. 
        /// </summary>
        /// <param name="id">ID of project being soft deleted from database sent from Project delete View</param>
        /// <returns>RedirectToAction to Index</returns>
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Project project = db.Project.Find(id);
			project.IsActive = false;//soft delete 
			project.ModifiedBy = User.Identity.Name;
			db.Entry(project).State = EntityState.Modified;
			db.SaveChanges();//saving soft delete
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
