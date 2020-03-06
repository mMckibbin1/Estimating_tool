using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class EstimateType
    {
        //Relationship Estimate Type 1 : * Estimate Headers
        public EstimateType()
        {
            EstimateHeaders = new List<EstimateHeader>();
        }
        //Relationship Estimate Type 1 : * Estimate Headers
        public List<EstimateHeader> EstimateHeaders { get; set; }

        //Estimate Type Id
        [Display(Name = "Estimate Type Id")]
        public int EstimateTypeId { get; set; }

        //Estimate Type Name
        [Required]
        [Display(Name = "Estimate Type Name")]
        [StringLength(50, ErrorMessage ="Estimate type should be less than 50 characters")]
        [Remote("EstimateTypeValidation","Validation",AdditionalFields = "EstimateTypeId")]
        public string EstimateTypeStr { get; set; }

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
        public bool IsActive { get; set; }
    }
}