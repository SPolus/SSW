using Newtonsoft.Json;
using SSW.Data.Entities;
using SSW.Data.Repositories;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SSW.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<User> _repository;

        public HomeController(IRepository<User> repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetUsers()
        {
            var users = await _repository.ToListAsync();
            var serialized = JsonConvert.SerializeObject(users, Formatting.Indented);
            return Json(serialized, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(User user)
        {
            await _repository.AddAsync(user);
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}