
using Estimating_Tool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estimating_Tool.DAL;

namespace Estimating_Tool.View_Model
{
	public class LineItemVM
	{
		public LineItemVM()
		{
			lineitemGroup = new List<LineItemTypeGroup>();
			lineItemType = new List<LineItemType>();
			LineItemGroupIdList = new List<SelectListItem>();
			LineItemTypeId = new List<SelectListItem>();
			LineItemIds = new List<SelectListItem>();

            using (var db = new Estimatingcontext())
            {
                LineItemGroupIdList = db.LineItemTypeGroup.Where(x => x.IsActive == true).Select(x => new SelectListItem {Text = x.LineItemTypeGroupStr,Value = x.LineItemTypeGroupId.ToString() }).ToList();
            }

              
		}

		public string selectedLineItemType { get; set; }
		public string selectedLineItemGroup { get; set; }
		public string selectedLineItem { get; set; }
		public List<LineItemType> lineItemType { get; set; }
		public List<LineItemTypeGroup> lineitemGroup { get; set; }

        public int LineItemId { get; set; }

        [Display(Name ="Line Item Group Id")]
		public int LineItemGroupId { get; set; }

		[Display(Name = "Line Item Group")]
		[Required]
		public List<SelectListItem> LineItemGroupIdList { get; set; }

		[Display(Name = "Line Item")]
		[Required]
		public List<SelectListItem> LineItemIds { get; set; }

	
		[Display(Name = "Created Date")]
		public virtual DateTime CreatedDate { get; set; }

		[Display(Name = "Created By")]
		public string CreatedBy { get; set; }

		[Display(Name = "Modified Date")]
		public virtual DateTime ModifiedDate { get; set; }

		[Display(Name = "Modified By")]
		public string ModifiedBy { get; set; }

		public bool IsActive { get; set; }
		public string selectedLineItemGroupId { get; set; }
		
		public string selectedLineItemTypeId { get; set; }
		public string selectedLineItemId { get; set; }

		[Display(Name = "Line Item Type")]
		[Required]
		public List<SelectListItem> LineItemTypeId { get; set; }
		public string lineItemType1 { get; set; }
		public string lineItemGroup { get; set; }

		[Display(Name = "Line Item Name")]
       [Remote("LineItemValidationName", "Validation",AdditionalFields = "LineItemId")]
        public string LineItemStr { get; set; }
		public Int16 Qty { get; set;}
		public double Price { get; set; }
		public double TotalAmount { get; set; }

	}

	public class LineItemDetail
	{
		public List<SelectListItem> LineItemGroupId { get; set; }

		public List<SelectListItem> LineItemTypeId { get; set; }

		public List<LineItemVM> LineItemDetails { get; set; }

	}
}