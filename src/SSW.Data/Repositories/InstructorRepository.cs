using SSW.Data.Contexts;
using SSW.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
                return await _context.Instructors
                    .Include(i => i.CourseAssignments.Select(c => c.Course))
                    .ToListAsync();
            }

            return await base.GetAllAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id, bool includeCourses = true)
        {
            if (includeCourses)
            {
                return await _context.Instructors
                    .Include(i => i.CourseAssignments.Select(c => c.Course))
                    .FirstOrDefaultAsync();
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
