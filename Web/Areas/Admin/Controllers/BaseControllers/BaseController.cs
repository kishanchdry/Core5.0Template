using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Services.IServices;
using System.Reflection.Metadata;
using Shared.Models.Base;

namespace Web.Areas.Admin.Controllers.BaseControllers
{
    /// <summary>
    /// Handle base requests
    /// </summary>
    public class BaseController : Controller
    {
        /*
        protected override void OnException(ExceptionContext exceptionContext)
        {
            if (!exceptionContext.ExceptionHandled)
            {
                string controllerName = (string)exceptionContext.RouteData.Values["controller"];
                string actionName = (string)exceptionContext.RouteData.Values["action"];

                if (exceptionContext.Exception != null)
                {
                    var exe = exceptionContext.Exception.GetExceptionModel();
                    try
                    {
                        dynamic expando = new ExpandoObject();

                        var modelstate = exceptionContext.ModelState;
                        var propertylist = modelstate.Keys;

                        foreach (var item in propertylist)
                        {
                            string propName = item;
                            string propValue = ModelState[item].AttemptedValue;

                            AddProperty(expando, propName, propValue);
                        }

                        var modelValuesString = Newtonsoft.Json.JsonConvert.SerializeObject(expando);

                        exe.ModelValues = modelValuesString;
                    }
                    catch (Exception ex)
                    {
                        ex.Log();
                    }
                    exe.Log();
                }
            }
        }

        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
        */
    }

    /// <summary>
    /// Generic base controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseController<T> : Controller where T : BaseModel
    {
        /// <summary>
        /// service
        /// </summary>
        protected readonly IGenericService<T> service;

        /// <summary>
        /// construct generic services
        /// </summary>
        /// <param name="service"></param>
        public BaseController(IGenericService<T> service)
        {
            this.service = service;
        }

        /// <summary>
        /// Base add update
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult AddUpdate(long id)
        {
            try
            {
                T model;
                if (id > 0)
                {
                    model = service.GetById(id);
                }
                else
                {
                    model = (T)Activator.CreateInstance(typeof(T));
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ex.Log();
                throw ex;
            }
        }
      
        /// <summary>
        /// Save add update data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult AddUpdate(T model)
        {
            try
            {
                bool flag;
                if ((model as BaseModel).Id > 0)
                {
                    flag = service.Update(model);
                }
                else
                {
                    flag = service.Add(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ex.Log();
                return RedirectToAction("AddUpdate", model);
            }
        }

        /// <summary>
        /// remove model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        internal virtual ActionResult Remove(T model)
        {
            try
            {
                var flag = service.Remove(model);
                return Json(new { flag });
            }
            catch (Exception ex)
            {
                ex.Log();
                throw ex;
            }
        }

        /// <summary>
        /// remove item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult Remove(long id)
        {
            try
            {
                var flag = service.Remove(id);
                return Json(new { flag });
            }
            catch (Exception ex)
            {
                ex.Log();
                throw ex;
            }
        }

        /// <summary>
        /// Get all item list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult GetAll()
        {
            try
            {
                var result = service.GetAllWitnInActive();
                return View(result);
            }
            catch (Exception ex)
            {
                ex.Log();
                throw ex;
            }
        }
        
        /// <summary>
        /// View by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult View(long Id)
        {
            try
            {
                var result = service.GetById(Id);
                return View(result);
            }
            catch (Exception ex)
            {
                ex.Log();
                throw ex;
            }
        }

        /// <summary>
        /// Get all by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual ActionResult ViewByUser(string userId)
        {
            try
            {
                var result = service.GetListByUserId(userId);
                return View(result);
            }
            catch (Exception ex)
            {
                ex.Log();
                throw ex;
            }
        }
    }
}
