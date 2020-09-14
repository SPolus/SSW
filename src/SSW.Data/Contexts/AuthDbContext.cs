using SSW.Data.Entitties.Auth;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSW.Data.Contexts
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext() : base("UniversityConnection")
        {
        }

        public DbSet<User> Users{ get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
