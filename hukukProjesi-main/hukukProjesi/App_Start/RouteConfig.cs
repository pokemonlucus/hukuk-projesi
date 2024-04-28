using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace hukukProjesi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "About",
                url: "Hakkimizda",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Bizkimiz",
                url: "Bizkimiz",
                defaults: new { controller = "About", action = "BizKimiz", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Avukat",
                url: "Hakkimizda/Avukatlarimiz",
                defaults: new { controller = "About", action = "Avukat", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "Giris",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Register",
                url: "Kayit",
                defaults: new { controller = "Login", action = "Register", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminGiris",
                url: "Admingiris",
                defaults: new { controller = "AuthorAdmin", action = "Login", id = UrlParameter.Optional }
            );
                routes.MapRoute(
                name: "AdminPanel",
                url: "Admin/Avukatlar",
                defaults: new { controller = "AuthorAdmin", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "AdminEkle",
                url: "Admin/Avukatekle",
                defaults: new { controller = "AuthorAdmin", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Blog",
                url: "Blog",
                defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Service",
                url: "Hizmetlerimiz",
                defaults: new { controller = "Service", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Detail",
                url: "Detay",
                defaults: new { controller = "Blog", action = "Detail", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Contact",
                url: "iletisim",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
