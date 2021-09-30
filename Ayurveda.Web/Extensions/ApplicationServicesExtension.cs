using Ayurveda.Web.Authorization.Policies;
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

namespace Ayurveda.Web.Extensions
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
            services.AddScoped<DataContext, DataContext>();
            services.AddScoped<AppIdentityContext, AppIdentityContext>();
            services.AddScoped(typeof(IGenericDataRepository<>), typeof(GenericDataRepository<>));

            services.AddScoped(typeof(GenericDataRepository<>), typeof(GenericDataRepository<>));

            services.AddScoped<IExceptionLogginService, ExceptionLogginService>();
            services.AddScoped<ExceptionLogginService, ExceptionLogginService>();
            services.AddScoped<SaveFormFileSerivce, SaveFormFileSerivce>();
            services.AddScoped<SendOTPService, SendOTPService>();
            services.AddScoped<IManageService, ManageService>();
            services.AddScoped<IRoleActionsService, RoleActionsService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<EmailFunctions, EmailFunctions>();
            services.AddScoped<EmailHelperCore, EmailHelperCore>();


            services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();
            services.AddScoped<IAuthorizationHandler, CustomRequirePolicyHandler>();

            return services;
        }
    }
}
