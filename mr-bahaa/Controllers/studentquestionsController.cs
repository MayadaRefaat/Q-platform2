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
    public class studentquestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: studentquestions
        [Authorize]
        public ActionResult Index()
        {

            var teachers = db.teachers.ToList();
            var imgs = db.teacherimgs.ToList();
            var models = new teacherviewmodel();
            var viewmodel = new List<teacherviewmodel>();
            foreach (var item in teachers)
            {
                foreach (var itemm in imgs)
                {
                    if (item.teacherid == itemm.teacherid)
                    {
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
                return View("Index - Copy");
            }
        }
        public ActionResult teacherquestions(int? teacherid)
        {

            var questions = db.Studentquestions.ToList();
            var teacherquestion = new List<studentquestions>();
            foreach (var item in questions)
            {
                if (item.teacherid==teacherid)
                {
                    teacherquestion.Add(item);
                }
            }
            return View(teacherquestion);

        }
        public ActionResult myquestion()
        {
            string currentUserId = User.Identity.GetUserId();
           

            var question = db.Studentquestions.Where(m=>m.studentid==currentUserId).ToList();
         
            return View(question);
        }

        // GET: studentquestions/Details/5
        [Authorize(Roles = "Students,can_mange")]
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentquestions studentquestions = db.Studentquestions.Find(id);
            if (studentquestions == null)
            {
                return HttpNotFound();
            }
            var photos = db.studnetquestionphotos.ToList();
            var photo = new studnetquestionphoto();
            foreach (var item in photos)
            {
                if (item.questionid == id)
                {
                    photo = item;

                }
            }
            var imgs = db.replayphotos.ToList();
            var img=new replayphotos();
            foreach (var item in imgs)
            {
                if (item.questionid == id)
                {
                    img = item;

                }
            }
            var viewmodel = new studentquestionreply
            {
                studentquestions = studentquestions,
                studnetquestionphoto = photo,
                replayphotos=img
            };

            return View(viewmodel);
        }

        // GET: studentquestions/Create
        public ActionResult Create()
        {
            ViewBag.teacherid = new SelectList(db.teachers, "teacherid", "name");

            return View();
        }

        // POST: studentquestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string question, HttpPostedFileBase files,int teacherid)
        {
            string currentUserId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                studentquestions studentquestions = new studentquestions();
                studentquestions.studentquestion = question;
                studentquestions.studentid = currentUserId;
                studentquestions.teacherreplay = "Your question has not been answered yet";
                studentquestions.teacherid = teacherid;
                db.Studentquestions.Add(studentquestions);
                db.SaveChanges();

                if (files!=null)
                {

                    string pic = currentUserId + Path.GetFileName(files.FileName);
                    string path = Path.Combine(Server.MapPath("~/Im" + "ages/profile"), pic);
                    // file is uploaded
                    files.SaveAs(path);
                    string x = "/Images/profile/" + pic;
                    studnetquestionphoto img = new studnetquestionphoto();
                    img.imgurl = x;
                    img.questionid = studentquestions.studentquestionid;
                    img.type = files.ContentType;
                    db.studnetquestionphotos.Add(img);
                    
                }

                db.SaveChanges();
                return RedirectToAction("Index");


            }

            return View(question);
        }
        [Authorize(Roles = "can_mange")]
        public ActionResult reply(int id)
        {
            studentquestions studentquestions = db.Studentquestions.Find(id);
            var photos = db.studnetquestionphotos.ToList();
            var img = new studnetquestionphoto();
            foreach (var item in photos)
            {
                if (item.questionid==id)
                {
                    img = item;

                } 
            }
            var student = new Student();
            foreach (var item in db.students)
            {
                if (item.userid == studentquestions.studentid) {
                    student = item;

                }
            }

            var viewmodel = new studentquestionviewmodel
            {
                studentquestions=studentquestions,
                studnetquestionphoto=img,
                student=student
               
            };



            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult reply(int id,string reply, HttpPostedFileBase files)
        {
            studentquestions studentquestions = db.Studentquestions.Find(id);

            if (ModelState.IsValid)
            {
               
                studentquestions.teacherreplay = reply;
                db.Entry(studentquestions).State = EntityState.Modified;
                if (files != null)
                {

                    string pic = studentquestions.studentquestionid+Path.GetFileName(files.FileName);
                    string path = Path.Combine(Server.MapPath("~/Im" + "ages/profile"), pic);
                    // file is uploaded
                    files.SaveAs(path);
                    string x = "/Images/profile/" + pic;
                    replayphotos img = new replayphotos();
                    img.imgurl = x;
                    img.questionid = studentquestions.studentquestionid;
                    img.type = files.ContentType;
                    db.replayphotos.Add(img);

                }

                db.SaveChanges();
                return RedirectToAction("Index");


            }
            var photos = db.studnetquestionphotos.ToList();
            var imgd = db.studnetquestionphotos.SingleOrDefault(m=>m.questionid==id);
          

            var viewmodel = new studentquestionviewmodel
            {
                studentquestions = studentquestions,
                studnetquestionphoto = imgd
            };

            return View(viewmodel);
        }
        // GET: studentquestions/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentquestions studentquestions = db.Studentquestions.Find(id);
            if (studentquestions == null)
            {
                return HttpNotFound();
            }
            return View(studentquestions);
        }

        // POST: studentquestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "studentquestionid,studentquestion,teacherreplay,studentid")] studentquestions studentquestions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentquestions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentquestions);
        }

        // GET: studentquestions/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            studentquestions studentquestions = db.Studentquestions.Find(id);
            if (studentquestions == null)
            {
                return HttpNotFound();
            }
            return View(studentquestions);
        }

        // POST: studentquestions/Delete/5
        [Authorize(Roles = "can_mange")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            studentquestions studentquestions = db.Studentquestions.Find(id);
            db.Studentquestions.Remove(studentquestions);
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
