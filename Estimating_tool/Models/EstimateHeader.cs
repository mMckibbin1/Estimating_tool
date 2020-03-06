using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class EstimateHeader
    {
        //Relationship Estimate Header 1 : * Estimate Detail 
        public EstimateHeader()
        {
            EstimateDetails = new List<EstimateDetail>();
        }

		public int? ConsultantId { get; set; }

		//Relationship Estimate Header 1 : * Estimate Detail 
		public List<EstimateDetail> EstimateDetails { get; set; }

        //Estimate Header Id
        [Display(Name = "Estimate Header Id")]
        public int EstimateHeaderId { get; set; }

        //Customer Id
        [Remote("CheckID", "EstimateHeader", ErrorMessage = "Customer Does not Exist")]
        [Display(Name = "Customer")]
        [Editable(true)]
        public int CustomerId { get; set; }

        //Object of Project
        public Project project { get; set; }

        //Project Id
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        //Estimate Name
        [Display(Name = "Estimate Name")]
        [StringLength(100, ErrorMessage = "Estimate Name length must not be less than 100 characters")]
        public string EstimateName { get; set; }

        //Unit Of Measure
        public UnitOfMeasure unitOfMeasure { get; set; }

        //ROM / Unit Of Measure
        [Display(Name = "ROM")]
        public int UnitOfMeasureId { get; set; }

        //Object of Estimate Type
        public EstimateType estimateType {get; set;}

        //Estimate Type Id
        [Display(Name = "Estimate Type")]
        public int EstimateTypeId { get; set; }

        //Object of Commercial type
        public CommercialType commercialType { get; set; }

        //Commercial Type Id
        [Display(Name = "Commercial Type")]
        public int CommercialTypeId { get; set; }

        //Object of Contingency Default Id
        public ContingencyDefault contingencyDefault { get; set; }

        //Contingency %
        [Display(Name = "Contingency %")]
        public int ContingencyDefaultId { get; set; }

        //Object of Estimate Status
        public EstimateStatus estimateStatus { get; set; }

        //Estimate Status Id
        [Display(Name = "Estimate Status")]
        public int EstimateStatusId { get; set; }

        //Created Date 
        [Column(TypeName = "datetime2")]
        [Display(Name = "Created Date")]
        public virtual DateTime CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string CreatedBy { get; set; }

        //Modified Date
        [Column(TypeName = "datetime2")]
        [Display(Name = "Modified Date")]
        public virtual DateTime ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }
    }
}