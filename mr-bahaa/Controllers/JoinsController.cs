using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mr_bahaa.Models;
using Microsoft.AspNet.Identity;

namespace mr_bahaa.Controllers
{
    [Authorize]
    public class JoinsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Joins
        [Authorize(Roles = "can_mange")]
        public ActionResult Index()
        {
            var joins = db.Joins.Include(j => j.Student).Include(j => j.subjectcoure);
            return View(joins.ToList());
        }

        // GET: Joins/Details/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joins joins = db.Joins.Find(id);
            if (joins == null)
            {
                return HttpNotFound();
            }
            return View(joins);
        }

        // GET: Joins/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create( int id)
        {
            var student = db.students.Find(id);
            var joins = new Joins
            {
                Student=student
            };
            ViewBag.Studentid = new SelectList(db.students, "id", "studentname", joins.Studentid);
            ViewBag.subjectcoureid = new SelectList(db.subjectcoures, "subjectcourseid", "name", joins.subjectcoureid);
            return View(joins);
        }

        // POST: Joins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "Joinsid,Studentid,subjectcoureid,apporved")] Joins joins ,Student student)
        {
            var stu = db.students.Find(student.id);
            var course = db.subjectcoures.Find(joins.subjectcoureid);
            joins.Student = stu;
            joins.subjectcoure = course;

            
                db.Joins.Add(joins);
                db.SaveChanges();
                return RedirectToAction("courses", "STudents", new { id = student.id });


            ViewBag.Studentid = new SelectList(db.students, "id", "studentname", joins.Studentid);
            ViewBag.subjectcoureid = new SelectList(db.subjectcoures, "subjectcourseid", "name", joins.subjectcoureid);
            return View(joins);
        }

         

        // GET: Joins/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joins joins = db.Joins.Find(id);
            if (joins == null)
            {
                return HttpNotFound();
            }
            ViewBag.Studentid = new SelectList(db.students, "id", "studentname", joins.Studentid);
            ViewBag.subjectcoureid = new SelectList(db.subjectcoures, "subjectcourseid", "name", joins.subjectcoureid);
            return View(joins);
        }

        // POST: Joins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "Joinsid,Studentid,subjectcoureid,apporved")] Joins joins)
        {
            if (ModelState.IsValid)
            {
                db.Entry(joins).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Studentid = new SelectList(db.students, "id", "studentname", joins.Studentid);
            ViewBag.subjectcoureid = new SelectList(db.subjectcoures, "subjectcourseid", "name", joins.subjectcoureid);
            return View(joins);
        }

        // GET: Joins/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Joins joins = db.Joins.Find(id);
            if (joins == null)
            {
                return HttpNotFound();
            }
            return View(joins);
        }

        // POST: Joins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult DeleteConfirmed(int id)
        {
            Joins joins = db.Joins.Find(id);
            db.Joins.Remove(joins);
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
