using SSW.Data.Entitties;
using SSW.Data.Entitties.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSW.Web.Auth
{
    public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }
        User Login(string login, string password, bool isPersistent);
        User Login(string login);
        void LogOut();
        IPrincipal CurrentUser { get; }
    }
}
