using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class LineItemTypeGroup
    {
        //Relationship of Line Item Type Group 1 : * Line Item
        public LineItemTypeGroup()
        {
            lineItems = new List<LineItem>();
        }
        //Relationship of Line Item Type Group 1 : * Line Item
        public List<LineItem> lineItems { get; set; }

        //Line Item type Group Id
        [Display(Name = "Line Item Type Group Id")]
        public int LineItemTypeGroupId { get; set; }

        //Line Item Type Id
        [Display(Name = "Line Item Type Id")]
        [ForeignKey("GetLineItemType")]
        public int LineItemType { get; set; }

        //Line Item type Name
        [Display(Name = "Line Item Type Group Name")]
        [StringLength(50, ErrorMessage = "Line Item type group should be less than 50 characters")]
        [Remote("LineItemTypeGroupValidation", "Validation", AdditionalFields = "LineItemTypeGroupId")]
        public string LineItemTypeGroupStr { get; set; }

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

        //Active
        public bool IsActive { get; set; }
 
        //Object of Line Item Type
        public LineItemType GetLineItemType { get; set; }
    }
}