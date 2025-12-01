using Airbb.Models;
using Airbb.Models.DataLayer;
using Airbb.Models.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airbb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageResidenceController : Controller
    {
        private AirbbContext context { get; set; }
        private Repository<Location> locationRepo { get; set; }
        private Repository<Residence> residenceRepo { get; set; }
        private Repository<User> userRepo { get; set; }
        public ManageResidenceController(AirbbContext ctx)
        {
            locationRepo = new Repository<Location>(ctx);
            residenceRepo = new Repository<Residence>(ctx);
            userRepo = new Repository<User>(ctx);
            context = ctx;
        }
        public IActionResult Index()
        {
            var options = new QueryOptions<Residence>
            {
                Includes = "Location,User",
                OrderBy = r => r.Name,
                OrderByDirection = "asc"
            };
            var residences = residenceRepo.List(options).ToList();
            return View(residences);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.action = "Add";
            var locationOptions = new QueryOptions<Location>
            {
                OrderBy = l => l.Name,
                OrderByDirection = "asc"
            };

            ViewBag.Locations = locationRepo.List(locationOptions).ToList();
            var userOptions = new QueryOptions<User>
            {
                OrderBy = u => u.Name,
                OrderByDirection = "asc"
            };

            ViewBag.Users = userRepo.List(userOptions).ToList();
            return View("Edit", new Residence());
        }

        [HttpPost]
        public IActionResult Edit(Residence residence)
        {
            if (TempData["okOwner"] == null)
            {
                string msg = Check.OwnerExists(context, residence.UserId.ToString());
                if (!string.IsNullOrEmpty(msg))
                {
                    ModelState.AddModelError(nameof(residence.UserId), msg);
                    TempData["message"] = "Please fix the error";
                }
            }

            if (ModelState.IsValid)
            {
                if (residence.ResidenceId == 0)
                {
                    residenceRepo.Insert(residence);
                    TempData["message"] = $"{residence.Name} added successfully.";
                }
                else
                {
                    residenceRepo.Update(residence);
                    TempData["message"] = $"{residence.Name} updated successfully.";
                }
                residenceRepo.Save();
                return RedirectToAction("Index", "ManageResidence");
            }
            else
            {
                ViewBag.Action = residence.ResidenceId == 0 ? "Add" : "Edit";
                var locationOptions = new QueryOptions<Location>
                {
                    OrderBy = l => l.Name,
                    OrderByDirection = "asc"
                };
                ViewBag.Locations = locationRepo.List(locationOptions).ToList();
                var userOptions = new QueryOptions<User>
                {
                    OrderBy = u => u.Name,
                    OrderByDirection = "asc"
                };
                ViewBag.Users = userRepo.List(userOptions).ToList();
                return View(residence);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var locationOptions = new QueryOptions<Location>
            {
                OrderBy = l => l.Name,
                OrderByDirection = "asc"
            };
            ViewBag.Locations = locationRepo.List(locationOptions).ToList();

            var userOptions = new QueryOptions<User>
            {
                OrderBy = u => u.Name,
                OrderByDirection = "asc"
            };
            ViewBag.Users = userRepo.List(userOptions).ToList();

            var residence = residenceRepo.Get(id);
            return View(residence);
        }

        [HttpPost]
        public IActionResult Delete(Residence residence)
        {
            residenceRepo.Delete(residence);
            residenceRepo.Save();
            TempData["message"] = $"{residence.Name} Deleted Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var residence = residenceRepo.Get(id);
            return View(residence);
        }
    }
}
