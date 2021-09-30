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
}
