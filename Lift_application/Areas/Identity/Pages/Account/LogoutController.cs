using Lift_application.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lift_application.Areas.Identity.Pages.Account
{
    public class LogoutController : Controller
    {

        private readonly SignInManager<Lift_applicationUser> _signInManage;
        
        [HttpPost]
        public async Task<IActionResult> Logout_log()
        {
            SignInManager<Lift_applicationUser> signInManage;
            
            await _signInManage.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
