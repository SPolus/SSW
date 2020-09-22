using Newtonsoft.Json;
using SSW.Data.Entities;
using SSW.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SSW.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IRepository<User> _repository;

        public TestController(IRepository<User> repository)
        {
            _repository = repository;
        }
        // GET: Test
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