using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model.Admin
{
    public class LineItemType
    {
        public int LineItemTypeId { get; set; }
        public string LineItemStr { get; set; }
        public bool IsActive { get; set; }
    }
}