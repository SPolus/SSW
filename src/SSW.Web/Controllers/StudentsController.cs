using SSW.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SSW.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository repository)
        {
            _repository = repository;
        }

        // GET: Students
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var students = await _repository.GetAllAsync(includeOptions: true);
            return View(students);
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var student = await _repository.GetByIdAsync(id, includeOptions: true);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }


    }
}