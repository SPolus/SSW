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
using SSW.Web.ViewModels.Instructor;
using SSW.Web.Filters;

namespace SSW.Web.Controllers
{
    public class InstructorsController : Controller
    {
        private readonly IInstructorRepository _repository;

        public InstructorsController(IInstructorRepository repository)
        {
            _repository = repository;
        }

        // GET: Instructors
        public async Task<ActionResult> Index()
        {
            var instructors = await _repository.GetAllAsync();

            return View(instructors);
        }


        // GET: Instructors/Details/5
        [CustomAuthorize(Roles = "instructor")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructor = await _repository.GetByIdAsync((int)id);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            return View();
        }

        //// POST: Instructors/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Email,Password,FirstName,LastName")] InstructorCreateVM instructor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool isExists = await _repository.IsInstructorExists(instructor.Email);

        //        if (isExists)
        //        {
        //            ModelState.AddModelError("EmailExist", "Instructor already exists");
        //            return View(instructor);
        //        }

        //        var newInstructor = new Instructor
        //        {
        //            FirstName = instructor.FirstName,
        //            LastName = instructor.LastName,
        //            Email = instructor.Email,
        //            Password = instructor.Password
        //            //Password = Crypto.HashPassword(Instructor.Password)
        //        };

        //        await _repository.AddAsync(newInstructor);
        //        return RedirectToAction("Index");
        //    }

        //    return View(instructor);
        //}

        // GET: Instructors/Edit/5
        [CustomAuthorize(Roles = "instructor")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructor = await _repository.GetByIdAsync((int)id);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,Password,FirstName,LastName")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(instructor);
                return RedirectToAction("Index");
            }

            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var instructor = await _repository.GetByIdAsync((int)id);

            if (instructor == null)
            {
                return HttpNotFound();
            }

            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var instructor = await _repository.GetByIdAsync((int)id);
            await _repository.DeleteAsync(instructor);

            return RedirectToAction("Index");
        }
    }
}
