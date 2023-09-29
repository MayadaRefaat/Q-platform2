using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mr_bahaa.Models;
using System.IO;
using mr_bahaa.viewmodel;
using System.Net.NetworkInformation;

namespace mr_bahaa.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {


           
                 

           




            var teachers = db.teachers.ToList();
            var imgs = db.teacherimgs.ToList();
            var viewmodel = new List<teacherviewmodel>();
            foreach (var item in teachers)
            {
                foreach (var itemm in imgs)
                {
                    if (item.teacherid == itemm.teacherid)
                    {
                        var models = new teacherviewmodel();

                        models.teacher = item;
                        models.img = itemm;
                        viewmodel.Add(models);
                        
                    }
                    
                }
                if (viewmodel.Count == 4)
                {
                    break;
                }
            }
            var subjectcoures = db.subjectcoures.Include(s => s.coursescategories).Include(s => s.teacher);
            var courses = subjectcoures.ToList();
            var img = db.subjectcoureimg.ToList();
            var subviewmodel = new List<subjectcourseviewmodel>();

            foreach (var item in courses)
            {
                foreach (var itemm in img)
                {
                    if (item.subjectcourseid == itemm.subjectcoureid)
                    {
                        var submodels = new subjectcourseviewmodel();

                        submodels.subjectcoure = item;
                        submodels.img = itemm;
                        subviewmodel.Add(submodels);
                       
                    }

                }
                if (subviewmodel.Count == 3)
                {
                    break;
                }
            }

            var lastviewmodel = new homeviewmodel
            {
                teachers=viewmodel,
                courses=subviewmodel
            };



            if (User.IsInRole("can_mange")|| User.IsInRole("Students"))
            {
                return View(lastviewmodel);
            }
            else if ( Request.IsAuthenticated)
            {
                return View("Index - Copy", lastviewmodel);
            }else
            {
                return View("Index - Copy", lastviewmodel);
            }
            
        }

        public ActionResult done()
        {
           

            return View();
        }
        public ActionResult how(int id)
        {
            var how = new Class1();
            if (id==1)
            {
                how.howwoed = "كيف تسجل علي المنصه؟";
                how.videos = "https://www.youtube.com/embed/uL1r_ODj4bg";
            }
            else if (id==2)
            {
                
                how.howwoed = "كيف تشترك في كورس علي المنصه؟";
                how.videos = "https://www.youtube.com/embed/SYHoKn6jmVM";
            }
            else if (id == 3)
            {

                how.howwoed = "ازاي تدخل علي الحصته بتاعتك؟";
                how.videos = "https://www.youtube.com/embed/a2y9qOhFkTk";
            }
            else if (id == 4)
            {

                how.howwoed = "ازاي تسال المدرس علي المنصه سؤالك؟";
                how.videos = "https://www.youtube.com/embed/7na2T2kpajk";
            }
            else if (id == 5)
            {

                how.howwoed = "ازاي تعرف درجات امتحاناتك؟";
                how.videos = "https://www.youtube.com/embed/B1USnxyF6GI";
            }
            else if (id == 6)
            {

                how.howwoed = "ازاي تسلم الواجب علي المنصه؟";
                how.videos = "https://www.youtube.com/embed/7sSBGgjEVFo";
            }


            return View(how);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult More()
        {


            return View();
        }
    }
}