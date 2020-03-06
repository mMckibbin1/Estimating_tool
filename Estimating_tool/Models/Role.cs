using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Estimating_Tool.Models
{
    public class Role
    {
        //Role Id
        [Display(Name = "Role Id")]
        public int Id { get; set; }

        //Role Name
        [Display(Name = "Role Name")]
        [StringLength(50, ErrorMessage = "Role name should be less than 50 characters")]
        [Required]
        [Remote("RoleNameValidation","Validation",AdditionalFields ="Id")]
        public string RoleName { get; set; }

        //Is Active
        [Display(Name = "Is Active")]
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