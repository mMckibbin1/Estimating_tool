using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estimating_Tool.DAL;

namespace Estimating_Tool.View_Model
{
    public class EstimateHeaderVM
    {
        public int id { get; set; }
        //Created Date 
        [Display(Name = "Created Date")]
        public virtual DateTime CreatedDate { get; set; }
        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string CreatedBy { get; set; }
        //Modified Date
        [Display(Name = "Modified Date")]
        public virtual DateTime ModifiedDate { get; set; }
        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        [Display(Name = "Estimate Type")]
        [Required]
        public int EstimateTypeId { get; set; }
        [Display(Name = "Estimate Status")]
        [Required]
        public int EstimateStatusId { get; set; }
        [Display(Name = "Project Name")]
        [Required]
        public int ProjectId { get; set; }

        [Display(Name = "ROM")]
        [Required]
        public int UnitofMeasureId { get; set; }
        [Display(Name = "Commercial Type")]
        [Required]
        public int CommercialTypeId { get; set; }
        [Display(Name = "Contingency Default %")]
        [Required]
        public int ContingencyDefaultId { get; set; }
        [Remote("CheckID", "Validation", ErrorMessage = "Customer Does not Exist")]
        [Display(Name = "Customer Name")]
        [Editable(true)]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name ="Estimate Name")]
        [Required]
        public string EstimateName { get; set; }
        [Display(Name = "Estimate Type")]
        [Required]
        public List<SelectListItem> EstimateTypeIdList { get; set; }
        [Display(Name = "Estimate Status")]
        [Required]
        public List<SelectListItem> EstimateStatusIdList { get; set; }
        [Display(Name = "Project Name")]
        [Required]
        public List<SelectListItem> ProjectIdList { get; set; }

        [Display(Name = "Unit of Measure")]
        [Required]
        public List<SelectListItem> UnitofMeasureIdList { get; set; }
        [Display(Name = "Commercial Type")]
        [Required]
        public List<SelectListItem> CommercialTypeIdList { get; set; }
        [Display(Name = "Contingency Default %")]
        [Required]
        public List <SelectListItem> ContingencyDefaultIdList { get; set; }

        [Remote("CheckID", "Validation", ErrorMessage = "Customer Does not Exist")]
        [Display(Name = "Customer Id")]
        [Editable(true)]
        [Required]
        public List<SelectListItem> CustomerIdList { get; set; }

		public EstimateHeaderVM()
        {
            using (var db = new Estimatingcontext())
            {
                EstimateTypeIdList = db.EstimateType.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.EstimateTypeStr, Value = x.EstimateTypeId.ToString() }).ToList();
                EstimateStatusIdList = db.EstimateStatus.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.EstimateStatusStr, Value = x.EstimateStatusId.ToString() }).ToList();
                ProjectIdList = db.Project.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.ProjectName, Value = x.ProjectId.ToString() }).ToList();
                UnitofMeasureIdList = db.UnitOfMeasure.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.UnitOfMeasureStr, Value = x.UnitOfMeasureId.ToString() }).ToList();
                CommercialTypeIdList = db.CommercialType.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.CommercialTypeStr, Value = x.CommercialTypeId.ToString() }).ToList();
                ContingencyDefaultIdList = db.ContingencyDefault.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.ContingencyDefaultInt.ToString(), Value = x.ContingencyDefaultId.ToString() }).ToList();
                CustomerIdList = db.Customer.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.CustomerName, Value = x.CustomerID.ToString() }).ToList();
            }
        }
    }
    
}