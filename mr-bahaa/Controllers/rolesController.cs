using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using mr_bahaa.Models;
using mr_bahaa.viewmodel;

namespace mr_bahaa.Controllers
{
    //[Authorize(Roles = "can_mange")]
    public class rolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: roles
        public ActionResult Index()
        {
            
            return View(db.Roles.ToList());
        }

        // GET: roles/Details/5
        public ActionResult Details(string Id)
        {
            var users = db.Users
     .Where(x => x.Roles.Select(y => y.RoleId).Contains(Id))
     .ToList();
            var rolename = db.Roles.Find(Id).Name;
            if (rolename== "can_mange")
            {
                string currentUserId = User.Identity.GetUserId();
                var adminuser = db.Users.Find(currentUserId);
                users.Remove(adminuser);
            }
            var viewmodel = new roleviewmodel { users = users,roleid=Id };
           
            return View(viewmodel);

        }

        public ActionResult addtorole(string Id)
        {
            var users = db.Users.ToList();
            var rolename = db.Roles.Find(Id).Name;
            if (rolename== "deactivation")
            {
                var usersinrole = db.Users
            .Where(x => x.Roles.Select(y => y.RoleId).Contains("44194a6d-8cc3-4da5-9352-2c7c31f4b7dd"))
            .ToList();

                users.RemoveAll(x => !usersinrole.Any(y => y.Id == x.Id));

            }
            else
            {
                var usersinrole = db.Users
          .Where(x => x.Roles.Select(y => y.RoleId).Contains(Id))
          .ToList();

                users.RemoveAll(x => usersinrole.Any(y => y.Id == x.Id));
            }



            var viewmodel = new roleviewmodel { users = users,roleid=Id };
            return View(viewmodel);

        }
        [HttpPost]
        public ActionResult addtorole(ApplicationUser user,string roleid)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            string rolename = db.Roles.Find(roleid).Name;

            if (rolename == "Students")
            {
                UserManager.AddToRole(user.Id, rolename);
                UserManager.RemoveFromRole(user.Id, "deactivation");


            }
            else if (rolename == "deactivation")
            {
                UserManager.AddToRole(user.Id, rolename);
                UserManager.RemoveFromRole(user.Id, "Students");
            }
            else
            {
                UserManager.AddToRole(user.Id, rolename);
                UserManager.RemoveFromRole(user.Id, "Students");

            }
            db.SaveChanges();
            return RedirectToAction("Details", "roles",new {Id= roleid });

        }
        public ActionResult remove(string Id,string roleid)
        {
            var user = db.Users.Find(Id).Id;
            var rolename = db.Roles.Find(roleid).Name;

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            UserManager.RemoveFromRole(user, rolename);
            db.SaveChanges();
            return RedirectToAction("Details", "roles", new { Id = roleid });

        }
        // GET: roles/Create
        public ActionResult Createrole()
        {
            return View("Createrole");
        }

        // POST: roles/Create
        [HttpPost]
        public ActionResult Createrole([Bind(Include = "Id,name")] role role)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (ModelState.IsValid)
            {
                var rolee = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                rolee.Name = role.Name;
                roleManager.Create(rolee);
               
                db.SaveChanges();
                return RedirectToAction("Index", "roles");
            }

            return View(role);
        }

        public ActionResult addadmin()
        {
            var users = db.Users.ToList();

            return View(User);
        }

        // GET: roles/Edit/5
        /*public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: roles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: roles/Delete/5
        public ActionResult Delete(string id)
        {
            var x=db.Roles.Find(id);
            return View(x);
        }

        // POST: roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, FormCollection collection)
        {
            var x = db.Roles.Find(id);
            db.Roles.Remove(x);
            db.SaveChanges();
            return RedirectToAction("Index");

        }*/
    }
}
