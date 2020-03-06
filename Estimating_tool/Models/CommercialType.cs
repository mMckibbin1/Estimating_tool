using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class CommercialType
    {
        //Relationship Commercial Type 1 : * Estimate Headers
        public CommercialType()
        {
            EstimateHeaders = new List<EstimateHeader>();
        }

        //Relationship Commercial Type 1 : * Estimate Headers
        public List<EstimateHeader> EstimateHeaders { get; set; }

        //Commercial Type Id
        [Display(Name = "Commercial Type Id")]
        public int CommercialTypeId { get; set; }

        //Commercial Type Name
        [Required]
        [Display(Name = "Commercial type Name")]
        [StringLength(50, ErrorMessage = "Commercial type name must be less than 50 characters")]
        [Remote("CommercialTypeNameUnique","Validation", AdditionalFields ="CommercialTypeId") ]
        public string CommercialTypeStr { get; set; }

        //Is Active
        public bool IsActive { get; set; }

        //create Date
        [Display(Name ="Created Date")]
        public DateTime? CreatedDate { get; set; }
        
        //Created By
        [Display(Name ="Created By")]
        public string CreatedBy { get; set; }

        //Modified Date
        [Display(Name ="Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        //modified By
        [Display(Name ="Modified By")]
        public string ModifiedBy { get; set; }

    }
}