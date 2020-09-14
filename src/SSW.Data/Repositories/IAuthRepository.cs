using SSW.Data.Entitties.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Repositories
{
    public interface IAuthRepository
    {
        IQueryable<Role> Roles { get; }
        bool CreateRole(Role instance);
        bool UpdateRole(Role instance);
        bool RemoveRole(Role instance);
    }
}
