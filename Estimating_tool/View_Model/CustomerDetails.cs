using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model
{
    public class CustomerDetails
    {
        //Customer Name
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        //Atlas Id
        [Display(Name = "Atlas ID")]
        public string CusAtlasID { get; set; }

        //Customer Id
        [Display(Name = "Customer ID")]
        public int CustomerID{ get; set; }

        //Created Date
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        //Modified Date
        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }

        //Relational link to CustomerProjects vm
        public List<CustomerProjects> project { get; set; }
    }

}