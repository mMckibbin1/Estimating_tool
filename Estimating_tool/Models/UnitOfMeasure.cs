using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class UnitOfMeasure
    {
        //Relationship of Unit of Measure 1 : * Estimate Header
        public UnitOfMeasure()
        {
            EstimateHeaders = new List<EstimateHeader>();
        }
        //Relationship of Unit of Measure 1 : * Estimate Header
        public List<EstimateHeader> EstimateHeaders { get; set; }

        //Unit Of Measure Id
        [Display(Name = "Unit Of Measure ID")]
        public int UnitOfMeasureId { get; set; }

        //Unit of Measure Name
        [Display(Name = "Unit of Measure name")]
        [StringLength(50, ErrorMessage = "Unit of Measure name should be less than 50 characters")]
        [Remote("UnitOfMeasureValidation","Validation",AdditionalFields = "UnitOfMeasureId")]
        [Required]
        public string UnitOfMeasureStr { get; set; }

        //Created Date
        [Display(Name = "Created Date")]
        public virtual DateTime CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Created By must be less than 30 characters")]
        public string CreatedBy { get; set; }

        //Modified Date
        [Display(Name = "ModifiedDate")]
        public virtual DateTime ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Modified By must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        //Active
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}