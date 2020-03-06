using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Estimating_Tool.Models
{
    public class EstimateStatus
    {
        //Relationship Estimate Status 1 : * Estimate Header
        public EstimateStatus()
        {
            EstimateHeaders = new List<EstimateHeader>();
        }
        //Relationship Estimate Status 1 : * Estimate Header
        public List<EstimateHeader> EstimateHeaders { get; set; }

        //Estimate Status Id
        [Display(Name = "Estimate Status Id")]
        public int EstimateStatusId { get; set; }

        //Estimate Status Id
        [Required]
        [Display(Name = "Estimate Status Name")]
        [StringLength(50, ErrorMessage = "Estimate status should be less than 50 characters")]
        [Remote("EstimateStatusNameUnique","Validation", AdditionalFields ="EstimateStatusId")]
        public string EstimateStatusStr { get; set; }

        //Is Active
        public bool IsActive { get; set; }

        //create Date
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        //Modified Date
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        //modified By
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
    }
}