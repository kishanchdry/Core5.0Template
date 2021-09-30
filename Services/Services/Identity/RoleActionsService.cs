using Data.Context.Identity;
using Data.Entities.Identity;
using Services.Generic;
using Services.IServices.Identity;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services.Identity
{
    public class RoleActionsService : IRoleActionsService
    {
        private readonly AppIdentityContext _repository;

        public RoleActionsService(AppIdentityContext repository)
        {
            _repository = repository;
        }
        public bool Save(string Id, List<string> controllerNames, List<string> actionNames)
        {
            try
            {
                var actions = _repository.RoleActions.Where(e => e.RoleId == Id);
                _repository.RoleActions.RemoveRange(actions);

                List<RoleAction> roleActions = new List<RoleAction>();

                roleActions.AddRange(controllerNames.Select(e => new RoleAction() { RoleId = Id, Controller = e }));
                roleActions.AddRange(actionNames.Select(e => new RoleAction() { RoleId = Id, Action = e }));

                _repository.RoleActions.AddRange(roleActions);

                _repository.SaveChanges();

                CacheService.ExpireCache(ApplicationConstants.CacheRoleActionsKey);

                return true;
            }
            catch (Exception ex)
            {
                ex.Log();
                return false;
            }
        }

        public bool SaveUserRoles(string Id, List<string> roleIds)
        {

            try
            {
                var actions = _repository.UserRoles.Where(e => e.UserId == Id);
                _repository.UserRoles.RemoveRange(actions);

                List<UserRole> roleActions = new List<UserRole>();

                roleActions.AddRange(roleIds.Select(e => new UserRole() { RoleId = e, UserId = Id }));

                _repository.UserRoles.AddRange(roleActions);

                _repository.SaveChanges();

                CacheService.ExpireCache(ApplicationConstants.CacheUserRoleKey);

                return true;
            }
            catch (Exception ex)
            {
                ex.Log();
                return false;
            }

        }

        public IList<RoleAction> GetAll()
        {
            return _repository.RoleActions.ToList();
        }

        public IList<UserRole> GetAllUserRole()
        {
            return _repository.UserRoles.ToList();
        }
    }
}
