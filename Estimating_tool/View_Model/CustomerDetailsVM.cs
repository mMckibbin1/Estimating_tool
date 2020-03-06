using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estimating_Tool.View_Model
{
    public class CustomerDetailsVM
    {
        /* 
         The purpose of this page is to be used to allow the page to get access to both the customers database
         and be able to join the linked projects over.
         Think of it as a link view_model
             */

        //Object of Customer Details
        public CustomerDetails CustomerDetails { get; set; }

        //Object of Customer / Projects
        public IEnumerable<CustomerProjects> CustomerProjects { get; set; }
    }
}