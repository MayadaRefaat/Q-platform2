using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using mr_bahaa.Models;
using mr_bahaa.viewmodel;

namespace mr_bahaa.Controllers
{
    [Authorize]
    public class homeworkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: homework
        [Authorize(Roles = "can_mange")]
        public ActionResult Index()
        {
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");



            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Index(int lectureid,int courseid)
        {

            var students = db.students.ToList();
            var studentincourse = new List<Student>();

            foreach (var item in students)
            {

              
            }



            var homeworksend = db.homeworks.ToList();

            var homew = new List<homework>();
            foreach (var item in homeworksend)
            {
                foreach (var student in studentincourse)
                {
                    if (item.studentid==student.userid&&item.lectureid==lectureid)
                    {
                        homew.Add(item);
                    }

                }
            }
            var homeview = new List<homeworkviewmodel>();

            foreach (var student in studentincourse)
            {
                foreach (var work in homew)
                {
                    if (work.studentid==student.userid)
                    {
                        var viewmodel = new homeworkviewmodel();

                        viewmodel.student = student;
                        viewmodel.homework = work;
                        homeview.Add(viewmodel);

                    }

                }


            }



            return View("show",homeview);

        }




        // GET: homework/Details/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            homework homework = db.homeworks.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        [Authorize(Roles = "can_mange")]
        public ActionResult evaluate(int id)
        {
            var homework = db.homeworks.Find(id);
            var student = new Student();
            foreach (var item in db.students)
            {
                if (item.userid==homework.studentid)
                {
                    student = item;
                }
            }



            

            var imgs = new List<imghomework>();
            foreach (var x in db.imghomeworks)
            {
                if (x.homeworkid==id)
                {
                    imgs.Add(x);
                }

            }

            var home = new evluateview
            {
                student=student,
                homework=homework,
                imgs=imgs


            };


            return View(home);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]



        [Authorize(Roles = "can_mange")]
        public ActionResult addhomework() {


            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");

            return View();

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult addhomework(lecture lecture)
        {

            var homes = new List<homework>();
            var student = db.students.ToList();
            foreach (var item in student)
            {
                homework home = new homework();

                home.lectureid = lecture.lectureid;
                home.studentid = item.userid;
                home.text = "لم يتم الحل";
                home.Evaluation = "Not yet evaluated";
                homes.Add(home);

            }
            db.homeworks.AddRange(homes);
            db.SaveChanges();
            return RedirectToAction("Index");



        }
        public ActionResult Myhomework()
        {

            string currentUserId = User.Identity.GetUserId();


            var home = db.homeworks.ToList();

            var studenthome=new List<homework>();
            foreach (var item in home)
            {
                if (item.studentid==currentUserId)
                {
                    studenthome.Add(item);

                }

            }
            return View(studenthome);
        }



        // GET: homework/Create
        public ActionResult Create()
        {

            Student student = new Student();
            string currentUserId = User.Identity.GetUserId();
            foreach (var item in db.students)
            {
                if (item.userid == currentUserId)
                {
                    student = item;
                }
            }
            var courses = db.Joins.ToList();

            var studentcourses = new List<Joins>();
            foreach (var item in courses)
            {
                if (item.Studentid == student.id)
                {

                    studentcourses.Add(item);
                }
            }

            var lectures = db.lectures.ToList();
            var studentlec = new List<lecture>();
            foreach (var item in lectures)
            {
                foreach (var itemm in studentcourses)
                {
                    if (item.subjectid==itemm.subjectcoureid)
                    {
                        studentlec.Add(item);

                    }
                }
               
            }

            ViewBag.lectureid = new SelectList(studentlec,"lectureid", "name");

            
            return View();
        }

        // POST: homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int lectureid)
        {

            Student student = new Student();
            string currentUserId = User.Identity.GetUserId();
            foreach (var item in db.students)
            {
                if (item.userid == currentUserId)
                {
                    student = item;
                }
            }
            var homework = new homework
            {studentid=student.userid,
            text="تم تسليم الواجب",
            lectureid=lectureid,
            Evaluation="جاري فحص الواجب",
            };
            db.homeworks.Add(homework);
            
            db.SaveChanges();

           

            return RedirectToAction("Myhomework");

        }




        // GET: homework/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            homework homework = db.homeworks.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // POST: homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            homework homework = db.homeworks.Find(id);
            db.homeworks.Remove(homework);
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
