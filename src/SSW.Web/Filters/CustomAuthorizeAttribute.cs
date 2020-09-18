using SSW.Data.Contexts;
using SSW.Data.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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

            string role = Task.Run(async () => await userRepository.GetRoleAsync(httpContext.User.Identity.Name)).Result;

            if (Roles.Trim().ToLower().Contains(role))
            {
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Error/Forbidden");
        }
    }

    
}