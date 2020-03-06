using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using Estimating_Tool.DAL;

namespace Estimating_Tool.Models
{
    public class Project
    {
        //Relationship of Project 1 : * Estimate Header  
        public Project()
        {
            Estimatingcontext db = new Estimatingcontext();
            EstimateHeaders = new List<EstimateHeader>();
            project_Joins = new List<Consultant_Project_join>();
        }
        //Relationship of Project 1 : * Estimate Header 
        public List<EstimateHeader> EstimateHeaders { get; set; }

        //Project Id
        public int ProjectId { get; set; }

        [Display(Name = "Atlas Id")]
        [StringLength(20, ErrorMessage = "Atlas Id must be less than 20 characters")]
        public string AtlasID { get; set; }

        //project Name
        [Display(Name ="Project Name")]
        [Required] 
        [StringLength(100, ErrorMessage = "Project Name must be less than 100 characters")]
        public string ProjectName { get; set; }

        //Currency linked from currency table
        public Currency currency { get; set; }
        
        [Required]
        
        public int CurrencyId { get; set; }

		[Required]
		public decimal Rate { get; set; }

        //Created Date
        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        //Created By
        [Display(Name = "Created By")]
        [StringLength(30, ErrorMessage = "Created By must be less than 30 characters")]
        public string CreatedBy { get; set; }

        //Modified Date
        [DataType(DataType.Date)]
        [Display(Name = "Modified Date")]
        public DateTime? ModifiedDate { get; set; }

        //Modified By
        [Display(Name = "Modified By")]
        [StringLength(30, ErrorMessage = "Modified by must be less than 30 characters")]
        public string ModifiedBy { get; set; }

        //Object of customer
        public Customer customer { get; set; }
		public Consultant consultant { get; set; }

        //Customer Id
        [Display(Name = "Customer Id")]
        public int CustomerID{ get; set; }

        //Active
        public bool? IsActive { get; set; }
        public List<Consultant_Project_join> project_Joins { get; set; }
        //public Consultant_Project_join consultant { get; set; }



    }
}