using Airbb.Models;
using Airbb.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Airbb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageLocationController : Controller
    {
        private Repository<Location> data { get; set; }
        public ManageLocationController(AirbbContext ctx)
        {
            data = new Repository<Location>(ctx);
        }
        public IActionResult Index()
        {
            var options = new QueryOptions<Location>
            {
                OrderBy = l => l.Name,
                OrderByDirection = "asc"
            };
            var locations = data.List(options).ToList();
            return View(locations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.action = "Add";
            return View("Edit", new Location());
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                if (location.LocationId == 0)
                {
                    data.Insert(location);
                    TempData["message"] = $"{location.Name} Added Successfully";
                }
                else
                {
                    data.Update(location);
                    TempData["message"] = $"{location.Name} Updated Successfully";
                }
                data.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.action = (location.LocationId == 0) ? "Add" : "Edit";
                return View("Edit", location);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.action = "Edit";
            var location = data.Get(id);
            return View(location);
        }

        [HttpPost]
        public IActionResult Delete(Location location)
        {
            data.Delete(location);
            data.Save();
            TempData["message"] = $"{location.Name} Deleted Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var options = new QueryOptions<Location>
            {
                Where = l => l.LocationId == id
            };
            var location = data.Get(options) ?? new Location();
            return View(location);
        }

    }
}
