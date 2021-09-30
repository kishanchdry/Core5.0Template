using Shared.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shared.Models.Base
{
    /// <summary>
    /// Generic repsonse class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponses<T>
    {
        /// <summary>
        /// Generic data type, In this propery we will send data to in response of any request
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Response code is just the code, which gives us the status of request as ok or error or validation etc.
        /// </summary>
        public short ResponseCode { get; set; }

        /// <summary>
        /// This property will be used when response is ok and need to show the message in ui
        /// </summary>
        public string SuccessMsg { get; set; }

        /// <summary>
        /// This property will be used when response is error and need to show the message in ui
        /// </summary>
        public string FailureMsg { get; set; }

        /// <summary>
        /// All validation error are listed in this, validation errors get from model of a request automatically
        /// </summary>
        public List<string> ValidationErrors { get; set; }

        /// <summary>
        /// we can use this property to tell the request about api which we have called
        /// </summary>
        public string ApiName { get; set; }

        public ApiResponses()
        {
        }

        public ApiResponses(ResponseMsg responseCode, T data, List<string> validationErrors, string successMsg = "", string failureMsg = "", string apiName = "")
        {
            ResponseCode = (short)responseCode;
            Data = data;
            SuccessMsg = successMsg;
            FailureMsg = failureMsg;
            ValidationErrors = validationErrors;
            ApiName = apiName;
        }
    }

    #region Response Enum Extension

    public static class ResponseExtensions
    {
        public static string ToDescriptionString(this Enum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    #endregion Response Enum Extension
}
