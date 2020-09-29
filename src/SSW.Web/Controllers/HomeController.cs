using System.Web.Mvc;

namespace SSW.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var a = View();
            return a;
        }
    }
}