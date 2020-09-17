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
    public class InstructorRepository : EfRepository<Instructor>, IInstructorRepository
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

        //public async Task<Instructor> GetByEmailAsync(string email)
        //{
        //    return await _context.Instructors.Where(i => i.Email == email).FirstOrDefaultAsync();
        //}

        //public async Task<bool> IsInstructorExists(string email)
        //{
        //    var instructor = await GetByEmailAsync(email);

        //    return instructor != null;
        //}
    }
}
