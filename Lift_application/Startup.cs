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
                .AddOAuth("VK", "VK",config =>
                {
                    config.ClientId = "51544073";
                    config.ClientSecret = "NhdxCteqtD7oQ21jZpFm";
                    config.ClaimsIssuer = "Vkontakte";
                    config.CallbackPath = new PathString("/sigin-vk-token");
                    config.AuthorizationEndpoint = "https://oauth.vk.com/authorize";
                    config.TokenEndpoint = "https://oauth.vk.com/access_token";
                    config.Scope.Add("email");
                    config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
                    config.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    config.SaveTokens  = true;
                });
            var builder = services.AddIdentityCore<IdentityUser>();
            builder.AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<AuthContext>();

            

            
            services.AddControllersWithViews();
            //services.AddRazorPages();
            

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