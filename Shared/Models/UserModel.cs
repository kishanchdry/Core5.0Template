using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class UserModel
    {
        /*
        AccessFailedCount = 1,
        ConcurrencyStamp = string.Empty,
        DisplayName = string.Empty,
        Email = string.Empty,
        EmailConfirmed = false,
        Id = string.Empty,
        LockoutEnabled = true,
        LockoutEnd = DateTime.Now.AddDays(5),
        NormalizedEmail = string.Empty,
        NormalizedUserName = string.Empty,
        PasswordHash = string.Empty,
        PhoneNumber = string.Empty,
        PhoneNumberConfirmed = false,
        SecurityStamp = string.Empty,
        TwoFactorEnabled = false,


        IsActive = false,
        UserName = string.Empty,
        CreatedBy = string.Empty,
        CreatedDate = DateTime.Now,
        IsDeleted = true,
        ModifiedBy = string.Empty,
        ModifiedDate = DateTime.Now
        */

        //
        // Summary:
        //     Gets or sets the date and time, in UTC, when any user lockout ends.
        //
        // Remarks:
        //     A value in the past means the user is not locked out.
        public DateTimeOffset? LockoutEnd { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if two factor authentication is enabled for this
        //     user.
        //
        // Value:
        //     True if 2fa is enabled, otherwise false.
        public bool TwoFactorEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their telephone address.
        //
        // Value:
        //     True if the telephone number has been confirmed, otherwise false.
        public bool PhoneNumberConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets a telephone number for the user.
        public string PhoneNumber { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a user is persisted to the store
        public string ConcurrencyStamp { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a users credentials change (password
        //     changed, login removed)
        public string SecurityStamp { get; set; }
        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public string PasswordHash { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their email address.
        //
        // Value:
        //     True if the email address has been confirmed, otherwise false.
        public bool EmailConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized email address for this user.
        public string NormalizedEmail { get; set; }
        //
        // Summary:
        //     Gets or sets the email address for this user.
        public string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized user name for this user.
        public string NormalizedUserName { get; set; }
        //
        // Summary:
        //     Gets or sets the user name for this user.
        public string UserName { get; set; }
        //
        // Summary:
        //     Gets or sets the primary key for this user.
        public string Id { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if the user could be locked out.
        //
        // Value:
        //     True if the user could be locked out, otherwise false.
        public bool LockoutEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets the number of failed login attempts for the current user.
        public int AccessFailedCount { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool RemeberMe { get; set; }
        public bool LockdownOnFalier { get; set; }


        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }


        private DateTime? createDate;
        public DateTime CreatedDate
        {
            get
            {
                if (createDate == null || createDate == DateTime.MinValue)
                {
                    createDate = DateTime.UtcNow.GetLocal();
                }
                return createDate.Value;
            }
            set { createDate = value; }
        }
        private DateTime? modificationDate;
        public DateTime ModifiedDate
        {
            get
            {
                if (modificationDate == null || modificationDate == DateTime.MinValue)
                {
                    modificationDate = DateTime.UtcNow.GetLocal();
                }
                return modificationDate.Value;
            }
            set { modificationDate = value; }
        }
        private bool? isDeleted;
        public bool IsDeleted
        {
            get
            {
                if (isDeleted == null)
                {
                    isDeleted = false;
                }
                return isDeleted.Value;
            }
            set
            {
                isDeleted = value;
            }
        }
    }
}
