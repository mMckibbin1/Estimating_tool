using Estimating_Tool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model
{
	public class ConsultantEdit
    {
        //need to pass manager names for selection to the view along with the Consultant
        public Consultant Consultant { get; set; }
        [DisplayName("Manager")]
        public Dictionary<string, int> Managers { get; set; }
        public string Manager { get; set; }

    }
}