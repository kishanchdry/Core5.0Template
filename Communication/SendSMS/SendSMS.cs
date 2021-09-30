using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace Communication.SendSMS
{
    public static class SMS
    {
        //private static readonly string ACCESS_KEY = "723b1a61-4cbd-4f34-8e42-29fc59ef0a26"; // change with your own key
        private static readonly string ACCESS_KEY = "a11fa3c7-c5d4-41a4-9a1f-a2c117c7d96c"; // change with your own key

        private static string Send(string sms, string number, int count)
        {
            try
            {
                // encode to send the SMS content in a HTTP requets
                String content = HttpUtility.UrlEncode(sms);

                string urlCOntent = string.Format("http://sms.hspsms.com/sendSMS?username=USOM&message={0}&sendername=USOMIT&smstype=TRANS&numbers={1}&apikey={2}", content, number, ACCESS_KEY);

                XmlDocument xmldoc = new XmlDocument();
                // HTTP GET request to Orange API server
                xmldoc.Load(urlCOntent);
                // display status of the post
                return string.Format("Status: " + xmldoc.SelectSingleNode("/response/status/status_msg").InnerText);
            }
            catch (Exception ex)
            {
                throw new Exception("Please check your internet conncetion for more info please see inner exception", ex);
            }
        }

        /// <summary>
        /// Send text message on mobile
        /// </summary>
        /// <param name="sms">text sms</param>
        /// <param name="number">mobile number with `,` saperated</param>
        /// <returns>Array of results with this format [{"responseCode":"XXXXXXX"},{"msgid":"XXXXX"}] responseCode:string|msgid:number </returns>
        public static string Send(string sms, string number)
        {

            //http://sms.usom.co.in/sendSMS?username=user&message=XXXXXXXXXX&sendername=XYZ&smstype=TRANS&numbers=<mobile_numbers>&apikey=key
            //http://sms.hspsms.com/sendSMS?username=vidyaguess&message={message}&sendername=VIDGUE&smstype=TRANS&numbers={phoneNumber}&apikey=a11fa3c7-c5d4-41a4-9a1f-a2c117c7d96c"
            //Your authentication key
            string authKey = ACCESS_KEY;
            //Multiple mobiles numbers separated by comma
            string mobileNumber = number;
            //Sender ID,While using route4 sender id should be 6 characters long.
            string senderId = "VIDGUE";
            //user name
            string userName = "vidyaguess";
            //sms type
            string smsType = "TRANS";
            //Your message to send, Add URL encoding here.
            string message = HttpUtility.UrlEncode(sms);

            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("username={0}", userName);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sendername={0}", senderId);
            sbPostData.AppendFormat("&smstype={0}", smsType);
            sbPostData.AppendFormat("&numbers={0}", mobileNumber);
            sbPostData.AppendFormat("&apikey={0}", authKey);

            try
            {
                //Call Send SMS API
                string sendSMSUri = "http://sms.usom.co.in/sendSMS";//"http://sms.hspsms.com/sendSMS";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();

                return responseString;
            }
            catch (SystemException ex)
            {
                throw new Exception("Please check your internet conncetion for more info please see inner exception", ex);
            }
        }
    }
}
