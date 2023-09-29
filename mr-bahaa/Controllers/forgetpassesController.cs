using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mr_bahaa.Models;

namespace mr_bahaa.Controllers
{
    [Authorize(Roles = "can_mange")]
    public class forgetpassesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: forgetpasses
        public ActionResult Index()
        {
            return View(db.forgetpass.ToList());
        }

        // GET: forgetpasses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forgetpass forgetpass = db.forgetpass.Find(id);
            if (forgetpass == null)
            {
                return HttpNotFound();
            }
            return View(forgetpass);
        }

        // GET: forgetpasses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: forgetpasses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "forgetid,studentname,school,phone,pass")] forgetpass forgetpass)
        {
            if (ModelState.IsValid)
            {
                db.forgetpass.Add(forgetpass);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(forgetpass);
        }

        // GET: forgetpasses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forgetpass forgetpass = db.forgetpass.Find(id);
            if (forgetpass == null)
            {
                return HttpNotFound();
            }
            return View(forgetpass);
        }

        // POST: forgetpasses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "forgetid,studentname,school,phone,pass")] forgetpass forgetpass)
        {
            if (ModelState.IsValid)
            {
                db.Entry(forgetpass).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(forgetpass);
        }

        // GET: forgetpasses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            forgetpass forgetpass = db.forgetpass.Find(id);
            if (forgetpass == null)
            {
                return HttpNotFound();
            }
            return View(forgetpass);
        }

        // POST: forgetpasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            forgetpass forgetpass = db.forgetpass.Find(id);
            db.forgetpass.Remove(forgetpass);
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
