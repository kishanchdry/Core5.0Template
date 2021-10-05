using Communication.Mail;
using Communication.Utilities;
using Data.Context;
using Data.Context.Identity;
using Data.IRepository;
using Data.Repository.GenericRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Services.Generic;
using Services.IServices;
using Services.IServices.Identity;
using Services.Services;
using Services.Services.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    /// <summary>
    /// Services injection resolver
    /// </summary>
    public static class ApplicationServicesExtension
    {
        /// <summary>
        /// Add services for injection
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<DataContext, DataContext>();
            services.AddTransient<AppIdentityContext, AppIdentityContext>();
            
            services.AddTransient(typeof(IGenericDataRepository<>), typeof(GenericDataRepository<>));
            services.AddTransient(typeof(GenericDataRepository<>), typeof(GenericDataRepository<>));

            services.AddScoped<IManageService, ManageService>();
            services.AddScoped<IRoleActionsService, RoleActionsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<EmailFunctions, EmailFunctions>();
            services.AddScoped<EmailHelperCore, EmailHelperCore>();

            services.AddSingleton<IExceptionLogginService, ExceptionLogginService>();

            return services;
        }
    }
}
