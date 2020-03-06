using Estimating_Tool.DAL;
using Estimating_Tool.Models;
using Estimating_Tool.View_Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Estimating_Tool.Controllers
{
	public class FeedbackController : Controller
    {
        private Estimatingcontext db = new Estimatingcontext();

        public ActionResult Index()
        {
            FeedbackConsultants consultants = new FeedbackConsultants();
            Dictionary<string, int> names = new Dictionary<string, int>();

            int id = (db.Managers
            .Where(m => m.Username.ToLower() == User.Identity.Name.ToLower())
            .Distinct()
            .ToList())[0]
            .Id;

            var query = (from c in db.Consultants
                        join m in db.Managers on c.ManagerId equals m.Id
                        where (c.ManagerId == id)
                        select new { c.Firstname, c.Lastname, c.Id }).ToList();
            foreach (var con in query)
            {
                names.Add(con.Firstname + " " + con.Lastname, con.Id);
            }
            consultants.Consultants = names;

            return View(consultants);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index(int Consultants, DateTime Month)
        //{   
        //    int Id = Consultants;
        //    FeedbackList toReturn = new FeedbackList();
        //    DateTime date = Month;
        //    List<Consultant> con = db.Consultants.Where(x => x.Id == Id).ToList();
        //    toReturn.Name = con[0].Firstname + " " + con[0].Lastname;

        //    var query = db.IPCs.Where(x => x.ConsultantId == Id
        //                                && x.ExtractedDate.Value.Month == Month.Month
        //                                && x.Excluded == false
        //                                && x.CheckComplete == true);

        //    toReturn.IPCs = query.ToList();
        //    if (toReturn.IPCs.Count() < 1)
        //    {
        //        TempData["FeedbackMessage"] = "Your search returned no results, please make another selection";
        //        return RedirectToAction("Index");
        //    }
        //    return View("Output", toReturn);
        //}

        //[HttpPost]
        //public JsonResult NotesSave(int ID, string feedback, bool feedbackGiven)
        //{
        //    string message = "";
        //    try
        //    {
        //        var id = ID;
        //        var notes = feedback;
        //        var given = feedbackGiven;

        //        var record = db.IPCs.SingleOrDefault(i => i.Id == id);
        //        if (record != null)
        //        {
        //            db.IPCs.Attach(record);
        //            db.Entry(record).State = EntityState.Modified;

        //            record.Feedback = feedback;
        //            record.FeedbackGiven = given;
        //            if (feedbackGiven)
        //            {
        //                record.FeedbackDate = DateTime.Now;
        //            }
        //            db.SaveChanges();
        //            message = "{ok}";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        message = e.Message.ToString();
        //    }
        //    return Json(message);
        //}


   //     public ActionResult Output()
   //     {
			////Filter
			//IEnumerable<FeedbackList> iPCs = db.EstimateHeader;

			//return View(iPCs.First());
   //     }
    }
}
