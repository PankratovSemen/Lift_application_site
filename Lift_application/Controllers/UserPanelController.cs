 using Lift_application.Areas.Identity.Data;

using Lift_application.Data;
using Lift_application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lift_application.Services;
using NuGet.Protocol.Plugins;
using Microsoft.Data.SqlClient;

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
        ParseArticlesContext parseArticles;

        public UserPanelController(SignInManager<Lift_applicationUser> signInManager,ArticlesContext context, EmailForSendContext context1, SenderEmailContext senderEmail, ILogger<AuthContext> logger, ParseContext context2,ParseArticlesContext context3) 
        { 
            SignInManager = signInManager;
            db = context;
            emsend = context1;
            SenderEmailContext = senderEmail;
            _logger = logger;
            parseContext = context2;
            parseArticles = context3;
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
            var deskr = articles.Description.Remove(400, articles.Description.Length - 400);
            var article = new Articles
            {
                Title = articles.Title,
                Description = deskr,
                Text = articles.Text,
                Author = articles.Author,
                date = articles.date,
                SourceInfo = articles.SourceInfo
            };
            db.Articles.Add(article);
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
                if (emsend.EmailForSend.FirstOrDefault(l => l.Email == email) == null)
                {


                    emsend.EmailForSend.Add(emailm);
                    emsend.SaveChanges();
                    return Redirect("~/UserPanel");
                }
                else
                {
                    _logger.LogWarning("Подписка: Пользователь уже состоит в подписке");
                    return Redirect("~/Home/Block");
                }
                
                
                
                
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

        public async Task<ActionResult> DeleteSub(string username)
        {
            if (User.IsInRole("admin"))
            {
                if(emsend.EmailForSend.FirstOrDefault(l => l.Email==username) != null)
                {
                    MailSenderService mailSender = new MailSenderService();
                    if (username == null) return Redirect("~/Home/Block");
                    var subscribe = emsend.EmailForSend.Where(m => m.Email == username).Select(m => m.Id).SingleOrDefault();
                    var delete = emsend.EmailForSend.Find(subscribe);



                    await mailSender.SendEmailAsync(username, "Удаление из рассылки", $"Здравствуйте для вашей электронной почты:{username.ToString()}, рассылка на наши новости приостоновлена.</br> Чтобы снова активировать рассылку перейдите в личный кабинет.");
                    _logger.LogWarning("Отправлено на " + username);
                    _logger.LogWarning("Удаление из рассылки пользователя " + username);
                    emsend.EmailForSend.Remove(delete);
                    emsend.SaveChanges();


                    return Redirect("~/UserPanel");
                }
                else
                {
                    return Redirect("~/Home/Block");
                }
                
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


        public async Task<ActionResult> Parser(string urls)
        {
            try
            {
                if (urls == null)
                {
                    return StatusCode(404);
                }
                AngleParse parse = new AngleParse();
                if (urls == "all")
                {
                    foreach (string str in await parse.ParseLink("https://myrosmol.ru/measures"))
                    {
                        if (parseContext.parses.FirstOrDefault(link => link.ParseResult == str) == null)
                        {
                            var parseresult = new ParseModel
                            {
                                ParseResult = str
                            };


                            await parseContext.parses.AddAsync(parseresult);
                            parseContext.SaveChanges();
                         
                               var title = await parse.ParseTitleH2(str);
                               var  text = await parse.ParseTextAisYoung(str);


                            var parsePublish = new ParseArticlesModel
                            {
                                Title = title,
                                Description = text,
                                Text = text + $"<a href='{str}'>Источник </a>",
                                Date = DateTime.UtcNow.ToString(),
                                SourceInfo = str
                            };

                            await parseArticles.AddAsync(parsePublish);
                            parseArticles.SaveChanges();
                        }
                        else
                        {
                            _logger.LogWarning("Такой сайт уже существует");
                        }


                    }

                }
                foreach (string str in await parse.ParseLink("https://роскультцентр.рф"))
                {
                    if (parseContext.parses.FirstOrDefault(link => link.ParseResult == str) == null)
                    {
                        var parseresult = new ParseModel
                        {
                            ParseResult = str
                        };


                        await parseContext.parses.AddAsync(parseresult);
                        parseContext.SaveChanges();
                        
                           var  title = await parse.ParseTitleRosCult(str);
                                              
                            var text = await parse.ParseTextRosCult(str);
                        
                        

                        var parsePublish = new ParseArticlesModel
                        {
                            Title = title,
                            Description = text,
                            Text = text + $"<a href='{str}'>Источник </a>",
                            Date = DateTime.UtcNow.ToString(),
                            SourceInfo = str
                        };

                        await parseArticles.AddAsync(parsePublish);
                        parseArticles.SaveChanges();
                    }
                    else
                    {
                        _logger.LogWarning("Такой сайт уже существует");
                    }


                }

            
                    foreach (string str in await parse.ParseLink("https://vsekonkursy.ru"))
                    {
                        if (parseContext.parses.FirstOrDefault(link => link.ParseResult == str) == null)
                        {
                            var parseresult = new ParseModel
                            {
                                ParseResult = str
                            };
                            await parseContext.parses.AddAsync(parseresult);
                            parseContext.SaveChanges();
                          
                            var  title = await parse.ParseTitle(str);
                            var text = await parse.ParseText(str);

                            var parsePublish = new ParseArticlesModel
                            {
                                Title = title,
                                Description = text,
                                Text = text + $"<a href='{str}'>Источник </a>",
                                Date = DateTime.UtcNow.ToString(),
                                SourceInfo = str
                            };

                            await parseArticles.AddAsync(parsePublish);
                            parseArticles.SaveChanges();

                        }
                        else
                        {
                            _logger.LogWarning("Такой сайт уже существует");
                        }

                    }

                    foreach (string str in await parse.ParseLink("https://bvbinfo.ru/catalog-news"))
                    {
                        if (parseContext.parses.FirstOrDefault(link => link.ParseResult == str) == null)
                        {
                            var parseresult = new ParseModel
                            {
                                ParseResult = str
                            };
                            await parseContext.parses.AddAsync(parseresult);
                            parseContext.SaveChanges();
                            var title = await parse.ParseTitle(str);
                            var text = await parse.ParseText(str);

                            var parsePublish = new ParseArticlesModel
                            {
                                Title = title,
                                Description = text,
                                Text = text + $"<a href='{str}'>Источник </a>",
                                Date = DateTime.UtcNow.ToString(),
                                SourceInfo = str
                            };

                            await parseArticles.AddAsync(parsePublish);
                            parseArticles.SaveChanges();

                        }
                        else
                        {
                            _logger.LogWarning("Такой сайт уже существует");
                        }

                    }
                foreach (string str in await parse.ParseLink("https://mmp38.ru"))
                {
                    if (parseContext.parses.FirstOrDefault(link => link.ParseResult == str) == null)
                    {
                        var parseresult = new ParseModel
                        {
                            ParseResult = str
                        };


                        await parseContext.parses.AddAsync(parseresult);
                        parseContext.SaveChanges();
                       
                           var title = await parse.ParseTitleH2MM(str);
                                            
                                              
                        
                        
                           var text = await parse.ParseTextMM(str);
                        
                       

                        var parsePublish = new ParseArticlesModel
                        {
                            Title = title,
                            Description = text,
                            Text = text + $"<a href='{str}'>Источник </a>",
                            Date = DateTime.UtcNow.ToString(),
                            SourceInfo = str
                        };

                        await parseArticles.AddAsync(parsePublish);
                        parseArticles.SaveChanges();
                    }
                    else
                    {
                        _logger.LogWarning("Такой сайт уже существует");
                    }


                }
                               
                foreach (string str in await parse.ParseLink(urls))
                {
                    if (parseContext.parses.FirstOrDefault(link => link.ParseResult == str) == null)
                    {
                        var parseresult = new ParseModel
                        {
                            ParseResult = str
                        };


                        await parseContext.parses.AddAsync(parseresult);
                        parseContext.SaveChanges();
                        var title = "";
                        if (urls == "https://myrosmol.ru/measures")
                        {
                            title = await parse.ParseTitleH2(str);
                        }
                       
                        
                        
                        else if(urls== "https://роскультцентр.рф")
                        {
                            title = await parse.ParseTitleRosCult(str);
                        }
                        else if(urls== "https://mmp38.ru")
                        {
                            title = await parse.ParseTitleH2MM(str);
                        }
                        else 
                        {
                            title = await parse.ParseTitle(str);

                        }
                        var text = "";
                        
                        if(urls== "https://роскультцентр.рф")
                        {
                            text = await parse.ParseTextRosCult(str);
                        }
                        else if (urls == "https://mmp38.ru")
                        {
                            text = await parse.ParseTextMM(str);
                        }
                        else if(urls== "https://myrosmol.ru/measures")
                        {
                            text = await parse.ParseTextAisYoung(str);
                        }
                        else
                        {
                            text = await parse.ParseText(str);
                        }

                        var parsePublish = new ParseArticlesModel
                        {
                            Title = title,
                            Description = text,
                            Text = text + $"<a href='{str}'>Источник </a>",
                            Date = DateTime.UtcNow.ToString(),
                            SourceInfo = str
                        };

                        await parseArticles.AddAsync(parsePublish);
                        parseArticles.SaveChanges();
                    }
                    else
                    {
                        _logger.LogWarning("Такой сайт уже существует");
                    }


                }

            }
            catch
            {
                
            }

            return Redirect("~/UserPanel/ParserPublish");
        }


        public ActionResult ParserResult()
        {
            return View(parseContext.parses.ToList());
        }

        



        public ActionResult ParserPublish()
        {
            return View(parseArticles.ParseArticles.ToList());
        }


        public ActionResult ParserPublishSend(int id)
        {
            //try
            //{
                if (id == null) return StatusCode(404);
                var parseArt = parseArticles.ParseArticles.Find(id);
                var deskr = parseArt.Description;
                 if (parseArt.Description.Length>=400) deskr = parseArt.Description.Remove(400, parseArt.Description.Length - 400);
            
                var article = new Articles
                {
                    Title = parseArt.Title,
                    Description = deskr,
                    Text = parseArt.Text,
                    date = DateTime.UtcNow.ToString(),
                    Author = "Администратор",
                    SourceInfo = parseArt.SourceInfo
                };

                db.Articles.Add(article);
                db.SaveChanges();
                return Redirect("~/UserPanel/ParserPublish");
            //}
            //catch
            //{
            //    return Redirect("~/UserPanel/ParserPublish");
            //}
        }


        public ActionResult ParserPublishDelete(int id)
        {
            try
            {
                if (id == null) return StatusCode(404);
                var parseArt = parseArticles.ParseArticles.Find(id);
                
               parseArticles.ParseArticles.Remove(parseArt);
                parseArticles.SaveChanges();
                return Redirect("~/UserPanel/ParserPublish");
            }
            catch
            {
                return Redirect("~/UserPanel/ParserPublish");
            }
        }


        public ActionResult OptionsSender()
        {
            
            return View();
        }
    }
}
