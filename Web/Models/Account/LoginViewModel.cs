using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using Microsoft.AspNetCore.Server.HttpSys;

namespace Web.Models.Account
{
    /// <summary>
    /// Login view model
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// email for user
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// user password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// is user want to remenber by browser
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// return url
        /// </summary>
        public string ReturnUrl { get; set; }
        
        /// <summary>
        /// External login list
        /// </summary>
        public List<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
