using Data.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.IServices.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    /// <summary>
    /// Global extention variables
    /// </summary>
    public static class GlobalVariables
    {
        /// <summary>
        /// Get logged in user id
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="httpContext">http context</param>
        /// <returns></returns>
        public async static Task<string> GetLoggedInUserId(this IUserService userManager, HttpContext httpContext)
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            return user.Id;
        }

        /// <summary>
        /// Get logged in user name
        /// </summary>
        /// <param name="userManager">User manager</param>
        /// <param name="httpContext">http context</param>
        /// <returns></returns>
        public async static Task<string> GetLoggedInUserName(this IUserService userManager, HttpContext httpContext)
        {
            var user = await userManager.GetUserAsync(httpContext.User);
            return user.UserName;
        }
    }
}
