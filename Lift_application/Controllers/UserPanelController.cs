using Microsoft.AspNetCore.Mvc;

namespace Lift_application.Controllers
{
    public class UserPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
