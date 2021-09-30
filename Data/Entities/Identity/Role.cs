using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities.Identity
{
    [Table("AspNetRoles")]
    public class Role : IdentityRole
    {
        public Role() { }
        public Role(string roleName) : base(roleName) { }

        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<RoleAction> RoleAtions { get; set; }
    }
}
