//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using Estimating_Tool.DAL;
//using Estimating_Tool.Models;
//using Estimating_Tool.View_Model;
//using PagedList;

//namespace Estimating_Tool.Controllers
//{
//	public class EstimateDetailController : Controller
//	{
//		private Estimatingcontext db = new Estimatingcontext();

//		// GET: EstimateDetail
//		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //declaring variables to be used
//		{
//			//Variables
//			//var estimateDetails = from s in db.EstimateDetail select s; //temp data stores

//			var estimateDetails = db.EstimateDetail.Include(p => p.estimateHeader).Include(p => p.LineItemType);

//			//Searching
//			if (!String.IsNullOrEmpty(searchString))
//			{
//				estimateDetails = estimateDetails.Where(s => s.estimateHeader.EstimateName.Contains(searchString));
//			}//string search method
//			if (searchString != null)
//			{
//				page = 1;
//			}
//			else
//			{
//				searchString = currentFilter;
//			}
//			ViewBag.CurrentFilter = searchString; //checking search


//			//Sorting
//			ViewBag.EstimateName = sortOrder == "EstimateName_desc" ? "EstimateName" : "EstimateName_desc";
//			ViewBag.LineItemTypeStr = sortOrder == "LineItemTypeStr_desc" ? "LineItemTypeStr" : "LineItemTypeStr_desc";
//			ViewBag.Estimate = sortOrder == "Estimate_desc" ? "Estimate" : "Estimate_desc";
//			ViewBag.NoteSortParm = sortOrder == "Note_desc" ? "Note" : "Note_desc";//runs sort methods in controller

//			switch (sortOrder) //Sort method
//			{
//				case "EstimateName":
//					estimateDetails = estimateDetails.OrderBy(s => s.estimateHeader.EstimateName);
//					break;

//				case "EstimateName_desc":
//					estimateDetails = estimateDetails.OrderByDescending(s => s.estimateHeader.EstimateName);
//					break;

//				case "LineItemTypeStr":
//					estimateDetails = estimateDetails.OrderBy(s => s.LineItemType.LineItemTypeStr);
//					break;

//				case "LineItemTypeStr_desc":
//					estimateDetails = estimateDetails.OrderByDescending(s => s.LineItemType.LineItemTypeStr);
//					break;

//				case "Estimate_desc":
//					estimateDetails = estimateDetails.OrderBy(s => s.Estimate);
//					break;

//				case "Estimate":
//					estimateDetails = estimateDetails.OrderByDescending(s => s.Estimate);
//					break;

//				case "Note":
//					estimateDetails = estimateDetails.OrderBy(s => s.Note);
//					break;

//				case "Note_desc":
//					estimateDetails = estimateDetails.OrderByDescending(s => s.Note);
//					break;

//				default:
//					estimateDetails = estimateDetails.OrderBy(s => s.estimateHeader.EstimateName);
//					break;
//			}

//			//Paging
//			int pageSize = 8;
//			int pageNumber = (page ?? 1);
//			return View(estimateDetails.ToPagedList(pageNumber, pageSize));
//		}

//		// GET: EstimateDetail/Details/5
//		public ActionResult Details(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//			EstimateDetail estimateDetail = db.EstimateDetail.Include(e => e.estimateHeader).Include(e => e.LineItemType).Where(e => e.EstimateDetailId == id).FirstOrDefault();
//			if (estimateDetail == null)
//			{
//				return HttpNotFound();
//			}
//			return View(estimateDetail);
//		}

//		// GET: EstimateDetail/Create
//		public ActionResult Create()
//		{
//            EstimateDetailsVM EDV = new EstimateDetailsVM();
//			return View(EDV);
//		}

//		// POST: EstimateDetail/Create
//		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//		[HttpPost]
//		[ValidateAntiForgeryToken]
//		public ActionResult Create([Bind(Include = "EstimateHeaderId,LineItemId,Estimate,Note")] EstimateDetailsVM estimateDetailsVM)
//		{
//            EstimateHeader header = db.EstimateHeader.Where(x => x.EstimateHeaderId == estimateDetailsVM.EstimateHeaderId).FirstOrDefault();
//            LineItemType itemType = db.LineItemType.Where(x => x.IsActive == true).Where(x => x.LineItemTypeId == estimateDetailsVM.LineItemId).FirstOrDefault();

//            EstimateDetail ED = new EstimateDetail { CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, CreatedBy ="Placeholder",ModifiedBy="Placeholder",
//            EstimateHeaderId = estimateDetailsVM.EstimateHeaderId, LineItemId = estimateDetailsVM.LineItemId, Estimate = estimateDetailsVM.Estimate, Note = estimateDetailsVM.Note,
//            estimateHeader = header, LineItemType = itemType};

//			if (ModelState.IsValid)
//			{
//				db.EstimateDetail.Add(ED);
//				db.SaveChanges();
//				return RedirectToAction("Index");
//			}

//			return View(estimateDetailsVM);
//		}

//		// GET: EstimateDetail/Edit/5
//		public ActionResult Edit(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//            EstimateDetail estimateDetail = db.EstimateDetail.Include(x => x.estimateHeader).Include(x => x.LineItemType).Where(x => x.EstimateDetailId == id).FirstOrDefault();
//            EstimateDetailsVM estimateDetailsVM = new EstimateDetailsVM{ EstimateHeaderId = estimateDetail.EstimateHeaderId, Estimate = estimateDetail.Estimate, Note = estimateDetail.Note
//            , LineItemId = estimateDetail.LineItemId };
//			if (estimateDetailsVM == null)
//			{
//				return HttpNotFound();
//			}
//			return View(estimateDetailsVM);
//		}

//		// POST: EstimateDetail/Edit/5
//		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//		[HttpPost]
//		[ValidateAntiForgeryToken]
//		public ActionResult Edit([Bind(Include = "EstimateHeaderId,LineItemId,Estimate,Note")] EstimateDetailsVM estimateDetailVM , int?id)
//		{
//            EstimateDetail estimateDetail = db.EstimateDetail.Where(x => x.EstimateDetailId == id).FirstOrDefault();
//            estimateDetail.ModifiedDate = DateTime.Now;
//            estimateDetail.Note = estimateDetailVM.Note;
//            estimateDetail.Estimate = estimateDetailVM.Estimate;
//            estimateDetail.LineItemId = estimateDetail.LineItemId;
//            estimateDetail.EstimateHeaderId = estimateDetail.EstimateHeaderId;
//            estimateDetail.estimateHeader = db.EstimateHeader.Where(x => x.EstimateHeaderId == estimateDetailVM.EstimateHeaderId).FirstOrDefault();
//            estimateDetail.LineItemType = db.LineItemType.Where(x => x.LineItemTypeId == estimateDetailVM.LineItemId).FirstOrDefault();
//			if (ModelState.IsValid)
//			{
//				db.Entry(estimateDetail).State = EntityState.Modified;
//				db.SaveChanges();
//				return RedirectToAction("Index");
//			}
//			return View(estimateDetail);
//		}

//		// GET: EstimateDetail/Delete/5
//		public ActionResult Delete(int? id)
//		{
//			if (id == null)
//			{
//				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//			}
//            EstimateDetail estimateDetail = db.EstimateDetail.Include(x => x.estimateHeader).Include(x => x.LineItemType).Where(x => x.EstimateDetailId == id).FirstOrDefault();
//			if (estimateDetail == null)
//			{
//				return HttpNotFound();
//			}
//			return View(estimateDetail);
//		}

//		// POST: EstimateDetail/Delete/5
//		[HttpPost, ActionName("Delete")]
//		[ValidateAntiForgeryToken]
//		public ActionResult DeleteConfirmed(int id)
//		{
//			EstimateDetail estimateDetail = db.EstimateDetail.Find(id);
//			db.EstimateDetail.Remove(estimateDetail);
//			db.SaveChanges();
//			return RedirectToAction("Index");
//		}

//		protected override void Dispose(bool disposing)
//		{
//			if (disposing)
//			{
//				db.Dispose();
//			}
//			base.Dispose(disposing);
//		}
//	}
//}
