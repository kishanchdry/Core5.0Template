using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Admin.Controllers
{
    /// <summary>
    /// basic management here
    /// </summary>
    [Authorize(Roles = "Admin, Manager")]
    [Area(areaName: "admin")]
    public class HomeController : Controller
    {
        /// <summary>
        /// view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// About us
        /// </summary>
        /// <returns></returns>
        public IActionResult AboutUs()
        {
            return View();
        }

        /// <summary>
        /// Contact us
        /// </summary>
        /// <returns></returns>
        public IActionResult ContactUS()
        {
            return View();
        }
    }
}
