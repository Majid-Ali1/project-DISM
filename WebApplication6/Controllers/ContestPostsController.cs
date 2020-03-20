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
    public class ContestPostsController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: ContestPosts
        public ActionResult Index()
        {
            var contestPosts = db.ContestPosts.Include(c => c.Contest).Include(c => c.User);
            return View(contestPosts.ToList());
        }

        // GET: ContestPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestPost contestPost = db.ContestPosts.Find(id);
            if (contestPost == null)
            {
                return HttpNotFound();
            }
            return View(contestPost);
        }

        // GET: ContestPosts/Create
        public ActionResult Create()
        {
            ViewBag.ContestId = new SelectList(db.Contests, "ContestId", "Title");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name");
            return View();
        }

        // POST: ContestPosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContestPostId,Recipe,SubmitedAt,ContestId,UserId")] ContestPost contestPost)
        {
            if (ModelState.IsValid)
            {
                db.ContestPosts.Add(contestPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContestId = new SelectList(db.Contests, "ContestId", "Title", contestPost.ContestId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", contestPost.UserId);
            return View(contestPost);
        }

        // GET: ContestPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestPost contestPost = db.ContestPosts.Find(id);
            if (contestPost == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContestId = new SelectList(db.Contests, "ContestId", "Title", contestPost.ContestId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", contestPost.UserId);
            return View(contestPost);
        }

        // POST: ContestPosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContestPostId,Recipe,SubmitedAt,ContestId,UserId")] ContestPost contestPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contestPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContestId = new SelectList(db.Contests, "ContestId", "Title", contestPost.ContestId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "Name", contestPost.UserId);
            return View(contestPost);
        }

        // GET: ContestPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContestPost contestPost = db.ContestPosts.Find(id);
            if (contestPost == null)
            {
                return HttpNotFound();
            }
            return View(contestPost);
        }

        // POST: ContestPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContestPost contestPost = db.ContestPosts.Find(id);
            db.ContestPosts.Remove(contestPost);
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
