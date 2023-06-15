﻿using System;
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
            ModelState.Remove("ClientID");
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
                        case "ADDCLIENT":
                            {
                                
                                var clientModel = model.Client;



                                TempData["SuccessMessage"] = "Success! New client Added.";

                            }
                            break;
                        case "EDITCLIENT":
                            {
                                var clientModel = model.Client;



                                TempData["SuccessMessage"] = "Success! Client Updated.";

                            }
                            break;
                        default:
                            TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";
                            break;
                    }
                }
                catch (Exception) 
                {
                    dbTransaction.Rollback();
                    TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";
                }
            }

            //var jsonResult = Json(new { Status, Message, Data = PartialViewDataString, CPTLID});
            //jsonResult.MaxJsonLength = int.MaxValue;
            //return jsonResult;
            //return View();
            return RedirectToAction("Index");
        }

        public ActionResult CPTLList()
        {
            return View();
        }
    }
}