using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        [HttpPost]
        public ActionResult CPTL(CPTLModel model, string submit)
        {
            var Status = "Error";
            var PartialViewDataString = "";
            var Message = "";
            int CPTLID = model.CPTLID;
            int? VehicleBodyTypeID = 0;
            int? VehicleTypeID = 0;

            using (var db = new ECOCEntities())
            using (var dbTransaction = db.Database.BeginTransaction())
            {

                try
                {
                    switch (submit) 
                    {
                        case "CLIENT":
                            {
                                var clientModel = model.Client;

                                Message = "Message: There's something wrong. Please contact Databridge support.";
                            }
                            break;
                        default:
                            Message = "Message: ErrorThere's something error. Please try again later";
                            break;
                    }
                }
                catch (Exception) 
                {
                    dbTransaction.Rollback();
                    Message = "Message: There's something wrong. Please contact Databridge support.";
                }
            }

            var jsonResult = Json(new { Status, Message, Data = PartialViewDataString, CPTLID});
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        public ActionResult CPTLList()
        {
            return View();
        }
    }
}