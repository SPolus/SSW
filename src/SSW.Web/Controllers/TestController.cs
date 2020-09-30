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

            foreach (var s in f)
            {
                students.Add(new Student
                {
                    Id = s.Id,
                    User = new User { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Email = s.Email, Password = s.Password }
                });
            }

            return View(students);
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
            //var st = await _repository.FirstOrDefaultAsync(x => x.User.Email == student.User.Email, s => new { });

            //if (st == null)
            //{
            //    return Json(new { success = false, statusCode = 400, statusCodeText = "Bad Request", responseText = "Student doesn't exists" });
            //}

            //var updated = new Student
            //{
            //    Id = st.Id,
            //    User = new User
            //    {
            //        Id = st.Id,
            //        FirstName = student.User.FirstName,
            //        LastName = student.User.LastName,
            //        Email = student.User.Email,
            //        Password = student.User.Password,
            //        Student = st.User.Student,
            //        Instructor = st.User.Instructor
            //    },
            //    Enrollments = st.Enrollments
            //};

            await _repository.UpdateAsync(student);

            return Json(new { success = true, responseText = "Success" });
        }
    }
}