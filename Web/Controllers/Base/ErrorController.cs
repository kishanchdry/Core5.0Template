using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Base
{
    /// <summary>
    /// Controller for show error pages
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// 404
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowError(int id)
        {
            return View(string.Format("Error_{0}", id));
        }
    }
}
