using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mr_bahaa.Models;
using System.IO;
using mr_bahaa.viewmodel;

namespace mr_bahaa.Controllers
{
    [Authorize]
    public class subjectcouresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: subjectcoures
        [AllowAnonymous]
        public ActionResult Index()
        {
            var courses = db.subjectcoures.Include(s => s.coursescategories).Include(s => s.teacher).ToList();
            


            var viewmodel = courses.Join(db.subjectcoureimg, c=>c.subjectcourseid,i=>i.subjectcoureid,(c,i) =>new subjectcourseviewmodel {subjectcoure=c,img=i }).ToList();

            
            if (User.IsInRole("can_mange"))
            {
                return View(viewmodel);

            }
            else
            {
                return View("Index - Copy", viewmodel);

            }
        }

        // GET: subjectcoures/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subjectcoure subjectcoure = db.subjectcoures.Find(id);
            var imgs = db.subjectcoureimg.ToList();
            var img = new subjectcoureimg();
            foreach (var item in imgs)
            {
                if (item.subjectcoureid==id)
                {
                    img = item;
                }
            }
            var listoflectures = db.lectures.ToList();
            var lectures = new List<lecture>();
           
                foreach (var item in listoflectures)
                {
                    if (item.subjectid == id)
                    {
                        lectures.Add(item);
                    }
                }
           
           
            var listofexams = db.exams.ToList();
            var exams = new List<exam>();
            foreach (var item in listofexams)
            {
                if (item.subjectid==id)
                {
                    exams.Add(item);
                }
            }

            var viewmodel = new subjectcourseviewmodel
            {
                subjectcoure=subjectcoure,
                img=img,
                lectures=lectures,
                exams=exams
            };


            if (subjectcoure == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("can_mange"))
            {
                return View(viewmodel);

            }
            else
            {
                return View("Details - Copy", viewmodel);

            }
        }



        public ActionResult report()
        {
            ViewBag.subjectcourseid = new SelectList(db.subjectcoures, "subjectcourseid", "name");
            ViewBag.subjectcourseid2 = new SelectList(db.subjectcoures, "subjectcourseid", "name");



            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult report(int subjectcourseid, int subjectcourseid2)
        {

             var firstcourse=new List<Joins>();
            var seccourse = new List<Joins>();


            var joins = db.Joins.Include(j => j.Student).Include(j => j.subjectcoure);

            foreach (var item in joins)
            {
                if (item.subjectcoureid==subjectcourseid&&item.apporved==true)
                {
                    firstcourse.Add(item);
                }
                else if (item.subjectcoureid == subjectcourseid2)
                {
                    seccourse.Add(item);

                }
            }
            var coursetwo = db.subjectcoures.Find(subjectcourseid2);
            var viewmodel = new List<reportresult>();
            foreach (var item in firstcourse)
            {
                var result = new reportresult();
                result.join = item;
                result.issub = false;
                ViewBag.courseone = item.subjectcoure.name;
                ViewBag.coursetwo = coursetwo.name;

                foreach (var sec in seccourse)
                {

                   
                    if (sec.Studentid==item.Studentid)
                    {
                        result.issub = sec.apporved;

                        break;
                    }
                        
                }

                viewmodel.Add(result);

            }

            return View("show",viewmodel);
        }




        // GET: subjectcoures/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create()
        {
            ViewBag.catigoriesid = new SelectList(db.coursescategories, "catigoriesid", "name");
            ViewBag.teacherid = new SelectList(db.teachers, "teacherid", "name");
            return View();
        }

        // POST: subjectcoures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "subjectcourseid,name,teacherid,catigoriesid")] subjectcoure subjectcoure, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                db.subjectcoures.Add(subjectcoure);
                db.SaveChanges();
                if (files != null)
                {
                    string pic = subjectcoure.subjectcourseid + Path.GetFileName(files.FileName);
                    string path = Path.Combine(Server.MapPath("~/Im" + "ages/courses"), pic);
                    // file is uploaded
                    files.SaveAs(path);
                    string x = "/Images/courses/" + pic;
                    subjectcoureimg photo = new subjectcoureimg();
                    photo.photoulr = x;
                    photo.subjectcoureid = subjectcoure.subjectcourseid;
                    db.subjectcoureimg.Add(photo);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }

            ViewBag.catigoriesid = new SelectList(db.coursescategories, "catigoriesid", "name", subjectcoure.catigoriesid);
            ViewBag.teacherid = new SelectList(db.teachers, "teacherid", "name", subjectcoure.teacherid);
            return View(subjectcoure);
        }

        // GET: subjectcoures/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subjectcoure subjectcoure = db.subjectcoures.Find(id);
            if (subjectcoure == null)
            {
                return HttpNotFound();
            }
            ViewBag.catigoriesid = new SelectList(db.coursescategories, "catigoriesid", "name", subjectcoure.catigoriesid);
            ViewBag.teacherid = new SelectList(db.teachers, "teacherid", "name", subjectcoure.teacherid);
            return View(subjectcoure);
        }

        // POST: subjectcoures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "subjectcourseid,name,teacherid,catigoriesid")] subjectcoure subjectcoure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subjectcoure).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.catigoriesid = new SelectList(db.coursescategories, "catigoriesid", "name", subjectcoure.catigoriesid);
            ViewBag.teacherid = new SelectList(db.teachers, "teacherid", "name", subjectcoure.teacherid);
            return View(subjectcoure);
        }

        // GET: subjectcoures/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subjectcoure subjectcoure = db.subjectcoures.Find(id);
            if (subjectcoure == null)
            {
                return HttpNotFound();
            }
            return View(subjectcoure);
        }

        // POST: subjectcoures/Delete/5
        [Authorize(Roles = "can_mange")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            subjectcoure subjectcoure = db.subjectcoures.Find(id);
            db.subjectcoures.Remove(subjectcoure);
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
