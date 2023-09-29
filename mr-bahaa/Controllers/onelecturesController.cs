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
using Microsoft.AspNet.Identity;

namespace mr_bahaa.Controllers
{
    public class onelecturesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: onelectures
        [AllowAnonymous]
        public ActionResult Index()
        {
            var onelectures = db.onelectures.Include(o => o.lecture).ToList();
            List<photolec> photo = db.photolecs.ToList();

            var viewmodel=onelectures.Join(photo,o=>o.lectureid,p=>p.lecid,(o,p)=> new onelectureimage { onelecture=o,photolec=p}).ToList();


            if (User.IsInRole("can_mange"))
            {
                return View(viewmodel);

            }
            else
            {
                return View("readonly", viewmodel);

            }
        }

        public ActionResult view(int? id)
        {
            var onelecture = db.onelectures.Find(id);
            return View(onelecture);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult view(onelecture onelecture,int codes)
        {


           
            lecture lecture = db.lectures.Find(onelecture.lectureid);

            if (!(User.IsInRole("can_mange")))
            {

                var allcodes = db.codes.ToList();
                var lecturecodes = new List<codes>();
                foreach (var item in allcodes)
                {
                    if (item.lectureid==lecture.lectureid)
                    {
                        lecturecodes.Add(item);
                    }
                }
                var lectuecode = new codes();
                foreach (var item in lecturecodes)
                {
                    if (item.code==codes&&item.lectureid==lecture.lectureid)
                    {
                        lectuecode = item;
                        break;
                    }
                }
               // string sMacAddress = Request.UserHostAddress;
                string sMacAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(sMacAddress))
                {
                    sMacAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }




                if (lectuecode.codeid== 0)
                {

                    return RedirectToAction("view","onelectures",new {id=onelecture.oneid });


                }
                else
                {
                    if (lectuecode.ipaddress==null)
                    {
                        lectuecode.ipaddress = sMacAddress;
                        db.Entry(lectuecode).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else if(lectuecode.ipaddress!=sMacAddress)
                    {
                       
                    }
                }
            }


            
            List<photolec> gggg = new List<photolec>();

            List<photolec> photo = db.photolecs.ToList();
            foreach (var item in photo)
            {
                if (item.lecid == lecture.lectureid)
                {
                    gggg.Add(item);
                }
            }
            List<photolec> imgs = new List<photolec>();
            List<photolec> vids = new List<photolec>();
            List<photolec> auds = new List<photolec>();
            List<photolec> pdf = new List<photolec>();
            foreach (var item in gggg)
            {
                if (item.type.Contains("image"))
                {
                    imgs.Add(item);

                }
                else if (item.type.Contains("video"))
                {
                    vids.Add(item);
                }
                else if (item.type.Contains("audio"))
                {
                    auds.Add(item);
                }
                else if (item.type.Contains("application/pdf"))
                {
                    pdf.Add(item);
                }

            }
            var link = (lecture.text).Split(',');
            List<string> iframe = new List<string>();
            iframe.AddRange(link);
            lectureviewmodel viewmodel = new lectureviewmodel
            {
                lecture = lecture,
                img = imgs,
                vid = vids,
                aud = auds,
                pdf = pdf,
                iframe = iframe
            };
            if (lecture == null)
            {
                return HttpNotFound();
            }
            

            return View("youtube",viewmodel);
        }


        // GET: onelectures/Details/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            onelecture onelecture = db.onelectures.Find(id);
            var lecture = db.lectures.Find(onelecture.lectureid);
            onelecture.lecture = lecture;
            if (onelecture == null)
            {
                return HttpNotFound();
            }
            var codes = db.codes.ToList();
            var list = new List<codes>();
            foreach (var item in codes)
            {
                if (item.lectureid==onelecture.lectureid)
                {
                    list.Add(item);
                }
            }
            var viewmodel = new onelectureviewmodel
            {
                onelecture=onelecture,
                codes= list
            };
            
            return View("detailscodes", viewmodel);
        }

        [Authorize(Roles = "can_mange")]
        public ActionResult print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            onelecture onelecture = db.onelectures.Find(id);
            var lecture = db.lectures.Find(onelecture.lectureid);
            onelecture.lecture = lecture;
            if (onelecture == null)
            {
                return HttpNotFound();
            }
            var codes = db.codes.ToList();
            var list = new List<codes>();
            foreach (var item in codes)
            {
                if (item.lectureid == onelecture.lectureid)
                {
                    list.Add(item);
                }
            }
            var viewmodel = new onelectureviewmodel
            {
                onelecture = onelecture,
                codes = list
            };

            return View("detailscodes - Copy", viewmodel);
        }
        // GET: onelectures/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create()
        {
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name");
            return View();
        }

        // POST: onelectures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "oneid,lectureid")] onelecture onelecture ,int codes)
        {
            if (ModelState.IsValid)
            {

                var list = new List<int>();
                var listofcodes = new List<codes>();

                var Random = new Random();
                for (int i = 0; i < codes; i++)
                {
                    var randomcode = Random.Next(100000, 999999);
                    if (list.Contains(randomcode))
                    {
                        i--;
                    }
                    else
                    {
                        list.Add(randomcode);
                    }

                }
                foreach (var item in list)
                {
                    var code = new codes();
                    code.lectureid = onelecture.lectureid;
                    code.lecture = onelecture.lecture;
                    code.code = item;
                    listofcodes.Add(code);
                }
                db.codes.AddRange(listofcodes);
                db.onelectures.Add(onelecture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", onelecture.lectureid);
            return View(onelecture);
        }

        // GET: onelectures/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            onelecture onelecture = db.onelectures.Find(id);
            if (onelecture == null)
            {
                return HttpNotFound();
            }
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", onelecture.lectureid);
            return View(onelecture);
        }

        // POST: onelectures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "oneid,lectureid")] onelecture onelecture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(onelecture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.lectureid = new SelectList(db.lectures, "lectureid", "name", onelecture.lectureid);
            return View(onelecture);
        }

        // GET: onelectures/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            onelecture onelecture = db.onelectures.Find(id);
            if (onelecture == null)
            {
                return HttpNotFound();
            }
            return View(onelecture);
        }

        // POST: onelectures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]

        public ActionResult DeleteConfirmed(int id)
        {
            onelecture onelecture = db.onelectures.Find(id);
            var codes = db.codes.ToList();
            foreach (var item in codes)
            {
                if (item.lectureid == onelecture.lectureid)
                {
                    db.codes.Remove(item);
                }
            }
            db.onelectures.Remove(onelecture);
           
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
