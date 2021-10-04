using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Helper
{
    /// <summary>
    /// claim principal extention
    /// </summary>
    public static class ClaimsPrincipleExtension
    {
        /// <summary>
        /// Get email from claim
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
