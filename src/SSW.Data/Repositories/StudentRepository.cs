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
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
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

        public Task<Student> GetByEmailAsync(string email)
        {
            return _context.Users.Where(e => e.Email == email).Select(x => x.Student).FirstOrDefaultAsync();
        }

        public Task<bool> IsStudentExists(string email)
        {
            return _context.Students.Select(x => x.User).AnyAsync(e => e.Email == email);
        }
    }
}
