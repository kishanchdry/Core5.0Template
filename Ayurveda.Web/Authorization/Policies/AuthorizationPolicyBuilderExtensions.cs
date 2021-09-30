using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ayurveda.Web.Authorization.Policies
{
    /// <summary>
    /// Authorization builder
    /// </summary>
    public static class AuthorizationPolicyBuilderExtensions
    {
        /// <summary>
        /// Require claim
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static AuthorizationPolicyBuilder RequireCustomClaim(this AuthorizationPolicyBuilder builder, string claimType)
        {
            builder.AddRequirements(new CustomRequireClaim(claimType));
            return builder;
        }

        /// <summary>
        /// custom policy
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static AuthorizationPolicyBuilder RequireCustomPolicy(this AuthorizationPolicyBuilder builder)
        {
            builder.AddRequirements(new CustomRequirePolicy());
            return builder;
        }
    }
}
