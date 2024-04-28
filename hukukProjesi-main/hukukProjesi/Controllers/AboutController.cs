using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hukukProjesi.Models;

namespace hukukProjesi.Controllers
{
    public class AboutController : Controller
    {

        hukukDBEntities db = new hukukDBEntities();
        // GET: About
        public ActionResult Index()
        {
            ViewBag.Title = "Hukuk | Hakkımızda";
            return View();
        }
        public ActionResult Avukat()
        {
            ViewBag.Title = "Hukuk | Avukatlarımız";
            var Avukat = db.Author.ToList();
            return View(Avukat);
        }

        public ActionResult BizKimiz()
        {
            ViewBag.Title = "Hukuk | Biz Kimiz?";
            return View();
        }

    }
}