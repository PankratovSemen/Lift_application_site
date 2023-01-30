 using Lift_application.Areas.Identity.Data;
using Lift_application.Data;
using Lift_application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lift_application.Services;

namespace Lift_application.Controllers
{
    public class UserPanelController : Controller
    {
        SignInManager<Lift_applicationUser> SignInManager;
        ArticlesContext db;
        EmailForSendContext emsend;
        string Emails;
        SenderEmailContext SenderEmailContext;
      

        public UserPanelController(SignInManager<Lift_applicationUser> signInManager,ArticlesContext context, EmailForSendContext context1, SenderEmailContext senderEmail) 
        { 
            SignInManager = signInManager;
            db = context;
            emsend = context1;
            SenderEmailContext = senderEmail;
           
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
        [HttpGet]
        public ActionResult CreateSender()
        {
            return View();
        }
        [HttpPost]
        public async Task <ActionResult> CreateSender(SenderEmailModel senderEmailModel)
        {
            MailSenderService mailSender = new MailSenderService();


                var sends = new SenderEmailModel
                {
                    
                Title = senderEmailModel.Title,
                    Subject = senderEmailModel.Subject,
                    TextMessage = senderEmailModel.TextMessage,
                    StatusSend = senderEmailModel.StatusSend
                };
                SenderEmailContext.ArticlesSender.Add(sends);
                SenderEmailContext.SaveChanges();
            foreach(var emailspost in emsend.EmailForSend)
            {
                await mailSender.SendEmailAsync(emailspost.Email, senderEmailModel.Subject, senderEmailModel.TextMessage);
            }
            
            return Redirect("CreateSender");
            
            
            
        }
    }
}
