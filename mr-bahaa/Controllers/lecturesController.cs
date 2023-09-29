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
using System.Net.NetworkInformation;

namespace mr_bahaa.Controllers
{
    [Authorize]
    public class lecturesController : Controller
    {





        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: lectures
        [Authorize(Roles = "can_mange")]
        public ActionResult Index()
        {
            if (User.IsInRole("can_mange")) {
                return View("Index",db.lectures.ToList());

            }
            else
            {
                string currentUserId = User.Identity.GetUserId();
                var user = db.Users.Find(currentUserId);
                var students = db.students.ToList();
                Student student = new Student();
                foreach (var item in students)
                {
                    if (item.userid==currentUserId)
                    {
                        student = item;
                    }
                }
                var lectures = db.lectures.ToList();
                
                var lectuercontrol = db.lecturecontrolviews.ToList();
                var viewlic = new List<lecture>();
                foreach (var item in lectures)
                {
                    foreach (var x in lectuercontrol)
                    {
                        if ((item.lectureid==x.lectureid&&x.view==true))
                        {
                            viewlic.Add(item);
                        }
                    }
                }

                return View("readonly", viewlic);

            }

        }


        // GET: lectures/Details/5
        public ActionResult Details(int? id)
        {





            // get current student 
            string currentUserIdd = User.Identity.GetUserId();
            Student student = db.students.FirstOrDefault(M => M.userid == currentUserIdd);

           
            lecture lecture = db.lectures.Find(id);



            bool examed = false;

            if (!(User.IsInRole("can_mange")))
            {
                //cheack if is in role
                var course = db.subjectcoures.Find(lecture.subjectid);

                var courses = db.Joins.ToList();
                
              
                if (courses.Any(m => m.apporved == true && m.subjectcoureid == course.subjectcourseid && m.Studentid == student.id))
                {
                    return View("natapprovd");
                }

                // homework first
                if (lecture.secexamid!=0)
                {
                    var studenthomework = db.lecturehomework.FirstOrDefault(m=>m.examid==lecture.secexamid &&m.Studentid==student.id && m.allow==true);
                

                    if (studenthomework.lecturehomeworkid == 0)
                    {
                        exam exam = db.exams.Find(lecture.secexamid);

                        return View("homwrorkfirst", exam);
                    }
                }
               


                //cheack if the student finish the exam

                if (lecture.examid != 0)
                {
                    examed = db.examescore.Any(m=>m.studentid==student.userid && m.examid==lecture.examid);
                  

                    if (examed == false&&lecture.homework==false)
                    {
                        exam exam = db.exams.Find(lecture.examid);
                        return View("examfirst", exam);
                    }
                }
                //cheack if the lecture locks
                var lecturelock = db.lecturelock.FirstOrDefault(m=>m.lectureid==lecture.lectureid && m.studentid==student.userid);
                
                if (lecturelock.lectureid==0)
                {
                    lecturelock.studentid = student.userid;
                    lecturelock.lectureid = lecture.lectureid;
                    DateTime now = DateTime.Now;
                    lecturelock.entered = now;
                    db.lecturelock.Add(lecturelock);
                    db.SaveChanges();

                }//if lecture is live 
                else if(!(lecture.name.Contains("live")))
                {
                    DateTime Now = DateTime.Now;
                    if (Now.DayOfYear-lecturelock.entered.DayOfYear>7)
                    {
                        return View("timeout");
                    }


                }

                //cheack ip address
                var studentip = db.studentips.FirstOrDefault(m=>m.lectureid==lecture.lectureid && m.studentid==student.userid);
               

                HttpCookie myCookie = Request.Cookies["cookieName"];
                HttpCookie myCookie2 = Request.Cookies["__RequestVerificationToken"];
                HttpCookie myCookie3 = Request.Cookies[".AspNet.ApplicationCookie"];
                HttpCookie myCookie4 = Request.Cookies["ASP.NET_SessionId"];
                HttpCookie myCookie5 = Request.Cookies["Name"];

             

                string sMacAddress = Request.UserHostAddress;
                string userAgent = Request.UserAgent;


                string macAddress = string.Empty;
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddress += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }

                if (studentip.studentipid == 0)
                {
                   

                    studentip.studentid = student.userid;
                    studentip.lectureid = lecture.lectureid;
                    studentip.ipaddress = userAgent;
                    db.studentips.Add(studentip);
                    db.SaveChanges();

                }


                //if lecture is live 
                else if (!(lecture.name.Contains("live")))
                {

                    if (studentip.ipaddress!= userAgent)
                    {
                        return View("ipnotapp");
                    }


                }






            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<photolec> gggg = new List<photolec>();

            List<photolec> photo = db.photolecs.Where(m => m.lecid == id).ToList();
            List<photolec> imgs = photo.Where(m=>m.type.Contains("image")).ToList();
            List<photolec> vids = photo.Where(m => m.type.Contains("video")).ToList();
            List<photolec> auds = photo.Where(m => m.type.Contains("audio")).ToList();
            List<photolec> pdf = photo.Where(m => m.type.Contains("application/pdf")).ToList();
         
            var link = (lecture.text).Split(',');
            List<string> iframe = new List<string>();
            iframe.AddRange(link);
            lectureviewmodel viewmodel = new lectureviewmodel
            {
                lecture = lecture,
                img=imgs,
                vid =vids,
                aud=auds,
                pdf=pdf,
                iframe=iframe
            };
            if (lecture == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("can_mange"))
            {
                return View("Details", viewmodel);

            }
            else
            {

               
                
                var absensen = db.absences.ToList();
                var control = false;
                foreach (var item in absensen)
                {
                    if (item.Studentid==student.id&&item.lectureid==lecture.lectureid)
                    {
                        control = true;
                        break;
                    }
                }
                if (! db.absences.Any(m=>m.Studentid==student.id && m.lectureid== id))
                {
                    var absense = new Absence(); ;
                    absense.lecture = lecture;
                    absense.Student = student;
                    absense.Presence = true;
                    db.absences.Add(absense);
                    db.SaveChanges();
                }
	
               

               
                
                if ((lecture.name).Contains("live"))
                {
                    return View("live", viewmodel);

                }
                else if(lecture.homework==true&&examed==false)
                {
                    return View("youtube - Copy", viewmodel);
                }
                else
                {
                    return View("youtube", viewmodel);

                }

            }
        }

        // GET: lectures/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create(int? id)
        {

            var listodexams = db.exams.ToList();
            var exam = new exam
            {
                name = "no exam",
                examid = 0
            };
            listodexams.Add(exam);

            var exams = new SelectList(listodexams, "examid", "name")
            {
            };


            ViewBag.examid = exams;
            ViewBag.secexamid = exams;

            var lecture = new lecture();

            if (id!=null)
            {

                lecture.subjectid = id.Value;
                

            }

            return View(lecture);
        }

        // POST: lectures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "lectureid,name,date,text,examid,secexamid,homework,subjectid")] lecture lecture, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                db.lectures.Add(lecture);
                db.SaveChanges();

                List<string> url = new List<string>();
                if (files[0]!=null)
                {

                    foreach (var httpPostedFileBase in files)
                    {
                        string pic = lecture.lectureid+Path.GetFileName(httpPostedFileBase.FileName);
                        string path = Path.Combine(Server.MapPath("~/Im" + "ages/profile"), pic);
                        // file is uploaded
                        httpPostedFileBase.SaveAs(path);
                        string x = "/Images/profile/" + pic;
                        photolec photo = new photolec();
                        photo.photoUrl = x;
                        photo.lecid = lecture.lectureid;
                        photo.type = httpPostedFileBase.ContentType;
                        db.photolecs.Add(photo);


                    }
                }
                var lecturecontrolview = new lecturecontrolview();
              
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(lecture);
        }

        // GET: lectures/Edit/5    
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecture lecture = db.lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            var listodexams = db.exams.ToList();
            var exam = new exam
            {
                name = "no exam",
                examid = 0
            };
            listodexams.Add(exam);

            var exams = new SelectList(listodexams, "examid", "name");
 
            ViewBag.examid = exams;
            ViewBag.secexamid = exams;


            return View(lecture);
        }

        // POST: lectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "lectureid,name,date,text,examid,secexamid,homework,subjectid")] lecture lecture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lecture);
        }




        [Authorize(Roles = "can_mange")]
        public ActionResult Editforlive(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecture lecture = db.lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            ViewBag.examid = new SelectList(db.exams, "examid", "name");
            ViewBag.secexamid = new SelectList(db.exams, "examid", "name");


            return View("Edit - Copy", lecture);
        }

        // POST: lectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Editforlive([Bind(Include = "lectureid,name,date,text,examid,secexamid,homework,subjectid")] lecture lecture, List<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lecture).State = EntityState.Modified;
                db.SaveChanges();
                List<string> url = new List<string>();
                if (files[0] != null)
                {

                    foreach (var httpPostedFileBase in files)
                    {
                        string pic = lecture.lectureid + Path.GetFileName(httpPostedFileBase.FileName);
                        string path = Path.Combine(Server.MapPath("~/Im" + "ages/profile"), pic);
                        // file is uploaded
                        httpPostedFileBase.SaveAs(path);
                        string x = "/Images/profile/" + pic;
                        photolec photo = new photolec();
                        photo.photoUrl = x;
                        photo.lecid = lecture.lectureid;
                        photo.type = httpPostedFileBase.ContentType;
                        db.photolecs.Add(photo);


                    }
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(lecture);
        }
        // GET: lectures/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lecture lecture = db.lectures.Find(id);
            if (lecture == null)
            {
                return HttpNotFound();
            }
            return View(lecture);
        }

        // POST: lectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult DeleteConfirmed(int id)
        {
            lecture lecture = db.lectures.Find(id);
            db.lectures.Remove(lecture);
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
