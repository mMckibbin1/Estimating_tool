using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class Currency
    {
        //Currency Id
        [Key]
        [Display(Name = "Id")]
        [Required]
        public int CurrencyId { get; set; }

        //Currency Name
        [Display(Name = "Currency")]
        [Required]
        [StringLength(50, ErrorMessage = "Currency name should be less than 50 characters")]
        [Remote("CurrencyValidation", "Validation", AdditionalFields = "CurrencyId")]
        public string CurrencyName { get; set; }

        //Created Date
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Created By must be less than 30 characters")]
        public string CreatedBy { get; set; }

        //Modified Date
        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Modified by must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        //Active
        public bool? IsActive { get; set; }

        // One to many relationship between currency 1 : * project
        public Currency()
        {
            Projects = new List<Project> ();
        }
        public List<Project> Projects { get; set; }
    }
}