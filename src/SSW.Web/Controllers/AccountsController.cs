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

namespace SSW.Web.Controllers
{
    public class AccountsController : Controller
    {
        private const int MOHTN = 43200;
        private const int HOUR = 60;
        private const string COOKIE = "__AUTH_COOKIE_STUDENT";
        private readonly IStudentRepository _repository;

        public AccountsController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // GET: Account
        public ActionResult Login()
        {
            //var a = HttpContext.Request.Cookies.Get(COOKIE);
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginVM userLogin)
        {
            var student = await _repository.GetByEmailAsync(userLogin.Email);

            if (student != null)
            {
                if (string.Compare(userLogin.Password, student.Password) == 0)
                //if (string.Compare(Crypto.HashPassword(userLogin.Password), student.Password) == 0)
                {
                    int timeout = userLogin.RememberMe ? MOHTN : HOUR;

                    var ticket = new FormsAuthenticationTicket(userLogin.Email, userLogin.RememberMe, timeout);
                    var encTicket = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(COOKIE)
                    {
                        Value = encTicket,
                        Expires = DateTime.Now.AddMinutes(timeout),
                    };

                    HttpContext.Response.Cookies.Set(cookie);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("IncorrectPassword", "Incorrect email or password");

            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}