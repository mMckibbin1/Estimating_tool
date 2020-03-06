using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Estimating_Tool.Models;
using Estimating_Tool.DAL;

namespace Estimating_Tool.View_Model
{
    public class ProjectVM
    {
        public int ProjectId { get; set; }

        [Remote("AtlasIDProjectValidation", "Validation", AdditionalFields = "ProjectId")]
        [Display(Name = "Atlas IDs")]
        public string AtlasID { get; set; }

        [Required]
        [Remote("ProjectNameValidation", "Validation", AdditionalFields = "ProjectId")]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Remote("CustomerNameValidation", "Validation")]
        [Display(Name = "Customer Name")]
        public int CustomerID { get; set; }

        [Required]
        [Display(Name = "Currency Type")]
        public int CurrencyId { get; set; }
		public decimal Rate { get; set; }
        public IEnumerable<int> SelectedConsultantIds { get; set; }
        public IEnumerable<Consultant> Consultants { get; set; }
        public MultiSelectList selectListItems { get; set; }

        public List<SelectListItem> CustomerNameList { get; set; }
        public List<SelectListItem> CurrencyNameList { get; set; }

        public ProjectVM()
        {
            Estimatingcontext db = new Estimatingcontext();
            CustomerNameList = db.Customer.Where(x => x.IsActive == true).Select(x => new SelectListItem {Text= x.CustomerName, Value= x.CustomerID.ToString() }).ToList();
            CurrencyNameList = db.Currency.Where(x => x.IsActive == true).Select(x => new SelectListItem { Text = x.CurrencyName, Value = x.CurrencyId.ToString() }).ToList();
            Consultants = db.Consultants;
            SelectedConsultantIds = new List<int>();
            selectListItems = new MultiSelectList(Consultants, "Id", "Username", Consultants.Select(x => x.Firstname));

        }

    }
}