using ECOCSystem.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECOCSystem.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [AuthorizeUser]
        public ActionResult Index()
        {
            return View("Error");   
        }
    }
}