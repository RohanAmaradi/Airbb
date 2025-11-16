using Airbb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airbb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageLocationController : Controller
    {
        private AirbbContext context { get; set; }
        public ManageLocationController(AirbbContext ctx) => context = ctx;
        public IActionResult Index()
        {
            var location = context.Location
                .OrderBy(m => m.Name)
                .ToList();

            return View(location);
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
                    context.Location.Add(location);
                    TempData["message"] = $"{location.Name} Added Successfully";
                }
                else
                {
                    context.Location.Update(location);
                    TempData["message"] = $"{location.Name} Updated Successfully";
                }
                context.SaveChanges();
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
            var location = context.Location.Find(id);
            return View(location);
        }

        [HttpPost]
        public IActionResult Delete(Location location)
        {
            context.Location.Remove(location);
            TempData["message"] = $"{location.Name} Deleted Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Location location = context.Location
                    .FirstOrDefault(p => p.LocationId == id) ?? new Location();
            return View(location);
        }
    }
}
