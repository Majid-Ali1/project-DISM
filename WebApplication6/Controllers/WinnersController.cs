using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class WinnersController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Winners
        public ActionResult Index()
        {
            var winners = db.Winners.Include(w => w.ContestPost);
            return View(winners.ToList());
        }

        // GET: Winners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Winner winner = db.Winners.Find(id);
            if (winner == null)
            {
                return HttpNotFound();
            }
            return View(winner);
        }

        // GET: Winners/Create
        public ActionResult Create()
        {
            ViewBag.ContestPostId = new SelectList(db.ContestPosts, "ContestPostId", "Recipe");
            return View();
        }

        // POST: Winners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WinnerId,CreatedAt,ContestPostId")] Winner winner)
        {
            if (ModelState.IsValid)
            {
                db.Winners.Add(winner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContestPostId = new SelectList(db.ContestPosts, "ContestPostId", "Recipe", winner.ContestPostId);
            return View(winner);
        }

        // GET: Winners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Winner winner = db.Winners.Find(id);
            if (winner == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContestPostId = new SelectList(db.ContestPosts, "ContestPostId", "Recipe", winner.ContestPostId);
            return View(winner);
        }

        // POST: Winners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WinnerId,CreatedAt,ContestPostId")] Winner winner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(winner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContestPostId = new SelectList(db.ContestPosts, "ContestPostId", "Recipe", winner.ContestPostId);
            return View(winner);
        }

        // GET: Winners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Winner winner = db.Winners.Find(id);
            if (winner == null)
            {
                return HttpNotFound();
            }
            return View(winner);
        }

        // POST: Winners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Winner winner = db.Winners.Find(id);
            db.Winners.Remove(winner);
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
