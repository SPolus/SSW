using Autofac;
using SSW.Data.Contexts;
using SSW.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;

namespace SSW.Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.Request.IsAuthenticated)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(Roles))
            {
                return true;
            }

            var userRepository = DependencyResolver.Current.GetService<IUserRepository>();
            var user = Task.Run(async () => await userRepository.GetByEmailAsync(httpContext.User.Identity.Name)).Result;

            string role = user.Student != null ? "student" : user.Instructor != null ? "instructor" : ""; // TODO: Add admin

            if (Roles.Trim().ToLower().Contains(role))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("Error/Forbidden");
        }
    }

    
}