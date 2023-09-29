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
using Microsoft.AspNet.Identity.EntityFramework;
using mr_bahaa.Models;
using mr_bahaa.viewmodel;

namespace mr_bahaa.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        [Authorize(Roles = "can_mange")]
        public ActionResult Index()
        {

            var students = db.students.ToList();
            return View(students.ToList());
        }

        // GET: Students/Details/5
   

        [Authorize(Roles = "can_mange")]
        public ActionResult att(int Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.students.Find(Id);

            string pro = " ";
           
           
            var studentabsenses = db.absences.Where(m=>m.Studentid==Id);
          
            var abviewmodel = new List<absenseviewmodel>();
            foreach (var item in studentabsenses)
            {
                var x = new absenseviewmodel();
                x.name = db.lectures.Find(item.lectureid).name;
                x.Presence = item.Presence;
                abviewmodel.Add(x);
            }

            string currentUserId = student.userid;

          

            var viewmodel = new scoreviewmodel
            {
                student = student,
                img = pro,
                absenseviewmodels = abviewmodel

            };
            if (student == null)
            {
                return HttpNotFound();
            }
            return View("myatt", viewmodel);

        }

        [Authorize(Roles = "can_mange")]
        public ActionResult scores(int Id)
        {

           

            Student student = db.students.Find(Id);
            List<quesscore> quesscores = new List<quesscore>();


            var examscores = db.examescore.ToList();

            foreach (var item in examscores)
            {
                if (item.studentid == student.userid)
                {
                    if (!(db.exams.Find(item.examid) == null))
                    {
                        var score = new quesscore();
                        score.name = db.exams.Find(item.examid).name;
                        score.score = item.degree;
                        score.from = item.total;
                        quesscores.Add(score);
                    }

                }
            }
            var viewmodel = new scoreviewmodel
            {
                student = student,

                quesscore = quesscores,


            };
            if (student == null)
            {
                return HttpNotFound();
            }

            return View("mydegree", viewmodel);



        }

        [Authorize(Roles = "can_mange")]
        public ActionResult courses(int id)
        {

            var studentcourses = db.Joins.Where(m=>m.Studentid==id).ToList();
           
            var student = db.students.Find(id);
            var viewmodel = new studentcoursesviewmodel
            {
                Joins=studentcourses,
                student=student
            };
            return View(viewmodel);
        }

        [Authorize(Roles = "can_mange")]
        public ActionResult lecturelock(int id)
        {
            var student = db.students.Find(id);

            var listoflocks = db.lecturelock.Where(m => m.studentid == student.userid).ToList();
           

            var viewmodel = new locksviewmodel
            {
                locks = listoflocks,
                Student = student
            };
            return View(viewmodel);
        }

        [Authorize(Roles = "can_mange")]
        public ActionResult studenip(int id)
        {
            var student = db.students.Find(id);

            var listofstudentips= db.studentips.Where(m => m.studentid == student.userid).ToList();

            var viewmodel = new studentipviewmodel
            {
                ip = listofstudentips,
                Student = student
            };
            return View(viewmodel);
        }

       

        [Authorize]
        public ActionResult myhomework()
        {

            string currentUserId = User.Identity.GetUserId();
            Student student = db.students.FirstOrDefault(m => m.userid == currentUserId);

            var home = db.homeworks.ToList();

            var studenthome = db.homeworks.Where(m=>m.studentid==currentUserId).ToList();



            var viewmodel = new scoreviewmodel
            {
                student = student,
              
                homeworks = studenthome

            };
            if (student == null)
            {
                return HttpNotFound();
            }
         
            else
            {
                return View("homework", viewmodel);

            }

        }

        [Authorize]
        public ActionResult myatt()
        {
            string currentUserId = User.Identity.GetUserId();
            Student student = db.students.FirstOrDefault(m => m.userid == currentUserId);
           
            var studentabsenses = db.absences.Where(m=>m.Studentid==student.id).ToList();
          
            var abviewmodel = new List<absenseviewmodel>();
            foreach (var item in studentabsenses)
            {
                var x = new absenseviewmodel();
                x.name = db.lectures.Find(item.lectureid).name;
                x.Presence = item.Presence;
                abviewmodel.Add(x);
            }



            var viewmodel = new scoreviewmodel
            {
                student = student,

                absenseviewmodels = abviewmodel

            };
            if (student == null)
            {
                return HttpNotFound();
            }

            return View("myatt", viewmodel);



        }

        [Authorize]
        public ActionResult mydegree()
        {

            string currentUserId = User.Identity.GetUserId();
            Student student = db.students.FirstOrDefault(m => m.userid == currentUserId);

            List<quesscore> quesscores = new List<quesscore>();


            var examscores = db.examescore.ToList();

            foreach (var item in examscores)
            {
                if (item.studentid == currentUserId)
                {
                    if (!(db.exams.Find(item.examid) == null))
                    {
                        var score = new quesscore();
                        score.name = db.exams.Find(item.examid).name;
                        score.score = item.degree;
                        score.from = item.total;
                        quesscores.Add(score);
                    }

                }
            }
            var viewmodel = new scoreviewmodel
            {
                student = student,

                quesscore = quesscores,


            };
            if (student == null)
            {
                return HttpNotFound();
            }

            return View("mydegree", viewmodel);



        }

        [Authorize]
        public ActionResult mycourses()
        {

            string currentUserId = User.Identity.GetUserId();
            Student student = db.students.FirstOrDefault(m => m.userid == currentUserId);

            var courses = db.Joins.ToList();
            var studentjoins = db.Joins.Include(s=>s.subjectcoure).Where(m=>m.Studentid==student.id && m.apporved==true).ToList();
          
            var subs = db.subjectcoures.ToList();

            var studentcourses = new List<subjectcoure>();
            foreach (var item in studentjoins)
            {
                var x = db.subjectcoures.Find(item.subjectcoureid);
                
                studentcourses.Add(x);
            }


            var img = db.subjectcoureimg.ToList();
            var viewmodel = new List<subjectcourseviewmodel>();

            foreach (var item in studentcourses)
            {
                foreach (var itemm in img)
                {
                    if (item.subjectcourseid == itemm.subjectcoureid)
                    {
                        var models = new subjectcourseviewmodel();

                        models.subjectcoure = item;
                        models.img = itemm;
                        viewmodel.Add(models);
                    }

                }
            }


            return View(viewmodel);



        }


  




        // GET: Students/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "can_mange")]
        public ActionResult accept(string id)
        {

            ApplicationUser user = db.Users.Find(id);
           

            ViewBag.day = user.Appointment;
            ViewBag.hour = user.Yourclass;


            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult accept(course course,string id)
        {
           
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

           

            ApplicationUser user = db.Users.Find(id);
            UserManager.AddToRole(user.Id, "Students");
            Student student = new Student();
            student.studentname = user.UserName;
            student.school = user.School;
            student.Email = user.Email;
            student.PhoneNumber = user.PhoneNumber;
            student.secPhoneNumber = user.secphonenumber;
            student.userid = user.Id;
            var students=db.students.ToList();
            var x = true;
            foreach (var item in students)
            {
                if (item.Email==student.Email)
                {
                    x = false;
                }
            }
            if (ModelState.IsValid&&x)
            {
                db.students.Add(student);
                db.SaveChanges();

                return RedirectToAction("Index","Students");
            }

            return RedirectToAction("Index", "Students");
        }
        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "id,studentname,PhoneNumber,school,courseid")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "id,studentname,PhoneNumber,secPhoneNumber,school,courseid,Email,userid")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.students.Find(id);
            var user = db.Users.Find(student.userid);
            db.students.Remove(student);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult changephoto(int id)
        {
            var student = db.students.Find(id);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult changephoto(int id, HttpPostedFileBase files)
        {
            var student = db.students.Find(id);

            var photo = new photo();
            foreach (var item in db.photos)
            {
                if (item.userid==(student.Email+ "profile"))
                {
                    photo = item;
                }
            }

            string pic = student.Email + "pro" + Path.GetFileName(files.FileName);
            string path = Path.Combine(Server.MapPath("~/Im" + "ages/profile"), pic);
            files.SaveAs(path);
            photo.photoUrl = "/Images/profile/" + pic;
            db.Entry(photo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Details", "Students", new { id = student.id });




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
