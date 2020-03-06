using Estimating_Tool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model
{
	public class ConsultantIndex
    {
        //need to pass the consultants Manager name to the view along with the consultant
        public Consultant Consultant { get; set; }
        public string Manager { get; set; }
    }
}