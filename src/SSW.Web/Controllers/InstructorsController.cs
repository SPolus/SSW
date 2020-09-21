//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using SSW.Data.Contexts;
//using SSW.Data.Entities;
//using SSW.Data.Repositories;
//using SSW.Web.ViewModels.Instructor;
//using SSW.Web.Filters;

//namespace SSW.Web.Controllers
//{
//    public class InstructorsController : Controller
//    {
//        private readonly IInstructorRepository _repository;
//        private readonly IRepository<Instructor> _repo;

//        public InstructorsController(IInstructorRepository repository, IRepository<Instructor> repo)
//        {
//            _repository = repository;
//            _repo = repo;
//        }

//        // GET: Instructors
//        public async Task<ActionResult> Index()
//        {

//            // Move to instructors controller
//            //var qwe = await _inst.ToListAsync(i => new
//            //{
//            //    i.User.Email,
//            //    CourseStudents = i.CourseAssignments.GroupBy(k => k.CourseId).Select(group => new
//            //    {
//            //        CourseId = group.Key,
//            //        CountStudents = group.Sum(a => a.Course.Enrollments.Count)
//            //    })
//            //});

//            //var qwe2 = await _inst.ToListAsync(i => new
//            //{
//            //    i.FirstName,
//            //    i.LastName,
//            //    CourseStudents = i.CourseAssignments.GroupBy(k => k.Course.Name).Select(group => new
//            //    {
//            //        CourseName = group.Key,
//            //        CountStudents = group.Sum(a => a.Course.Enrollments.Count)
//            //    })
//            //});
//            var instructors = await _repository.GetAllAsync();

//            var results = new List<InstructorIndexVM>();

//            // TODO: query
//            foreach (var instructor in instructors)
//            {
//                results.Add(new InstructorIndexVM
//                {
//                    Id = instructor.Id,
//                    FullName = $"{instructor.LastName} {instructor.FirstName}",
//                    CourseStudents = new List<CourseStudents> { new CourseStudents { CourseName = "Course name", StudentsCount = 0 } }
//                });
//            }

//            return View(results);
//        }


//        // GET: Instructors/Details/5
//        [CustomAuthorize(Roles = "instructor")]
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            var instructor = await _repository.GetByIdAsync((int)id);

//            if (instructor == null)
//            {
//                return HttpNotFound();
//            }

//            return View(instructor);
//        }

//        // GET: Instructors/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Instructors/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Email,Password,FirstName,LastName")] InstructorCreateVM instructor)
//        {
//            if (ModelState.IsValid)
//            {
//                bool isExists = await _repository.IsInstructorExists(instructor.Email);

//                if (isExists)
//                {
//                    ModelState.AddModelError("EmailExist", "Instructor already exists");
//                    return View(instructor);
//                }

//                var newInstructor = new Instructor
//                {
//                    FirstName = instructor.FirstName,
//                    LastName = instructor.LastName,
//                    User = new User { Email = instructor.Email, Password = instructor.Password }
//                };

//                await _repository.AddAsync(newInstructor);
//                return RedirectToAction("Index");
//            }

//            return View(instructor);
//        }

//        // GET: Instructors/Edit/5
//        [CustomAuthorize(Roles = "instructor")]
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            var instructor = await _repository.GetByIdAsync((int)id);

//            if (instructor == null)
//            {
//                return HttpNotFound();
//            }

//            return View(instructor);
//        }

//        // POST: Instructors/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,Password,FirstName,LastName")] Instructor instructor)
//        {
//            if (ModelState.IsValid)
//            {
//                await _repository.UpdateAsync(instructor);
//                return RedirectToAction("Index");
//            }

//            return View(instructor);
//        }

//        // GET: Instructors/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            var instructor = await _repository.GetByIdAsync((int)id);

//            if (instructor == null)
//            {
//                return HttpNotFound();
//            }

//            return View(instructor);
//        }

//        // POST: Instructors/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            var instructor = await _repository.GetByIdAsync((int)id);
//            await _repository.DeleteAsync(instructor);

//            return RedirectToAction("Index");
//        }
//    }
//}
