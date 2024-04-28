using hukukProjesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hukukProjesi.Controllers
{
    public class HomeController : Controller
    {
        hukukDBEntities db = new hukukDBEntities();
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Hukuk | 2024";
            return View();
        }
        public ActionResult Slider()
        {
            return View();
        }
         public ActionResult kvkk()
        {
            return View();
        }
        public ActionResult info()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Proje()
        {
            return View();
        }
        public ActionResult Counter()
        {
            return View();
        }
        public ActionResult Counter2()
        {
            return View(db.Blog.ToList());
        }

    }
}