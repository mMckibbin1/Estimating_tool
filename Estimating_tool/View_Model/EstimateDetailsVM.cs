using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Estimating_Tool.DAL;

namespace Estimating_Tool.View_Model
{
    public class EstimateDetailsVM
    {

        [Required]
        [Display(Name = "Estimate Header Name")]
        public int EstimateHeaderId { get; set; }
        [Display(Name = "Line Item Type")]
        public int LineItemId { get; set; }
        public decimal Estimate { get; set; }
        public string Note { get; set; }

        public List<SelectListItem> EstimateNameList { get; set; }
        public List<SelectListItem> LineItemTypeList { get; set; }

        public EstimateDetailsVM()
        {
            Estimatingcontext db = new Estimatingcontext();

            EstimateNameList = db.EstimateHeader.Select(x => new SelectListItem { Text = x.EstimateName, Value = x.EstimateHeaderId.ToString() }).ToList();
            LineItemTypeList = db.LineItemType.Select(x => new SelectListItem { Text = x.LineItemTypeStr, Value = x.LineItemTypeId.ToString() }).ToList();
        }

}   }

