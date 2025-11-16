using Airbb.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airbb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageUserController : Controller
    {
        private AirbbContext context { get; set; }
        public ManageUserController(AirbbContext ctx) => context = ctx;
        public IActionResult Index()
        {
            var user = context.User
                .OrderBy(m => m.Name)
                .ToList();

            return View(user);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new User());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var user = context.User.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.UserId == 0)
                {
                    context.User.Add(user);
                    TempData["message"] = $"{user.Name} Added Successfully";
                }
                else
                {
                    context.User.Update(user);
                    TempData["message"] = $"{user.Name} Updated Successfully";
                }
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.action = (user.UserId == 0) ? "Add" : "Edit";
                return View("Edit", user);
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = context.User.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            context.User.Remove(user);
            TempData["message"] = $"{user.Name} Deleted Successfully";
            context.SaveChanges();
            return RedirectToAction("Index", "ManageUser");
        }
    }
}
