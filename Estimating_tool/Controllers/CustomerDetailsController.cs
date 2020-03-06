using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estimating_Tool.View_Model;
using Estimating_Tool.Models;
using Estimating_Tool.DAL;
using PagedList;
using System.Net;

namespace Estimating_Tool.Controllers
{
	public class CustomerDetailsController : Controller
	{
		public Customer GetCustomer(int? CustomerID)//returns a customer with matching customer ID to the parameter passed in
        {
			if (CustomerID != null)
			{
				using (var context = new Estimatingcontext())
				{
					var Customer = context.Customer.AsNoTracking().Where(x => x.CustomerID == CustomerID).Where(x=> x.IsActive == true).FirstOrDefault();
					return Customer;
				}
			}
			return null;
		}

		// GET: CustomerDetails
		public ActionResult Details(int? id, int? page, string SortOrder, string CurrentFillterProjectName, string CurrentFillterProjectID, string CurrentFilterAtlasID, string ProjectName, string ProjectID, string AtlasID)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			CustomerDetailsVM viewModel = new CustomerDetailsVM();//creating instance of view_model_main class
			using (var context = new Estimatingcontext())
			{
				var customer = GetCustomer(id);//creates a instance of customer with the customer ID requested

				if (customer != null)
				{
					var customerListVm = new CustomerDetails()//saves customer into the customer details view model
					{
						CustomerName = customer.CustomerName,
						CusAtlasID = customer.AtlasID,
						CustomerID = customer.CustomerID,
						CreatedDate = customer.CreatedDate,
						CreatedBy = customer.CreatedBy,
						ModifiedDate = customer.ModifiedDate,
						ModifiedBy = customer.ModifiedBy
					};

					viewModel.CustomerDetails = customerListVm;//save customer into customer class

					viewModel.CustomerProjects = CustomerProjects(id, page, SortOrder, CurrentFillterProjectName, CurrentFillterProjectID, CurrentFilterAtlasID, ProjectName, ProjectID, AtlasID);//saves projects to view modle using method

					return View(viewModel);
				}
				else
				{
					return HttpNotFound();
				}
			}
		}

		//GET: Associated projects of the customer
		public IEnumerable<CustomerProjects> CustomerProjects(int? id, int? page, string SortOrder, string CurrentFillterProjectName, string CurrentFillterProjectID, string CurrentFilterAtlasID, string ProjectName, string ProjectID, string AtlasID)
		{
			int pageSize = 2;
			int PageNumber = (page ?? 1);

			var customer = GetCustomer(id);//creates a instance of customer with the customer ID requested
			CustomerDetailsVM viewModel = new CustomerDetailsVM();//creating instance of view_model_main class
			using (var context = new Estimatingcontext())
			{
				var projectList = context.Project.AsNoTracking().Where(x => x.CustomerID == id && x.IsActive == true).OrderBy(x => x.ProjectId).Select(x =>
					   new CustomerProjects//gets projects that are related to the customer by customer ID 
					   {
						   CustomerID = x.CustomerID,
						   projectName = x.ProjectName,
						   Rate = x.Rate,
						   ProAtlasID = x.AtlasID,
						   ProjectId = x.ProjectId,
					   }).ToList();
				//setting type of sort
				ViewBag.CurrentSort = SortOrder;
				ViewBag.Rate = SortOrder == "Rate_desc" ? "Rate" : "Rate_desc";
				ViewBag.NameSortParm = SortOrder == "ProjectName_desc" ? "ProjectName_asce" : "ProjectName_desc";
				ViewBag.AtlasIDSort = SortOrder == "AtlasID_desc" ? "AtlasID_asce" : "AtlasID_desc";
				ViewBag.IDSort = SortOrder == "ID_desc" ? "ID_asce" : "ID_desc";

				//Checking to see if new filter or older filter still in effected
				if (ProjectName != null || AtlasID != null || ProjectID != null)
				{
					page = 1;
				}
				else
				{
					ProjectName = CurrentFillterProjectName;
					AtlasID = CurrentFilterAtlasID;
					ProjectID = CurrentFillterProjectID;
				}

				ViewBag.CurrentFillterProjectName = ProjectName;
				ViewBag.CurrentFillterProjectID = ProjectID;
				ViewBag.CurrentFilterAtlasID = AtlasID;
				//filter projects
				if (!string.IsNullOrEmpty(ProjectName))
				{
					projectList = projectList.Where(s => s.projectName.Contains(ProjectName)).ToList();
				}

				if (!string.IsNullOrEmpty(AtlasID))
				{
					projectList = projectList.Where(s => s.ProAtlasID.Contains(AtlasID)).ToList();
				}

				if (!string.IsNullOrEmpty(ProjectID))
				{
					projectList = projectList.Where(s => s.ProjectId.ToString().Contains(ProjectID)).ToList();
				}
				//sorting projects to match sort set above
				switch (SortOrder)
				{
					case "ProjectName_desc": projectList = projectList.OrderByDescending(c => c.projectName).ToList(); break;
					case "AtlasID_desc": projectList = projectList.OrderByDescending(c => c.ProAtlasID).ToList(); break;
					case "ProjectName_asce": projectList = projectList.OrderBy(c => c.projectName).ToList(); break;
					case "AtlasID_asce": projectList = projectList.OrderBy(c => c.ProAtlasID).ToList(); break;
					case "ID_desc": projectList = projectList.OrderByDescending(c => c.ProjectId.ToString()).ToList(); break;
					case "ID_asce": projectList = projectList.OrderBy(c => c.ProjectId.ToString()).ToList(); break;

					default: projectList = projectList.OrderByDescending(c => c.projectName).ToList(); break;
				}
				viewModel.CustomerProjects = projectList;//saving projects list to view model
				ViewBag.page = viewModel.CustomerProjects.ToPagedList(PageNumber, pageSize);//sends projects list to project partail view as a pagelist
				return viewModel.CustomerProjects;//returning view model      
			}
		}
	}
}