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
                    //.Include(e => e.Enrollments.Select(g => g.Grade))
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
                    //.Include(e => e.Enrollments.Select(g => g.Grade))
                    .Include(e => e.Enrollments)
                    .Include(e => e.Enrollments.Select(c => c.Course))
                    .FirstOrDefaultAsync(s => s.Id == id);
            }

            return await base.GetByIdAsync(id);
        }
    }
}
