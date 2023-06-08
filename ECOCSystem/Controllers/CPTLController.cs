using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;

namespace ECOCSystem.Controllers
{
    public class CPTLController : Controller
    {
        // GET: CPTL
        public ActionResult Index()
        {
            CPTLModel Model = new CPTLModel();
            return View(Model);
        }
        public ActionResult Client(ClientModel model)
        {

            //return PartialView("_Client", model);
            return RedirectToAction("Index");
        }

        public ActionResult CPTLList()
        {
            return View();
        }
    }
}