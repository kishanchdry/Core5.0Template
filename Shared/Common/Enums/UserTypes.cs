using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Shared.Common.Enums
{
    public enum UserTypes
    {
        [Description("Admin")]
        Admin = 1,
        [Description("Manager")]
        Manager = 2,
        [Description("User")]
        User = 3,
        [Description("Guest")]
        Guest = 4
    }
}
