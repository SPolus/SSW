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
        private readonly IRepository<Student> _repository;

        public TestController(IRepository<Student> repository)
        {
            _repository = repository;
        }

        // GET: Test
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var f = await _repository.ToListAsync(s => new
            {
                s.Id,
                s.User.FirstName,
                s.User.LastName,
                s.User.Email,
                s.User.Password
            }).ConfigureAwait(false);

            var students = new List<Student>();

            if (f != null)
            {
                foreach (var s in f)
                {
                    students.Add(new Student
                    {
                        Id = s.Id,
                        User = new User
                        {
                            FirstName = s.FirstName,
                            LastName = s.LastName,
                            Email = s.Email,
                            Password = s.Password
                        }
                    });
                }
            }

            return View(students);
        }
    }
}