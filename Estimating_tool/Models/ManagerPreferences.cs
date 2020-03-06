using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Estimating_Tool.Models
{
		public class ManagerPreferences
		{
			//Default values
			public int Id { get; set; }
			public int ManagerId { get; set; }
			public bool Customer { get; set; }
			public bool Project { get; set; }





			//Optional fields

			
			public ManagerPreferences()
			{

				//Core scoring areas will always be visible

				Project = true;
				Customer = true;
				
			}
		}
	}