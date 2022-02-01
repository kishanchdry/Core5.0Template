using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Data.Context.Identity;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Web.Authorization.Policies;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Data.Context;
using Web.Extensions;
using Web.Helper;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Common.Enums;
using Shared.Models.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shared.Models.API;
using Shared.Common;
using Microsoft.AspNetCore.DataProtection;

namespace Web
{
    /// <summary>
    /// web app startup program
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// web configuration
        /// </summary>

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment Env { get; }

        /// <summary>
        /// web startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        /// <summary>
        /// web service configuration
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();

            services.Configure<JwtTokenSettings>(Configuration.GetSection("JwtTokenSettings"));//for jwt token
            services.Configure<ConfigurationKeys>(Configuration.GetSection("ConfigurationKeys"));//for configuration keys

         

            #region Contexes
            services.AddDbContext<DataContext>(config =>
                {
                    config.UseSqlServer(Configuration.GetConnectionString("IdentityConection"));
                });

            services.AddDbContext<AppIdentityContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("IdentityConection"));
            });

            services.AddDataProtection().PersistKeysToDbContext<AppIdentityContext>().SetApplicationName("App");

            #endregion

            #region Identity
            //AddIdentity register the services
            services.AddIdentity<User, Role>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 2;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            }).
                AddEntityFrameworkStores<AppIdentityContext>().
                AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddGoogle(option =>
                {
                    option.ClientId = "620481322673-mmm3d49qha1ie4qv0fmb9cf8dj46fv37.apps.googleusercontent.com";
                    option.ClientSecret = "2YrLPLxsh5o6R0paavYT8fbl";
                });
            #endregion

            #region Cookies
            services.ConfigureApplicationCookie(options =>
                {
                    //Cookie settings
                    //options.Cookie.Name = "Identitye.Cookie";
                    //options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    //options.SlidingExpiration = true;
                });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            #endregion

            #region Authorization
            //add Web and API level authorization
            services.AddAuthorization(config =>
            {
                config.AddPolicy("AuthorisedUser", policy =>
                {
                    policy.RequireClaim("UserId");
                });

                config.AddPolicy("AdminRolePolicy", policy =>
                {
                    policy.RequireClaim("Device");
                    policy.RequireClaim("DeviceType");
                    policy.RequireClaim("Offset");
                });

                config.AddPolicy("RoleAccess", policy =>
                {
                    policy.RequireCustomPolicy();
                });

                config.AddPolicy("AdminAccess", policy =>
                {
                    policy.RequireClaim("UserRole",
                        UserTypes.Admin.ToDescriptionString());
                });

                config.AddPolicy("ManagerAccess", policy =>
                {
                    policy.RequireClaim("UserRole",
                        UserTypes.Admin.ToDescriptionString(),
                        UserTypes.Manager.ToDescriptionString());
                });

                config.AddPolicy("UserAccess", policy =>
                {
                    policy.RequireClaim("UserRole",
                        UserTypes.Admin.ToDescriptionString(),
                        UserTypes.Manager.ToDescriptionString(),
                        UserTypes.User.ToDescriptionString());
                });

                config.AddPolicy("GuestAccess", policy =>
                {
                    policy.RequireClaim("UserRole",
                        UserTypes.Admin.ToDescriptionString(),
                        UserTypes.Manager.ToDescriptionString(),
                        UserTypes.User.ToDescriptionString(),
                        UserTypes.Guest.ToDescriptionString());
                });

            });
            #endregion

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServices();

            #region Runtime allow page editing 
            IMvcBuilder builder = services.AddRazorPages();
#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif 
            #endregion

   

        }

        /// <summary>
        /// web host configuration
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Account/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            #region Error Pages
            //show error page based on the error
            IList<int> errorList = new List<int>() { 400, 401, 403, 404, 405, 406, 407, 412, 414, 415, 500, 501, 502, 503 };
            //handle error pages
            app.Use(async (context, next) =>
            {
                await next();

                if (errorList.Contains(context.Response.StatusCode))
                {
                    context.Request.Path = string.Format("/Error/ShowError/{0}", context.Response.StatusCode);
                    await next();
                }
                else if (context.Response.StatusCode != 200)
                {
                    context.Request.Path = "/Account/Error";
                    await next();
                }
            });
            #endregion

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

           

            #region Routing
            app.UseEndpoints(endpoints =>
                {
                    //area routings
                    endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                    //default routings
                    endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "{controller=Home}/{action=Index}/{id?}");

                    //admin routings
                    endpoints.MapAreaControllerRoute("admin",
                            "admin",
                            "{area=admin}/{controller=Home}/{action=Index}/{id?}");

                    
                });
            #endregion
        }
    }
}
