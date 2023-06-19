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

            int Individual = Convert.ToInt32(TitleTypeEnum.Individual);
            int Corporate = Convert.ToInt32(TitleTypeEnum.Corporate);
            int CorporateWithAssignee = Convert.ToInt32(TitleTypeEnum.CorporateWithAssignee);


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
                                    //ViewData.TemplateInfo.HtmlFieldPrefix = "Address";
                                    //PartialViewDataString = PartialView("_Address", model.ClientAddress).PartialViewToString();
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
                        case "ADDVEHICLE":
                            {
                                if (model.ClientID == 0)
                                {

                                    Status = "Info";
                                    Message = "Message: Select Client first!";
                                    //ViewData.TemplateInfo.HtmlFieldPrefix = "Address";
                                    //PartialViewDataString = PartialView("_Address", model.ClientAddress).PartialViewToString();
                                    //return RedirectToAction("Index");

                                }
                                else
                                {
                                    var newVehicle = new VehicleInfo();

                                    newVehicle.EngineNumber = model.ClientVehicle.EngineNumber;
                                    newVehicle.ChassisNumber = model.ClientVehicle.ChassisNumber;
                                    newVehicle.PlateNumber = model.ClientVehicle.PlateNumber;
                                    newVehicle.Year = model.ClientVehicle.Year;
                                    newVehicle.MVFileNumber = model.ClientVehicle.MVFileNumber;

                                    newVehicle.VehicleTypeID = model.ClientVehicle.SelectedVehicleTypeID;
                                    newVehicle.VehicleColorID = model.ClientVehicle.SelectedColorID;
                                    newVehicle.MakeID = model.ClientVehicle.SelectedMakeID;
                                    newVehicle.BodyTypeID = model.ClientVehicle.SelectedBodyTypeID;
                                    newVehicle.SeriesID = model.ClientVehicle.SelectedSeriesID;

                                    newVehicle.ClientID = model.ClientID;
                                    newVehicle.Active = true;
                                    newVehicle.CreatedBy = CurrentUser.Details.ID;
                                    newVehicle.CreatedDate = DateTime.Now;


                                    db.VehicleInfo.Add(newVehicle);

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
        [HttpPost]
        public ActionResult GetClientVehicleList(int ClientID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {
                    //var tableData = (from a in db.ClientAddress
                    //                 from b in db.Client.Where(o => o.ID == a.ClientID).DefaultIfEmpty()
                    //                 from c in db.City.Where(o => o.CityID == a.CityID).DefaultIfEmpty()
                    //                 from d in db.Province.Where(o => o.ProvinceID == a.ProvinceID).DefaultIfEmpty()
                    //                 from e in db.AddressType.Where(o => o.ID == a.AddressTypeID).DefaultIfEmpty()
                    //                 where
                    //                 a.Active == true &&
                    //                 a.ClientID == ClientID
                    //                 select new
                    //                 {
                    //                     AddressID = a.ID,
                    //                     HouseBldgNo = a.HouseBldgNo,
                    //                     StreetName = a.StreetSubdivision,
                    //                     Barangay = a.Barangay,
                    //                     ZipCode = a.ZipCode,
                    //                     City = c.CityName,
                    //                     Province = d.ProvinceName,
                    //                     AddressType = e.Name,
                    //                     EmailAddress = a.EmailAddress,
                    //                     MobileNo = a.MobileNo,
                    //                     TelephoneNo = a.TelephoneNo

                    //                 }
                    //                 ).ToList();
                    var tableData = (from a in db.VehicleInfo
                                     from b in db.VehicleType.Where(o => o.VehicleTypeID == a.VehicleTypeID).DefaultIfEmpty()
                                     from c in db.VehicleMake.Where(o => o.VehicleMakeID == a.MakeID).DefaultIfEmpty()
                                     from d in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == a.BodyTypeID).DefaultIfEmpty()
                                     from e in db.VehicleSeries.Where(o => o.VehicleSeriesID == a.SeriesID).DefaultIfEmpty()
                                     where
                                     a.Active == true &&
                                     a.ClientID == ClientID
                                     select new
                                     { 
                                        VehicleID = a.VehicleID,
                                        Chassis = a.ChassisNumber,
                                        PlateNo = a.PlateNumber,
                                        Year = a.Year,
                                        VehicleType = b.Name,
                                        VehicleMake = c.VehicleMakeName,
                                        VehicleBody = d.VehicleBodyTypeName,
                                        VehicleSeries = e.Name,
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