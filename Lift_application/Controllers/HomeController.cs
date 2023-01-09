using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Lift_application.Models;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        ArticlesContext db;
        public HomeController(ArticlesContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Articles.ToList());
        }
    }
}