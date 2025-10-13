using Microsoft.AspNetCore.Mvc;

namespace Airbb.Controllers
{
    public class ResidenceController : Controller
    {
        public IActionResult List(string id = "All")
        {
            return Content($"Residence List Page - Area: Public, Controller: Residence, Action: List, ID: {id}");
        }
    }
}
