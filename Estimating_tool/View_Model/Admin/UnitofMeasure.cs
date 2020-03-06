using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Estimating_Tool.View_Model.Admin
{
    public class UnitofMeasure
    {
        public int UnitOfMeasureId { get; set; }
        [Display(Name ="Unit of Measure")]
        public string UnitOfMeasureStr { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
    }
}
