using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Generic;
using Services.IServices.Identity;
using Services.Services.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ayurveda.Web.Extensions;

namespace Ayurveda.Web.Authorization.Policies
{
    /// <summary>
    /// Authorize with custom claim
    /// </summary>
    public class CustomRequirePolicy : IAuthorizationRequirement
    {
        /// <summary>
        /// custom requie claim
        /// </summary>
        public CustomRequirePolicy()
        {
        }
    }

    /// <summary>
    /// Require claim handler
    /// </summary>
    public class CustomRequirePolicyHandler : AuthorizationHandler<CustomRequirePolicy>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRoleActionsService _roleActionsService;
        private readonly IUserService _userManager;

        /// <summary>
        /// construct srvices
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="roleActionsService"></param>
        /// <param name="userManager"></param>
        public CustomRequirePolicyHandler(IHttpContextAccessor httpContextAccessor, IRoleActionsService roleActionsService, IUserService userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _roleActionsService = roleActionsService;
            _userManager = userManager;
        }

        /// <summary>
        /// Handle claims
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirePolicy requirement)
        {
            List<string> s = new List<string>() { "_EditUser" };
            connectedAction.Add("EditUser", s);

            var actions = CacheService.GetOrSet<RoleAction>(ApplicationConstants.CacheRoleActionsKey, _roleActionsService.GetAll);
            var userRoles = CacheService.GetOrSet<UserRole>(ApplicationConstants.CacheUserRoleKey, _roleActionsService.GetAllUserRole);

            var userId = _userManager.GetUserId(context.User);

            bool hasAccess = false;
            var values = _httpContextAccessor.HttpContext.Request.RouteValues;
            string controllerName = values["controller"].ToString();
            string actionName = values["action"].ToString();

            if (connectedAction.Any(e => e.Value.Contains(actionName)))
            {
                var roleReuired = connectedAction.First(e => e.Value.Contains(actionName));
                actionName = roleReuired.Key;
            }

            if (actions.Any(e => userRoles.Any(x => x.UserId == userId && e.RoleId == x.RoleId) && (e.Controller == controllerName || e.Action == string.Format("{0}Controller/{1}", controllerName, actionName))))
            {
                hasAccess = true;
            }

            if (hasAccess)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }

        Dictionary<string, List<string>> connectedAction = new Dictionary<string, List<string>>();
    }
}
