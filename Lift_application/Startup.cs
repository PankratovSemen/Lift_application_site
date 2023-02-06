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
using AspNet.Security.OAuth.VK;

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
                .AddVK(options =>
                {
                    options.ClientId = "51544911";
                    options.ClientSecret = "AY3dWua7YnvTR3FqkUuG";

                    // Request for permissions https://vk.com/dev/permissions?f=1.%20Access%20Permissions%20for%20User%20Token
                    options.Scope.Add("email");

                    // Add fields https://vk.com/dev/objects/user
                    options.Fields.Add("uid");
                    options.Fields.Add("first_name");
                    options.Fields.Add("last_name");
                    options.CallbackPath = new PathString("/sign-in-vk");
                    // In this case email will return in OAuthTokenResponse, 
                    // but all scope values will be merged with user response
                    // so we can claim it as field
                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "uid");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
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