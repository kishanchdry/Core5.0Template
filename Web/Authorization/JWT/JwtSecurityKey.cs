using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Authorization.JWT
{
    /// <summary>
    /// jwt token security key
    /// </summary>
    public class JwtSecurityKey
    {
        /// <summary>
        /// Create symmetric key
        /// </summary>
        /// <param name="secrete"></param>
        /// <returns></returns>
        public static SymmetricSecurityKey Create(string secrete)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secrete));
        }
    }
}
