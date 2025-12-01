using Airbb.Models;
using Airbb.Models.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace Airbb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageUserController : Controller
    {
        private Repository<User> data { get; set; }
        public ManageUserController(AirbbContext ctx)
        {
            data = new Repository<User>(ctx);
        }
        public IActionResult Index()
        {
            var options = new QueryOptions<User>
            {
                OrderBy = u => u.Name,
                OrderByDirection = "asc"
            };
            var users = data.List(options).ToList();
            return View(users);
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
            var user = data.Get(id);
            return View(user);
        }


        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.UserId == 0)
                {
                    data.Insert(user);
                    TempData["message"] = $"{user.Name} added successfully.";
                }
                else
                {
                    data.Update(user);
                    TempData["message"] = $"{user.Name} updated successfully.";
                }
                data.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Action = user.UserId == 0 ? "Add" : "Edit";
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = data.Get(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(User user)
        {
            data.Delete(user);
            data.Save();
            TempData["message"] = $"{user.Name} Deleted Successfully";
            return RedirectToAction("Index", "ManageUser");
        }
    }
}
