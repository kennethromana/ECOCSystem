using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;
using ECOCSystem.Tools;

namespace ECOCSystem.Controllers
{
    public class CompanyController : Controller
    {
        ECOCEntities db = new ECOCEntities();
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Company()
        {
            var model = new CompanyModel();

            model.CompanyListView = db.Company.Where(o => o.Active == true).ToList();

            return View(model);
        }
    }
}