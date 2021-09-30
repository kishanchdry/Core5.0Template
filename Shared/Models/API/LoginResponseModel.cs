using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models.API
{
    public class LoginResponseModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AuthorizationToken { get; set; }
        public DateTime TokenExpiredOn { get; set; }
        public int UserType { get; set; }
        public bool IsOnline { get; set; }
    }
}
