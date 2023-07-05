using ECOCSystem.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;

namespace ECOCSystem.Controllers
{
    [AuthorizeUser]
    public class DivisionController : Controller
    {
        // GET: Division
        public ActionResult Index()
        {
            var model = new DivisionModel();
            return View(model);
        }
    }
}