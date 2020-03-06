using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estimating_Tool.DAL;
namespace Estimating_Tool.View_Model
{
    public class LineItemTypeGroupVM
    {
        public string selectedLIneItemType { get; set; }

        [Display(Name ="Line Item Type")]
        [Required]
        public List<SelectListItem> LineItemTypeList { get; set; }

        [Display(Name = "Line Item Type Group Name")]
        [Required]
        [StringLength(50, ErrorMessage = "Line Item should be less than 50 characters")]
        [Remote("LineItemTypeGroupValidation","Validation",AdditionalFields = nameof(LineItemTypeGroupId))]
        public string LineItemTypeGroupStr { get; set; }

		public int LineItemTypeGroupId { get; set; }

     
        [Display(Name = "Created Date")]
        public virtual DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string CreatedBy { get; set; }

      
        [Display(Name = "Modified Date")]
        public virtual DateTime ModifiedDate { get; set; }

        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public int LineItemType { get; set; }

        public LineItemTypeGroupVM()
        {
            LineItemTypeList = new List<SelectListItem>();
                
        }
    }
}