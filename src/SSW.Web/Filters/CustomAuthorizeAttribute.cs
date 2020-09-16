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
            var encriptedCookie = httpContext.Request.Cookies[ConfigurationManager.AppSettings["AuthCookie"]]?.Value;

            if (encriptedCookie == null)
            {
                return false;
            }
            
            var cookie = FormsAuthentication.Decrypt(encriptedCookie);

            var studentRepository = DependencyResolver.Current.GetService<IStudentRepository>();
            var instructorRepository = DependencyResolver.Current.GetService<IInstructorRepository>();
            
            bool isStudent = Task.Run(async () => await studentRepository.IsStudentExists(cookie.Name)).Result;
            bool isInstructor = Task.Run(async () => await instructorRepository.IsInstructorExists(cookie.Name)).Result;
            bool isAdmin = false;

            string role = isStudent ? "student" : isInstructor ? "instructor" : isAdmin ? "admin" : "";

            if (string.IsNullOrWhiteSpace(Roles) && (isStudent || isInstructor || isAdmin))
            {
                return true;
            }

            if (Roles.Trim().ToLower().Contains(role))
            {
                return true;
            }

            return false;

        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("Accounts/Login");
        }
    }

    
}