using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Shared.Common
{
    public class ConfigurationKeys
    {
        public string MailServer { get; set; }
        public int Port { get; set; }
        public string MailAuthUser { get; set; }
        public string MailAuthPass { get; set; }
        public bool EnableSSL { get; set; }
        public string EmailFromAddress { get; set; }
        public string EmailFromName { get; set; }
        public string EmailBCC { get; set; }
        public string EmailPath { get; set; }
        public string WebRootPath { get; set; }
    }
    public static class ApplicationConstants
    {
        public const string AdminId = null;
        //"Admin", "Manager", "User", "Guest"
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
        public const string Guest = "Guest";
        public const bool LockoutOnFailure = true;

        public const string ComanyName = "Template";
        public const string AdminWelcomMessage = "You can manage web settings and products here.";
        public const string AboutUs = "About US.";
        public const string ContactUs = "Contact US.";
        public const string VerifyEmailByOTP = "Template Verification OTP";


        public const string CacheRoleActionsKey = "RoleActions";
        public const string CacheUserRoleKey = "UserRoles";
    }
}