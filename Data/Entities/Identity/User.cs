using Microsoft.AspNetCore.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities.Identity
{
    [Table("AspNetUsers")]
    public class User : IdentityUser
    {
        public User()
        {
            this.CreatedDate = DateTime.UtcNow.GetLocal();
            this.ModifiedDate = DateTime.UtcNow.GetLocal();
            this.IsDeleted = false;
            this.IsActive = true;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public string PhoneOTP { get; set; }
        public string EmailOTP { get; set; }


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
        public string ResetToken { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
    class MyClass
    {
        public MyClass()
        {
            var a = new User
            {
                AccessFailedCount = 1,
                ConcurrencyStamp = string.Empty,
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
            };
        }
    }
}