using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace API.Authorization.JWT
{
    /// <summary>
    /// JWT token
    /// </summary>
    public class JwtToken
    {
        private JwtSecurityToken securityToken;

        internal JwtToken(JwtSecurityToken token)
        {
            this.securityToken = token;
        }

        /// <summary>
        /// valid to
        /// </summary>
        public DateTime ValidTo => securityToken.ValidTo;

        /// <summary>
        /// jwt token value
        /// </summary>
        public string Value => new JwtSecurityTokenHandler().WriteToken(this.securityToken);
    }
}
