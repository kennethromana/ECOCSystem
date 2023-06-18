using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;
using ECOCSystem.Tools;

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
        public ActionResult CPTL(CPTLModel model, string submitType)
        {
            int currentCompany = (int)CurrentUser.Details.CompanyID;
            int currentBranch = (int)CurrentUser.Details.CompanyBranchID;
            var Status = "Error";
            var PartialViewDataString = "";
            var Message = "";
            int ClientID = model.ClientID;
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
                    switch (submitType) 
                    {
                        case "ADDCLIENT":
                            {

                                var NewClient = new Client();

                                int TitletType =   (from a in db.Title.Where(o => o.ID == model.Client.TitleID)
                                                    from b in db.TitleType.Where(o => o.ID == a.TitleTypeID).DefaultIfEmpty()
                                                    where a.Active == true
                                                    select new
                                                    {
                                                        b.ID
                                                    }).FirstOrDefault().ID;

                                if (TitletType == Individual)
                                {
                                    NewClient.FirstName = model.Client.FirstName.Trim();
                                    NewClient.LastName = model.Client.LastName.Trim();
                                    NewClient.MiddleName = model.Client.MiddleName.Trim();
                                   
                                }
                                else if (TitletType == Corporate)
                                {
                                    NewClient.CorpName = model.Client.CorpName.Trim();
                                }
                                else if (TitletType == CorporateWithAssignee)
                                {
                                    NewClient.CorpName = model.Client.CorpName.Trim();
                                    NewClient.FirstName = model.Client.FirstName.Trim();
                                    NewClient.LastName = model.Client.LastName.Trim();
                                    NewClient.MiddleName = model.Client.MiddleName.Trim();
                                }

                                NewClient.CompanyID = currentCompany;
                                NewClient.BranchID = currentBranch;
                                NewClient.TitleID = model.Client.TitleID;
                                NewClient.Active = true;
                                NewClient.CreatedBy = CurrentUser.Details.ID;
                                NewClient.CreatedDate = DateTime.Now;


                                db.Client.Add(NewClient);
                                db.SaveChanges();
                                dbTransaction.Commit();

                                Status = "Success";
                                Message = "New client Added Successfully!";
                            }
                            break;
                        case "EDITCLIENT":
                            {
                                var clientModel = model.Client;
                                Status = "Success";
                                Message = "Client Updated Successfully!";

                            }
                            break;
                        case "ADDADDRESS":
                            {
                                if (model.ClientID == 0)
                                {

                                    Status = "Info";
                                    Message = "Message: Select Client first!";
                                    ViewData.TemplateInfo.HtmlFieldPrefix = "Address";
                                    PartialViewDataString = PartialView("_Address", model.ClientAddress).PartialViewToString();
                                    //return RedirectToAction("Index");

                                }
                                else 
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
                                    newAddress.CityID = model.ClientAddress.CityID;
                                    newAddress.ProvinceID = model.ClientAddress.ProvinceID;

                                    newAddress.Active = true;
                                    newAddress.CreatedBy = CurrentUser.Details.ID;
                                    newAddress.CreatedDate = DateTime.Now;


                                    db.ClientAddress.Add(newAddress);
                                    db.SaveChanges();
                                    dbTransaction.Commit();

                                    Status = "Success";
                                    Message = "New Address added Successfully!";
                                }
                                

                            }
                            break;
                        default:
                            //TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";
                            Status = "Error";
                            Message = "Message: ErrorThere's something error. Please try again later.";
                            break;
                    }
                }
                catch (Exception) 
                {
                    dbTransaction.Rollback();
                    //TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";
                    Status = "Warning";
                    Message = "Error. Please contact Databridge support to assist you.";
                }
            }

            var jsonResult = Json(new { Status, Message, Data = PartialViewDataString, ClientID });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        public ActionResult CPTLList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetClientAddressList(int ClientID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {
                    // Query data
                    // Check if batch is null
                    //var tableData = (from a in db.VehicleInfo.Where(a => a.Active).DefaultIfEmpty()
                    //                 from b in db.DealerInvoice.Where(b => b.VehicleID == a.VehicleID && b.Active).DefaultIfEmpty()
                    //                 from c in db.vwCustomerList.Where(c => c.CustomerID == b.CustomerID && b.Active).DefaultIfEmpty()
                    //                 from d in db.Insurance.Where(d => d.InsuranceID == b.InsuranceID && b.Active).DefaultIfEmpty()
                    //                 from e in db.LTMS_MV.Where(e => e.VehicleID == a.VehicleID).DefaultIfEmpty()
                    //                 from f in db.vwVehicleList.Where(f => f.VehicleID == a.VehicleID).DefaultIfEmpty()
                    //                 from g in db.LTMS_VehicleType.Where(g => g.ID == e.VehicleTypeID).DefaultIfEmpty()
                    //                 from h in db.LTMS_VehicleBodyType.Where(h => h.ID == e.VehicleBodyTypeID).DefaultIfEmpty()
                    //                 from i in db.LTMS_PaymentMethod.Where(i => i.ID == b.PaymentMethodID && i.Active).DefaultIfEmpty()

                    //                 where
                    //                 (((int?)e.VehicleID ?? 0) != 0) // Select vehicle only on LTMS_MV table
                    //                 &&
                    //                 (CurrentUser.Details.ConsolidateSubmission ? (a.DealerID == (int)CurrentUser.Details.ReferenceID) : (a.DealerID == (int)CurrentUser.Details.ReferenceID && a.DealerBranchID == (int)CurrentUser.Details.SubReferenceID)) //Dealer Validation
                    //                 && a.LTOSubmitted == false //Registration validation
                    //                 && (!string.IsNullOrEmpty(a.CSRNumber) && !string.IsNullOrEmpty(a.EngineNumber) && !string.IsNullOrEmpty(a.ChassisNumber))
                    //                 &&
                    //                 (Status == "Owner" ? (((int?)b.CustomerID ?? 0) == 0)
                    //                 : Status == "SalesInvoice" ? //SI Validation
                    //                    ((((int?)b.CustomerID ?? 0) != 0)
                    //                    && (string.IsNullOrEmpty(b.InvoiceContentType) || string.IsNullOrEmpty(b.InvoiceNumber)))
                    //                 : Status == "Insurance" ? // Insurance Validation
                    //                    ((((int?)b.CustomerID ?? 0) != 0)
                    //                    && (!string.IsNullOrEmpty(b.InvoiceContentType) && !string.IsNullOrEmpty(b.InvoiceNumber))
                    //                    && (b.InsuranceID == null && string.IsNullOrEmpty(b.COC)))
                    //                 : Status == "PNP-HPG" ? //PNP Validation
                    //                    ((((int?)b.CustomerID ?? 0) != 0)
                    //                    && (!string.IsNullOrEmpty(b.InvoiceContentType) && !string.IsNullOrEmpty(b.InvoiceNumber))
                    //                    && (b.InsuranceID != null && !string.IsNullOrEmpty(b.COC))
                    //                    && (a.AutoPNP ? (string.IsNullOrEmpty(a.PNPReceiptReferenceNumber) || string.IsNullOrEmpty(a.PNPReceiptContentType)) : (string.IsNullOrEmpty(a.HPGControlNumber) || string.IsNullOrEmpty(a.PNPContentType))))
                    //                 : false)
                    //                 select new
                    //                 {
                    //                     a.VehicleID,
                    //                     CSR = a.CSRNumber,
                    //                     Engine = a.EngineNumber,
                    //                     Chassis = a.ChassisNumber,

                    //                     Make = f.VehicleMakeName,
                    //                     Series = f.VehicleModelName + " " + f.Variant,
                    //                     Color = f.VehicleColorName,
                    //                     Year = a.Year,
                    //                     BodyType = h.Name,
                    //                     MVType = g.Name,
                    //                     GVW = a.GrossVehicleWeight,
                    //                     CPN = a.ConductionSticker,

                    //                     OwnerType = c.TitleTypeID == (int)TitleTypeEnum.Individual ? "Individual" : c.TitleTypeID == (int)TitleTypeEnum.Corporation ? "Organization" : "",

                    //                     BusID = c.BusinessID,
                    //                     ClientID = c.LTOClientID,

                    //                     OwnerName = c.TitleTypeID == (int)TitleTypeEnum.Individual ? (c.LastName + ", " + c.FirstName + " " + c.MiddleName) : c.TitleTypeID == (int)TitleTypeEnum.Corporation ? c.CorpName : "",

                    //                     Emailadd = c.EmailAddress,
                    //                     Mobilenum = c.ContactNumber,

                    //                     SI = b.InvoiceNumber,
                    //                     SIDate = (DateTime?)b.InvoiceDate,

                    //                     Paytype = i.PaymentDescription,
                    //                     FinanceComp = b.FinancingCompany,

                    //                     Insurancetype = b.AutoGenerateCOC ? "ECOC" : "COC",

                    //                     Insurance = d.InsuranceName,
                    //                     COC = b.COC,

                    //                     MVCCSBR = a.AutoPNP ? "SBR" : "MVCC",

                    //                     MVCC = a.AutoPNP ? a.PNPReceiptReferenceNumber : a.HPGControlNumber
                    //                 }).ToList();

                    var tableData = (from a in db.ClientAddress
                                     from b in db.Client.Where(o => o.ID == a.ClientID).DefaultIfEmpty()
                                     from c in db.City.Where(o => o.CityID == a.CityID).DefaultIfEmpty()
                                     from d in db.Province.Where(o => o.ProvinceID == a.ProvinceID).DefaultIfEmpty()
                                     from e in db.AddressType.Where(o => o.ID == a.AddressTypeID).DefaultIfEmpty()
                                     where
                                     a.Active == true &&
                                     a.ClientID == ClientID
                                     select new
                                     {
                                         AddressID = a.ID,
                                         HouseBldgNo = a.HouseBldgNo,
                                         StreetName = a.StreetSubdivision,
                                         Barangay = a.Barangay,
                                         ZipCode = a.ZipCode,
                                         City = c.CityName,
                                         Province = d.ProvinceName,
                                         AddressType = e.Name,
                                         EmailAddress = a.EmailAddress,
                                         MobileNo = a.MobileNo,
                                         TelephoneNo = a.TelephoneNo

                                     }
                                     ).ToList();

                    //Returning Json Data    
                    return Json(new { data = tableData });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}