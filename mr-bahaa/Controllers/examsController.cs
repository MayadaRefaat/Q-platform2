using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class examsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: exams

       

        [Authorize(Roles = "can_mange")]
        public ActionResult Index()
        {
            if (User.IsInRole("can_mange"))
            {
                return View("Index",db.exams.ToList());
            }
            else
            {
                string currentUserId = User.Identity.GetUserId();
                var user = db.Users.Find(currentUserId);
                Student student = db.students.FirstOrDefault(M=>M.userid==currentUserId);
              
                
                return View("readonley");

            }

        }


        //homework
        public ActionResult homweork(int id, int? x, int? no, int? qeustionid)
        {
            List<questions> question = db.questions.Where(M => M.examid == id).ToList();



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exam exam = db.exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }

            questionviewmodel viewmodel = new questionviewmodel
            {
                exam = exam
            };
            string currentUserId = User.Identity.GetUserId();
            var student = db.students.FirstOrDefault(M => M.userid == currentUserId);




            var qeustions = question.Join(db.Imgs, M => M.questionid, P => P.questionid, (M, P) => new qeustioncheakpoint { question = M, img = P }).ToList();

          
           
            viewmodel.questions = qeustions;

            if (User.IsInRole("can_mange"))
            {
                return View("Details", viewmodel);
            }

            if (no==1)
            {
                ViewBag.text = "عذرا لم تصل للنسبه المطلوبه من الاجوبه الصحيحه لحل الواجب";
            }
            
                return View("homework", viewmodel);

            


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult homweork(FormCollection formCollection)
        {


            var count = formCollection.Count;
            var answars = new List<string>();
            for (int i = 1; i < count - 1; i++)
            {
                string c = Convert.ToString(i);
                answars.Add(formCollection[c]);

            }



            var examid = int.Parse(formCollection["exam.examid"]);



            string currentUserId = User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            Student student = db.students.FirstOrDefault(M => M.userid == currentUserId);

            var questions = db.questions.Where(M => M.examid == examid).ToList();
            var scores = new List<questions>();
            int degree = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                var question = questions[i];
                var choise = answars[i];




                if (question.correct == choise)
                {
                    var qscore = new questions();
                    qscore = question;
                    qscore.choise = choise;
                    degree++;
                    scores.Add(qscore);


                }
                else
                {
                    var qscore = new questions();
                    qscore = question;
                    qscore.choise = choise;
                    scores.Add(qscore);

                }









            }
            //db.quese_Scores.AddRange(scores);
            ggogleformviewmodel google = new ggogleformviewmodel
            {
                question = scores,
                degree = degree,
                total = scores.Count
            };

            var exam = db.exams.Find(examid);
            var lecturehomeworks = db.lecturehomework.ToList();
            lecturehomework homework = new lecturehomework();
            homework.Studentid = student.id;
            homework.examid = examid;
            var allow = ((degree * 100) / scores.Count);
            if (allow>exam.percentage)
            {
                homework.allow = true;

            }
            else
            {
                homework.allow = false;
             //  ViewBag.home="عذرا لم تصل الي النسبه المطلوبه لحل الواجب من "
                return RedirectToAction("homweork", "exams", new { id = exam.examid ,no=1 });


            }
            google.text = "لقد تم حل الواجب تستطيع الان الذهاب الي الحصه والدخول";
            int control = 0;
            foreach (var item in lecturehomeworks)
            {
                if (item.Studentid==student.id&&item.examid==examid)
                {
                    control = 1;
                    break;
                }
            }

            if (control == 0)
            {

                db.lecturehomework.Add(homework);
                db.SaveChanges();
            }
            foreach (var item in db.lectures)
            {

                if (item.secexamid== examid)
                {
                    ViewBag.lectureid = item.lectureid;
                }
            }

            ViewBag.score = degree;
            ViewBag.total = scores.Count;


            return View("end - Copy", google);





        }



        // GET: exams/Details/5
        // GET: exams/Details/5
        public ActionResult Details(int id, int? x, int? qeustionid)
        {
            List<questions> question = new List<questions>();
            foreach (var item in db.questions)
            {
                if (item.examid == id)
                {
                    question.Add(item);
                }
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exam exam = db.exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }

            questionviewmodel viewmodel = new questionviewmodel
            {
                exam = exam
            };
            string currentUserId = User.Identity.GetUserId();
            var student = db.students.FirstOrDefault(M => M.userid == currentUserId);

            var extime = db.examtimes.FirstOrDefault(M=>M.examid==id &&M.Studentid==student.id);

            var isentered =  extime.examid==0;
           

            var qeustions = question.Join(db.Imgs, M => M.questionid, P => P.questionid, (M, P) => new qeustioncheakpoint { question = M, img = P }).ToList();

          
            viewmodel.questions = qeustions;

            if (User.IsInRole("can_mange"))
            {
                return View("Details", viewmodel);
            }
            var course = db.subjectcoures.Find(exam.subjectid);

            var courses = db.Joins.ToList();
            var controll = false;
            foreach (var item in courses)
            {
                if (item.subjectcoureid == course.subjectcourseid && item.Studentid == student.id && item.apporved == true)
                {
                    controll = true;
                }
            }
            if (controll == false)
            {
                return View("natapprovd");
            }




            if (x == 1)
            {

                return View("readonlydetails - Copy", viewmodel);

            }

           
            // if he entered before 
            if (isentered == true)
            {
                //if he come from question


                var newtime = DateTime.Now - extime.timeodentring;
                var time = exam.timeofexam - newtime.Minutes;
                viewmodel.time = time;
                return View("readonlydetails", viewmodel);

            }
            else //if he enter first time 
            {

                var examtime = new examtime();
                examtime.examid = id;
                examtime.Studentid = student.id;
                examtime.timeodentring = DateTime.Now;
                db.examtimes.Add(examtime);
                db.SaveChanges();
                viewmodel.time = exam.timeofexam;





                return View("readonlydetails", viewmodel);

            }
            return View("readonlydetails - Copy", viewmodel);


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(FormCollection formCollection)
        {


            var count = formCollection.Count;
            var answars = new List<string>();
            for (int i = 1; i < count - 1; i++)
            {
                string c = Convert.ToString(i);
                answars.Add(formCollection[c]);

            }



            var examid = int.Parse(formCollection["exam.examid"]);



            string currentUserId = User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            var student = db.students.FirstOrDefault(M => M.userid == currentUserId);


            var questions = db.questions.Where(m=>m.examid==examid).ToList();
           
            var scores = new List<questions>();
            int degree = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                var question = questions[i];
                var choise = answars[i];




                if (question.correct == choise)
                {
                    var qscore = new questions();
                    qscore= question;
                    qscore.choise = choise;
                    degree++;
                    scores.Add(qscore);


                }
                else
                {
                    var qscore = new questions();
                    qscore = question;
                    qscore.choise = choise;
                    scores.Add(qscore);

                }









            }
            //db.quese_Scores.AddRange(scores);
            ggogleformviewmodel google = new ggogleformviewmodel
            {
                question = scores,
                degree = degree,
                total = scores.Count
            };
            examscore score = new examscore();
            score.degree = degree;
            score.examid = examid;
            score.studentid = currentUserId;
            score.total = scores.Count;
            var escores = db.examescore.ToList();
            int control=0;
            foreach (var item in escores)
            {
                if (item.examid==examid&&item.studentid==currentUserId)
                {
                    control = 1;
                    break;
                }
            }

            if (control == 0)
            {
                
                    db.examescore.Add(score);
                    db.SaveChanges();
            }
            else
            {
                google.text = " ";

            }

            foreach (var item in db.lectures)
            {

                if (item.examid == examid)
                {
                    ViewBag.lectureid = item.lectureid;
                }
            }


            return View("newasnwar",google);





        }



        // GET: exams/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create(int? id)
        {

            var exam = new exam();
            if (id!=null)
            {
                exam.subjectid = Convert.ToInt32(id);
            }
            return View(exam);
        }


        public ActionResult end()
        {
            return View();
        }
        // POST: exams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "examid,name,Date,open,timeofexam,subjectid,percentage")] exam exam)
        {
            if (ModelState.IsValid)
            {
                db.exams.Add(exam);
                db.SaveChanges();
                var lecturecontrolview = new examviewcontrol();
              
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exam);
        }

        // GET: exams/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exam exam = db.exams.Find(id);
            var listofcourses = db.subjectcoures.ToList();
            ViewBag.subjectid = new SelectList(listofcourses, "subjectcourseid", "name");

            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: exams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "examid,name,Date,open,timeofexam,subjectid,percentage")] exam exam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exam);
        }

        // GET: exams/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            exam exam = db.exams.Find(id);
            if (exam == null)
            {
                return HttpNotFound();
            }
            return View(exam);
        }

        // POST: exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult DeleteConfirmed(int id)
        {
            exam exam = db.exams.Find(id);
            db.exams.Remove(exam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }





        public ActionResult answers()
        {
            string currentUserId = User.Identity.GetUserId();
            var students = db.students.ToList();
            var student = new Student();
            foreach (var item in students)
            {
                if (item.userid == currentUserId)
                {
                    student = item;
                }
            }

            var exams = db.exams.ToList();


            var times = db.examtimes.ToList();
            var myexam = new List<exam>();
            var n = DateTime.Now;
            var naw = n.DayOfYear;
            foreach (var e in exams)
            {


                foreach (var item in times)
                {

                    if (e.examid == item.examid && item.Studentid == student.id)
                    {
                        
                            myexam.Add(e);
                        
                    }
                }

            }


            return View(myexam);
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
