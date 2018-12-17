using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using lab1.Models;

namespace lab1.Controllers
{
    public class PlayersController : Controller
    {
        private SportContext db = new SportContext();

        /// <summary>
        /// Gets the teams in which the player plays
        /// </summary>
        /// <param name="playerId">The player id</param>
        /// <returns></returns>
        private List<Team> GetPlayerTeams(int playerId)
        {
            var tbl = from con in db.Contracts
                      join tm in db.Teams on con.TeamId equals tm.TeamId
                      where con.PlayerId == playerId
                      select tm;
            return tbl.ToList();
        }

        // GET: Players
        public ActionResult Index()
        {           
            var model = db.Players.ToList().Select(pl => new DisplayPlayerModel
            {
                Player = pl,
                TeamNames = GetPlayerTeams(pl.PlayerId).Select(t => t.Name)
            }).ToList();
            return View(model);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            var model = new AddAndEditPlayerModel();
            model.AllTeams = db.Teams.ToList();
            return View(model);
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create(AddAndEditPlayerModel model)
        {
            if (model.FirstName != null && model.LastName != null)
            {
                var player = new Player();
                player.FirstName = model.FirstName;
                player.LastName = model.LastName;
                player = db.Players.Add(player);                
                if (model.SelctedTeamIds != null)
                {
                    var contracts = model.SelctedTeamIds.Select(idTeam => new Contract
                    {
                        TeamId = idTeam,
                        PlayerId = model.PlayerId
                    });
                    db.Contracts.AddRange(contracts);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            AddAndEditPlayerModel model = new AddAndEditPlayerModel(player);
            model.PlayerId = (int)id;
            model.SelctedTeamIds = GetPlayerTeams(player.PlayerId).Select(t => t.TeamId).ToList();
            model.AllTeams = db.Teams.ToList();
            return View(model);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddAndEditPlayerModel player)
        {
            var p = db.Players.Find(player.PlayerId);
            p.FirstName = player.FirstName;
            p.LastName = player.LastName;
            var con = db.Contracts.Select(c => c).Where(c => c.PlayerId == p.PlayerId);
            db.Contracts.RemoveRange(con);
            var newCon = player.SelctedTeamIds.Select(idTeam => new Contract { TeamId = idTeam, PlayerId = p.PlayerId });
            db.Contracts.AddRange(newCon);
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Player player = db.Players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Player player = db.Players.Find(id);
            db.Players.Remove(player);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
