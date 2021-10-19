using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Web.Areas.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Communication.Mail;
using Services.IServices.Identity;
using Shared.Models.API;
using Shared.Models.Base;
using Shared.Common.Enums;
using API.Authorization.JWT;
using Data.Entities.Identity;
using Services.Services;
using Shared.Common;

namespace API.Controllers
{
    /// <summary>
    /// User account controller
    /// This controller will contains all method related to user login,signup,change password.
    /// </summary>

    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer", Policy = "AuthorisedUser")]
    [Route("api/[controller]")]
    public class UserAccountController : BaseAPIController
    {
        #region [ Variables & Ctrs]

        private List<string> _errors = new List<string>();
        private readonly JwtTokenSettings jwtTokenSettings;
        private readonly ISignInService signInService;
        private readonly IUserService userservice;
        private readonly EmailFunctions _emailFunctions;
        private readonly ConfigurationKeys _configurationKeys;

        /// <summary>
        /// Initialize connstructor
        /// </summary>
        /// <param name="jwtOptions"></param>
        /// <param name="signInService"></param>
        /// <param name="userservice"></param>
        /// <param name="emailFunctions"></param>

        public UserAccountController(IOptions<JwtTokenSettings> jwtOptions, IOptions<ConfigurationKeys> configurationKeys, ISignInService signInService, IUserService userservice, EmailFunctions emailFunctions)
        {
            this.signInService = signInService;
            this.jwtTokenSettings = jwtOptions.Value;
            this.userservice = userservice;
            _emailFunctions = emailFunctions;
            _configurationKeys = configurationKeys.Value;
        }

        #endregion [ Variables & Ctrs]

        #region [LOGIN AND LOGOUT SECTION]

        /// <summary>
        /// Login user from app
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("Login")]
        [HttpPost, AllowAnonymous]
        public async Task<ApiResponses<LoginResponseModel>> Login([FromBody] UserLoginViewModel request)
        {
            if (ModelState.IsValid)
            {
                var loginModel = await userservice.FindByEmailAsync(request.Email);

                if (loginModel != null)
                {
                    if (!loginModel.EmailConfirmed)
                    {
                        return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.EmailNotVarified, apiName: "Login");
                    }
                    if (!(await userservice.CheckPasswordAsync(loginModel, request.Password)))
                    {
                        return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.InvalidPassword, apiName: "Login");
                    }

                    if (!(loginModel.IsActive))
                    {
                        return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.UserAccountInActiveContactAdmin, apiName: "Login");
                    }

                    var userSignInResult = await signInService.PasswordSignInAsync(loginModel, request.Password, true, true);

                    if (userSignInResult.Succeeded)
                    {
                        if (loginModel.IsActive)
                        {
                            JwtTokenBuilder tokenBuilder = new JwtTokenBuilder();

                            var roles = await userservice.GetRolesAsync(loginModel);

                            var token = tokenBuilder.GetToken(jwtTokenSettings, loginModel.Id, roles.FirstOrDefault() ?? UserTypes.Guest.ToDescriptionString());

                            userservice.ManageLoginDeviceInfo(loginModel.Id, request.DeviceType, request.DeviceToken, token.Value, request.TimezonoffsetInseconds);

                            LoginResponseModel responseModel = new LoginResponseModel();

                            responseModel = new LoginResponseModel()
                            {
                                UserId = loginModel.Id,
                                AuthorizationToken = token.Value,
                                TokenExpiredOn = token.ValidTo,
                                Email = request.Email,
                                Name = loginModel.UserName,
                            };

                            return new ApiResponses<LoginResponseModel>(ResponseMsg.Ok, responseModel, _errors, successMsg: ResponseStatus.success, apiName: "Login");
                        }
                        else
                        {
                            return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.AccountDeactivated, apiName: "Login");
                        }
                    }
                    else
                    {
                        return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.InvalidCredentials, apiName: "Login");
                    }
                }
                else
                {
                    return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.InvalidEmailCredentials, apiName: "Login");
                }
            }
            else
            {
                return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ValidationErrors(), apiName: "Login");
            }
        }

        /// <summary>
        /// Signup a new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("SignUp")]
        [HttpPost, AllowAnonymous]
        public async Task<ApiResponses<LoginResponseModel>> SignUp([FromBody] UserSignUpModel request)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = new User();

                    user.Email = request.Email;
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.UserName = request.UserName;

                    var result = await userservice.CreateAsync(user, request.Password);

                    await userservice.AddToRoleAsync(user, UserTypes.Guest.ToDescriptionString());

                    JwtTokenBuilder tokenBuilder = new JwtTokenBuilder();

                    var token = tokenBuilder.GetToken(jwtTokenSettings, user.Id, UserTypes.Guest.ToDescriptionString());

                    userservice.ManageLoginDeviceInfo(user.Id, request.DeviceType, request.DeviceToken, token.Value, request.TimezonoffsetInseconds);

                    LoginResponseModel responseModel = new LoginResponseModel();

                    responseModel = new LoginResponseModel()
                    {
                        UserId = user.Id,
                        AuthorizationToken = token.Value,
                        TokenExpiredOn = token.ValidTo,
                        Email = request.Email,
                        Name = user.UserName,
                    };

                    return new ApiResponses<LoginResponseModel>(ResponseMsg.Ok, responseModel, _errors, successMsg: ResponseStatus.success, apiName: "SignUp");
                }
                catch (Exception)
                {
                    return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.FailedToCreatePofile, apiName: "SignUp");
                }
            }
            else
            {
                return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ValidationErrors(), apiName: "SignUp");
            }
        }


        /// <summary>
        /// log out the user
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [HttpPost]
        public async Task<ApiResponses<bool>> Logout()
        {
            if (!string.IsNullOrWhiteSpace(UserId))
            {
                await signInService.SignOutAsync();
                return userservice.Logout(UserId);
            }
            else
                return new ApiResponses<bool>(ResponseMsg.Error, false, _errors, failureMsg: ResponseStatus.InvalidToken, apiName: "Logout");
        }

        /// <summary>
        /// refresh token
        /// </summary>
        /// <returns></returns>
        [Route("RefreshToken")]
        [HttpGet]
        public async Task<ApiResponses<string>> RefreshToken()
        {
            try
            {
                var user = await userservice.FindByIdAsync(UserId);
                if (user == null)
                {
                    return new ApiResponses<string>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.userNotFound, apiName: "RefreshToken");
                }
                else
                {
                    JwtTokenBuilder tokenBuilder = new JwtTokenBuilder();

                    var roles = await userservice.GetRolesAsync(user);
                    var token = tokenBuilder.GetToken(jwtTokenSettings, user.Id, roles.FirstOrDefault() ?? UserTypes.Guest.ToDescriptionString());
                    userservice.UpdateUserToken(UserId, token.Value);
                    return new ApiResponses<string>(ResponseMsg.Ok, token.Value, _errors, successMsg: ResponseStatus.TokenRefershed, apiName: "RefreshToken");
                }
            }
            catch (Exception ex)
            {
                ex.Log();
                return new ApiResponses<string>(ResponseMsg.Error, null, _errors, failureMsg: ValidationErrors(), apiName: "RefreshToken");
            }
        }

        #endregion [LOGIN AND LOGOUT SECTION]

        #region Change Password

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<ApiResponses<bool>> ChangePassword(ChangePasswordModel model)
        {
            if (!string.IsNullOrWhiteSpace(UserId))
            {
                var user = await userservice.FindByIdAsync(UserId);

                bool isPassCorrect = (await userservice.CheckPasswordAsync(user, model.OldPassword));
                if (isPassCorrect == true)
                {
                    if (model.OldPassword == model.NewPassword)
                    {
                        return new ApiResponses<bool>(ResponseMsg.Error, false, _errors, failureMsg: ResponseStatus.OldPasswordNewPasswordSame, apiName: "ChangePassword");
                    }
                    else
                    {
                        var result = userservice.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                        if (result.Result.Succeeded == false)
                        {
                            return new ApiResponses<bool>(ResponseMsg.Error, false, _errors, failureMsg: result.Result.Errors.FirstOrDefault().Description == "Incorrect password." ? "Incorrect old password." : result.Result.Errors.FirstOrDefault()?.Description, apiName: "ChangePassword");
                        }
                        else
                        {
                            return new ApiResponses<bool>(ResponseMsg.Ok, true, _errors, successMsg: ResponseStatus.PasswordChangeSuccess, apiName: "ChangePassword");
                        }
                    }
                }
                else
                {
                    return new ApiResponses<bool>(ResponseMsg.Error, false, _errors, failureMsg: ResponseStatus.WrongOldPassword, apiName: "ChangePassword");
                }
            }
            else
                return new ApiResponses<bool>(ResponseMsg.Error, false, _errors, failureMsg: ResponseStatus.InvalidToken, apiName: "ChangePassword");
        }

        #endregion

        #region User Status

        /// <summary>
        /// update user status
        /// </summary>
        /// <returns></returns>
        [Route("UserStatus")]
        [HttpPost]
        public async Task<ApiResponses<LoginResponseModel>> UserStatus()
        {
            LoginResponseModel responseModel = new LoginResponseModel();
            if (!string.IsNullOrWhiteSpace(UserId))
            {
                var user = await userservice.FindByIdAsync(UserId);
                if (user != null)
                {
                    if (user.IsActive == true)
                    {
                        responseModel = new LoginResponseModel()
                        {
                            UserId = user.Id,
                            Email = user.Email,
                            Name = user.UserName,
                        };
                        return new ApiResponses<LoginResponseModel>(ResponseMsg.Ok, responseModel, _errors, successMsg: ResponseStatus.success, apiName: "UserStatus");
                    }
                    else
                    {
                        return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.userNotFound, apiName: "UserStatus");
                    }
                }
                else
                {
                    return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.userNotFound, apiName: "UserStatus");
                }
            }
            else
            {
                return new ApiResponses<LoginResponseModel>(ResponseMsg.Error, null, _errors, failureMsg: ResponseStatus.InvalidToken, apiName: "UserStatus");
            }
        }

        #endregion

        #region [FORGOT PASSWORD ]

        /// <summary>
        /// Forget user password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("ForgotPassword")]
        [HttpPost, AllowAnonymous]
        public async Task<ApiResponses<bool>> ForgotPassword([FromBody] ForgotPassword request)
        {
            if (ModelState.IsValid)
            {
                var user = await userservice.FindByEmailAsync(request.EmailID);

                if (user != null && await userservice.IsEmailConfirmedAsync(user))
                {
                    var token = await userservice.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = $"{_configurationKeys.WebRootPath}Account/ResetPassword?email={request.EmailID}&token={System.Web.HttpUtility.UrlEncode(token)}";
                    // Url.Action("ResetPassword", "Account", new { email = request.EmailID, token = token }, Request.Scheme);
                    _emailFunctions.SendResetPasswordEmail(request.EmailID, "Forgot Password", (string.IsNullOrWhiteSpace(user?.UserName) ? user.NormalizedUserName : user?.UserName), passwordResetLink);

                    await userservice.SaveToken(user.Id, token, false);

                    return new ApiResponses<bool>(ResponseMsg.Ok, true, _errors, successMsg: ResponseStatus.ForgotPasswordSuccess, apiName: "ForgotPassword");
                }
                else
                {
                    return new ApiResponses<bool>(ResponseMsg.Error, false, _errors, failureMsg: ResponseStatus.ForgotPasswordFailed, apiName: "ForgotPassword");
                }
            }
            else
            {
                return new ApiResponses<bool>(ResponseMsg.Error, false, _errors, failureMsg: ValidationErrors(), apiName: "ForgotPassword");
            }
        }

        #endregion [FORGOT PASSWORD ]

        #region Validation error

        private string ValidationErrors()
        {
            return string.Join(", ", ModelState.Values.Where(E => E.Errors.Count > 0).SelectMany(E => E.Errors).Select(E => E.ErrorMessage)).ToString().Trim(',').Trim();
        }

        #endregion
    }
}
