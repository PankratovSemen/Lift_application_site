 using Lift_application.Areas.Identity.Data;

using Lift_application.Data;
using Lift_application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lift_application.Services;
using NuGet.Protocol.Plugins;

namespace Lift_application.Controllers
{
    public class UserPanelController : Controller
    {
        SignInManager<Lift_applicationUser> SignInManager;
        ArticlesContext db;
        EmailForSendContext emsend;
        string Emails;
        SenderEmailContext SenderEmailContext;
        private readonly ILogger<AuthContext> _logger;
        ParseContext parseContext;

        public UserPanelController(SignInManager<Lift_applicationUser> signInManager,ArticlesContext context, EmailForSendContext context1, SenderEmailContext senderEmail, ILogger<AuthContext> logger, ParseContext context2) 
        { 
            SignInManager = signInManager;
            db = context;
            emsend = context1;
            SenderEmailContext = senderEmail;
            _logger = logger;
            parseContext = context2;
           
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
            if (User.IsInRole("admin"))
            {
                return View();
            }
            else
            {
                return StatusCode(403);
            }
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
                foreach (var emailspost in emsend.EmailForSend)
                {
                    await mailSender.SendEmailAsync(emailspost.Email, senderEmailModel.Subject, senderEmailModel.TextMessage);
                }

                return Redirect("CreateSender");
            
            
            
            
            
        }
        public ActionResult SubscribeList()
        {
            return View(emsend.EmailForSend.ToList());
        }

        public async Task<ActionResult> DeleteSub(int id)
        {
            if (User.IsInRole("admin"))
            {
                MailSenderService mailSender = new MailSenderService();
                if (id == null) return Redirect("~/Home/Block");
                var subscribe = emsend.EmailForSend.Find(id);
                
                
                    await mailSender.SendEmailAsync(subscribe.Email, "Удаление из рассылки", $"Здравствуйте для вашей электронной почты:{subscribe.Email.ToString()}, рассылка на наши новости приостоновлена.</br> Чтобы снова активировать рассылку перейдите в личный кабинет.");
                _logger.LogWarning("Отправлено на " + subscribe.Email);
                _logger.LogWarning("Удаление из рассылки пользователя " + subscribe.Email);
                emsend.EmailForSend.Remove(subscribe);
                emsend.SaveChanges();


                return Redirect("~/UserPanel");
            }
            else
            {
                return StatusCode(404);
            }
        }

        public ActionResult ArchiveSendsList()
        {
            return View(SenderEmailContext.ArticlesSender.ToList());
        }

        public ActionResult ArchiveSends(int id)
        {
            var sends = SenderEmailContext.ArticlesSender.Find(id);
            return View(sends);
        }

        public async Task<ActionResult> ArchiveSendsPost(int id)
        {
            if (id == null) return Redirect("~/Home/Block");
            MailSenderService mailSender = new MailSenderService();
            var archive = SenderEmailContext.ArticlesSender.Find(id);
            foreach (var emailspost in emsend.EmailForSend)
            {
                await mailSender.SendEmailAsync(emailspost.Email, archive.Subject, archive.TextMessage);
            }

            return Redirect("~/UserPanel/ArchiveSendsList");
        }

        public ActionResult ArchiveDelete(int id)
        {
            if (id == null) return StatusCode(404);
            var archive = SenderEmailContext.ArticlesSender.Find(id);
            SenderEmailContext.ArticlesSender.Remove(archive);
            SenderEmailContext.SaveChanges();
            return Redirect("~/UserPanel/ArchiveSendsList");
        }


        public async Task<ActionResult> Parser(string url)
        {
            if (url == null)
            {
                return StatusCode(404);
            }
            AngleParse parse = new AngleParse();
            var parseresult = new ParseModel
            {
                ParseResult = await parse.Parse(url)
            };
            await parseContext.parses.AddAsync(parseresult);
            parseContext.SaveChanges();

            return Redirect("~/UserPanel/ParserResult");
        }


        public ActionResult ParserResult()
        {
            return View(parseContext.parses.ToList());
        }
    }
}
