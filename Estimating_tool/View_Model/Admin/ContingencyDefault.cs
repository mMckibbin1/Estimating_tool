using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Estimating_Tool.View_Model.Admin
{
    public class ContingencyDefault
    {
        public int ContingencyDefaultId { get; set; }
        [Display(Name = "Contingency Default Number")]
        public int ContingencyDefaultInt { get; set; }
        [Display(Name = "IS Active")]
        public bool IsActive { get; set; }
    }
}