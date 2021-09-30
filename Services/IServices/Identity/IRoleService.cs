using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Services.IServices.Identity
{
    public interface IRoleService
    {
        ILogger Logger { get; set; }
        IQueryable<Role> Roles { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }

        Task<IdentityResult> AddClaimAsync(Role role, Claim claim);
        Task<IdentityResult> CreateAsync(Role role);
        Task<IdentityResult> DeleteAsync(Role role);
        bool Equals(object obj);
        Task<Role> FindByIdAsync(string roleId);
        Task<Role> FindByNameAsync(string roleName);
        Task<IList<Claim>> GetClaimsAsync(Role role);
        int GetHashCode();
        Task<string> GetRoleIdAsync(Role role);
        Task<string> GetRoleNameAsync(Role role);
        string NormalizeKey(string key);
        Task<IdentityResult> RemoveClaimAsync(Role role, Claim claim);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> SetRoleNameAsync(Role role, string name);
        Task<IdentityResult> UpdateAsync(Role role);
        Task UpdateNormalizedRoleNameAsync(Role role);
    }
}