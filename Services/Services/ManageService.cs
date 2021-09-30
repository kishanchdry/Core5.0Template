using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Services.IServices;
using Services.IServices.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ManageService : IManageService
    {
        private readonly IUserService _userManager;

        public ManageService(IUserService userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> VerifyPhoneOTP(string OTP, string userId)
        {
            var encOTP = Helper.Encrypt(string.Format("{0}", OTP));
            var user = await _userManager.FindByIdAsync(userId);

            if (string.Equals(user?.PhoneOTP, encOTP))
            {
                user.PhoneOTP = null;
                user.PhoneNumberConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> VerifyEmailOTP(string OTP, string userId)
        {
            var encOTP = Helper.Encrypt(string.Format("{0}", OTP));
            var user = await _userManager.FindByIdAsync(userId);

            if (string.Equals(user?.EmailOTP, encOTP))
            {
                user.EmailOTP = null;
                user.EmailConfirmed = true;
                var result = await _userManager.UpdateAsync(user);
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }

        public string GenrateOTP()
        {
            Random rnd = new Random();
            var otp = rnd.Next(100000, 999999);
            return Helper.Encrypt(string.Format("{0}", otp));
        }
    }
}
