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

            //var Status = "Error";
            //var PartialViewDataString = "";
            //var Message = "";
            //int CPTLID = model.CPTLID;
            //int? VehicleBodyTypeID = 0;
            //int? VehicleTypeID = 0;

            int Individual = 1;
            int Corporate = 2;
            int CorporateWithAssignee = 3;


            using (var db = new ECOCEntities())
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    switch (submit) 
                    {
                        case "ADDCLIENT":
                            {

                                var NewClient = new Client();
                                if (model.Client.TitleID == Individual)
                                {
                                    NewClient.FirstName = model.Client.FirstName.Trim();
                                    NewClient.LastName = model.Client.LastName.Trim();
                                    NewClient.MiddleName = model.Client.MiddleName.Trim();
                                   
                                }
                                else if (model.Client.TitleID == Corporate)
                                {
                                    NewClient.CorpName = model.Client.CorpName.Trim();
                                }
                                else if (model.Client.TitleID == CorporateWithAssignee)
                                {
                                    NewClient.CorpName = model.Client.CorpName.Trim();
                                    NewClient.FirstName = model.Client.FirstName.Trim();
                                    NewClient.LastName = model.Client.LastName.Trim();
                                    NewClient.MiddleName = model.Client.MiddleName.Trim();
                                }

                                NewClient.TitleID = model.Client.TitleID;
                                NewClient.Active = true;
                                NewClient.CreatedBy = 1;
                                NewClient.CreatedDate = DateTime.Now;


                                db.Client.Add(NewClient);               
                                //db.SaveChanges();
                                //dbTransaction.Commit();
                                TempData["SuccessMessage"] = "Success! New client Added.";
                            }
                            break;
                        case "EDITCLIENT":
                            {
                                var clientModel = model.Client;
                                TempData["SuccessMessage"] = "Success! Client Updated.";

                            }
                            break;
                        case "ADDADDRESS":
                            {
                                var newAddress = new ClientAddress();

                                newAddress.AddressTypeID = model.ClientAddress.AddressTypeID;
                                newAddress.ClientID = model.ClientID;
                                newAddress.HouseBldgNo = model.ClientAddress.HouseBldgNo;
                                newAddress.StreetSubdivision = model.ClientAddress.StreetSubdivision;
                                newAddress.Barangay = model.ClientAddress.Barangay;
                                newAddress.ZipCode = model.ClientAddress.ZipCode;
                                newAddress.EmailAddress = model.ClientAddress.EmailAddress;
                                newAddress.TelephoneNo = model.ClientAddress.TelephoneNo;
                                newAddress.MobileNo = model.ClientAddress.MobileNo;

                                newAddress.Active = true;
                                newAddress.CreatedBy = 1;
                                newAddress.CreatedDate = DateTime.Now;


                                db.ClientAddress.Add(newAddress);
                                //db.SaveChanges();
                                //dbTransaction.Commit();
                                TempData["SuccessMessage"] = "Success! New Address added.";

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