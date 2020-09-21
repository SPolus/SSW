using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSW.Data.Contexts;
using SSW.Data.Entities;
using SSW.Data.Repositories;
using SSW.Web.ViewModels.Student;
using System.Web.Security;
using System.Web.Helpers;
using SSW.Web.Filters;

namespace SSW.Web.Controllers
{
    [CustomAuthorize(Role.Student, Role.Instructor)]
    public class StudentsController : Controller
    {
        private readonly IRepository<Student> _repository;

        public StudentsController(IRepository<Student> repository)
        {
            _repository = repository;
        }

        // GET: Students
        public async Task<ActionResult> Index()
        {
            var f = await _repository.ToListAsync(s => new
            {
                s.Id,
                s.User.FirstName,
                s.User.LastName,
                Grades = s.Enrollments.Where(e => e.Grade != null).Select(g => (int)g.Grade)
            });

            var students = new List<StudentIndexVM>();

            foreach (var student in f)
            {
                students.Add(new StudentIndexVM
                {
                    Id = student.Id,
                    FullName = $"{student.LastName} {student.FirstName}",
                    AvgGrade = student.Grades.ToList().Count != 0 ? (Grade?)Math.Round(student.Grades.Average(), 0, MidpointRounding.AwayFromZero) : null
                });
            }

            return View(students);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await _repository.FirstOrDefaultAsync((int)id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,Email,Password")] StudentCreateVM student)
        {
            if (ModelState.IsValid)
            {
                bool isExists = await _repository.Exist(x => x.User.Email == student.Email);

                if (isExists)
                {
                    ModelState.AddModelError("EmailExist", "Student already exists");
                    return View(student);
                }

                var newStudent = new Student
                {
                    User = new User { Email = student.Email, Password = student.Password, FirstName = student.FirstName, LastName = student.LastName }
                };

                await _repository.AddAsync(newStudent);
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await _repository.FirstOrDefaultAsync((int)id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName")] Student student)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await _repository.FirstOrDefaultAsync((int)id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var student = await _repository.FirstOrDefaultAsync((int)id);
            await _repository.DeleteAsync(student);
            
            return RedirectToAction("Index");
        }
    }
}
