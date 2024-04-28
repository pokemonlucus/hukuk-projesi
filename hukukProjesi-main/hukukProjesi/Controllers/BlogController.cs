using hukukProjesi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;

namespace hukukProjesi.Controllers
{
    public class BlogController : Controller
    {
        hukukDBEntities db = new hukukDBEntities();

        // GET: Blog
        public ActionResult Search(string searchString)
        {
            // Arama metni boşsa, tüm blogları göster
            if (string.IsNullOrEmpty(searchString))
            {
                return View("Index", db.Blog.ToList());
            }

            // Arama metni doluysa, metni içeren blogları filtrele
            var searchResults = db.Blog.Where(b => b.Baslik.Contains(searchString) || b.Ozet.Contains(searchString)).ToList();
            return View(searchResults);
        }
        public ActionResult Index(Blog blog)
        {
            
            HttpContext.Session["BlogId"] = blog.BlogId;

            ViewBag.Title = "Hukuk | Blog";
            var Blog = db.Blog.ToList();

            return View(Blog);
        }
        public ActionResult Populer(Blog blog)
        {
            HttpContext.Session["BlogId"] = blog.BlogId;

            ViewBag.Title = "Hukuk | Blog";
            var Blog = db.Blog.ToList();

            return View(Blog);
        }
        public ActionResult Detail(int? BlogId)
        {
            var author = db.Author.FirstOrDefault();
            if (author != null)
            {
                HttpContext.Session["NameSurname"] = author.NameSurname;
            }

            if (BlogId == null || BlogId == 0)
            {
                return HttpNotFound();
            }

            ViewBag.Title = "Hukuk | Blog Detay";
            Blog blog = db.Blog.Find(BlogId);
            return View(blog);
        }

        [HttpPost]
        public ActionResult Detail(Blog blog, HttpPostedFileBase File)
        {
            if (blog != null)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.Entry(blog).Property(m => m.EklenmeTarihi).IsModified = false;
                blog.DüzenlenmeTarihi = DateTime.Now;
                db.SaveChanges();
            }

            return RedirectToAction("Detail", new { Blogs = blog.BlogId });
        }

    }
}