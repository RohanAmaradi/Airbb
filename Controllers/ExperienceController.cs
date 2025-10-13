using Microsoft.AspNetCore.Mvc;

namespace Airbb.Controllers
{
    public class ExperienceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Experiences List Page - Area: Public, Controller: Experience, Action: List, ID: {id}");
        }
    }
}
