using SSW.Data.Entitties;
using SSW.Data.Entitties.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SSW.Web.Auth
{
    public class CustomAuthentication : IAuthentication
    {
        public HttpContext HttpContext { get; set; }


        // TODO: Inject repository

        public IPrincipal CurrentUser => throw new NotImplementedException();

        public User Login(string login, string password, bool isPersistent)
        {
            throw new NotImplementedException();
        }

        public User Login(string login)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}