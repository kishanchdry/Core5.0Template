using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Authorization.Policies
{
    /// <summary>
    /// Authorize with custom claim
    /// </summary>
    public class CustomRequireClaim : IAuthorizationRequirement
    {
        /// <summary>
        /// custom requie claim
        /// </summary>
        /// <param name="claimType"></param>
        public CustomRequireClaim(string claimType)
        {
            ClaimType = claimType;
        }

        /// <summary>
        /// claim type
        /// </summary>
        public string ClaimType { get; }
    }

    /// <summary>
    /// Require claim handler
    /// </summary>
    public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
    {
        /// <summary>
        /// Handle claims
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, CustomRequireClaim requirement)
        {
            var hasClaim = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
            if (hasClaim)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
