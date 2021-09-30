using Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Services.IServices.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services.Identity
{
    public class RoleService : RoleManager<Role>, IRoleService
    {
        public RoleService(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger) :
            base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public override ILogger Logger { get => base.Logger; set => base.Logger = value; }

        public override IQueryable<Role> Roles => base.Roles;

        public override bool SupportsQueryableRoles => base.SupportsQueryableRoles;

        public override bool SupportsRoleClaims => base.SupportsRoleClaims;

        protected override CancellationToken CancellationToken => base.CancellationToken;

        public override Task<IdentityResult> AddClaimAsync(Role role, Claim claim)
        {
            return base.AddClaimAsync(role, claim);
        }

        public override Task<IdentityResult> CreateAsync(Role role)
        {
            return base.CreateAsync(role);
        }

        public override Task<IdentityResult> DeleteAsync(Role role)
        {
            return base.DeleteAsync(role);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override Task<Role> FindByIdAsync(string roleId)
        {
            return base.FindByIdAsync(roleId);
        }

        public override Task<Role> FindByNameAsync(string roleName)
        {
            return base.FindByNameAsync(roleName);
        }

        public override Task<IList<Claim>> GetClaimsAsync(Role role)
        {
            return base.GetClaimsAsync(role);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task<string> GetRoleIdAsync(Role role)
        {
            return base.GetRoleIdAsync(role);
        }

        public override Task<string> GetRoleNameAsync(Role role)
        {
            return base.GetRoleNameAsync(role);
        }

        public override string NormalizeKey(string key)
        {
            return base.NormalizeKey(key);
        }

        public override Task<IdentityResult> RemoveClaimAsync(Role role, Claim claim)
        {
            return base.RemoveClaimAsync(role, claim);
        }

        public override Task<bool> RoleExistsAsync(string roleName)
        {
            return base.RoleExistsAsync(roleName);
        }

        public override Task<IdentityResult> SetRoleNameAsync(Role role, string name)
        {
            return base.SetRoleNameAsync(role, name);
        }

        public override Task<IdentityResult> UpdateAsync(Role role)
        {
            return base.UpdateAsync(role);
        }

        public override Task UpdateNormalizedRoleNameAsync(Role role)
        {
            return base.UpdateNormalizedRoleNameAsync(role);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override Task<IdentityResult> UpdateRoleAsync(Role role)
        {
            return base.UpdateRoleAsync(role);
        }

        protected override Task<IdentityResult> ValidateRoleAsync(Role role)
        {
            return base.ValidateRoleAsync(role);
        }
    }
}
