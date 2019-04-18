using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant.Controllers
{
    public class NamesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Names
        public ActionResult Index(int? page, string searchQuery)
        {
            ViewBag.SearchQuery = searchQuery;
            int pageSize = 2;// display three blog posts at a time on this page
            int pageNumber = (page ?? 1);
            var nameQuery = db.Names.OrderBy(p => p.RestaurantName).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchQuery))
                nameQuery = nameQuery.Where(p => p.RestaurantName.Contains(searchQuery)
                || p.Information.Contains(searchQuery)
                || p.UserId.Contains(searchQuery));
        }
        var name = nameQuery.ToPagedList(pageNumber, pageSize);
            return View(name);
    }

    // GET: Names/Details/5
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Name name = db.Names.Find(id);
        if (name == null)
        {
            return HttpNotFound();
        }
        return View(name);
    }

    // GET: Names/Create
    public ActionResult Create()
    {
        ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
        return View();
    }

    // POST: Names/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,RestaurantName,Information,UserId")] Name name)
    {
        if (ModelState.IsValid)
        {
            db.Names.Add(name);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.UserId = new SelectList(db.Users, "Id", "Email", name.UserId);
        return View(name);
    }

    // GET: Names/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Name name = db.Names.Find(id);
        if (name == null)
        {
            return HttpNotFound();
        }
        ViewBag.UserId = new SelectList(db.Users, "Id", "Email", name.UserId);
        return View(name);
    }

    // POST: Names/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,RestaurantName,Information,UserId")] Name name)
    {
        if (ModelState.IsValid)
        {
            db.Entry(name).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.UserId = new SelectList(db.Users, "Id", "Email", name.UserId);
        return View(name);
    }

    // GET: Names/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Name name = db.Names.Find(id);
        if (name == null)
        {
            return HttpNotFound();
        }
        return View(name);
    }

    // POST: Names/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Name name = db.Names.Find(id);
        db.Names.Remove(name);
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