using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Estimating_Tool.Models
{
	public class Team
	{
		public int Id { get; set; }
		[DisplayName("Team Name")]
		public string TeamName { get; set; }
		public int? ManagerId { get; set; }
		public virtual ICollection<Consultant> Consultants { get; set; }
	}
}