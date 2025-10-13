using Microsoft.AspNetCore.Mvc;

namespace Airbb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Support()
        {
            return Content("Area: Public, Controller: Home, Action: Support");
        }
        public IActionResult CancellationPolicy()
        {
            return Content("Area: Public, Controller: Home, Action: CancellationPolicy");
        }
        public IActionResult TermsConditions()
        {
            return Content("Area: Public, Controller: Home, Action: Terms and Conditions");
        }
        public IActionResult CookiesPolicy()
        {
            return Content("Area: Public, Controller: Home, Action: Cookie Policies");
        }
    }
}
