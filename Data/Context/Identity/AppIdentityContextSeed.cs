using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Context.Identity
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedAsync(IServiceProvider services, AppIdentityContext dbContext)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleMgr = services.GetRequiredService<RoleManager<Role>>();

            if (!dbContext.Roles.Any())
            {
                var rolesData = File.ReadAllText("../Data/Context/Identity/SeedData/Roles.json");
                var roles = JsonSerializer.Deserialize<List<string>>(rolesData);
                foreach (var item in roles)
                {
                    await roleMgr.CreateAsync(new Role(item));
                }
            }

            if (!userManager.Users.Any())
            {
                var userData = File.ReadAllText("../Data/Context/Identity/SeedData/Users.json");
                var users = JsonSerializer.Deserialize<List<User>>(userData);
                foreach (var item in users)
                {
                    //TODO handle psswords
                    //TODO handle role
                    await userManager.CreateAsync(item, "123456");
                    if (item?.UserName?.ToLower().Contains(ApplicationConstants.Admin.ToLower()) == true)
                    {
                        await userManager.AddToRoleAsync(item, "Admin");
                    }
                    else if (item?.UserName?.ToLower().Contains(ApplicationConstants.Manager.ToLower()) == true)
                    {
                        await userManager.AddToRoleAsync(item, ApplicationConstants.Manager);
                    }
                    else if (item?.UserName?.ToLower().Contains(ApplicationConstants.User.ToLower()) == true)
                    {
                        await userManager.AddToRoleAsync(item, ApplicationConstants.User);
                    }
                    else if (item?.UserName?.ToLower().Contains(ApplicationConstants.Guest.ToLower()) == true)
                    {
                        await userManager.AddToRoleAsync(item, ApplicationConstants.Guest);
                    }
                }
                //userManager.AddToRoleAsync(user, adminRole.Name).GetAwaiter().GetResult();
            }
        }
    }
}
