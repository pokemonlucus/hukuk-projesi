using hukukProjesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hukukProjesi.Controllers
{
    
    public class ContactController : Controller
    {
        hukukDBEntities db = new hukukDBEntities();
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Hukuk | İletişim";
            return View();
        }
        [HttpPost]
        public ActionResult Index(Contacts cot)
        {
            ViewBag.Title = "Hukuk | İletişim";
            db.Contacts.Add(cot);
            db.SaveChanges();
            return RedirectToAction("Second");
        }
        public ActionResult Second()
        {
            
            return View();
        }
    }
}