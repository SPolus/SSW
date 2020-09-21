using SSW.Data.Entities;
using SSW.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async JsonResult AjaxGetData()
        {
            var users = await _repository.ToListAsync();
            var result = users.FirstOrDefault();
            return Json(users, JsonRequestBehavior.AllowGet);
        }
    }
}