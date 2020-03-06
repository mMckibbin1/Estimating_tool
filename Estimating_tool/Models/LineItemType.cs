using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class LineItemType
    {
        //Relationship of Line Item Type 1 : * Estimate Detail
        //Relationship of Line Item Type 1 : * LineItemTypeGroups
        public LineItemType()
        {
            //EstimateDetail = new List<EstimateDetail>();
            lineItemTypeGroups = new List<LineItemTypeGroup>();
        }
        //Relationship of Line Item Type 1 : * Estimate Detail
       // public List<EstimateDetail> EstimateDetail { get; set; }
        //Relationship of Line Item Type 1 : * LineItemTypeGroups
        public List<LineItemTypeGroup> lineItemTypeGroups { get; set; }

        //Line Item Type Id
        [Display(Name = "Line Item Type Id")]
        public int LineItemTypeId { get; set; }

        //Line Item type Name
        [Display(Name = "Line Item Type Name")]
        [StringLength(100, ErrorMessage = "Line Item should be less than 100 characters")]
        [Remote("LineItemTypeValidation","Validation",AdditionalFields ="LineItemTypeId")]
        [Required]
        public string LineItemTypeStr { get; set; }

        //Created Date
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public virtual DateTime CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string CreatedBy { get; set; }

        //Modified Date
        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public virtual DateTime ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        //Is Active
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}