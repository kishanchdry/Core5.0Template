using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Extensions;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services.IServices;
using Services.IServices.Identity;

namespace Web.Controllers.Base
{
    /// <summary>
    /// manager user
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IUserService _userManager;
        private readonly ISignInService _signInManager;
        private readonly SendOTPService _sendOTPService;
        private readonly IManageService _manageService;

        /// <summary>
        /// construct services
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="sendOTPService"></param>
        /// <param name="manageService"></param>
        public ManageController(IUserService userManager, ISignInService signInManager, SendOTPService sendOTPService, IManageService manageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sendOTPService = sendOTPService;
            _manageService = manageService;
        }

        /// <summary>
        /// get user details
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// view user
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> UserView()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }

        /// <summary>
        /// verify phone no.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> VerifyPhoneNumber(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var otp = await _sendOTPService.SendPhoneOTP(user?.PhoneNumber);

            user.PhoneOTP = otp;

            await _userManager.UpdateAsync(user);

            return View(user);
        }

        /// <summary>
        /// verify email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> VerifyEmail(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var otp = await _sendOTPService.SendMailOTP(user);

            user.EmailOTP = otp;

            await _userManager.UpdateAsync(user);

            return View(user);
        }

        /// <summary>
        /// get otp on phone
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> SendPhoneOTP(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var otp = await _sendOTPService.SendPhoneOTP(user?.PhoneNumber);

            user.PhoneOTP = otp;
            await _userManager.UpdateAsync(user);

            return Json(new { result = otp });
        }

        /// <summary>
        /// send otp on email
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> SendMailOTP(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var otp = await _sendOTPService.SendMailOTP(user);

            user.PhoneOTP = otp;

            await _userManager.UpdateAsync(user);

            return Json(new { result = otp });
        }

        /// <summary>
        /// verify opt
        /// </summary>
        /// <param name="id"></param>
        /// <param name="OTP"></param>
        /// <param name="isPhoneOTP"></param>
        /// <returns></returns>
        public async Task<IActionResult> VerifyOTP(string id, string OTP, bool? isPhoneOTP)
        {
            bool flag = false;
            if (isPhoneOTP == true)
            {
                flag = await _manageService.VerifyPhoneOTP(OTP, id);
            }
            else
            {
                flag = await _manageService.VerifyEmailOTP(OTP, id);
            }
            return Json(new { flag });
        }

        /// <summary>
        /// changes user status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> ChangeStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            user.IsActive = !user.IsActive;
            var result = await _userManager.UpdateAsync(user);
            return Json(new { flag = result.Succeeded });
        }
    }
}
