using SSW.Data.Contexts;
using SSW.Data.Entities;
using SSW.Data.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SSW.Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly Role[] _roles;

        public CustomAuthorizeAttribute(params Role[] roles)
        {
            _roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated)
                return false;

            if (_roles.Length == 0)
                return true;

            var userRepository = DependencyResolver.Current.GetService<IRepository<User>>();

            var f = Task.Run(() => userRepository.FirstOrDefaultAsync(x => x.Email == httpContext.User.Identity.Name, u => new { u.Student, u.Instructor })).Result;

            Role role = f.Student != null ? Role.Student : f.Instructor != null ? Role.Instructor : Role.Admin;

            return _roles.Contains(role);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Error/Forbidden");
        }
    }

    public enum Role
    {
        Student,
        Instructor,
        Admin
    }

    
}