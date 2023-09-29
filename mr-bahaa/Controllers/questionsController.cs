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
    public class questionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: questions
        [Authorize(Roles = "can_mange")]

        public ActionResult Index()
        {
            return View(db.questions.ToList());
        }

        // GET: questions/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            questions questions = db.questions.Find(id);
            img img = new img();
            var imgs = db.Imgs.ToList();
            foreach (var item in imgs)
            {
                if (item.questionid==id)
                {
                    img = item;
                }
            }
            var viewmodel = new qviewmodel
            {
                questions = questions,
                img = img
            };
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(viewmodel);
        }

        // GET: questions/Create
        public ActionResult Create(int examid)
        {
            return View();
        }

        // POST: questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "questionid,question,questiongrade,first,sec,third,fourth,correct,Explanation,examid")] questions questions, HttpPostedFileBase files)
        {



            questions.first = questions.first.Trim();
            questions.sec = questions.sec.Trim();
            questions.third = questions.third.Trim();
            questions.fourth = questions.fourth.Trim();
            questions.correct = questions.correct.Trim();


            if (ModelState.IsValid)
            {
                db.questions.Add(questions);
                db.SaveChanges();
                if (files!=null)
                {
                    string pic = questions.questionid+ Path.GetFileName(files.FileName);
                    string path = Path.Combine(Server.MapPath("~/Im" + "ages/profile"), pic);
                    // file is uploaded
                    files.SaveAs(path);
                    string x = "/Images/profile/" + pic;
                    img img = new img();
                    img.imgurl = x;
                    img.questionid = questions.questionid;
                    db.Imgs.Add(img);
                    db.SaveChanges();
                }
               
                
                return RedirectToAction("Details", "exams", new { id = questions.examid });
            }

            return View(questions);
        }
        [Authorize]
        public ActionResult solve(int questionid,bool xxx)
        {
            questions quest=db.questions.Find(questionid);

            img img = new img();
            var imgs = db.Imgs.ToList();
            foreach (var item in imgs)
            {
                if (item.questionid == questionid)
                {
                    img = item;
                }
            }
            var viewmodel = new qviewmodel
            {
                questions = quest,
                img = img,
                xxx=xxx
            };


            return View(viewmodel);
        }

        // POST: questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult solve(qviewmodel qviewmodel, questions viewmodel,bool xxx)
        {
            questions question = db.questions.Find(qviewmodel.questions.questionid);

            if (User.IsInRole("Students")) {
                string currentUserId = User.Identity.GetUserId();

                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                var students = db.students.ToList();
                Student student = null;
                foreach (var item in students)
                {
                    if (item.Email == currentUser.Email)
                    {
                        student = item;
                    }
                }

                var qcontrol = db.qcontrols.ToList();
                int xx = 0;
                foreach (var item in qcontrol)
                {
                    if (item.id == student.id && item.questionid == question.questionid)
                    {
                        xx = 1;
                    }
                }


                if (xx == 1)
                {
                    if (question.correct == viewmodel.choise)
                    {
                        question.questiongrade = "correct answer";

                    }
                    else
                    {
                        question.questiongrade = "Wrong answer";


                    }

                }
                else
                {
                    if (question.correct == viewmodel.choise)
                    {
                        question.questiongrade = "correct answer";
                        var qscore = new quese_score();
                        qscore.examid = question.examid;
                        qscore.id = student.id;
                        qscore.qpoints = 1;
                        db.quese_Scores.Add(qscore);


                        db.SaveChanges();
                    }
                    else
                    {
                        question.questiongrade = "Wrong answer";
                        var qscore = new quese_score();
                        qscore.examid = question.examid;
                        qscore.id = student.id;
                        qscore.qpoints = 0;
                        db.quese_Scores.Add(qscore);
                        db.SaveChanges();

                    }
                    var control = new qcontrol();
                    control.id = student.id;
                    control.questionid = question.questionid;
                    db.qcontrols.Add(control);
                    db.SaveChanges();

                }

            }
            else
            {
                if (question.correct == viewmodel.choise)
                {
                    question.questiongrade = "correct answer";

                }
                else
                {
                    question.questiongrade = "Wrong answer";


                }
            }

           


            img img = new img();
            var imgs = db.Imgs.ToList();
            foreach (var item in imgs)
            {
                if (item.questionid == question.questionid)
                {
                    img = item;
                }
            }
            var view = new qviewmodel
            {
                questions = question,
                img = img
            };

            if (xxx == true)
            {
                return View("answar - Copy", view);
            }
            else if(xxx==false)
            {
                return View("answar", view);
            }
            else
            {
                return View("answar - Copy", view);

            }
            
        }

        public ActionResult answar(int questionid)
        {
            string currentUserId = User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var students = db.students.ToList();
            Student student = null;
            foreach (var item in students)
            {
                if (item.Email == currentUser.Email)
                {
                    student = item;
                }
            }

            questions question = db.questions.Find(questionid);


            img img = new img();
            var imgs = db.Imgs.ToList();
            foreach (var item in imgs)
            {
                if (item.questionid == question.questionid)
                {
                    img = item;
                }
            }
            var view = new qviewmodel
            {
                questions = question,
                img = img
            };

            return View("answar",view);
        }

        // GET: questions/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            questions questions = db.questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // POST: questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "questionid,question,questiongrade,first,sec,third,fourth,correct,Explanation,examid")] questions questions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "exams", new { id = questions.examid });
            }
            return View(questions);
        }

        // GET: questions/Delete/5   
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            questions questions = db.questions.Find(id);
            if (questions == null)
            {
                return HttpNotFound();
            }
            return View(questions);
        }

        // POST: questions/Delete/5   
        [Authorize(Roles = "can_mange")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            questions questions = db.questions.Find(id);
            db.questions.Remove(questions);
            db.SaveChanges();
            return RedirectToAction("Details", "exams", new { id = questions.examid });
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
