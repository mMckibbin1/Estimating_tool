using Estimating_Tool.DAL;
using Estimating_Tool.Models;
using Estimating_Tool.View_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Estimating_Tool.Controllers
{
	public class TeamController : Controller
    {
        private Estimatingcontext db = new Estimatingcontext();

        // GET: Team
        public ActionResult Index()
        {

            List<TeamIndex> teams = new List<TeamIndex>();
            List<Manager> Managers = db.Managers.ToList();
            foreach (Team team in db.Teams)
            {
                TeamIndex teamIndex = new TeamIndex();
                teamIndex.team = team;

                List<Manager> tempManager = Managers.Where(x => x.Id == teamIndex.team.ManagerId).ToList();
                if (teamIndex.team.ManagerId == null)
                { }
                else
                {
                    teamIndex.ManagerName = tempManager[0].Firstname + " " + tempManager[0].Lastname;
                }
                teams.Add(teamIndex);


            }

            return PartialView(teams);
        }

        public ActionResult ToList()
        {
            Session["ActiveTab"] = "Tab3";
            return RedirectToAction(@"..\Admin\Index");
        }

        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            TeamEdit newTeam = new TeamEdit();
            newTeam.Managers = new Dictionary<string, int>();
            foreach (Manager manager in db.Managers)
            {
                string managerName = manager.Firstname + " " + manager.Lastname;

                newTeam.Managers.Add(managerName.ToString(), (int)manager.Id);
            }
            return View(newTeam);
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeamName,ManagerId")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(team);
                db.SaveChanges();
                TempData["TeamMessage"] = "Team Created successfully";
                return RedirectToAction(@"..\Admin\Index");
            }

            return View(team);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamEdit team = new TeamEdit();
            team.Managers = new Dictionary<string, int>();
            team.Team = db.Teams.Find(id);
            //Team team = db.Teams.Find(id);
            foreach (Manager manager in db.Managers)
            {
                string managerName = manager.Firstname + " " + manager.Lastname;
                team.Managers.Add(managerName.ToString(), (int)manager.Id);
            }

            if (team == null)
            {
                return HttpNotFound();
            }

            List<Manager> man = db.Managers.Where(m => m.Username.ToLower().ToString() == User.Identity.Name.ToLower().ToString()).ToList();
            team.Manager = man[0].Id.ToString();
            return View(team);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeamName,ManagerId")] Team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                Session["ActiveTab"] = "Tab3";
                TempData["TeamMessage"] = "Team Updated successfully";
                return RedirectToAction(@"..\Admin\Index");
            }
            return View(team);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team team = db.Teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }

            var man = db.Managers.Where(x => x.Id == team.ManagerId).ToList();
            TeamEdit teamToDelete = new TeamEdit();
            teamToDelete.Team = team;
            teamToDelete.Managers = new Dictionary<string, int>();
            teamToDelete.Manager = man[0].Firstname.ToString() + " " + man[0].Lastname.ToString();

            return View(teamToDelete);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Team team = db.Teams.Find(id);
            db.Teams.Remove(team);
            db.SaveChanges();
            Session["ActiveTab"] = "Tab3";
            TempData["TeamMessage"] = "Team Deleted successfully";
            return RedirectToAction(@"..\Admin\Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
