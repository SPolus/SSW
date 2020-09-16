using SSW.Data.Repositories;
using SSW.Web.ViewModels.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

            var results = new List<InstructorIndexVM>();

            foreach (var instructor in instructors)
            {
                

                results.Add(new InstructorIndexVM
                {
                    Id = instructor.Id,
                    FullName = $"{instructor.LastName} {instructor.FirstName}",
                });
            }

            return View();
        }
    }
}