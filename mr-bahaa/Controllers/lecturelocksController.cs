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
    public class lecturelocksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: lecturelocks
        public ActionResult Index()
        {
            var lecturelock = db.lecturelock.Include(l => l.lecture);
            return View(lecturelock.ToList());
        }

        // GET: lecturelocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecturelock lecturelock = db.lecturelock.Find(id);
            if (lecturelock == null)
            {
                return HttpNotFound();
            }
            return View(lecturelock);
        }

        // GET: lecturelocks/Create
        public ActionResult Create()
        {
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");
            return View();
        }

        // POST: lecturelocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "lecturelockid,lectureid,studentid,entered")] lecturelock lecturelock)
        {
            if (ModelState.IsValid)
            {
                db.lecturelock.Add(lecturelock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", lecturelock.lectureid);
            return View(lecturelock);
        }

        // GET: lecturelocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecturelock lecturelock = db.lecturelock.Find(id);
            if (lecturelock == null)
            {
                return HttpNotFound();
            }
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", lecturelock.lectureid);
            return View(lecturelock);
        }

        // POST: lecturelocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "lecturelockid,lectureid,studentid,entered")] lecturelock lecturelock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecturelock).State = EntityState.Modified;
                db.SaveChanges();
                Student student = new Student();
                foreach (var item in db.students)
                {
                    if (item.userid == lecturelock.studentid)
                    {
                        student = item;
                    }
                }
                return RedirectToAction("lecturelock", "Students", new { id = student.id });
            }
           

            return View(lecturelock);
        }

        // GET: lecturelocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecturelock lecturelock = db.lecturelock.Find(id);
            if (lecturelock == null)
            {
                return HttpNotFound();
            }
            return View(lecturelock);
        }

        // POST: lecturelocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lecturelock lecturelock = db.lecturelock.Find(id);
            db.lecturelock.Remove(lecturelock);
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
