using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.IServices.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Helper
{
    /// <summary>
    /// User manager extention
    /// </summary>
    public static class UserManagerExtension
    {
        /// <summary>
        /// find user by claims
        /// </summary>
        /// <param name="input"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        //This will help to get the address associated with email instead of injecting appIdentityContext
        public static async Task<User> FindUserByClaimsPrincipleWithAddressAsync(this IUserService input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        /// <summary>
        /// find user from claims
        /// </summary>
        /// <param name="input"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static async Task<User> FindByEmailFromClaimsPrinciple(this IUserService input, ClaimsPrincipal user)
        {
            var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            return await input.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}
