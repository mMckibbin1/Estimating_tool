using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Estimating_Tool.Models;

namespace Estimating_Tool.View_Model
{
    public class EstimationViewModelS
    {
        public ParentEstimation ParentEstimation { get; set; }
        public EstimateHeader EstimateHeader { get; set; }

        public EstimateHeaderVM EstimateHeaderVM { get; set; }

    }
}