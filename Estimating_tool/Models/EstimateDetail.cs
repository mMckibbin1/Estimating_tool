using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Estimating_Tool.Models
{
    public class EstimateDetail
    {
        //Estimate Detail Id
        [Display(Name = "Estimate Detail Id")]
        public int EstimateDetailId { get; set; }

        //Estimate Header Id
        [Display(Name = "Estimate Header Id")]
        public int EstimateHeaderId { get; set; }

        //Line item Id
        [ForeignKey("lineItem")]
        [Display(Name = "Line Item Id")]
        public int LineItemId { get; set; }

        //Estimate
        [Display(Name = "Estimate")]
        public decimal Estimate { get; set; }

        //Note
        [Display(Name = "Note")]
        [StringLength(100, ErrorMessage = "Note must be less than 100 characters")]
        public string Note { get; set; }

        //public decimal Rate { get; set; }

        //Created Date
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Created Date")]
        public virtual DateTime CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string CreatedBy { get; set; }

        //Modified Date
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Modified Date")]
        public virtual DateTime ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string ModifiedBy { get; set; }


        public decimal Rate { get; set; }

        //Object for estimate header
        public EstimateHeader estimateHeader { get; set; }

        //Object for LineItemType
        public LineItem lineItem { get; set; }

        //public int ContingencyDefaultId { get; set; }
        //public ContingencyDefault ContingencyDefault { get; set; }
    }
}