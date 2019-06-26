using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehiclePortal.Models;
using VehiclePortal.Data;
using AutoMapper;
using VehiclePortal.Common.Mapper;
using VehiclePortal.Services.Implementations;
using VehiclePortal.Services.Interfaces;

namespace VehiclePortal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<VehiclePortalDbContext>(options =>
                     options.UseSqlServer(
                         Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<VehiclePortalUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<VehiclePortalDbContext>();

            Mapper.Initialize(cfg => cfg.AddProfile<MapperConfig>());

            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IUserService, UserService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<VehiclePortalDbContext>();

                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<VehiclePortalUser>>();

                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                }

                if (userManager.FindByNameAsync("admin").Result == null)
                {
                    var adminUser = new VehiclePortalUser();
                    adminUser.VehiclePortalUsername = "admin";
                    adminUser.UserName = "admin";
                    adminUser.Email = "admin@admin.com";
                    adminUser.Address = "admindiana";
                    adminUser.Name = "admin";
                    adminUser.Balance = 9999999m;

                    string adminPassword = "123456";

                    IdentityResult result = userManager.CreateAsync(adminUser, adminPassword).Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser,"Admin").Wait();
                    }
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
