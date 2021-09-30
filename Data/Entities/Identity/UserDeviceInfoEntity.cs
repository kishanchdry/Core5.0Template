using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Identity
{
    [Table("UserDeviceInfo")]
    public class UserDeviceInfoEntity
    {
        public long Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public string AuthorizationToken { get; set; }
        public short? DeviceType { get; set; }
        public string DeviceToken { get; set; }
        public int? TimezoneOffsetInSeconds { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public User User { get; set; }
    }
}