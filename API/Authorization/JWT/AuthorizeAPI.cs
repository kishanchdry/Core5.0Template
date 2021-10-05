using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.IServices.Identity;
using Shared.Common.Enums;
using Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Authorization.JWT
{
    /// <summary>
    /// Authorize an api call
    /// </summary>
    public class AuthorizeAPI : ActionFilterAttribute
    {
        IUserService _userService;

        /// <summary>
        /// initializa api authorization params
        /// </summary>
        /// <param name="userService"></param>
        public AuthorizeAPI(IUserService userService)
        {
            _userService = userService;
        }
        /// <inheritdoc />
        /// <summary>
        /// On request comes check for valid request
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var claimsIdentity = actionContext.HttpContext.User.Identity as ClaimsIdentity;
            var userId = string.Empty;
            if (claimsIdentity != null)
            {
                userId = claimsIdentity.FindFirst("UserId").Value.ToString();
                if (!string.IsNullOrEmpty(userId) && !(_userService.FindByIdAsync(userId).Result.IsActive))
                {
                    var response =
                         (new ApiResponses<string>(ResponseMsg.IncorrectUserLogin, null, new List<string>(), failureMsg: ResponseMsg.IncorrectUserLogin.ToDescriptionString()));
                    actionContext.Result = new ObjectResult(response);
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}