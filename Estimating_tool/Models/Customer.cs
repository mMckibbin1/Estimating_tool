using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Estimating_Tool.Models
{
    public class Customer
    { 
        //Relationship Customer 1 : * Estimate Headers
        public Customer()
        {
            projects = new List<Project>();
        }

        //Relationship Customer 1 : * Estimate Headers
        public List<Project> projects { get; set; }

        //Customer Id
        [Key]
        [Display(Name ="Customer ID")]
        public int CustomerID{ get; set; }

        //Atlas Id
        [Display(Name = "Atlas ID")]
        [Remote("CustomerAtlasValidation","Validation",AdditionalFields ="CustomerID")]
        public string AtlasID { get; set; }

        //Customer Name
        [Required]
        [Display(Name = "Customer Name")]
        [Remote("CustomerNamesValidation", "Validation", AdditionalFields = "CustomerID")]
        public string CustomerName { get; set; }

        //Created Date
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        [Range(typeof(DateTime), "1/1/2019", "1/1/2050")] //Date range until 2050
        public DateTime? CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string CreatedBy { get; set; }

        //Modified Date
        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        [Range(typeof(DateTime), "1/1/2019", "1/1/2050")] //Date range until 2050
        public DateTime? ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Commercial type name must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        //Is Active
        public bool? IsActive { get; set; }        
    }
}