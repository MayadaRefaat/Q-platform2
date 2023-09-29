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
    public class TeachersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teachers
        [AllowAnonymous]
        public ActionResult Index()
        {
            var teachers = db.teachers.ToList();
            var imgs = db.teacherimgs.ToList();
            var viewmodel = new List<teacherviewmodel>();
            foreach (var item in teachers)
            {
                foreach (var itemm in imgs)
                {
                    if (item.teacherid==itemm.teacherid)
                    {
                        var models = new teacherviewmodel();

                        models.teacher = item;
                        models.img = itemm;
                        viewmodel.Add(models);
                    }
                }
            }

            if (User.IsInRole("can_mange"))
            {
                return View(viewmodel);

            }
            else
            {
                return View("Index - Copy", viewmodel);

            }

        }

        // GET: Teachers/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.teachers.Find(id);
            var imgs = db.teacherimgs.ToList() ;
            var img =new teacherimg();
            foreach (var item in imgs)
            {
                if (item.teacherid==id)
                {
                    img = item;
                }
            }


            var subjectcoures = db.subjectcoures.Include(s => s.coursescategories).Include(s => s.teacher);
            var courses = subjectcoures.ToList();
            var teachercoures = new List<subjectcoure>();
            foreach (var item in courses)
            {
                if (item.teacherid==id)
                {
                    teachercoures.Add(item);
                }
            }
            var imgofcourses = db.subjectcoureimg.ToList();
            var viewmodelcourses = new List<subjectcourseviewmodel>();

            foreach (var item in teachercoures)
            {
                foreach (var itemm in imgofcourses)
                {
                    if (item.subjectcourseid == itemm.subjectcoureid)
                    {
                        var models = new subjectcourseviewmodel();

                        models.subjectcoure = item;
                        models.img = itemm;
                        viewmodelcourses.Add(models);
                    }

                }
            }



            var viewmodel = new teacherviewmodel
            {

                teacher=teacher,
                img=img,
                courses=viewmodelcourses
            };


            if (teacher == null)
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

        // GET: Teachers/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "teacherid,name,info,subject")] Teacher teacher, HttpPostedFileBase files)
        {
            if (ModelState.IsValid)
            {
                db.teachers.Add(teacher);
                db.SaveChanges();
                if (files != null)
                {
                    string pic = teacher.teacherid + Path.GetFileName(files.FileName);
                    string path = Path.Combine(Server.MapPath("~/Im" + "ages/teacher"), pic);
                    // file is uploaded
                    files.SaveAs(path);
                    string x = "/Images/teacher/" + pic;
                    teacherimg photo = new teacherimg();
                    photo.photoulr = x;
                    photo.teacherid = teacher.teacherid;
                    db.teacherimgs.Add(photo);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "teacherid,name,info,subject")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.teachers.Find(id);
            db.teachers.Remove(teacher);
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
