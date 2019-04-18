using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class TypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Types
        public ActionResult Index(int? page, string searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;
            int pageSize = 2; // display three blog posts at a time on this page
            int pageNumber = (page ?? 1);
            var typeQuery = db.Types.OrderBy(p => p.RestaurantType).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                typeQuery = typeQuery.Where(p => p.RestaurantType.Contains(searchQuery)
                || p.Comments.Contains(searchQuery)
                || p.UserId.Contains(searchQuery));
            }
            var type = typeQuery.ToPagedList(pageNumber, pageSize);
            return View(type);
        }

        // GET: Types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: Types/Create
        public ActionResult Create()
        {
            ViewBag.NameId = new SelectList(db.Names, "Id", "RestaurantName");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RestaurantType,Price,Comments,Ratings,NameId,UserId")] Models.Type type)
        {
            if (ModelState.IsValid)
            {
                db.Types.Add(type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NameId = new SelectList(db.Names, "Id", "RestaurantName", type.NameId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", type.UserId);
            return View(type);
        }

        // GET: Types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            ViewBag.NameId = new SelectList(db.Names, "Id", "RestaurantName", type.NameId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", type.UserId);
            return View(type);
        }

        // POST: Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RestaurantType,Price,Comments,Ratings,NameId,UserId")] Models.Type type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NameId = new SelectList(db.Names, "Id", "RestaurantName", type.NameId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", type.UserId);
            return View(type);
        }

        // GET: Types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Type type = db.Types.Find(id);
            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Type type = db.Types.Find(id);
            db.Types.Remove(type);
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
