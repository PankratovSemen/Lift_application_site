using Microsoft.AspNetCore.Mvc;
using Lift_application.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Lift_application.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        SignInManager<Lift_applicationUser> _signManager;
        [HttpGet]
        public IActionResult Logout()
        {
            
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout_()
        {
            

            
            await _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public LoginController(SignInManager<Lift_applicationUser> signManager)
        {
            _signManager = signManager;
        }
    }
}
