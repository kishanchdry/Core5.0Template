using API.Authorization.JWT;
using Data.Context;
using Data.Context.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Shared.Common.Enums;
using Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using API.Extensions;
using Shared.Models.API;
using Shared.Common;

namespace API
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

            services.AddControllers();

            #region JWT token authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateIssuerSigningKey = true,
                       ValidateLifetime = true,
                       ValidIssuer = Configuration.GetValue<string>("JwtTokenSettings:IsUser"),
                       ValidAudience = Configuration.GetValue<string>("JwtTokenSettings:Audience"),
                       IssuerSigningKey = JwtSecurityKey.Create(Configuration.GetValue<string>("JwtTokenSettings:Secrete")),

                       //RequireExpirationTime = true,
                       //ClockSkew = TimeSpan.Zero
                   };


                   options.Events = new JwtBearerEvents
                   {
                       OnAuthenticationFailed = context =>
                       {
                           context.NoResult();
                           context.Response.StatusCode = ResponseCode.Unauthorized;
                           context.Response.ContentType = "application/json";
                           context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(
                               new ApiResponses<string> { FailureMsg = ResponseStatus.UnauthorizedMessage, ValidationErrors = new List<string>(), ResponseCode = (short)ResponseCode.Unauthorized, Data = null }

                           , new JsonSerializerSettings
                           {
                               ContractResolver = new CamelCasePropertyNamesContractResolver()
                           })).Wait();
                           return Task.CompletedTask;
                       },
                       OnTokenValidated = context =>
                       {
                           return Task.CompletedTask;
                       }
                   };
               });
            #endregion

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
            });
            #endregion

            #region Swagger
            //swagger integration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Apna Fram API",
                    Description = "The objective of this API is to develop an effective mobile app for customers get grocery direct from farm.",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            #endregion


            services.Configure<JwtTokenSettings>(Configuration.GetSection("JwtTokenSettings"));//for jwt token
            services.Configure<ConfigurationKeys>(Configuration.GetSection("ConfigurationKeys"));//for configuration keys
            services.AddServices();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            #region Swagger UI
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apna Farm");
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //redirect api part to the swagger
                endpoints.MapGet("API", async context =>
                {
                    context.Response.Redirect("/swagger/");
                });
            });
        }
    }
}
