using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Lift_application.Models;   // пространство имен моделей
using Microsoft.EntityFrameworkCore; // пространство имен EntityFramework
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Lift_application.Areas.Identity.Data;
using Lift_application.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;


namespace Lift_application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ArticlesContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<EmailForSendContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<SenderEmailContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ParseContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ParseArticlesContext>(options => options.UseSqlServer(connection));


            services.AddControllersWithViews();
            services.AddAuthentication()
                .AddVkontakte(options =>
                {
                    options.ApiVersion = "5.8";
                    options.ClientId = Configuration["VkAuth:AppId"];
                    options.ClientSecret = Configuration["VkAuth:AppSecret"];
                    options.Scope.Add("email");
                    options.SaveTokens = true;
                    options.CallbackPath = new PathString("/auth-vk");

                    options.Events.OnCreatingTicket = ctx =>
                    {
                        var tokens = ctx.Properties.GetTokens() as List<AuthenticationToken>;
                        tokens.Add(new AuthenticationToken()
                        {
                            Name = "TicketCreated",
                            Value = DateTime.UtcNow.ToString()
                        });
                        ctx.Properties.StoreTokens(tokens);
                        return Task.CompletedTask;
                    }; ;
                });
            var builder = services.AddIdentityCore<IdentityUser>();
            builder.AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<AuthContext>();

            

            
            services.AddControllersWithViews();
            //services.AddRazorPages();
            

        }

        private Task OnFailure(RemoteFailureContext arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=600");
                }
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            



        }
    }
}