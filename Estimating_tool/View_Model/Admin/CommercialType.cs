using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model.Admin
{
    public class CommercialType
    {
        public int CommercialTypeid { get; set; }
        [Display(Name = "Commercial Type Name")]
        public string CommercialtypeStr { get; set; }
        [Display(Name = "IS Active")]
        public bool IsActive { get; set; }
    }
}