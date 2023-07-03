using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Tools;
using ECOCSystem.Model;

namespace ECOCSystem.Controllers
{
    [AuthorizeUser]
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult Index()
        {
            var model = new MakeModel();
            return View(model);

        }

        public ActionResult GetMakeList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    //select all insurance comapny exclude databridge asia which has ID = 1
                    var tableData = db.VehicleMake.Where(o => o.Active).ToList();


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