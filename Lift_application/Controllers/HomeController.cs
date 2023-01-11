using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lift_application.Models;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        ArticlesContext db;
        Articles art;
        public HomeController(ArticlesContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Articles.ToList());
            
        }
        public IActionResult About()
        {
            return View();
        }

       
        public IActionResult Articles(int? id)
        {
           
            if (id == null) return RedirectToAction("Index");
            ViewBag.ArticleId = id;
            


            return View();
        }
    }
}