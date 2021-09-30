using Communication.Models;
using Communication.Utilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Communication.Mail
{
    public class EmailFunctions
    {
        private readonly ConfigurationKeys _configurationKey;
        private readonly IOptions<ConfigurationKeys> _optionsConfigurationKey;
        private readonly string _webTemplatePath;
        private readonly string _mailLogoPath;
        private readonly EmailHelperCore _emailHelper;

        public EmailFunctions(IOptions<ConfigurationKeys> configurationKey)
        {
            _configurationKey = configurationKey.Value;
            _optionsConfigurationKey = configurationKey;
            _webTemplatePath = _configurationKey.EmailPath;
            _mailLogoPath = string.Format("{0}/favicon.ico", _configurationKey.WebRootPath);
            _emailHelper = new EmailHelperCore(configurationKey);
        }

        /// <summary>
        /// By this method we can send email through deligate
        /// </summary>
        /// <param name="emailHelper"></param>
        public void SendEmailThroughDelegate(EmailHelperCore emailHelper)
        {
            try
            {
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    emailHelper.Send();
                });
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Sends the generate password email.
        /// </summary>
        /// <param name="toEmail">To email.</param>
        /// <param name="emailsubject">The emailsubject.</param>
        /// <param name="Name">The name.</param>
        /// <param name="ResetUrl">The reset URL.</param>
        /// <returns></returns>
        public string SendGeneratePasswordEmail(string toEmail, string emailsubject, string Name, string ResetUrl)
        {
            try
            {
                var serverFilePath = string.Format("{0}/{1}", _webTemplatePath, "GeneratePassword.html");

                var emailHelp = new EmailHelperCore(_optionsConfigurationKey)
                {

                    Body = _emailHelper.GenerateEmailTemplateWithfull(serverFilePath,
                        new MessageKeyValue("##Name##", Name),
                         new MessageKeyValue("##Email##", toEmail),
                        new MessageKeyValue("##ResetUrl##", ResetUrl),
                        new MessageKeyValue("##ImagePath##", _mailLogoPath)
                       ),

                    RecipientBCC = _configurationKey.EmailBCC,
                    Recipient = toEmail.ToLower(), //== ConfigurationKey.AdminMailId.ToLower() ? ConfigurationKey.AdminForgetEmail : toEmail,
                    Subject = emailsubject
                };
                SendEmailThroughDelegate(emailHelp);
            }
            catch (Exception ex)
            {
                string json = JsonConvert.SerializeObject(ex);
                return json + " --------------  " + Path.GetFullPath(Path.Combine(_configurationKey.EmailPath + "/GeneratePassword.html"));
            }
            return "";
        }

        /// <summary>
        /// Sends the reset password email.
        /// </summary>
        /// <param name="toEmail">To email.</param>
        /// <param name="emailsubject">The emailsubject.</param>
        /// <param name="Name">The name.</param>
        /// <param name="ResetUrl">The reset URL.</param>
        public void SendResetPasswordEmail(string toEmail, string emailsubject, string Name, string ResetUrl)
        {
            var serverFilePath = string.Format("{0}/{1}", _webTemplatePath, "ResetPassword.html");
            var emailHelp = new EmailHelperCore(_optionsConfigurationKey)
            {
                Body = _emailHelper.GenerateEmailTemplateWithfull(serverFilePath,
                    new MessageKeyValue("##Name##", Name),
                    new MessageKeyValue("##ResetUrl##", ResetUrl),
                    new MessageKeyValue("##ImagePath##", _mailLogoPath)
                   ),
                RecipientBCC = _configurationKey.EmailBCC,
                Recipient = toEmail.ToLower(),
                Subject = emailsubject
            };
            SendEmailThroughDelegate(emailHelp);
        }

        /// <summary>
        /// Testings email.
        /// </summary>
        /// <param name="toEmail">To email.</param>
        /// <param name="emailsubject">The emailsubject.</param>
        /// <param name="Name">The name.</param>
        public void TestingEmail(string toEmail, string emailsubject, string Name)
        {
            var emailHelp = new EmailHelperCore(_optionsConfigurationKey)
            {
                Body = "This is a testing mail",
                RecipientBCC = _configurationKey.EmailBCC,
                Recipient = toEmail.ToLower(),
                Subject = emailsubject
            };
            SendEmailThroughDelegate(emailHelp);
        }

    }
}