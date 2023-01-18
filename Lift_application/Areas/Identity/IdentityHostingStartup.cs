using System;
using Lift_application.Areas.Identity.Data;
using Lift_application.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;

[assembly: HostingStartup(typeof(Lift_application.Areas.Identity.IdentityHostingStartup))]
namespace Lift_application.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthContextConnection")));
                services.AddDefaultIdentity<Lift_applicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AuthContext>();


                
                // services.AddAuthentication()
                //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme + 2);


                // services.AddAuthorization(options => options.DefaultPolicy =
                //     new AuthorizationPolicyBuilder
                //         (CookieAuthenticationDefaults.AuthenticationScheme,
                //          CookieAuthenticationDefaults.AuthenticationScheme + 2)
                //         .RequireAuthenticatedUser()
                //         .Build());


                //services.AddDefaultIdentity<Lift_applicationUser>(options =>
                //{
                //    options.SignIn.RequireConfirmedAccount = false;
                //    options.Password.RequireLowercase= false;
                //    options.Password.RequireUppercase= false;
                //})
                //    .AddEntityFrameworkStores<AuthContext>();


            });
        }
    }
}
