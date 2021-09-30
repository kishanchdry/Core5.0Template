using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Models.API
{
    /// <summary>
    /// Forgot password request
    /// </summary>
    public class ForgotPassword
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is required")]
        public string EmailID { get; set; }
    }
}
