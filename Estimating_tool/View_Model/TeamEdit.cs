using Estimating_Tool.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model
{
	public class TeamEdit
    {
        public Team Team { get; set; }
        [DisplayName("Manager")]
        public Dictionary<string, int> Managers { get; set; }
        public string Manager { get; set; }
    }
}