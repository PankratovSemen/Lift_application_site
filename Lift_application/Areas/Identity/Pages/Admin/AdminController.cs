using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lift_application.Areas.Identity.Pages.Admin
{
    public class AdminController : Controller
    {
        [Authorize(Roles ="user")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
