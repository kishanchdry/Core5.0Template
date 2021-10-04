using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Models.API
{
    public class ChangePasswordModel
    {
        [Display(Name = "Old Password")]
        [Required(ErrorMessage = "Old password required")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New password required")]
        [MinLength(8, ErrorMessage = "New password should be minimum 8 chars.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm password required")]
        [MinLength(8, ErrorMessage = "Confirm password should be minimum 8 chars.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password and Confirm password must match")]
        public string ConfirmPassword { get; set; }
    }


    public class ResetPasswordModel
    {
        [EmailAddress(ErrorMessage = "Email address is invalid")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password should be minimum 8 chars.")]
        //[StringLength(20, ErrorMessage = "New password reached with max 20 characters")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[MinLength(8, ErrorMessage = "Confirm password should be minimum 8 chars.")]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm password must match")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
