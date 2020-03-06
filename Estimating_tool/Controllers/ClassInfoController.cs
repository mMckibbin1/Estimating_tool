using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Estimating_Tool.Models;

namespace Estimating_Tool.Controllers
{
    public class ClassInfoController : Controller
    {
        // GET: ClassInfo
        public ActionResult Index()
        {
            List<ClassInfo> data = new List<ClassInfo>();
            Random rnd = new Random();
            for (int i = 1; i < 8; i++)
            {
                data.Add(new ClassInfo() { ClassName = "Class-" + i.ToString(), Students = rnd.Next(10, 50) });
            }
            return View(data);
        }
    }
}