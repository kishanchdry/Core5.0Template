using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities.Identity
{
    [Table("RoleActions")]
    public class RoleAction : BaseEntity
    {
        [ForeignKey("Role")]
        public string RoleId { get; set; }
        public Role Role { get; set; }

        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
