using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using hukukProjesi.Models;

namespace hukukProjesi.Controllers
{
    public class AuthorAdminController : Controller
    {
        // GET: AuthorAdmin
        hukukDBEntities db = new hukukDBEntities();
        public ActionResult Index()
        {
            return View(db.Author.ToList());
        }
        public ActionResult Contact()
        {
            var contacts = db.Contacts.ToList();
            return View(contacts);
        }

        // BLOG //
        public ActionResult Blog()
        {
            return View(db.Blog.ToList());
        }
        public ActionResult BlogEkle()
        {
            Blog bosBlog = new Blog
            {
                Konu = ""
            };

            return View(bosBlog);
        }
        [HttpPost]
        public ActionResult BlogEkle(Blog blog, HttpPostedFileBase File2)
        {
            
                // Eklenme tarihini şu anki zaman olarak ayarla
                blog.EklenmeTarihi = DateTime.Now;

                // Eğer dosya yüklenmişse
                if (File2 != null)
                {
                FileInfo fileinfo = new FileInfo(File2.FileName);
                WebImage img = new WebImage(File2.InputStream);
                string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                img.Resize(200, 180, false, false);
                string tamyol = "~/images/blog/" + uzanti;
                img.Save(Server.MapPath(tamyol));
                blog.image = "/images/blog/" + uzanti;
            }

                // Blogu veritabanına ekle
                db.Blog.Add(blog);
                db.SaveChanges();

                // Index sayfasına yönlendir
                return RedirectToAction("Blog","AuthorAdmin");
            
        }
        public ActionResult BlogDelete(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            Blog blog = db.Blog.Find(Id);
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Blog","AuthorAdmin");
        }

        public ActionResult BlogDetails(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            Blog blog = db.Blog.Find(id);
            return PartialView(blog);
        }

        public ActionResult BlogEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            Blog blog = db.Blog.Find(id);
            return View(blog);
        }

        [HttpPost]
        public ActionResult BlogEdit(int id, Blog updatedBlog, HttpPostedFileBase File3)
        {
            // Veritabanından ilgili blogu çek
            var existingBlog = db.Blog.FirstOrDefault(b => b.BlogId == id);

            if (existingBlog == null)
            {
                return HttpNotFound(); // veya uygun bir hata mesajı veya sayfa döndür
            }

            // Güncelleme işlemlerini yap
            existingBlog.Baslik = updatedBlog.Baslik;
            existingBlog.Ozet = updatedBlog.Ozet;
            existingBlog.Konu = updatedBlog.Konu;
            existingBlog.EkleyenKisi = "Admin";
            existingBlog.EklenmeTarihi = DateTime.Now;

            if (File3 != null)
            {
                FileInfo fileinfo = new FileInfo(File3.FileName);
                WebImage img = new WebImage(File3.InputStream);
                string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                img.Resize(225, 180, false, false);
                string tamyol = "~/images/users/" + uzanti;
                img.Save(Server.MapPath(tamyol));
                existingBlog.image = "/images/users/" + uzanti;
            }

            db.SaveChanges();

            return RedirectToAction("Blog", "AuthorAdmin");
        }


        //END BLOG//
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Author author, HttpPostedFileBase File)
        {
            var authorExist = db.Author.Any(m => m.Email == author.Email);

            if (authorExist == false)
            {
                author.AddedDate = DateTime.Now;
                author.AddedBy = "Admin";
                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(200, 180, false, false);
                    string tamyol = "~/images/users/" + uzanti;
                    img.Save(Server.MapPath(tamyol));
                    author.image = "/images/users/" + uzanti;
                }
                db.Author.Add(author);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return HttpNotFound();
            }
            Author author = db.Author.Find(Id);
            db.Author.Remove(author);
            db.SaveChanges();
            return RedirectToAction("");
        }

        public ActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            Author author = db.Author.Find(id);
            return PartialView(author);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return HttpNotFound();
            }

            Author author = db.Author.Find(id);
            return View(author);
        }
        [HttpPost]
        public ActionResult Edit(Author author, HttpPostedFileBase File)
        {
            if (author != null)
            {
                db.Entry(author).State = System.Data.Entity.EntityState.Modified;
                db.Entry(author).Property(m => m.AddedBy).IsModified = false;
                db.Entry(author).Property(m => m.AddedDate).IsModified = false;
                if (File != null)
                {
                    FileInfo fileinfo = new FileInfo(File.FileName);
                    WebImage img = new WebImage(File.InputStream);
                    string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                    img.Resize(225, 180, false, false);
                    string tamyol = "~/images/users/" + uzanti;
                    img.Save(Server.MapPath(tamyol));
                    author.image = "/images/users/" + uzanti;
                }
                else
                {
                    db.Entry(author).Property(m => m.image).IsModified = false;
                }
                author.ModifyBy = "Mesut Kaya";
                author.ModifyDate = DateTime.Now;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "AuthorAdmin");
        }

        public ActionResult Login(string email, string password)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                var user = db.Author.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    // Kullanıcının bilgilerini sessiona at
                    HttpContext.Session["NameSurname"] = user.NameSurname;
                    HttpContext.Session["UserId"] = user.Id;
                    HttpContext.Session["UserEmail"] = user.Email;
                    HttpContext.Session["UserAbout"] = user.About;
                    HttpContext.Session["UserImage"] = user.image;
                    HttpContext.Session["UserDeneyim"] = user.Deneyim;
                    HttpContext.Session["UserAciklama"] = user.Aciklama;
                    HttpContext.Session["UserEgitim"] = user.Egitim;

                    if (user.Role == "Admin")
                    {
                        Session["UserId"] = user.Id;
                        return RedirectToAction("Index", "AuthorAdmin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Bu sayfaya erişim izniniz yok.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı adı ve şifre boş olamaz.");
            }

            // Hata durumunda Login sayfasını yeniden göster
            return View("Login");
        }

        


    }
}