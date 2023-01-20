using Lift_application.Areas.Identity.Data;
using Lift_application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lift_application.Controllers
{
    public class UserPanelController : Controller
    {
        SignInManager<Lift_applicationUser> SignInManager;
        ArticlesContext db;

        public UserPanelController(SignInManager<Lift_applicationUser> signInManager,ArticlesContext context)
        {
            SignInManager = signInManager;
            db = context;
        }

        public IActionResult Index()
        {
            if (SignInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Block");
            }
        }
        [HttpGet]
        public IActionResult Publish()
        {
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else
            {
                return Redirect("~/Home/Block");
            }
        }
        [HttpPost]

        public IActionResult Publish(Articles articles)
        {
            db.Articles.Add(articles);
            db.SaveChanges();
            return Redirect("~/UserPanel/Publish");
        }

    }
}
