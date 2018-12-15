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

        // GET: Players
        public ActionResult Index()
        {

            //var l = from pl in db.Players
            //        join con in db.Contracts on pl.PlayerId equals con.PlayerId 
            //        join tm in db.Teams on con.TeamId equals tm.TeamId into eg
            //        from b in eg.DefaultIfEmpty()                
            //        //from t in db.Teams
            //        select new PlayerWithTeam
            //        {
            //            PlayerId = pl.PlayerId,
            //            FirstName = pl.FirstName,
            //            LastName = pl.LastName,
            //            TeamName = b.Name
            //            //TeamName = pl.PlayerId == con.PlayerId && t.TeamId == con.TeamId ? t.Name : "",
            //        };

            var players = new List<ShowPlayerModel>();
            foreach (var player in db.Players)
            {
                var modelItem = new ShowPlayerModel();
                modelItem.Player = player;
                var tbl = from con in db.Contracts
                          join tm in db.Teams on con.TeamId equals tm.TeamId
                          where con.PlayerId == player.PlayerId
                          select new SelectListItem
                          {
                              Text = tm.Name
                          };
                modelItem.Teams = tbl.ToList();
                players.Add(modelItem);
            }
            //return View(db.Players.ToList());
            return View(players);
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            var model = new AddPlayerModel();
            model.AllTeams = new List<SelectListItem>();
            foreach (var team in db.Teams)
            {
                var selecterItem = new SelectListItem()
                {
                    Text = team.Name,
                    Value = team.TeamId.ToString()
                };
                model.AllTeams.Add(selecterItem);
            }
            return View(model);
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PlayerId,FirstName,LastName")] Player player)
        public ActionResult Create(AddPlayerModel player)
        {
            if (ModelState.IsValid)
            {
                var pl = new Player();
                //pl.PlayerId = player.PlayerId;
                pl.FirstName = player.FirstName;
                pl.LastName = player.LastName;
                db.Players.Add(pl);
                if (player.SelctedTeams != null)
                {
                    foreach (var team in player.SelctedTeams)
                    {
                        var con = new Contract();
                        con.PlayerId = player.PlayerId;
                        con.TeamId = int.Parse(team);
                        db.Contracts.Add(con);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(player);
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
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlayerId,FirstName,LastName")] Player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(player);
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
