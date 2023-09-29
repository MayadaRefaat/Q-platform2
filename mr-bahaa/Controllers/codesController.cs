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
    public class codesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: codes
        public ActionResult Index()
        {
            var codes = db.codes.Include(c => c.lecture);
            return View(codes.ToList());
        }

        // GET: codes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            codes codes = db.codes.Find(id);
            if (codes == null)
            {
                return HttpNotFound();
            }
            return View(codes);
        }

        // GET: codes/Create
        public ActionResult Create()
        {
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");
            return View();
        }

        // POST: codes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "codeid,lectureid,ipaddress,code")] codes codes)
        {
            if (ModelState.IsValid)
            {
                db.codes.Add(codes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", codes.lectureid);
            return View(codes);
        }

        // GET: codes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            codes codes = db.codes.Find(id);
            if (codes == null)
            {
                return HttpNotFound();
            }
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", codes.lectureid);
            return View(codes);
        }

        // POST: codes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "codeid,lectureid,ipaddress,code")] codes codes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", codes.lectureid);
            return View(codes);
        }

        // GET: codes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            codes codes = db.codes.Find(id);
            if (codes == null)
            {
                return HttpNotFound();
            }
            return View(codes);
        }

        // POST: codes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            codes codes = db.codes.Find(id);
            db.codes.Remove(codes);
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
