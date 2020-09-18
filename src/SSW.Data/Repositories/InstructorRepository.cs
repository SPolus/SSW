using SSW.Data.Contexts;
using SSW.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public class InstructorRepository : BaseRepository<Instructor>, IInstructorRepository
    {
        private readonly UniversityDbContext _context;

        public InstructorRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Instructor>> GetAllAsync(bool includeCourses = true)
        {
            if (includeCourses)
            {
                var a = await _context.Instructors
                    .Select(i => new
                    {
                        i.Id,
                        i.FirstName,
                        i.LastName,
                        i.CourseAssignments,
                        i.User
                    })
                    .ToListAsync();

                var instructors = new List<Instructor>();

                foreach (var instructor in a)
                {
                    instructors.Add(new Instructor
                    {
                        Id = instructor.Id,
                        FirstName = instructor.FirstName,
                        LastName = instructor.LastName,
                        CourseAssignments = instructor.CourseAssignments,
                        User = instructor.User
                    });
                }

                return instructors;
            }

            return await base.GetAllAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id, bool includeCourses = true)
        {
            if (includeCourses)
            {
                var a = await _context.Instructors
                    .Select(i => new
                    {
                        i.Id,
                        i.FirstName,
                        i.LastName,
                        i.CourseAssignments,
                        i.User
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);

                var instructor = new Instructor
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    CourseAssignments = a.CourseAssignments,
                    User = a.User
                };

                return instructor;
            }

            return await base.GetByIdAsync(id);
        }

        public Task<Instructor> GetByEmailAsync(string email)
        {
            return _context.Users.Where(e => e.Email == email).Select(x => x.Instructor).FirstOrDefaultAsync();
        }

        public Task<bool> IsInstructorExists(string email)
        {
            return _context.Instructors.Select(x => x.User).AnyAsync(e => e.Email == email);
        }
    }
}
