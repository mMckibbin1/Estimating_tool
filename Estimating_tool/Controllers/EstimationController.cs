using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
    public class EstimationController : Controller
    {
        private EstimationViewModelS EstimationViewModelS = new EstimationViewModelS();
        private Estimatingcontext db = new Estimatingcontext();
        private static int ID;//stores the id of the estitmate header being displayed this is then used in methods that will not be passed an id from the view
        // GET: Estimation
        public ActionResult Index(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No ID provided");
            }

            ID = Convert.ToInt32(Id);

            //getting all line items not in development group
            var lineItems = db.LineItem.AsNoTracking().Include(x=> x.LineItemTypeGroup).Where(x => x.IsActive == true).Where(x=> x.LineItemGroupId !=2);
            
            //getting estimate header for estimate header Id
            var header = db.EstimateHeader.Include(x => x.project).AsNoTracking().Where(x => x.EstimateHeaderId == Id).Where(x=> x.IsActive ==true).FirstOrDefault();

            if(header == null)
            {
                return HttpNotFound();
            }

            //getting current estimate details related to estimate header ID passed in
            var CurrentValues = db.EstimateDetail.AsNoTracking().Where(x => x.EstimateHeaderId == Id);

			ParentEstimation parentEstimation = new ParentEstimation();// instance of ParentEstmation to be stored in EstimationViewModelS

            foreach (var item in lineItems)
            {
                //creating an instance of Estimation View model foreach lineitem which is not in the development work group.
                //if there is a current value stored for the lineitem in the estimate details table then this data is passed into the 
                //view model.
                EstimationVM vM = new EstimationVM
                {
                    LineItem = item,
                    LineItemId = item.LineItemId,
                    LineItemTypeGroupId = item.LineItemTypeGroup.LineItemTypeGroupId,
                    group = item.LineItemTypeGroup,
                    EstimateHeaderId = Convert.ToInt32(Id),
                    estimateHeader = header,
                    Note = CurrentValues.Where(x => x.LineItemId == item.LineItemId)
                    .Select(x => x.Note).FirstOrDefault(),
                    Duration = CurrentValues.Where(x => x.LineItemId == item.LineItemId).Select(x => x.Estimate).FirstOrDefault(),
                    Id = CurrentValues.Where(x => x.LineItemId == item.LineItemId).Select(x => x.EstimateDetailId).FirstOrDefault(),


                //ContingencyDefault = CurrentValues.Where(x => x.LineItemId == item.LineItemId).Select(x => x).FirstOrDefault()
                };
                if (CurrentValues.Any(x => x.EstimateHeaderId == Id))
                {
                    vM.Rate = CurrentValues.Where(x => x.LineItemId == item.LineItemId).Select(x => x.Rate).FirstOrDefault();
                }
                else
                {
                    vM.Rate = header.project.Rate;
                }

                parentEstimation.estimationVMs.Add(vM);//adding General estimation EsimationVM to parent estimation
            }

            //getting lineitems that are in the development work group.  Also getting the any estimate details that relate to these lineitems.
            var specLineItems = db.LineItem.Include(x => x.LineItemTypeGroup).Where(x => x.LineItemTypeGroup.LineItemTypeGroupId == 2);
            var specCurrentValues = db.EstimateDetail.Include(x => x.lineItem).Include(x => x.lineItem.LineItemTypeGroup).Include(x => x.lineItem.LineItemTypeGroup.GetLineItemType)
				.Where(x => x.EstimateHeaderId == Id).Where(x => x.lineItem.LineItemTypeGroup.LineItemTypeGroupId == 2);

            //making one instance of Estimation View Model for each development work line item.
            //this ViewModel will be placed in a hidden row that will be used to copy from when adding a new row to a table.
            foreach (var item in specLineItems)
            {
                EstimationVM estimation = new EstimationVM
                {
                    LineItem = item,
                    LineItemId = item.LineItemId,
                    LineItemTypeGroupId = item.LineItemTypeGroup.LineItemTypeGroupId,
                    group = item.LineItemTypeGroup,
                    EstimateHeaderId = Convert.ToInt32(Id),
                    estimateHeader = header,
                };

                parentEstimation.SpecificTasks.Add(estimation);
            }

            //making instance of Estimation View Model for each estimate Detial saved in the database related to development work. 
            foreach (var item in specCurrentValues)
            {
                EstimationVM vM = new EstimationVM
                {
                    LineItem = item.lineItem,
                    LineItemId = item.LineItemId,
                    LineItemTypeGroupId = item.lineItem.LineItemTypeGroup.LineItemTypeGroupId,
                    group = item.lineItem.LineItemTypeGroup,
                    EstimateHeaderId = Convert.ToInt32(Id),
                    estimateHeader = header,
                    Note = specCurrentValues.Where(x => x.EstimateDetailId == item.EstimateDetailId).Select(x => x.Note).FirstOrDefault(),
                    Duration = item.Estimate,
                    Id = specCurrentValues.Where(x => x.EstimateDetailId == item.EstimateDetailId).Select(x => x.EstimateDetailId).FirstOrDefault(),
                    //ContingencyDefault = item.
                };
                parentEstimation.SpecificTasks.Add(vM);
            }




    


            EstimationViewModelS.ParentEstimation = parentEstimation;
            EstimationViewModelS.EstimateHeader = header;
            EstimationViewModelS.EstimateHeaderVM = Edit(Id);

            return View(EstimationViewModelS);
        }

      
        /// <summary>
        /// method to return the list of line items for an estitmation
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>list of line items of estitmations</returns>
        public ParentEstimation parentEstimation(int Id)
        {
            var lineItems = db.LineItem.Include(x => x.LineItemTypeGroup).Where(x => x.IsActive == true);
            var header = db.EstimateHeader.Where(x => x.EstimateHeaderId == Id).FirstOrDefault();
            ParentEstimation parentEstimation = new ParentEstimation();
            foreach (var item in lineItems)
            {
                EstimationVM vM = new EstimationVM { LineItemTypeId = item.LineItemTypeGroup.LineItemType, group = item.LineItemTypeGroup, EstimateHeaderId = Id, estimateHeader = header };
                parentEstimation.estimationVMs.Add(vM);
            }
            return parentEstimation;
        }

        // GET: EstimateHeader
        /// <summary>
        /// used to populate view model that is used to display the estimate header data on the estimate header tab
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>populated view model for EstimateHeader</returns>
        public EstimateHeaderVM Edit(int? Id)
        {

            var VM = new EstimateHeaderVM();

            EstimateHeader es = db.EstimateHeader.Where(x => x.EstimateHeaderId == Id).FirstOrDefault();
            //populating view model veribels with data from the estitmate header table
            VM.EstimateName = es.EstimateName;
            VM.EstimateTypeId = es.EstimateTypeId;
            VM.ProjectId = es.ProjectId;
            VM.EstimateStatusId = es.EstimateStatusId;
            VM.UnitofMeasureId = es.UnitOfMeasureId;
            VM.CommercialTypeId = es.CommercialTypeId;
            VM.ContingencyDefaultId = es.ContingencyDefaultId;
            VM.CustomerId = es.CustomerId;
            VM.CreatedBy = es.CreatedBy;
            VM.CreatedDate = es.CreatedDate;
            VM.ModifiedBy = es.ModifiedBy;
            VM.ModifiedDate = es.ModifiedDate;
            VM.id = Convert.ToInt32(Id);

            return VM;
        }

        // POST: EstimateHeader/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// method used to save changes to the estimate header information
        /// </summary>
        /// <param name="estimateHeader"></param>
        /// <param name="Id"></param>
        /// <returns>
        /// saves chnages to estitmate header and returns user to index
        /// if model state is not vaild returns user to edit page 
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EstimateHeaderId,CustomerId,ProjectId,EstimateName,Value,UnitOfMeasureId,EstimateTypeId,CommercialTypeId,ContingencyDefaultId,EstimateStatusId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] EstimateHeader estimateHeader, int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                estimateHeader.ModifiedBy = User.Identity.Name;//setting modified by value to logged in user name
                estimateHeader.EstimateHeaderId = Convert.ToInt32(ID);// setting id for estitmate header to be modififed
                estimateHeader.IsActive = true;
                estimateHeader.ModifiedBy = User.Identity.Name;
                estimateHeader.ModifiedDate = DateTime.Now;
                db.Entry(estimateHeader).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",new {Id = ID });
            }
            else
            {//populateing view model to be set back to view
                var VM = new View_Model.EstimateHeaderVM();
                VM.CommercialTypeId = estimateHeader.CommercialTypeId;
                VM.ContingencyDefaultId = estimateHeader.ContingencyDefaultId;
                VM.CustomerId = estimateHeader.CustomerId;
                VM.EstimateStatusId = estimateHeader.EstimateStatusId;
                VM.EstimateTypeId = estimateHeader.EstimateTypeId;
                VM.ProjectId = estimateHeader.ProjectId;
                VM.UnitofMeasureId = estimateHeader.UnitOfMeasureId;

                EstimationViewModelS.ParentEstimation = parentEstimation(ID);
                EstimationViewModelS.EstimateHeaderVM = Edit(ID);

                return View("Index", EstimationViewModelS);
            }

        }

        //this method is used to create/edit update estimate details that have been added or changed on the estimation page.
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "groups")] ParentEstimation parent)
        {
            //saving/udating all General estimations submit
            foreach (var item in parent.estimationVMs)
                {
                    EstimateDetail estimateDetail = new EstimateDetail
                    {
                        EstimateHeaderId = item.EstimateHeaderId,
                        CreatedBy = User.Identity.Name,
                        CreatedDate = DateTime.Now,
                        Note = item.Note,
                        LineItemId = item.LineItemId,
                        Estimate = Convert.ToDecimal(item.Duration),
                        ModifiedBy = User.Identity.Name,
                        ModifiedDate = DateTime.Now,
                        lineItem = item.LineItem,
                        estimateHeader = item.estimateHeader,
                        EstimateDetailId = item.Id,
                        Rate = item.Rate
                        
                    };
                    db.Set<EstimateDetail>().AddOrUpdate(estimateDetail);
                }

            //saving/updating all development work estimation where they have a note or duration
            foreach (var item in parent.SpecificTasks.Where(x => x != null).Where(x => x.Duration.HasValue||!string.IsNullOrWhiteSpace(x.Note)))
            {

                EstimateDetail estimateDetail = new EstimateDetail
                {
                    EstimateHeaderId = item.EstimateHeaderId,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    Note = item.Note,
                    LineItemId = item.LineItemId,
                    Estimate = Convert.ToDecimal(item.Duration),
                    ModifiedBy = User.Identity.Name,
                    ModifiedDate = DateTime.Now,
                    lineItem = item.LineItem,
                    EstimateDetailId = item.Id,
                    estimateHeader = item.estimateHeader
                };
                db.Set<EstimateDetail>().AddOrUpdate(estimateDetail);
            }
            db.SaveChanges();

            return RedirectToAction("Index", new { Id = ID });
        }

        //method called by ajax request from estimation page to remove database entry for row that is deleted on page.
        public void RemoveRecord(int? id)
        {
            try
            {
                if (id != 0)
                {
                    var estimateDetails = db.EstimateDetail.Find(id);
                    if (estimateDetails != null)
                    {
                        db.EstimateDetail.Remove(estimateDetails);
                        db.SaveChanges();
                    }
                }
               
            }
            catch(Exception)
            {
                
            }
        }
    }

  
}