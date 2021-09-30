using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Communication.Mail
{
    public class SendMail
    {
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="reciver">Reciver email</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="body">Email body</param>
        /// <returns>Taks object</returns>
        public Task Send(string reciver, string subject, string body)
        {
            return Task.Run(() => SendMainAsync(reciver, subject, body));
        }

        private async void SendMainAsync(string reciver, string subject, string body)
        {
            #region constants
            var html = HttpUtility.HtmlEncode(body);
            var Credentials = new System.Net.NetworkCredential("identitytestef@gmail.com", "Aa@12345");
            //TODO update mailing credentials
            string sender = "test.app@gmail.com";
            var Host = "smtp.gmail.com";
            var EnableSsl = true;
            var Port = 587;
            bool useDefaultCred = false;

            #endregion

            int MethodUseToSendMail = 4;

            switch (MethodUseToSendMail)
            {
                case 2:
                    #region Method 2
                    var message = new MailMessage
                    {
                        From = new MailAddress(sender),
                        To = { reciver },
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                        DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                    };
                    using (SmtpClient smtpClient2 = new SmtpClient(Host))
                    {
                        smtpClient2.UseDefaultCredentials = useDefaultCred;
                        smtpClient2.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient2.Credentials = Credentials;
                        smtpClient2.Port = Port;
                        smtpClient2.EnableSsl = EnableSsl;
                        smtpClient2.Send(message);
                    }
                    #endregion
                    break;
                case 3:
                    #region Method 3
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(sender);
                        mail.To.Add(reciver);
                        mail.Subject = subject;
                        mail.Body = HttpUtility.HtmlEncode(body);
                        mail.IsBodyHtml = true;

                        //mail.Attachments.Add(new Attachment("C:\\file.zip"));
                        //mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                        //mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                        using (SmtpClient smtp = new SmtpClient(Host, Port))
                        {
                            smtp.Credentials = Credentials;
                            smtp.EnableSsl = EnableSsl;
                            smtp.Send(mail);
                        }
                    }
                    #endregion
                    break;
                case 4:
                    #region Method 4

                    dynamic MailMessage = new MailMessage();
                    MailMessage.From = new MailAddress(sender);
                    MailMessage.To.Add(reciver);
                    MailMessage.Subject = subject;
                    MailMessage.Body = body;
                    MailMessage.IsBodyHtml = true;
                    //MailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Plain));
                    MailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html));
                    

                    SmtpClient SmtpClient = new SmtpClient();
                    SmtpClient.Host = Host;
                    SmtpClient.EnableSsl = EnableSsl;
                    SmtpClient.Port = Port;
                    SmtpClient.Credentials = Credentials;

                    try
                    {
                        try
                        {
                            SmtpClient.Send(MailMessage);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        for (int i = 0; i <= ex.InnerExceptions.Length; i++)
                        {
                            SmtpStatusCode status = ex.StatusCode;
                            if ((status == SmtpStatusCode.MailboxBusy) | (status == SmtpStatusCode.MailboxUnavailable))
                            {
                                System.Threading.Thread.Sleep(5000);
                                SmtpClient.Send(MailMessage);
                            }
                        }
                    }

                    #endregion
                    break;
                default:
                    #region Method 1
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress("test.app@gmail.com");
                    msg.To.Add(new MailAddress(reciver));
                    msg.Subject = subject;
                    msg.Body = body;
                    msg.IsBodyHtml = true;
                    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Plain));
                    msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                    SmtpClient smtpClient = new SmtpClient(Host, Convert.ToInt32(Port));
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential("identitytestef@gmail.com", "Aa@12345"); ;
                    smtpClient.EnableSsl = EnableSsl;
                    smtpClient.Send(msg);

                    #endregion
                    break;
            }
        }
    }
}
