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
    [Authorize]
    public class coursescategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: coursescategories
        public ActionResult Index()
        {

            if (User.IsInRole("can_mange"))
            {
                return View(db.coursescategories.ToList());

            }
            else
            {
                return View("Index - Copy", db.coursescategories.ToList());

            }
        }

        // GET: coursescategories/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursescategories coursescategories = db.coursescategories.Find(id);

            
            var catcoures = db.subjectcoures.Include(s => s.coursescategories).Include(s => s.teacher).Where(m=>m.catigoriesid==id).ToList();
          
            var imgofcourses = db.subjectcoureimg.ToList();
            var viewmodelcourses = catcoures.Join(imgofcourses, c=>c.subjectcourseid, i=>i.subjectcoureid,
            (c,i) =>new subjectcourseviewmodel { subjectcoure=c,img=i }).ToList();


            foreach (var item in catcoures)
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
            var viewmodel = new categoriesviewmodel
            {
                coursescategories=coursescategories,
                subjectcourseviewmodel=viewmodelcourses

            };
            if (coursescategories == null)
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

        // GET: coursescategories/Create
        [Authorize(Roles = "can_mange")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: coursescategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Create([Bind(Include = "catigoriesid,name")] coursescategories coursescategories)
        {
            if (ModelState.IsValid)
            {
                db.coursescategories.Add(coursescategories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coursescategories);
        }

        // GET: coursescategories/Edit/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursescategories coursescategories = db.coursescategories.Find(id);
            if (coursescategories == null)
            {
                return HttpNotFound();
            }
            return View(coursescategories);
        }

        // POST: coursescategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult Edit([Bind(Include = "catigoriesid,name")] coursescategories coursescategories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursescategories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coursescategories);
        }

        // GET: coursescategories/Delete/5
        [Authorize(Roles = "can_mange")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            coursescategories coursescategories = db.coursescategories.Find(id);
            if (coursescategories == null)
            {
                return HttpNotFound();
            }
            return View(coursescategories);
        }

        // POST: coursescategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "can_mange")]
        public ActionResult DeleteConfirmed(int id)
        {
            coursescategories coursescategories = db.coursescategories.Find(id);
            db.coursescategories.Remove(coursescategories);
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
