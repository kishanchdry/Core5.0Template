using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Models.API
{
    public class UserSignUpModel
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Please enter email address.")]
        [StringLength(100, ErrorMessage = "Max length should be 100 characters of Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string DeviceToken { get; set; }
        public short DeviceType { get; set; }
        public int TimezonoffsetInseconds { get; set; }
    }
}
