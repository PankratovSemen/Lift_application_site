using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lift_application.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Owin;

using Microsoft.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Lift_application.Controllers
{

    public class HomeController : Controller
    {
        ArticlesContext db;
        Articles art = new Articles();
        public HomeController(ArticlesContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Articles.ToList());

        }
        [Authorize]
        public IActionResult About()
        {

            //if (User.Identity.IsAuthenticated)
            //{
            //    // get user's claims

            //    return View(claims);

            //}
           
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