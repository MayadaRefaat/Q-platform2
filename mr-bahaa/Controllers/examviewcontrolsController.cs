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
    public class examviewcontrolsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: examviewcontrols
        public ActionResult Index()
        {
            var exams = db.exams.ToList();
            return View(exams);
        }

        public ActionResult show(string id)
        {
            var exams = db.exams.ToList();
            return View(exams);
        }

        // GET: examviewcontrols/Details/5
       /* public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            examviewcontrol examviewcontrol = db.examviewcontrols.Find(id);
            if (examviewcontrol == null)
            {
                return HttpNotFound();
            }
            return View(examviewcontrol);
        }*/

        // GET: examviewcontrols/Create
        public ActionResult Create()
        {
            ViewBag.courseid = new SelectList(db.courses, "courseid", "day");
            ViewBag.examid = new SelectList(db.exams, "examid", "name");
            return View();
        }

        // POST: examviewcontrols/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "controlid,examid")] examviewcontrol examviewcontrol)
        {
            if (ModelState.IsValid)
            {
                var courses = db.courses.ToList();
                foreach (var item in courses)
                {
                    
                    var examviewcontrolnew = new examviewcontrol();
                    examviewcontrolnew.examid = examviewcontrol.examid;
                    examviewcontrolnew.courseid = item.courseid;
                    examviewcontrolnew.view = false;


                    var controls = db.examviewcontrols.ToList();
                    var x = false;
                    foreach (var itemm in controls)
                    {
                        if (itemm.courseid==examviewcontrolnew.courseid&&itemm.examid==examviewcontrolnew.examid)
                        {
                            x = true;
                        }
                    }
                    if (x==false)
                    {

                        db.examviewcontrols.Add(examviewcontrolnew);
                        db.SaveChanges();

                    }
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.courseid = new SelectList(db.courses, "courseid", "day", examviewcontrol.courseid);
            ViewBag.examid = new SelectList(db.exams, "examid", "name", examviewcontrol.examid);
            return View(examviewcontrol);
        }

        public ActionResult allcourses(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var examviewcontrol = db.examviewcontrols.ToList();
            var examview = new List<examviewcontrol>();
            foreach (var item in examviewcontrol)
            {
                if (item.examid == id)
                {
                    examview.Add(item);
                }
            }



            return View(examview);
        }

        // GET: examviewcontrols/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            examviewcontrol examviewcontrol = db.examviewcontrols.Find(id);
            if (examviewcontrol == null)
            {
                return HttpNotFound();
            }
           
            return View(examviewcontrol);
        }

        // POST: examviewcontrols/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "controlid,examid,courseid,view")] examviewcontrol examviewcontrol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examviewcontrol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseid = new SelectList(db.courses, "courseid", "day", examviewcontrol.courseid);
            ViewBag.examid = new SelectList(db.exams, "examid", "name", examviewcontrol.examid);
            return View(examviewcontrol);
        }

        // GET: examviewcontrols/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            examviewcontrol examviewcontrol = db.examviewcontrols.Find(id);
            if (examviewcontrol == null)
            {
                return HttpNotFound();
            }
            return View(examviewcontrol);
        }

        // POST: examviewcontrols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            examviewcontrol examviewcontrol = db.examviewcontrols.Find(id);
            db.examviewcontrols.Remove(examviewcontrol);
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
