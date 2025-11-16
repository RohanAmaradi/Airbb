using Microsoft.AspNetCore.Mvc;
using Airbb.Models;

namespace Airbb.Controllers
{
    public class ValidationController : Controller
    {
        private AirbbContext context;
        public ValidationController(AirbbContext ctx) => context = ctx;

        public JsonResult CheckOwnerId(string UserId)
        {
            string msg = Check.OwnerExists(context, UserId);
            if (string.IsNullOrEmpty(msg))
            {
                TempData["okValid"] = true;
                return Json(true);
            }
            else return Json(msg);
        }
    }
}
