using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estimating_Tool.Models
{
    public class ClassInfo
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int Students { get; set; }
    }
}