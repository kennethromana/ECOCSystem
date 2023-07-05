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
        ECOCEntities db = new ECOCEntities();
        public ActionResult Index()
        {
            var model = new DivisionModel();
            return View(model);
        }
        public ActionResult GetRegionList() 
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    //select all insurance comapny exclude databridge asia which has ID = 1
                    var tableData = db.Region.Where(o => o.Active).ToList();


                    //Returning Json Data    
                    return Json(new { data = tableData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}