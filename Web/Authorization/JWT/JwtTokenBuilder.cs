using Web.Authorization.JWT;
using Microsoft.IdentityModel.Tokens;
using Shared.Models.API;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PlantM.Web.Common
{
    /// <summary>
    /// JWT token builder
    /// </summary>
    public class JwtTokenBuilder
    {
        private SecurityKey securityKey = null;
        private string subject = "";
        private string isUser = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expiryInMinutes = 2880;

        /// <summary>
        /// add jwt sqcurity key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public JwtTokenBuilder AddSecurityKey(SecurityKey key)
        {
            this.securityKey = key;
            return this;
        }

        /// <summary>
        /// add subject
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public JwtTokenBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        /// <summary>
        /// add user 
        /// </summary>
        /// <param name="isUser"></param>
        /// <returns></returns>
        public JwtTokenBuilder AddIsUser(string isUser)
        {
            this.isUser = isUser;
            return this;
        }

        /// <summary>
        /// add audience
        /// </summary>
        /// <param name="audience"></param>
        /// <returns></returns>
        public JwtTokenBuilder AddAudience(string audience)
        {
            this.audience = audience;
            return this;
        }

        /// <summary>
        /// add claims
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public JwtTokenBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        /// <summary>
        /// add claims
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public JwtTokenBuilder AddClaim(Dictionary<string, string> claims)
        {
            this.claims.Union(claims);
            return this;
        }

        /// <summary>
        /// add expiry
        /// </summary>
        /// <param name="expiryInMinutes"></param>
        /// <returns></returns>
        public JwtTokenBuilder AddExpiry(int expiryInMinutes)
        {
            this.expiryInMinutes = expiryInMinutes;
            return this;
        }

        /// <summary>
        /// build token
        /// </summary>
        /// <returns></returns>
        public JwtToken Build()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,this.subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            }
            .Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken
                (
                issuer: this.isUser,
                audience: this.audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(this.expiryInMinutes),
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtToken(token);
        }

        private void EnsureArguments()
        {
            if (this.securityKey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(this.isUser))
                throw new ArgumentNullException("isUser");

            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("audience");
        }

        /// <summary>
        /// Get jwt token after login success
        /// </summary>
        /// <param name="jwtTokenSettings"></param>
        /// <param name="UserId"></param>
        /// <param name="UserRole"></param>
        /// <returns>JWT token</returns>
        public JwtToken GetToken(JwtTokenSettings jwtTokenSettings, string UserId, string UserRole)
        {
            JwtToken token = new JwtTokenBuilder()
                .AddSubject(jwtTokenSettings.Subject)
                .AddSecurityKey(JwtSecurityKey.Create(jwtTokenSettings.Secrete))
                .AddIsUser(jwtTokenSettings.IsUser)
                .AddAudience(jwtTokenSettings.Audience)
                .AddClaim("UserId", UserId)
                .AddClaim("UserRole", UserRole)
                .AddExpiry(jwtTokenSettings.Expiry)
                .Build();

            return token;
        }
    }
}