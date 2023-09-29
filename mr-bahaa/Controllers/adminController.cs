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
    public class adminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: admin
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            var role=db.Roles.ToList();

            foreach (var item in role)
            {
                var usersinole = db.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(item.Id)) .ToList();
                users.RemoveAll(x => usersinole.Any(y => y.Id == x.Id));


            }
           
            return View(users);
        }
        

        // GET: admin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            string pro="";
            string deg ="";
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
           
            var photos = db.photos.ToList();
            foreach (var item in photos)
            {
                if (item.userid==(applicationUser.Email)+ "profile")
                {
                    pro =  item.photoUrl;
                }
                if (item.userid == (applicationUser.Email) + "degree")
                {
                    deg = item.photoUrl;
                }

            }
            adminviewmodel viewmodel = new adminviewmodel
             {user=applicationUser,
             pro=pro,
             deg=deg

             };
            
            return View(viewmodel);
        }

        // GET: admin/Create
        public ActionResult Create()
        {
            return RedirectToAction("Register", "Account");
        }

        // GET: admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,School,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: admin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
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
