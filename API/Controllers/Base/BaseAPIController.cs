using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;
using Shared.Models.Base;
using Shared.Common.Enums;

namespace Web.Areas.API.Controllers.Base
{
    /// <summary>
    /// Base api controller for authorize the user
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        /// <summary>
        /// base acuthorization
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        [NonAction]
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authKey = context.HttpContext.Request.Headers["UserId"].ToString();
            var Offset = Convert.ToString(context.HttpContext.Request.Headers["Offset"].ToString());

            if (string.IsNullOrEmpty(Offset))
            {
                var response = new ApiResponses<string>(ResponseMsg.EmailRequiredFBLogin, null, new List<string>(), failureMsg: ResponseMsg.OffsetNotSent.ToDescriptionString());

                context.Result = new BadRequestObjectResult(response);
            }
            else
            {
                //ValidateAuthKey(authKey);
                await next();
            }
        }

        private ClaimsIdentity Identity
        {
            get
            {
                return (ClaimsIdentity)User.Identity;
            }
        }

        private ClaimsIdentity AdminIdentity
        {
            get
            {
                return (ClaimsIdentity)User.Identity;
            }
        }

        private IQueryable<Claim> IdentityClaim()
        {
            return Identity.Claims.AsQueryable();
        }

        private string GetClaimByValue(string value)
        {
            return IdentityClaim().Where(c => c.Type.Equals(value)).Select(c => c.Value).SingleOrDefault();
        }

        /// <summary>
        /// UserID
        /// </summary>
        public string UserId
        {
            get
            {
                var userId = GetClaimByValue("UserId");
                if (userId != null)
                {
                    return userId;
                }
                return "";
            }
        }

        /// <summary>
        /// UserRole
        /// </summary>
        public string UserRole
        {
            get
            {
                var userRole = GetClaimByValue("UserRole");
                if (userRole != null)
                {
                    return userRole;
                }
                return UserTypes.Guest.ToDescriptionString();
            }
        }

        /// <summary>
        /// set Admin user Id
        /// </summary>
        public string AdminUserId
        {
            get
            {
                var userId = Identity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    return userId;
                }
                return "";
            }
        }

        /// <summary>
        /// device
        /// </summary>
        public string Device
        {
            get
            {
                var userId = GetClaimByValue("Device");
                if (userId != null)
                {
                    return userId;
                }
                return "";
            }
        }

        /// <summary>
        /// device type
        /// </summary>
        public string DeviceType
        {
            get
            {
                var devicetype = GetClaimByValue("DeviceType");
                if (devicetype != null)
                {
                    return devicetype;
                }
                return "";
            }
        }

        /// <summary>
        /// device time offset
        /// </summary>
        public string Offset
        {
            get
            {
                var offset = GetClaimByValue("Offset");
                if (offset != null)
                {
                    return offset;
                }
                return "";
            }
        }
    }
}
