using ECOCSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECOCSystem.Controllers
{
    public class PremiumController : Controller
    {
        // GET: Premium
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetAllPremiumList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    var tableData = (from a in db.CTPL
                                     from b in db.CTPLTerm.Where(o => o.CPTLTermID == a.CTPLTermID).DefaultIfEmpty()
                                     from c in db.VehicleClassification.Where(o => o.VehicleClassificationID == a.VehicleClassificationID).DefaultIfEmpty()
                                     where a.Active == true
                                     select new
                                     {
                                         PremiumID = a.CTPLID,
                                         Classification = c.VehicleClassificationName,
                                         Term = b.TermDescription,
                                         BasicPremium = a.BasicPremium,
                                         Taxes = a.Taxes,
                                         AuthFee = a.AuthenticationFee,
                                         GrossPremium = a.GrossPremium,
                                         VAT = a.VAT,
                                         DST = a.DST,
                                         LGT = a.LGT,
                                     }
                                     ).ToList();


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