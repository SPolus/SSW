using SSW.Data.Entitties;
using System.Web;
using System.Web.Security;

namespace SSW.Web.Services
{
    public class CookieService
    {
        public void SetAuthenticationToken(string name, bool isPersistent, User userData, int timeout)
        {
            var ticket = new FormsAuthenticationTicket(userData.Email, isPersistent, timeout);

            var cookieData = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieData)
            {
                HttpOnly = true,
                Expires = ticket.Expiration
            };

            //FormsAuthentication.SetAuthCookie(userData.Email, isPersistent);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}