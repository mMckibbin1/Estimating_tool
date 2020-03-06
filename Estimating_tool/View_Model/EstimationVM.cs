using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Estimating_Tool.Models;
using Estimating_Tool.DAL;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Estimating_Tool.View_Model
{
    public class ParentEstimation
    {
        public ParentEstimation()
        {
            Estimatingcontext db = new Estimatingcontext();
            estimationVMs = new List<EstimationVM>();
            groups = db.LineItemTypeGroup.ToList();
            SpecificTasks = new List<EstimationVM>();
        }
        public List<EstimationVM> estimationVMs { get; set; }
        public List<LineItemTypeGroup> groups { get; set; }
        public List<EstimationVM> SpecificTasks { get; set; }

    }
    public class EstimationVM
    {
        public int Id { get; set; }
        public int LineItemId { get; set; }
        public int LineItemTypeGroupId { get; set; }
        public int LineItemTypeId { get; set; }
        public int EstimateHeaderId { get; set; }
        //[Remote("DurationCheck", "Validation")]
        [RegularExpression("^[0-9]*(\\.[0-9]{1,2})?$",ErrorMessage ="Numbers only")]
        public decimal? Duration { get; set; }
        //public decimal Rate { get; set; }
        public LineItem LineItem { get; set; }
        public LineItemTypeGroup group { get; set; }
        public LineItemType LineItemType { get; set; }
        public string Note { get; set; }
        public int? ContingencyDefault { get; set; }
        public decimal Rate { get; set; }

        public EstimateHeader estimateHeader { get; set; }

    }
}