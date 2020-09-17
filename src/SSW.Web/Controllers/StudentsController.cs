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
using SSW.Data.Entitties;
using SSW.Data.Repositories;
using SSW.Web.ViewModels.Student;
using System.Web.Security;
using System.Web.Helpers;
using SSW.Web.Filters;

namespace SSW.Web.Controllers
{
    [CustomAuthorize(Roles = "instructor")]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // GET: Students
        public async Task<ActionResult> Index()
        {
            var students = await _repository.GetAllAsync();

            var results = new List<StudentIndexVM>();

            foreach (var student in students)
            {
                var avg = student.Enrollments.Average(x => (int?)x.Grade);

                if (avg != null)
                {
                    avg = Math.Round((double)avg, 0, MidpointRounding.AwayFromZero);
                }

                results.Add(new StudentIndexVM
                {
                    Id = student.Id,
                    FullName = $"{student.LastName} {student.FirstName}",
                    AvgGrade = (Grade?)avg
                });
            }

            return View(results);
        }

        // GET: Students/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await _repository.GetByIdAsync((int)id);

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
                bool isExists = await _repository.IsStudentExists(student.Email);

                if (isExists)
                {
                    ModelState.AddModelError("EmailExist", "Student already exists");
                    return View(student);
                }

                var newStudent = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    User = new User { Email = student.Email, Password = student.Password}
                };

                await _repository.AddAsync(newStudent);
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        [CustomAuthorize(Roles = "student")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = await _repository.GetByIdAsync((int)id);

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

            var student = await _repository.GetByIdAsync((int)id);

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
            var student = await _repository.GetByIdAsync((int)id);
            await _repository.DeleteAsync(student);
            
            return RedirectToAction("Index");
        }
    }
}
