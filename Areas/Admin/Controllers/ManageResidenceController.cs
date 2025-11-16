using Airbb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airbb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageResidenceController : Controller
    {
        private AirbbContext context { get; set; }
        public ManageResidenceController(AirbbContext ctx) => context = ctx;
        public IActionResult Index()
        {
            var residence = context.Residence
                .Include(r => r.Location)
                .Include(r => r.User)
                .OrderBy(m => m.Name)
                .ToList();

            return View(residence);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.action = "Add";
            ViewBag.Locations = context.Location.OrderBy(g => g.Name).ToList();
            ViewBag.Users = context.User.OrderBy(g => g.Name).ToList();
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
                    context.Residence.Add(residence);
                    TempData["message"] = $"{residence.Name} added successfully.";
                }
                else
                {
                    context.Residence.Update(residence);
                    TempData["message"] = $"{residence.Name} updated successfully.";
                }

                context.SaveChanges();
                return RedirectToAction("Index", "ManageResidence");
            }
            else
            {
                ViewBag.Action = residence.ResidenceId == 0 ? "Add" : "Edit";
                ViewBag.Locations = context.Location.OrderBy(g => g.Name).ToList();
                ViewBag.Users = context.User.OrderBy(g => g.Name).ToList();
                return View(residence);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Locations = context.Location.OrderBy(g => g.Name).ToList();
            ViewBag.Users = context.User.OrderBy(g => g.Name).ToList();
            var residence = context.Residence.Find(id);
            return View(residence);
        }

        [HttpPost]
        public IActionResult Delete(Residence residence)
        {
            context.Residence.Remove(residence);
            TempData["message"] = $"{residence.Name} Deleted Successfully";
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var residence = context.Residence.Find(id);
            return View(residence);
        }
    }
}
