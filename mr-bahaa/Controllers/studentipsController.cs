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
    public class studentipsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: studentips
        public ActionResult Index()
        {
            var studentips = db.studentips.Include(s => s.lecture);
            return View(studentips.ToList());
        }

        // GET: studentips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentip studentip = db.studentips.Find(id);
            if (studentip == null)
            {
                return HttpNotFound();
            }
            return View(studentip);
        }

        // GET: studentips/Create
        public ActionResult Create()
        {
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");
            return View();
        }

        // POST: studentips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentipid,lectureid,studentid,ipaddress")] studentip studentip)
        {
            if (ModelState.IsValid)
            {
                db.studentips.Add(studentip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", studentip.lectureid);
            return View(studentip);
        }

        // GET: studentips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentip studentip = db.studentips.Find(id);
            if (studentip == null)
            {
                return HttpNotFound();
            }
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", studentip.lectureid);
            return View(studentip);
        }

        // POST: studentips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentipid,lectureid,studentid,ipaddress")] studentip studentip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", studentip.lectureid);
            return View(studentip);
        }

        // GET: studentips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentip studentip = db.studentips.Find(id);
            if (studentip == null)
            {
                return HttpNotFound();
            }
            return View(studentip);
        }

        // POST: studentips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentip studentip = db.studentips.Find(id);
            db.studentips.Remove(studentip);
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
