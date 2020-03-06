using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model.Admin
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool IsActive { get; set; }
    }
}