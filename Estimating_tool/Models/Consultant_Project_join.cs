using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estimating_Tool.Models
{
    public class Consultant_Project_join
    {
        public int Id { get; set; }
        public int ProjectId {get;set;}
        public int ConsultantId { get; set; }
        public string ConsultantUserName { get; set; }

        public Project project { get; set; }
        public Consultant Consultant { get; set; }
    }
}