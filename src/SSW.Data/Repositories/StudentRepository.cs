using SSW.Data.Contexts;
using SSW.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
                var a = await _context.Students
                    .Select(s => new
                    {
                        s.Id,
                        s.FirstName,
                        s.LastName,
                        s.Enrollments,
                        s.User
                    })
                    .ToListAsync();

                var students = new List<Student>();

                foreach (var student in a)
                {
                    students.Add(new Student
                    {
                        Id = student.Id,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        Enrollments = student.Enrollments,
                        User = student.User
                    });
                }

                return students;
            }

            return await base.GetAllAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool includeOptions = true)
        {
            if (includeOptions)
            {
                var a = await _context.Students
                    .Select(s => new
                    {
                        s.Id,
                        s.FirstName,
                        s.LastName,
                        s.Enrollments,
                        s.User
                    })
                    .FirstOrDefaultAsync(x => x.Id == id);

                var student = new Student
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Enrollments = a.Enrollments,
                    User = a.User
                };

                return student;
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
