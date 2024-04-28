using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hukukProjesi.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            ViewBag.Title = "Hukuk | Hizmetlerimiz";
            return View();
        }
    }
}