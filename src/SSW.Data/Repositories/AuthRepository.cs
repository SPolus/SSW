using SSW.Data.Contexts;
using SSW.Data.Entitties.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;

        // TODO: dependency injection
        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }


        public IQueryable<Role> Roles => _context.Roles;

        public bool CreateRole(Role instance)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRole(Role instance)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRole(Role instance)
        {
            throw new NotImplementedException();
        }
    }
}
