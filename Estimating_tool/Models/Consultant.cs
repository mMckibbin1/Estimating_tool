using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estimating_Tool.Models
{
	public class Consultant
	{
		public int Id { get; set; }
		[Required]
		[Display(Name = "Username")]
		public string Username { get; set; }
		[Required]
		[Display(Name = "First Name")]
		public string Firstname { get; set; }
		[Required]
		[Display(Name = "Last Name")]
		public string Lastname { get; set; }
		[Display(Name = "Manager")]
		public int? ManagerId { get; set; }
		public virtual ICollection<Team> Teams { get; set; }
	}
}