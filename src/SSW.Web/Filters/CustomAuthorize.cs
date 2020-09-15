using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSW.Web.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        private const string StudentCookie = "__AUTH_COOKIE_STUDENT";
        private const string InstructorCookie = "__AUTH_COOKIE_INSTRUCTOR";
        public CustomAuthorize()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
        }
    }

    
}