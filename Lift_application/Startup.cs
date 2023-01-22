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

            services.AddControllersWithViews();
            var builder = services.AddIdentityCore<IdentityUser>();
            builder.AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<AuthContext>();

            

            
            services.AddControllersWithViews();
            //services.AddRazorPages();
            

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

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