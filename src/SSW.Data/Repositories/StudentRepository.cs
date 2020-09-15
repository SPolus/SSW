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
    public class StudentRepository : EfRepository<Student>, IStudentRepository
    {
        private readonly UniversityDbContext _context;

        public StudentRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Student>> GetAllAsync(bool includeOptions = true)
        {
            if (includeOptions)
            {
                return await _context.Students
                    .Include(e => e.Enrollments)
                    .Include(e => e.Enrollments.Select(c => c.Course))
                    .ToListAsync();
            }

            return await base.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool includeOptions = true)
        {
            if (includeOptions)
            {
                return await _context.Students
                    .Include(e => e.Enrollments)
                    .Include(e => e.Enrollments.Select(c => c.Course))
                    .FirstOrDefaultAsync(s => s.Id == id);
            }

            return await base.GetByIdAsync(id);
        }

        public async Task<Student> GetByEmailAsync(string email)
        {
            return await _context.Students.Where(s => s.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> IsStudentExists(string email)
        {
            var student = await _context.Students.Where(s => s.Email == email).FirstOrDefaultAsync();

            return student != null;
        }
    }
}
