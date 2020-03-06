using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class LineItem
    {
        //Line Item Id
        [Display(Name = "Line Item Id")]
        public int LineItemId { get; set; }

        //Line Item Group Id
        [Required]
        [Display(Name = "Line Item Group")]
        [ForeignKey("LineItemTypeGroup")]
        public int LineItemGroupId { get; set; }

        //Line Item Name
        [Required]
        [Display(Name = "Line Item Name")]
        [StringLength(100, ErrorMessage = "Line Item should be less than 100 characters")]
        public string LineItemStr { get; set; }

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

        //Object of Line Item Type Group
        public LineItemTypeGroup LineItemTypeGroup { get; set; }
    }
}