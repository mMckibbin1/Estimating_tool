using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model
{
    public class CustomerProjects
    {
        //Customer Id
        public int CustomerID{ get; set; }

        //Project Id
        [Display(Name = "Project ID")]
        public int ProjectId { get; set; }

        //Project Name
        [Display(Name = "Project Name")]
        public string projectName { get; set; }
        
        //Atlas Id
        [Display(Name = "Atlas ID")]
        public string ProAtlasID { get; set; }
		[Display(Name = "Rate")]

		public decimal Rate { get; set; } 
    }
}