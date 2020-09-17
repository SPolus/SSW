using SSW.Data.Repositories;
using SSW.Web.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;

namespace SSW.Web.Controllers
{
    public class AccountsController : Controller
    {
        private const int LONG = 43200; // 43200 minutes = 1 month
        private const int SHORT = 5; // minutes

        private readonly IStudentRepository _studentRepo;
        private readonly IInstructorRepository _instructorRepo;

        public AccountsController(IStudentRepository studentRepo, IInstructorRepository instructorRepo)
        {
            _studentRepo = studentRepo;
            _instructorRepo = instructorRepo;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(UserLoginVM userLogin)
        //{
        //    var student = await _studentRepo.GetByEmailAsync(userLogin.Email);
        //    var instructor = await _instructorRepo.GetByEmailAsync(userLogin.Email);

        //    if (student == null && instructor == null)
        //    {
        //        ModelState.AddModelError("IncorrectPassword", "Incorrect email or password");
        //        return View();
        //    }

        //    if (student != null)
        //    {
        //        if (string.Compare(userLogin.Password, student.Password) == 0)
        //        //if (string.Compare(Crypto.HashPassword(userLogin.Password), student.Password) == 0)
        //        {
        //            int timeout = userLogin.RememberMe ? LONG : SHORT;

        //            var ticket = new FormsAuthenticationTicket(userLogin.Email, userLogin.RememberMe, timeout);
        //            var encTicket = FormsAuthentication.Encrypt(ticket);
        //            var cookie = new HttpCookie(ConfigurationManager.AppSettings["AuthCookie"])
        //            {
        //                Value = encTicket,
        //                Expires = DateTime.Now.AddMinutes(timeout),
        //            };

        //            HttpContext.Response.Cookies.Set(cookie);

        //            return RedirectToAction("Index", "Home");
        //        }
        //    }

        //    if (instructor != null)
        //    {
        //        if (string.Compare(userLogin.Password, instructor.Password) == 0)
        //        //if (string.Compare(Crypto.HashPassword(userLogin.Password), student.Password) == 0)
        //        {
        //            int timeout = userLogin.RememberMe ? LONG : SHORT;

        //            var ticket = new FormsAuthenticationTicket(userLogin.Email, userLogin.RememberMe, timeout);
        //            var encTicket = FormsAuthentication.Encrypt(ticket);
        //            var cookie = new HttpCookie(ConfigurationManager.AppSettings["AuthCookie"])
        //            {
        //                Value = encTicket,
        //                Expires = DateTime.Now.AddMinutes(timeout),
        //            };

        //            HttpContext.Response.Cookies.Set(cookie);

        //            return RedirectToAction("Index", "Home");
        //        }
        //    }

        //    ModelState.AddModelError("IncorrectPassword", "Incorrect email or password");

        //    return View();
        //}

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}