using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common.Enums
{
    /// <summary>
    /// Response code
    /// </summary>
    public static class ResponseCode
    {
        public static short Ok = 200;
        public static short Error = 201;
        public static short Logout = 203;
        public static short UserExist = 501;
        public static short BadRequest = 400;
        public static short Unauthorized = 401;
        public static short NotFound = 404;
    }
}
