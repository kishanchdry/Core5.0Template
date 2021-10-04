using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Extensions;
using Data.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Generic;
using Services.IServices.Identity;
using Shared.Models;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Role management controller
    /// </summary>
    [Authorize(Roles = "Admin, Manager")]
    [Area(areaName: "admin")]
    public class RoleManagementController : Controller
    {
        private readonly IUserService _userManager;
        private readonly IRoleService _rolemanager;
        private readonly IRoleActionsService _roleActionsService;

        //var roleStore = new RoleStore<IdentityRole>(context);
        //var roleMngr = new RoleManager<IdentityRole>(roleStore);

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="rolemanager"></param>
        /// <param name="roleActionsService"></param>
        public RoleManagementController(IUserService userManager, IRoleService rolemanager, IRoleActionsService roleActionsService)
        {
            _userManager = userManager;
            _rolemanager = rolemanager;
            _roleActionsService = roleActionsService;
        }

        /// <summary>
        /// View roles
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<Role> result = _rolemanager.Roles.ToList();
            return View(result);
        }

        /// <summary>
        /// View roles
        /// </summary>
        /// <returns></returns>
        public IActionResult UserRoleManagement()
        {
            List<User> result = _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToList();
            return View(result);
        }


        /// <summary>
        /// Edit role details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult Edit(string Id)
        {
            List<string> nameSpaces = new List<string> { "Web.Controllers", "Web.Areas.Admin.Controllers" };

            List<ClassDetailsModel> classes = new List<ClassDetailsModel>();

            foreach (var myNameSpace in nameSpaces)
            {
                classes.AddRange(GetTypesInNamespace(Assembly.GetExecutingAssembly(), myNameSpace));
            }

            ViewBag.Clasess = classes;//List<ClassDetailsModel>

            var role = _rolemanager.Roles.Include(e => e.RoleAtions).FirstOrDefault(e => e.Id == Id);
            return View(role);
        }
        /// <summary>
        /// Edit role details
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IActionResult EditUserRoles(string Id)
        {
            var user = _userManager.Users.Include(e => e.UserRoles).ThenInclude(e => e.Role).FirstOrDefault(e => e.Id == Id);
            ViewBag.Roles = _rolemanager.Roles.ToList();

            return View(user);
        }

        /// <summary>
        /// Save role permission
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="controllerNames"></param>
        /// <param name="actionNames"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(string Id, List<string> controllerNames, List<string> actionNames)
        {
            var result = _roleActionsService.Save(Id, controllerNames, actionNames);
            return Json(new { result });
        }

        /// <summary>
        /// Save user roles
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveUserRoles(string Id, List<string> roleIds)
        {
            var result = _roleActionsService.SaveUserRoles(Id, roleIds);
            return Json(new { result });
        }


        private List<ClassDetailsModel> GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.InvariantCultureIgnoreCase) && !t.Name.Contains("Account") && t.Name.Contains("Controller"))
                      .Select(e => new ClassDetailsModel()
                      {
                          Name = e.Name,
                          Details = e,
                          Methods = GetMethods(e)
                      })
                      .ToList<ClassDetailsModel>();
        }

        private List<MethodDetailsModel> GetMethods(Type type)
        {
            List<MethodDetailsModel> methodDetails = new List<MethodDetailsModel>();
            foreach (var method in ((System.Reflection.MethodInfo[])((System.Reflection.TypeInfo)type).DeclaredMethods))//((System.Reflection.MethodInfo[])((System.Reflection.TypeInfo)type).DeclaredMethods)[7]
            {
                var parameters = method.GetParameters();

                List<ParamDetailsModel> paramDetails = new List<ParamDetailsModel>();

                if (parameters?.Length > 0)
                {
                    foreach (var item in parameters)
                    {
                        paramDetails.Add(
                            new ParamDetailsModel
                            {
                                Name = item.Name,
                                Type = item.ParameterType,
                                Info = item
                            });
                    }
                }

                methodDetails.Add(new MethodDetailsModel
                {
                    Name = method.Name,
                    ReturnType = method.ReturnType,
                    Details = method,
                    Description = method.GetSummary(),
                    Params = paramDetails
                });
            }
            return methodDetails;
        }

    }
}
