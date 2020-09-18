using SSW.Data.Repositories;
using SSW.Web.ViewModels.Account;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using SSW.Web.Services;

namespace SSW.Web.Controllers
{
    public class AccountsController : Controller
    {
        private const int _cookieTimeoutInMinutes = 10;

        private readonly IUserRepository _repository;
        private readonly CookieService _cookieService;

        public AccountsController(IUserRepository repository, CookieService cookieService)
        {
            _repository = repository;
            _cookieService = cookieService;
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

            _cookieService.SetAuthenticationToken(authenticatedUser.Email, user.RememberMe, authenticatedUser, _cookieTimeoutInMinutes);
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