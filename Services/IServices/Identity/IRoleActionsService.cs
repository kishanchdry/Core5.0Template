using Data.Entities.Identity;
using System.Collections.Generic;

namespace Services.IServices.Identity
{
    public interface IRoleActionsService
    {
        bool Save(string Id, List<string> controllerNames, List<string> actionNames);
        bool SaveUserRoles(string Id, List<string> roleIds);
        IList<RoleAction> GetAll();
        IList<UserRole> GetAllUserRole();
    }
}