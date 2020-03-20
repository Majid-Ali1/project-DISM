using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class RecipesController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Recipes
        public ActionResult Index()
        {
            var recipies = db.Recipies.Include(r => r.Category);
            return View(recipies.ToList());
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipies.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecipeId,Title,Description,recipeImage,CategoryId")] Recipe recipe,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imgFile.FileName);
                    string imgPath = Path.Combine(Server.MapPath("~/images/"), fileName);
                    imgFile.SaveAs(imgPath);
                    recipe.recipeImage = fileName;
                }
                catch
                {
                }

                db.Recipies.Add(recipe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipies.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecipeId,Title,Description,recipeImage,CategoryId")] Recipe recipe,HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string oldImage = recipe.recipeImage;

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imgFile.FileName);
                    string imgPath = Path.Combine(Server.MapPath("~/images/recipe"), fileName);
                    imgFile.SaveAs(imgPath);
                    recipe.recipeImage = fileName;

                    string fullPath = Request.MapPath("~/images/recipe" + oldImage);
                    if(System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                catch 
                {
                }

                db.Entry(recipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", recipe.CategoryId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipies.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recipe recipe = db.Recipies.Find(id);
            db.Recipies.Remove(recipe);
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
