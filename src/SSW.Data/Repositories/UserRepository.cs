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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UniversityDbContext _context;

        public UserRepository(UniversityDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<User> GetByEmailAndPasswordAsync(string email, string password)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public Task<User> GetByEmailAsync(string email)
        {
            return _context.Users.FirstOrDefaultAsync(e => e.Email == email);
        }
    }
}
