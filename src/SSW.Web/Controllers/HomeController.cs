using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SSW.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cookies = HttpContext.Request.Cookies.AllKeys;

            //var a = HttpContext.Request.Cookies["__AUTH_COOKIE_STUDENT"].Value;
            //var b = FormsAuthentication.Decrypt(a);
            //var c = b.Name;
            //var d = b.IsPersistent;
            //var e = b.Expiration;
            //var f = b.UserData;

            return View();
        }
    }
}