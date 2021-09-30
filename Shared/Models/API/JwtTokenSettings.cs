using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.API
{
    /// <summary>
    /// JWT Tooken variables
    /// </summary>
    public class JwtTokenSettings
    {
        /// <summary>
        /// Expiry
        /// </summary>
        public int Expiry { get; set; }

        /// <summary>
        /// Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Secrete
        /// </summary>
        public string Secrete { get; set; }

        /// <summary>
        /// IsUser
        /// </summary>
        public string IsUser { get; set; }
    }

    /// <summary>
    /// app setting
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        ///  site url
        /// </summary>
        public string SiteUrl { get; set; }
    }
}
