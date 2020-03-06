using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Estimating_Tool.DAL;
using System.Web.Helpers;

namespace Estimating_Tool.Controllers
{
	public class AdminController : Controller
    {
        Parser parser = new Parser();

        //called only from the main Nav Bar and so sets the first tab as the active tab instead of the last tab visited by user
        public ActionResult NavIndex()
        {
            Session["ActiveTab"] = "Tab1";
            int errors = parser.CheckForAssignedManagers();
            if (errors > 0)
            {
                ViewBag.ManagersAssigned = "There are Team(s) / Consultant(s) with no manager assigned!";
            }
            return View("Index");
        }


        public ActionResult Index()
        {
            int errors = parser.CheckForAssignedManagers();
            if (errors > 0)
            {
                ViewBag.ManagersAssigned = "There are Team(s) / Consultant(s) with no manager assigned!";
            }
            return View();
        }

        public ActionResult LoadIPCs()
        {
            return PartialView();
        }


        //[System.Web.Http.HttpPost]
        //public ActionResult FromDatabase(IPCKPIAppV2.Models.ViewModels.DatesForQuery dates)
        //{

        //    //var startDate = dates.StartDate;
        //    //var endDate = dates.EndDate;
        //    LandeskAccess lan = new LandeskAccess();
        //    DataTable data = lan.GetData();

        //    List<string> consultantsFromDLandesk = new List<string>();
        //    List<string> teamsFromLandesk = new List<string>();

        //    List<IPC> IPCs = new List<IPC>();
        //    foreach (DataRow row in data.Rows)
        //    {
        //        consultantsFromDLandesk.Add(row["LastAnalyst"].ToString());
        //        teamsFromLandesk.Add(row["AssignedTeam"].ToString());

        //        IPCs.Add(parser.RowConvert(row));
        //    }
        //    parser.AddMissingTeamsAndConsultants(consultantsFromDLandesk, teamsFromLandesk);
        //    parser.FixTablesAndAdd(IPCs);

        //    return View("~/Views/Admin/Index.cshtml");
        //}
        ////Called by the Admin Index page
        ////used to upload a local .csv so that the data can be added to the database.
        //[System.Web.Http.HttpPost]
        public ActionResult UploadFile()
        {
            //ViewBag.fileUpdateStatus = "";
            string errorMessage;
            foreach (string file in Request.Files)
            {

                if (Request.Files[file] == null)
                {
                    errorMessage = "No FIle transfered";
                    Session["ActiveTab"] = "Tab1";
                    return RedirectToAction(@"..\Admin\Index");
                }
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/SeedData/");
                    Directory.CreateDirectory(folderPath);
                    string savedFileName = Server.MapPath("~/SeedData/uploadedData.csv");
                    hpf.SaveAs(savedFileName);
                }
            }
            //Check to see if the stored file exists. (parser.ConvertFile deletes the file after importing it to prevent duplicates being imported) 
            //Also checks to see if the table being uploaded has the correct amount of columns and if Consultant names exist in the Consultant database 

            if (System.IO.File.Exists(ConfigurationManager.AppSettings["uploadedCSV"]))
            {
                errorMessage = parser.ConvertFile(ConfigurationManager.AppSettings["uploadedCSV"]);
            }
            else
            {
                errorMessage = "IPCs not uploaded - please specify a valid file to upload";
            }

            if (errorMessage.Length != 0 && errorMessage == "Invalid file - incorrect number of columns!")
            {
                TempData["UploadMessage"] = "Invalid file - incorrect number of columns!";

            }
            else if (errorMessage.Length != 0 && errorMessage == "IPCs not uploaded - please specify a valid file to upload")
            {
                TempData["UploadMessage"] = "IPCs not uploaded - please specify a valid file to upload";
            }
            else
            {
                TempData["UploadMessage"] = "File uploaded";

            }
            int errors = parser.CheckForAssignedManagers();
            if (errors > 0)
            {
                ViewBag.ManagersAssigned = "There are Team(s) / Consultant(s) with no manager assigned!";
            }
            Session["ActiveTab"] = "Tab1";
            return RedirectToAction(@"..\Admin\Index");
        }
    }
}
