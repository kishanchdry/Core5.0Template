using Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common
{
    public static class ExtentionMethods
    {
        public static DateTime GetLocal(this DateTime datetime)
        {
            if (datetime == DateTime.MinValue)
            {
                datetime = DateTime.UtcNow;
            }
            return TimeZoneInfo.ConvertTimeFromUtc(datetime, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }
    }
}
