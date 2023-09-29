using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mr_bahaa.Models;

namespace mr_bahaa.Controllers
{
    [Authorize(Roles = "can_mange")]
    public class paymentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: payments
        public ActionResult Index()
        {
            var payments = db.payments.Include(p => p.Student);
            return View(payments.ToList());
        }

        // GET: payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            payment payment = db.payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: payments/Create
        public ActionResult Create()
        {
            foreach (var item in db.students)
            {
                var pay = new payment();
                pay.Student = item;
                db.payments.Add(pay);
              

            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult deactivated(string id)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


             
                UserManager.AddToRole(id, "deactivation");
                UserManager.RemoveFromRole(id, "Students");
            var list = db.payments.ToList();
            foreach (var item in list)
            {
                if (item.Student.userid==id)
                {
                    db.payments.Remove(item);
                }
            }
            db.SaveChanges();

            return RedirectToAction("Index");


        }


        // POST: payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /* [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Create([Bind(Include = "paymentid,id")] payment payment)
         {
             if (ModelState.IsValid)
             {
                 db.payments.Add(payment);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             ViewBag.id = new SelectList(db.students, "id", "studentname", payment.id);
             return View(payment);
         }

         // GET: payments/Edit/5
         public ActionResult Edit(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             payment payment = db.payments.Find(id);
             if (payment == null)
             {
                 return HttpNotFound();
             }
             ViewBag.id = new SelectList(db.students, "id", "studentname", payment.id);
             return View(payment);
         }

         // POST: payments/Edit/5
         // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
         // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit([Bind(Include = "paymentid,id")] payment payment)
         {
             if (ModelState.IsValid)
             {
                 db.Entry(payment).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             ViewBag.id = new SelectList(db.students, "id", "studentname", payment.id);
             return View(payment);
         }

         // GET: payments/Delete/5
         public ActionResult Delete(int? id)
         {
             if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             payment payment = db.payments.Find(id);
             if (payment == null)
             {
                 return HttpNotFound();
             }
             return View(payment);
         }
         */
        // POST: payments/Delete/5

        public ActionResult Delete(int id)
        {
            payment payment = db.payments.Find(id);
            db.payments.Remove(payment);
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
