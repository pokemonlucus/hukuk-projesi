using System.IO;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web;
using System.Web.Mvc;
using hukukProjesi.Models;

namespace hukukProjesi.Controllers
{
    public class LoginController : Controller
    {
        private hukukDBEntities db = new hukukDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            
            // Kullanıcı adı ve şifreyi kontrol et
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
                // Kullanıcı varsa, oturum aç ve ana sayfaya yönlendir
                Session["UserId"] = user.Id;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Kullanıcı yoksa, hata mesajı ekle ve aynı sayfada kal
                ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Author author, HttpPostedFileBase File)
        {
            var authorExist = db.Author.Any(m => m.Email == author.Email);

            if (authorExist == false)
            {
                author.AddedDate = DateTime.Now;
                author.AddedBy = "Mesut Kaya";
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

                // Yönlendirme LoginController'ın Index action'ına yapılmalı
                return RedirectToAction("Index", "Login");
            }

            // Eğer kullanıcı zaten varsa, kayıt sayfasına geri dön
            return View();
        }
    }
}

