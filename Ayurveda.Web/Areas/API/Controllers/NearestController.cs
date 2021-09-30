using Ayurveda.Web.Areas.API.Controllers.Base;
using Communication.Mail;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.IServices.Identity;
using Shared.Common.Enums;
using Shared.Models.API;
using Shared.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ayurveda.Web.Areas.API.Controllers
{
    /// <summary>
    /// Get nearest store user and other available services
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AuthorisedUser")]
    public class NearestController : BaseAPIController
    {
        private List<string> _errors = new List<string>();
        private readonly JwtTokenSettings _jwtTokenSettings;
        private readonly IUserService _userservice;
        private readonly EmailFunctions _emailFunctions;

        /// <summary>
        /// Initialized services
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="userservice"></param>
        /// <param name="emailFunctions"></param>
        public NearestController(IOptions<JwtTokenSettings> jwtOptions, IUserService userservice, EmailFunctions emailFunctions)
        {
            _jwtTokenSettings = jwtOptions.Value;
            _userservice = userservice;
            _emailFunctions = emailFunctions;
        }

        /// <summary>
        /// Get all admin users
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllAdminUsers"), Authorize(AuthenticationSchemes = "Bearer", Policy = "AdminAccess")]
        public async Task<ApiResponses<IList<User>>> GetAllAdminUsers()
        {
            var roles = await _userservice.GetUsersInRoleAsync(UserRole);

            return new ApiResponses<IList<User>>(ResponseMsg.Ok, roles, _errors);
        }

        /// <summary>
        /// Get all manager users
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllManagerUsers"), Authorize(AuthenticationSchemes = "Bearer", Policy = "ManagerAccess")]
        public async Task<ApiResponses<IList<User>>> GetAllManagerUsers()
        {
            var roles = await _userservice.GetUsersInRoleAsync(UserRole);

            return new ApiResponses<IList<User>>(ResponseMsg.Ok, roles, _errors);
        }


        /// <summary>
        /// Get all normal users
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllNormalUsers"), Authorize(AuthenticationSchemes = "Bearer", Policy = "UserAccess")]
        public async Task<ApiResponses<IList<User>>> GetAllNormalUsers()
        {
            var roles = await _userservice.GetUsersInRoleAsync(UserRole);

            return new ApiResponses<IList<User>>(ResponseMsg.Ok, roles, _errors);
        }


        /// <summary>
        /// Get all guest users
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetAllGuestUsers"), Authorize(AuthenticationSchemes = "Bearer", Policy = "GuestAccess")]
        public async Task<ApiResponses<IList<User>>> GetAllGuestUsers()
        {
            var roles = await _userservice.GetUsersInRoleAsync(UserRole);

            return new ApiResponses<IList<User>>(ResponseMsg.Ok, roles, _errors);
        }

        /// <summary>
        /// Get all guest users
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("TestEmail"), Authorize(AuthenticationSchemes = "Bearer", Policy = "GuestAccess")]
        public async Task<ApiResponses<bool>> TestEmail()
        {
            _emailFunctions.SendResetPasswordEmail("k96choudhary@gmail.com", "Reset Password", "Kishan Choudhar", Url.Action("Index", "Home", new { area = "" }));
            return new ApiResponses<bool>(ResponseMsg.Ok, true, _errors);
        }

    }
}
