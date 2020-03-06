using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Estimating_Tool.Models
{
	public class Manager
	{
		//need to add a regular expression to include v1\\ at the start of every entry
		public int Id { get; set; }
		[Required]
		[Display(Name = "Username")]
		[DefaultValue("V1\\")]
		[StringLength(30, MinimumLength = 6)]
		public string Username { get; set; }
		[Required]
		[Display(Name = "First name")]
		[StringLength(30, MinimumLength = 2)]
		public string Firstname { get; set; }
		[Required]
		[Display(Name = "Last name")]
		[StringLength(30, MinimumLength = 2)]
		public string Lastname { get; set; }

		public virtual ICollection<Team> Teams { get; set; }
		[NotMapped]
		public virtual ICollection<Consultant> Consultants { get; set; }
	}
}