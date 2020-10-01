using Newtonsoft.Json;
using SSW.Data.Entities;
using SSW.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace SSW.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IRepository<Student> _repository;
        private readonly IRepository<User> _userRepository;

        public TestController(IRepository<Student> repository, IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        

        // GET: Test
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var students = await _repository.ToListAsync(s => new
            {
                Id = s.Id,
                s.User.FirstName,
                s.User.LastName,
                s.User.Email,
                s.User.Password
            });

            var results = new List<Student>();

            foreach (var s in students)
            {
                results.Add(new Student
                {
                    Id = s.Id,
                    User = new User { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Email = s.Email, Password = s.Password }
                });
            }

            return View(results);
        }

        [HttpPost]
        public async Task<JsonResult> Create(Student student)
        {
            bool isExists = await _repository.Exist(x => x.User.Email == student.User.Email);

            if (isExists)
            {
                return Json(new { success = false, statusCode = 401, statusCodeText = "Unauthorized", responseText = "Student already exists" });
            }

            await _repository.AddAsync(student);

            return Json(new { success = true, responseText = "Success" });
        }


        [HttpPost]
        public async Task<JsonResult> Edit(Student student)
        {
            var user = await _userRepository.FirstOrDefaultAsync(x => x.Email == student.User.Email);

            if (user == null)
            {
                return Json(new { responseText = "Student doesn't exists" });
            }

            user.FirstName = student.User.FirstName;
            user.LastName = student.User.LastName;
            user.Email = student.User.Email;
            user.Password = student.User.Password;

            await _userRepository.SaveChangesAsync();

            return Json(new { responseText = "Success" });

            
            ////return Json($"{HttpContext.Response.StatusCode} {HttpContext.Response.StatusDescription}");
        }
    }
}