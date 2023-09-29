using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mr_bahaa.Models;

namespace mr_bahaa.Controllers
{
    [Authorize(Roles = "can_mange")]
    public class lecturecontrolviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: lecturecontrolviews
        public ActionResult Index()
        {
            var lectures = db.lectures.ToList();
            return View(lectures);
        }
        public ActionResult show(string id)
        {
            
           
                var lectures = db.lectures.ToList();
                return View( lectures);

           


        }
        // GET: lecturecontrolviews/Details/5
        /*  public ActionResult Details(int? id)
          {
              if (id == null)
              {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
              }
              lecturecontrolview lecturecontrolview = db.lecturecontrolviews.Find(id);
              if (lecturecontrolview == null)
              {
                  return HttpNotFound();
              }
              return View(lecturecontrolview);
          }*/

        // GET: lecturecontrolviews/Create
        public ActionResult Create()
        {
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");
            ViewBag.courseid = new SelectList(db.courses, "courseid", "day");

            return View();
        }
        public ActionResult closeall()
        {

            foreach (var item in db.lecturecontrolviews)
            {
                item.view = false;

            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // POST: lecturecontrolviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "controlid,lectureid")] lecturecontrolview lecturecontrolview)
        {
            if (ModelState.IsValid)
            {
                var courses = db.courses.ToList();
                foreach (var item in courses)
                {
                    
                    var leccontrol = new lecturecontrolview();
                    leccontrol.lectureid = lecturecontrolview.lectureid;
                    leccontrol.courseid = item.courseid;
                    leccontrol.view = false;
                    var controls = db.lecturecontrolviews.ToList();
                    var x = 0;
                    foreach (var i in controls)
                    {
                        if (leccontrol.lectureid==i.lectureid && leccontrol.courseid==i.courseid)
                        {
                            x = 1;
                        }
                    }
                    if (x==0)
                    {
                        db.lecturecontrolviews.Add(leccontrol);
                        db.SaveChanges();

                    }
                  
                }
               
                return RedirectToAction("Index");
            }

            ViewBag.courseid = new SelectList(db.courses, "courseid", "day", lecturecontrolview.courseid);
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", lecturecontrolview.lectureid);
            return View(lecturecontrolview);
        }

        public ActionResult allcourses(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lecturecontrol = db.lecturecontrolviews.ToList();
            var lectureview = new List<lecturecontrolview>();
            foreach (var item in lecturecontrol)
            {
                if (item.lectureid == id)
                {
                    lectureview.Add(item);
                }
            }



            return View(lectureview);
        }

        // GET: lecturecontrolviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lecturecontrol = db.lecturecontrolviews.Find(id);
           
           
            return View(lecturecontrol);
        }

        // POST: lecturecontrolviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "controlid,lectureid,courseid,view")] lecturecontrolview lecturecontrolview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecturecontrolview).State = EntityState.Modified;
                db.SaveChanges();
                var abbs = new List<Absence>();
                if (lecturecontrolview.view == true)
                {
                    var students = db.students.ToList();
                    var studentincours = new List<Student>();
                    foreach (var item in students)
                    {
                        if (item.courseid == lecturecontrolview.courseid)
                        {
                            studentincours.Add(item);
                        }
                    }
                    foreach (var item in studentincours)
                    {
                        int x = 0;
                        foreach (var itemm in db.absences)
                        {
                            if (itemm.lectureid == lecturecontrolview.lectureid && itemm.Studentid == item.id)
                            {
                                x = 1;
                            }

                        }
                        if (x == 0)
                        {
                            var absense = new Absence
                            {
                                lectureid = lecturecontrolview.lectureid,
                                Studentid = item.id,
                                Presence = false
                            };
                            abbs.Add(absense);
                        }

                    }


                }
                db.absences.AddRange(abbs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseid = new SelectList(db.courses, "courseid", "day", lecturecontrolview.courseid);
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", lecturecontrolview.lectureid);
            return View(lecturecontrolview);
        }

        // GET: lecturecontrolviews/Delete/5
        /* public ActionResult Delete(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             lecturecontrolview lecturecontrolview = db.lecturecontrolviews.Find(id);
             if (lecturecontrolview == null)
             {
                 return HttpNotFound();
             }
             return View(lecturecontrolview);
         }

         // POST: lecturecontrolviews/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public ActionResult DeleteConfirmed(int id)
         {
             lecturecontrolview lecturecontrolview = db.lecturecontrolviews.Find(id);
             db.lecturecontrolviews.Remove(lecturecontrolview);
             db.SaveChanges();
             return RedirectToAction("Index");
         }*/

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
