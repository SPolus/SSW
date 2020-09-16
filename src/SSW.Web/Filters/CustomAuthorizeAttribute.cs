using SSW.Data.Contexts;
using SSW.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SSW.Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.Cookies.AllKeys.Contains("__AUTH_COOKIE_STUDENT"))
            {
                var encriptedCookie = httpContext.Request.Cookies["__AUTH_COOKIE_STUDENT"].Value;
                var cookie = FormsAuthentication.Decrypt(encriptedCookie);

                using (UniversityDbContext context = new UniversityDbContext())
                {
                    var student = context.Students.Where(s => s.Email == cookie.Name).FirstOrDefault();

                    bool isStudent = student != null;

                    return Roles.ToLower().Contains("student") && isStudent;
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("Home");
            //base.HandleUnauthorizedRequest(filterContext);
        }
    }

    
}