using Communication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Shared.Common;
using Microsoft.Extensions.Options;

namespace Communication.Utilities
{
    /// <summary>
    /// Email helper class
    /// </summary>
    public class EmailHelperCore
    {
        /// <summary>
        /// timeout time
        /// </summary>
        private const int Timeout = 180000;
        private readonly ConfigurationKeys _configurationKey;

        /// <summary>
        /// smtp host
        /// </summary>
        private readonly string _host;

        /// <summary>
        /// smtp port
        /// </summary>
        private readonly int _port;

        /// <summary>
        /// smtp user details
        /// </summary>
        private readonly string _user;

        /// <summary>
        /// smtp password
        /// </summary>
        private readonly string _pass;

        /// <summary>
        /// smtp ssl available or not
        /// </summary>
        private readonly bool _ssl;

        /// <summary>
        /// Sender name
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// Sender name
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Recipient addresses saprated by ", or ;"
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// Recipient BCC addresses saprated by ", or ;"
        /// </summary>
        public string RecipientBCC { get; set; }

        /// <summary>
        /// Recipient CC addresses saprated by ", or ;"
        /// </summary>
        public string RecipientCC { get; set; }

        /// <summary>
        /// Message subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Message body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// attachment file if any file
        /// </summary>
        //public List<string> AttachmentFile { get; set; }
        public string[] AttachmentFile { get; set; }

        /// <summary>
        /// mail sender name
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        ///  Constructor of email helper take value from config
        ///  Keys = MailServer, Port, MailAuthUser, MailAuthPass, EnableSSL, EmailFromAddress
        /// </summary>
        public EmailHelperCore(IOptions<ConfigurationKeys> configurationKey)//, IOptions<ConfigurationKeys> jwtOptions
        {
            _configurationKey = configurationKey.Value;
            //MailServer - Represents the SMTP Server
            _host = _configurationKey.MailServer;
            //Port- Represents the port number
            _port = _configurationKey.Port;
            //MailAuthUser and MailAuthPass - Used for Authentication for sending email
            _user = _configurationKey.MailAuthUser;
            _pass = _configurationKey.MailAuthPass;
            _ssl = _configurationKey.EnableSSL;
            Sender = _configurationKey.EmailFromAddress;
            SenderName = _configurationKey.EmailFromName;
            RecipientBCC = _configurationKey.EmailBCC;
        }

        /// <summary>
        /// send mail after all object send
        /// </summary>
        public bool Send()
        {
            try
            {
                var message = new MailMessage()
                {
                    IsBodyHtml = true,
                    Subject = Subject,
                    Body = Body,
                    From = new MailAddress(Sender, SenderName ?? Sender)
                };

                string[] arrRecipent = Recipient.Split(',');
                foreach (var recipent in arrRecipent)
                {
                    string[] arrRecipentFromSimiColon = recipent.Split(';');
                    foreach (var recipentSC in arrRecipentFromSimiColon)
                    {
                        message.To.Add(new MailAddress(recipentSC));
                    }
                }

                //var message = new MailMessage(new MailAddress(Sender, SenderName ?? Sender), new MailAddress(Recipient)) { IsBodyHtml = true, Subject = Subject, Body = Body };
                if (ReplyTo != null)
                {
                    message.ReplyToList.Add(ReplyTo);
                }
                if (!string.IsNullOrEmpty(ReplyTo))
                {
                    //message.Headers.Add("messageid", ((ReplyTo.Split("@")[0]).ToString().Split("-")[2]).ToString());
                }

                if (!string.IsNullOrEmpty(RecipientBCC))
                {
                    string[] arrRecipientBCC = RecipientBCC.Split(';');
                    foreach (var itemRecipientBCC in arrRecipientBCC)
                    {
                        message.Bcc.Add(new MailAddress(itemRecipientBCC));
                    }
                }

                if (!string.IsNullOrEmpty(RecipientCC))
                {
                    string[] arrRecipientCC = RecipientCC.Split(';');
                    foreach (var itemRecipientCC in arrRecipientCC)
                    {
                        message.CC.Add(new MailAddress(itemRecipientCC));
                    }
                }

                var smtp = new SmtpClient();

                if (AttachmentFile != null)
                {
                    foreach (var item in AttachmentFile)
                    {
                        message.Attachments.Add(new System.Net.Mail.Attachment(item));
                    }
                }

                if (_user.Length > 0 && _pass.Length > 0)
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Host = _host;
                    smtp.Port = _port;
                    smtp.Credentials = new NetworkCredential(_user, _pass);
                    smtp.EnableSsl = _ssl;
                }

                smtp.Send(message);

                //if (att != null)
                //    att.Dispose();
                //message.Dispose();
                //smtp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                return ex != null && false;
            }
        }

        /// <summary>
        /// generate email body from template, get template folder from config "MailTemplateFolder"
        /// </summary>
        /// <param name="templateName">template name</param>
        /// <param name="args">MessageKeyValue arguments to set value in template</param>
        /// <returns>Html email body from template</returns>
        public string GenerateEmailTemplateFor(string templateName, params MessageKeyValue[] args)
        {
            var mailFolderTemplatePath = _configurationKey.EmailPath + "/";
            var filePath = Path.Combine(mailFolderTemplatePath, templateName);
            return GenerateEmailTemplateWithfull(filePath, args);
        }

        /// <summary>
        /// generate email body from template
        /// </summary>
        /// <param name="filePath">Template full path</param>
        /// <param name="args">MessageKeyValue arguments to set value in template</param>
        /// <returns>Html email body from template</returns>
        public string GenerateEmailTemplateWithfull(string filePath, params MessageKeyValue[] args)
        {
            if (File.Exists(filePath))
            {
                var htmlString = File.ReadAllText(filePath);

                return args == null ? htmlString : args.Aggregate(htmlString, (current, item) => current.Replace(item.HtmlKey, item.HtmlValue));
            }
            else
            {
                throw new System.ArgumentException("Template file Not found");
            }
        }

        /// <summary>
        /// Determines whether an email address is valid.
        /// </summary>
        /// <param name="emailAddress">The email address to validate.</param>
        /// <returns>
        /// 	<c>true</c> if the email address is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidEmailAddress(string emailAddress)
        {
            // An empty or null string is not valid
            if (String.IsNullOrEmpty(emailAddress))
            {
                return (false);
            }

            // Regular expression to match valid email address
            string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            // Match the email address using a regular expression
            Regex re = new Regex(emailRegex);
            if (re.IsMatch(emailAddress))
                return (true);
            else
                return (false);
        }
    }
}
