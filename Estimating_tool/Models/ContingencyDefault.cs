using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class ContingencyDefault
    {
        //Relationship Contingency Default 1 : * Estimate Header
        public ContingencyDefault()
        {
            EstimateHeaders = new List<EstimateHeader>();
        }

        //Relationship Contingency Default 1 : * Estimate Header
        public List<EstimateHeader> EstimateHeaders { get; set; }

        //Contingency Default Id
        [Display(Name = "Contingency Default Id")]
        public int ContingencyDefaultId { get; set; }

        //Contingency Default Amount %
        [Display(Name = " Contingency Amount %")]
        [Range(0,100, ErrorMessage = "Contingency Amount can only be between 0 and 100")]
        [Required]
        [Remote("ContingencyDefaultValidation","Validation",AdditionalFields ="ContingencyDefaultId")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Whole Numbers only")]
        public int ContingencyDefaultInt { get; set; }

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