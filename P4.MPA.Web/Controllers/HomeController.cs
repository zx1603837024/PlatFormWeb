using System.Web.Mvc;

namespace P4.Web.Controllers
{
    public class HomeController : P4ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}