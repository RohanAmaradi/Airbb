using Microsoft.AspNetCore.Mvc;

namespace Airbb.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Service(string id = "All")
        {
            return Content($"Services List Page - Area: Public, Controller: Service, Action: List, ID: {id}");
        }
    }
}
