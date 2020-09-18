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
using SSW.Web.Services;

namespace SSW.Web.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly CookieService _cookieService = new CookieService();

        public AccountsController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginVM user)
        {
            var authenticatedUser = await _repository.GetByEmailAndPasswordAsync(user.Email, user.Password);

            if (authenticatedUser == null)
            {
                ModelState.AddModelError("IncorrectPassword", "Incorrect email or password");
                return View();
            }

            _cookieService.SetAuthenticationToken(authenticatedUser.Email, user.RememberMe, authenticatedUser, 5);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", new { controller = "Home", area = string.Empty });
        }

    }
}