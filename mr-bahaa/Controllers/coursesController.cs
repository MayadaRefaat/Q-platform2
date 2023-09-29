using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mr_bahaa.Models;
using mr_bahaa.viewmodel;

namespace mr_bahaa.Controllers
{
    [Authorize(Roles = "can_mange")]
    public class coursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: courses

        public ActionResult Index()
        {
            return View(db.courses.ToList());
        }



        // GET: courses/Details/5
        public ActionResult Details(int? id)
        {
            var list = db.students.ToList();
            List<Student> student = new List<Student>();
            foreach (var item in list)
            {
                if (item.courseid==id)
                {
                    student.Add(item);
                }

            }
            ViewBag.students = student;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            var viewmodel = new courseviewmodel
            {
                course = course,
                students = student
            };
            return View(viewmodel);
        }
        public ActionResult add(int  courseid)
        {
            var course = db.courses.Find(courseid);
            ViewBag.course = course;
            var student = db.students.ToList();
            var courseviewmodel = new courseviewmodel
            {
                course=course,
                students=student
            };

            return View(courseviewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult add([Bind(Include = "courseid,id")] Student student)
        {

            var studentt = db.students.Find(student.id);
            studentt.courseid = student.courseid;
               
                db.SaveChanges();
                return RedirectToAction("Details", "courses", new { id = student.courseid });
            

            
            
        }



        public ActionResult addtocourse(int courseid)
        {
            course course = db.courses.Find(courseid);

            ViewBag.subjectcoureid = new SelectList(db.subjectcoures, "subjectcourseid", "name");

            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult addtocourse(course course, Joins joins)
        {
            var subjectcourse = db.subjectcoures.Find(joins.subjectcoureid);
            var students = new List<Student>();
            var listofstudents = db.students.ToList();

            foreach (var item in listofstudents)
            {

                if (item.courseid==course.courseid)
                {
                    students.Add(item);
                }
            }
            var joinstodatabase = new List<Joins>();
            foreach (var item in students)
            {
                var join = new Joins
                {
                    Student=item,
                    subjectcoure=subjectcourse,
                    apporved=true

                };
                joinstodatabase.Add(join);

            }
            db.Joins.AddRange(joinstodatabase);

           
            db.SaveChanges();
            return RedirectToAction("index");




        }

        public ActionResult change(int id)
        {
            var student = db.students.Find(id);
            var courses = db.courses.ToList();
            var viewmodel = new studentviewmodel
            {
                student = student,
                courses = courses
            };

            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult change([Bind(Include = "id,courseid")] Student student)
        {

            var studentt = db.students.Find(student.id);
            studentt.courseid = student.courseid;

            db.SaveChanges();
            return RedirectToAction("Details", "courses", new { id = student.courseid });




        }

        // GET: courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "courseid,day,time")] course course)
        {
            if (ModelState.IsValid)
            {
                db.courses.Add(course);
                db.SaveChanges();
                var examscontrol = db.exams.ToList();
                foreach (var item in examscontrol)
                {
                    var control = new examviewcontrol();
                    control.courseid = course.courseid;
                    control.examid = item.examid;
                    control.view = false;
                    db.examviewcontrols.Add(control);
                }
                var lecs = db.lectures.ToList();
                foreach (var item in lecs)
                {
                    var leccontrol = new lecturecontrolview();
                    leccontrol.courseid = course.courseid;
                    leccontrol.lectureid = item.lectureid;
                    leccontrol.view = false;
                    db.lecturecontrolviews.Add(leccontrol);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }

        // GET: courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "courseid,day,time")] course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            course course = db.courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            course course = db.courses.Find(id);
            var students = db.students.ToList();
            var deletestudents = new List<Student>();
            foreach (var item in students)
            {
                if (item.courseid==id)
                {
                    deletestudents.Add(item);
                }
            }

            var users = db.Users.ToList();
            users.RemoveAll(x => !deletestudents.Any(y => y.userid == x.Id));
            foreach (var item in users)
            {
                db.Users.Remove(item);
            }

            db.courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult trans()
        {
            ViewBag.courseidfrom = new SelectList(db.courses, "courseid", "day");
            ViewBag.courseidto = new SelectList(db.courses, "courseid", "day");



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult trans(int courseidfrom, int courseidto)
        {

            var list = db.students.ToList();
            foreach (var item in list)
            {
                if (item.courseid == courseidfrom)
                {
                    item.courseid = courseidto;
                }

            }
            db.SaveChanges();

            return RedirectToAction("Details", "courses", new { id = courseidto });

        }
        public ActionResult attendance()
        {
            ViewBag.courseid = new SelectList(db.courses, "courseid", "day");
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult attendance(int courseid , int lectureid)
        {

            var course = db.courses.Find(courseid);
            var lecture = db.lectures.Find(lectureid);
            var students = new List<Student>();
            foreach (var item in db.students)
            {
                if (item.courseid == courseid)
                {
                    students.Add(item);
                }
            }
            var attendance = new List<Absence>();
            foreach (var itemmm in students)
            {
                foreach (var item in db.absences)
                {
                    if (item.Studentid ==itemmm.id&&item.lectureid==lectureid)
                    {
                        attendance.Add(item);
                    }
                }
            }
            return View("attendancelist", attendance);


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
