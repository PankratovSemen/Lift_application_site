using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lift_application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Owin;

using Microsoft.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Lift_application.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace Lift_application.Controllers
{
    
    public class HomeController : Controller
    {
        SignInManager<Lift_applicationUser> signInManager;
        ArticlesContext db;
        Articles art = new Articles();
        public HomeController(ArticlesContext context, SignInManager<Lift_applicationUser> sign)
        {
            db = context;
            signInManager = sign;
        }
        public IActionResult Index()
        {
            return View(db.Articles.ToList());

        }
        
        public IActionResult About()
        {
            var role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role: manage";
             var claims = User.Claims.ToList();
            if (signInManager.IsSignedIn(User))
            {
                if (claims[4].ToString() == role)
                {
                    return View();
                }

                else
                {
                    return RedirectToAction("Block", "Home");
                }
            }
            else
            {
                return RedirectToAction("Block", "Home");
            }
                



        }
        public IActionResult Block()
        {
            return View();
        }
        public string Word()
        {
            var claims = User.Claims.ToList();
            return claims[4].ToString();
        }
        
        public IActionResult Articles(int? id)
        {
           
            if (id == null) return RedirectToAction("Index");
            var article = db.Articles.Find(id);
                        
            return View(article);
        }

        
    }
}