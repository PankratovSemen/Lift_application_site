 using Lift_application.Areas.Identity.Data;
using Lift_application.Data;
using Lift_application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lift_application.Controllers
{
    public class UserPanelController : Controller
    {
        SignInManager<Lift_applicationUser> SignInManager;
        ArticlesContext db;
        EmailForSendContext emsend;
        string Emails;
      

        public UserPanelController(SignInManager<Lift_applicationUser> signInManager,ArticlesContext context, EmailForSendContext context1) 
        { 
            SignInManager = signInManager;
            db = context;
            emsend = context1;
           
           
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

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("admin"))
            {
                Articles b = db.Articles.Find(id);
                if (b == null)
                {
                    return StatusCode(404);
                }
                return View(b);
            }
            else
            {
                return StatusCode(403);
            }
                
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.IsInRole("admin"))
            {
                Articles b = db.Articles.Find(id);
                if (b == null)
                {
                    return StatusCode(404);
                }
                db.Articles.Remove(b);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode(403);
            }
            
        }

        [HttpGet]
        public ActionResult Sender(string email)
        {
            
            Emails = email;
            EmailForSend emailm = new EmailForSend
            {
                
                Email = Emails
            };
            if (Emails != null)
            {
                emsend.EmailForSend.Add(emailm);
                emsend.SaveChanges();
                return Redirect("~/UserPanel");
            }
            else
            {
                return StatusCode(404);
            }

            
            
        }
        //[HttpPost]
        //public ActionResult Sender(string Email)
        //{
            
        //}
    }
}
