using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SiteLenroo.Models;

[assembly: HostingStartup(typeof(SiteLenroo.Areas.Identity.IdentityHostingStartup))]
namespace SiteLenroo.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<SiteLenrooContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SiteLenrooContextConnection")));

                services.AddDefaultIdentity<LenrooUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<SiteLenrooContext>();

                services.AddAuthorization(options =>
                {
                    options.AddPolicy("readpolicy",
                        builders => builders.RequireRole("moderator"));
                    options.AddPolicy("writepolicy", 
                        builders => builders.RequireRole("admin"));
                });
            });
        }
    }
}