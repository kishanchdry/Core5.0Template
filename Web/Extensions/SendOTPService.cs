using Data.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Services.IServices;
using Services.Services;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Extensions
{
    /// <summary>
    /// send otp service
    /// </summary>
    public class SendOTPService
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IManageService _manageService;

        /// <summary>
        /// construct service
        /// </summary>
        /// <param name="webHostingEnvironment"></param>
        /// <param name="configuration"></param>
        /// <param name="manageService"></param>
        public SendOTPService(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration, IManageService manageService)
        {

            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
            _manageService = manageService;
        }

        /// <summary>
        /// send OTP on phone.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public async Task<string> SendPhoneOTP(string phoneNumber)
        {
            try
            {
                string templatePath = _configuration.GetValue<string>("TemplatePath:PhoneOTP");
                var serverFilePath = string.Format("{0}{1}", _webHostingEnvironment.WebRootPath, templatePath);

                var otp = _manageService.GenrateOTP();

                var template = GetTeamplateData(serverFilePath);
                template = template?.Replace("@@OTP", Shared.Common.Helper.Decrypt(otp));

                Communication.SendSMS.SMS.Send(template, phoneNumber);

                return otp;
            }
            catch (Exception ex)
            {
                ex.Log();
                return null;
            }
        }

        /// <summary>
        /// Send OTP on mail.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> SendMailOTP(User user)
        {
            try
            {
                string templatePath = _configuration.GetValue<string>("TemplatePath:VerifyEmailByOTP");
                var serverFilePath = string.Format("{0}{1}", _webHostingEnvironment.WebRootPath, templatePath);

                var otp = _manageService.GenrateOTP();

                var template = GetTeamplateData(serverFilePath);
                template = template?.Replace("@@Title", ApplicationConstants.VerifyEmailByOTP);
                template = template?.Replace("@@Name", (!string.IsNullOrWhiteSpace(user?.FirstName) || !string.IsNullOrWhiteSpace(user?.LastName)) ?
                    string.Format("{0} {1}", user?.FirstName, user?.LastName).Trim() : user?.UserName);
                template = template?.Replace("@@OTP", Shared.Common.Helper.Decrypt(otp));

                var mail = new Communication.Mail.SendMail();
                await mail.Send(user?.Email, ApplicationConstants.VerifyEmailByOTP, template);

                return otp;
            }
            catch (Exception ex)
            {
                ex.Log();
                return null;
            }
        }

        /// <summary>
        /// Get template
        /// </summary>
        /// <param name="templatePath"></param>
        /// <returns></returns>
        public string GetTeamplateData(string templatePath)
        {
            StreamReader filePtr;
            string fileData = "";
            filePtr = File.OpenText(templatePath);
            fileData = filePtr.ReadToEnd();
            filePtr.Close();
            filePtr = null;
            return fileData;
        }
    }
}
