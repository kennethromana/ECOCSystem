﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;
using ECOCSystem.Tools;
using Microsoft.Reporting.WebForms;

namespace ECOCSystem.Controllers
{
    public class CPTLController : Controller
    {
        // GET: CPTL
        [AuthorizeUser]
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
            int AddressID = model.AddressID;

            int Individual = Convert.ToInt32(TitleTypeEnum.Individual);
            int Corporate = Convert.ToInt32(TitleTypeEnum.Corporate);
            int CorporateWithAssignee = Convert.ToInt32(TitleTypeEnum.CorporateWithAssignee);

            var CurrentSubmit = "";
            var FileName = "";


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
                                    NewClient.MiddleName = model.Client.MiddleName == null ? "": model.Client.MiddleName.Trim();
                                   
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
                                    NewClient.MiddleName = model.Client.MiddleName == null ? "" : model.Client.MiddleName.Trim();
                                }

                                NewClient.EmailAddress = model.Client.EmailAddress;
                                NewClient.BusinessPhone = model.Client.BusinessPhone;
                                NewClient.MobileNo = model.Client.MobileNo;
                                NewClient.CompanyID = currentCompany;
                                NewClient.BranchID = currentBranch;
                                NewClient.TitleID = model.Client.TitleID;
                                NewClient.Active = true;
                                NewClient.CreatedBy = CurrentUser.Details.ID;
                                NewClient.CreatedDate = DateTime.UtcNow;


                                db.Client.Add(NewClient);
                                db.SaveChanges();
                                dbTransaction.Commit();

                                Status = "Success";
                                Message = "New client Added Successfully!";
                                CurrentSubmit = "Client";
                                ClientID = NewClient.ID;
                            }
                            break;
                        case "EDITCLIENT":
                            {
                                var updateClient = db.Client.Where(o => o.ID == ClientID).FirstOrDefault();


                                int TitletType = (from a in db.Title.Where(o => o.ID == model.Client.TitleID)
                                                  from b in db.TitleType.Where(o => o.ID == a.TitleTypeID).DefaultIfEmpty()
                                                  where a.Active == true
                                                  select new
                                                  {
                                                      b.ID
                                                  }).FirstOrDefault().ID;

                                if (TitletType == Individual)
                                {
                                    updateClient.FirstName = model.Client.FirstName.Trim();
                                    updateClient.LastName = model.Client.LastName.Trim();
                                    updateClient.MiddleName = model.Client.MiddleName == null ? "" : model.Client.MiddleName.Trim();

                                }
                                else if (TitletType == Corporate)
                                {
                                    updateClient.CorpName = model.Client.CorpName.Trim();
                                }
                                else if (TitletType == CorporateWithAssignee)
                                {
                                    updateClient.CorpName = model.Client.CorpName.Trim();
                                    updateClient.FirstName = model.Client.FirstName.Trim();
                                    updateClient.LastName = model.Client.LastName.Trim();
                                    updateClient.MiddleName = model.Client.MiddleName == null ? "" : model.Client.MiddleName.Trim();
                                }

                                updateClient.EmailAddress = model.Client.EmailAddress;
                                updateClient.BusinessPhone = model.Client.BusinessPhone;
                                updateClient.MobileNo = model.Client.MobileNo;
                                updateClient.TitleID = model.Client.TitleID;

                                updateClient.UpdatedBy = CurrentUser.Details.ID;
                                updateClient.UpdatedDate = DateTime.UtcNow;


                                db.SaveChanges();
                                dbTransaction.Commit();

                                Status = "Success";
                                Message = "client Updated Successfully!";
                                CurrentSubmit = "Client";

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
                                    newAddress.CreatedDate = DateTime.UtcNow;


                                    db.ClientAddress.Add(newAddress);
                                    db.SaveChanges();
                                    var updateClient = db.Client.Where(o => o.ID == ClientID).FirstOrDefault();
                                    updateClient.SelectedAddressID = newAddress.ID;
                                    db.SaveChanges();
                                    dbTransaction.Commit();

                                    Status = "Success";
                                    Message = "New Address added Successfully!";

                                }
                                CurrentSubmit = "Address";


                            }
                            break;
                        case "EDITADDRESS":
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

                                    var updateAddress = db.ClientAddress.Where(o => o.ID == AddressID).FirstOrDefault();

                                    updateAddress.AddressTypeID = model.ClientAddress.AddressTypeID;
                                    updateAddress.HouseBldgNo = model.ClientAddress.HouseBldgNo;
                                    updateAddress.StreetSubdivision = model.ClientAddress.StreetSubdivision;
                                    updateAddress.Barangay = model.ClientAddress.Barangay;
                                    updateAddress.ZipCode = model.ClientAddress.ZipCode;
                                    updateAddress.EmailAddress = model.ClientAddress.EmailAddress;
                                    updateAddress.TelephoneNo = model.ClientAddress.TelephoneNo;
                                    updateAddress.MobileNo = model.ClientAddress.MobileNo;
                                    updateAddress.CityID = model.ClientAddress.CityID;
                                    updateAddress.ProvinceID = model.ClientAddress.ProvinceID;

                                    updateAddress.UpdatedBy = CurrentUser.Details.ID;
                                    updateAddress.UpdatedDate = DateTime.UtcNow;


                                    db.SaveChanges();
                                    dbTransaction.Commit();

                                    Status = "Success";
                                    Message = "Address updated Successfully!";

                                }
                                CurrentSubmit = "Address";


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
                                    newVehicle.ClassificationID = model.ClientVehicle.SelectedClassificationID;

                                    newVehicle.VehicleColorID = model.ClientVehicle.SelectedColorID;
                                    newVehicle.MakeID = model.ClientVehicle.SelectedMakeID;
                                    newVehicle.VariantID = model.ClientVehicle.SelectedVariantID;

                                    newVehicle.ClientID = model.ClientID;
                                    newVehicle.Active = true;
                                    newVehicle.CreatedBy = CurrentUser.Details.ID;
                                    newVehicle.CreatedDate = DateTime.UtcNow;



                                    db.VehicleInfo.Add(newVehicle);
                                    db.SaveChanges();
                                    var updateClient = db.Client.Where(o => o.ID == ClientID).FirstOrDefault();
                                    updateClient.SelectedVehicleID = newVehicle.VehicleID;
                                    db.SaveChanges();

                                    dbTransaction.Commit();

                                    Status = "Success";
                                    Message = "New Vehicle added Successfully!";

                                }
                                CurrentSubmit = "Vehicle";

                            }
                            break;
                        case "EDITVEHICLE":
                            {
                                Status = "Success";
                                Message = "Vehicle update Successfully!";
                                CurrentSubmit = "Vehicle";

                            }
                            break;
                        case "CTPLAPPLICATION":
                            {
                                if (model.ClientID == 0) 
                                {
                                    Status = "Info";
                                    Message = "Client Information is required.";
                                    break;
                                }

                                if (model.VehicleID == 0)
                                {
                                    Status = "Info";
                                    Message = "Client Vehicle Information is required.";
                                    break;
                                }
                                if (model.AddressID == 0)
                                {
                                    Status = "Info";
                                    Message = "Client Address Information is required";
                                    break;

                                }

       

                                 FileName = CoCReport(model.ClientID,model.AddressID,model.VehicleID,model.SelectedRegistrationTypeID);

                                if (FileName != null)
                                {
                                    Status = "Success";
                                    Message = "COC Applied Successfully!";
                                }
                                else 
                                {
                                    Status = "Error";
                                    Message = "COC failed. Please contact Databridge support to assist you.";
                                }
                                CurrentSubmit = "CTPL";


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

            var jsonResult = Json(new { Status, Message, Data = PartialViewDataString, ClientID ,CurrentSubmit, FileName });
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
                                     from f in db.Client.Where(o => o.ID == ClientID).DefaultIfEmpty()
                                     where
                                     a.Active == true &&
                                     a.ClientID == ClientID
                                     select new
                                     {
                                         isChecked = (f.SelectedAddressID ?? 0) == a.ID ? "checked" : "checkStatus='false'",
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


                    var tableData = (from a in db.VehicleInfo
                                     from b in db.VehicleType.Where(o => o.VehicleTypeID == a.VehicleTypeID).DefaultIfEmpty()
                                     from c in db.VehicleMake.Where(o => o.VehicleMakeID == a.MakeID).DefaultIfEmpty()                                  
                                     from d in db.VehicleVariant.Where(o => o.VariantID == a.VariantID).DefaultIfEmpty()
                                     from e in db.VehicleModel.Where(o => o.VehicleModelID == d.VehicleModelID).DefaultIfEmpty()
                                     from f in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == e.BodyTypeID).DefaultIfEmpty()
                                     from g in db.Client.Where(o => o.ID == ClientID).DefaultIfEmpty()
                                     where
                                     a.Active == true &&
                                     a.ClientID == ClientID
                                     select new
                                     { 
                                        isChecked = (g.SelectedVehicleID ?? 0) == a.VehicleID ? "checked" : "checkStatus='false'",
                                         VehicleID = a.VehicleID,
                                        Chassis = a.ChassisNumber,
                                        PlateNo = a.PlateNumber,
                                        Year = a.Year,
                                        VehicleType = b.VehicleTypeDescription,
                                        VehicleMake = c.VehicleMakeName,
                                        VehicleBody = f.VehicleBodyTypeName,
                                        VehicleSeries = e.VehicleModelName + " " + d.VariantName,
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
        public string CoCReport(int ClientID,int AddressID,int VehicleID,int RegistrationTypeID)
        {
            using (var db = new ECOCEntities())
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    LocalReport lr = new LocalReport();
                    string path = Path.Combine(Server.MapPath("~/Reports/RDLC"), "CoCReport.rdlc");
                    if (System.IO.File.Exists(path))
                    {
                        lr.ReportPath = path;
                    }
                    else
                    {
                        return null;
                    }


                    //var Vehicle = GetVehicleInfoV2(VehicleID);
                    var Vehicle = db.VehicleInfo.Where(o => o.VehicleID == VehicleID).FirstOrDefault();

                    //DealerInvoice invoice = new DealerInvoice();

                    CTPLApplication invoice = new CTPLApplication();

                    //Customer customer = new Customer();
                    Client customer = new Client();


                    City City = new City();

                    //Barangay oBarangay = new Barangay();
                    Province oProvince = new Province();
                    MVModel VehicleModel = new MVModel();
                    VehicleClassification VC = new VehicleClassification();

                    //CTPL CTPL = new CTPL();
                    //CTPLTerm CTPLTerm = new CTPLTerm();

                    //Insurance Insurance = new Insurance();
                    Company Insurance = new Company();


                    //InsuranceCOCSeries CoCSeries = new InsuranceCOCSeries();



                    int Individual = (int)TitleTypeEnum.Individual;
                    int Corporate = (int)TitleTypeEnum.Corporate;
                    int CorporateWithAssignee = (int)TitleTypeEnum.CorporateWithAssignee;

                    //get client info
                    var ClientInfo = (from a in db.Client
                                      from b in db.Title.Where(o => o.ID == a.TitleID).DefaultIfEmpty()
                                      from c in db.TitleType.Where(o => o.ID == b.TitleTypeID).DefaultIfEmpty()
                                      from d in db.ClientAddress.Where(o => o.ClientID == a.ID).DefaultIfEmpty()
                                      from e in db.City.Where(o => o.CityID == d.CityID).DefaultIfEmpty()
                                      from f in db.Province.Where(o => o.ProvinceID == d.ProvinceID).DefaultIfEmpty()
                                      where
                                      a.Active == true &&
                                      a.ID == ClientID &&
                                      d.ID == AddressID
                                      select new
                                      {
                                          Client = c.ID == Individual ? a.FirstName.ToUpper() + " " + a.MiddleName.ToUpper() + " " + a.LastName.ToUpper()
                                                                : c.ID == Corporate ? a.CorpName.ToUpper()
                                                                : c.ID == CorporateWithAssignee ? a.FirstName.ToUpper() + " " + a.MiddleName.ToUpper() + " " + a.LastName.ToUpper()
                                                                : "-",
                                          TitleType = c.ID,
                                          Address = d.HouseBldgNo + ", " + d.StreetSubdivision.ToUpper() + ", " + d.Barangay.ToUpper() + ", " + e.CityName.ToUpper() + ", " + f.ProvinceName.ToUpper()
                                      }
                                      ).FirstOrDefault();

                    //GET VEHICLE INFO
                    var VehicleInfo = (from a in db.VehicleInfo
                                       from b in db.VehicleType.Where(o => o.VehicleTypeID == a.VehicleTypeID).DefaultIfEmpty()
                                       from c in db.VehicleMake.Where(o => o.VehicleMakeID == a.MakeID).DefaultIfEmpty()
                                       from d in db.VehicleVariant.Where(o => o.VariantID == a.VariantID).DefaultIfEmpty()
                                       from e in db.VehicleModel.Where(o => o.VehicleModelID == d.VehicleModelID).DefaultIfEmpty()
                                       from f in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == e.BodyTypeID).DefaultIfEmpty()
                                       from g in db.VehicleColor.Where(o => o.VehicleColorID == a.VehicleColorID).DefaultIfEmpty()
                                       where
                                       a.Active == true &&
                                       a.VehicleID == VehicleID
                                       select new
                                       {
                                           VehicleID = a.VehicleID,
                                           Chassis = a.ChassisNumber,
                                           Engine = a.EngineNumber,
                                           PlateNo = a.PlateNumber,
                                           Year = a.Year,
                                           VehicleType = b.VehicleTypeDescription,
                                           VehicleMake = c.VehicleMakeName,
                                           VehicleBody = f.VehicleBodyTypeName,
                                           VehicleSeries = e.VehicleModelName.ToUpper() + " " + d.VariantName.ToUpper(),
                                           Color = g.VehicleColorName,
                                           ClassificationID = a.ClassificationID,
                                           MVFileNo = a.MVFileNumber,
                                       }).FirstOrDefault();

                    //GET INSURANCE PARAMOUNT SERIES WHICH IS 4
                    var InsuranceCOC = db.InsuranceCOCSeries.Where(o => o.InsuranceID == 4 && o.Active == true).FirstOrDefault();

                    var CTPL = db.CTPL.Where(o => o.VehicleClassificationID == VehicleInfo.ClassificationID).FirstOrDefault();
                    var CTPLTerm = db.CTPLTerm.Where(o => o.CPTLTermID == CTPL.CTPLTermID).FirstOrDefault();

                    var InceptionDate = DateTime.Now;
                    var COCExpirationDate = DateTime.Now.AddYears(CTPLTerm.CoverageYear);

                    var COCNo = InsuranceCOC.COCPrefix + (InsuranceCOC.CurrentSeries + 1);


                    //string imagePath = new Uri(Server.MapPath("~/Logos/" + Insurance.Logo)).AbsoluteUri;
                    lr.EnableExternalImages = true;
                    lr.EnableHyperlinks = true;

                    ReportParameter[] prm = new ReportParameter[24];
                    prm[0] = new ReportParameter("NameParameter", ClientInfo.Client);
                    prm[1] = new ReportParameter("AddressParameter", ClientInfo.Address);
                    prm[2] = new ReportParameter("AuthenticationParameter",/* invoice.COCAuthenticationCode*/"");
                    prm[3] = new ReportParameter("PolicyParameter", /*invoice.COCPolicyNumber*/"");
                    prm[4] = new ReportParameter("BusinessParameter", "");
                    prm[5] = new ReportParameter("CoCParameter", COCNo);
                    prm[6] = new ReportParameter("DateIssuedParameter", InceptionDate.ToString("MMM dd, yyyy"));
                    prm[7] = new ReportParameter("ORParameter", "");
                    prm[8] = new ReportParameter("PeriodFromParameter", InceptionDate.ToString("MMM dd, yyyy"));
                    prm[9] = new ReportParameter("PeriodToParameter", COCExpirationDate.ToString("MMM dd, yyyy"));
                    prm[10] = new ReportParameter("ModelParameter", VehicleInfo.VehicleSeries);
                    prm[11] = new ReportParameter("MakeParameter", VehicleInfo.VehicleMake);
                    prm[12] = new ReportParameter("BodyParameter", VehicleInfo.VehicleBody);
                    prm[13] = new ReportParameter("ColorParameter", VehicleInfo.Color);
                    prm[14] = new ReportParameter("MVFileNoParameter", VehicleInfo.MVFileNo);
                    prm[15] = new ReportParameter("PlateParameter", VehicleInfo.PlateNo);
                    prm[16] = new ReportParameter("SerialOrChassisParameter", VehicleInfo.Chassis);
                    prm[17] = new ReportParameter("MotorParameter", VehicleInfo.Engine);
                    prm[18] = new ReportParameter("CapacityParameter", "");
                    prm[19] = new ReportParameter("UnLadenWghtParameter",/* Vehicle.GrossVehicleWeight.ToString()*/"");
                    prm[20] = new ReportParameter("LiabilityParameter", "100,000.00");
                    prm[21] = new ReportParameter("PremiumParameter", CTPL.GrossPremium.ToString("#,##0.00"));
                    prm[22] = new ReportParameter("InsuranceAddress", "");
                    prm[23] = new ReportParameter("InsuranceLogoParameter", /*imagePath*/ "");
                    lr.SetParameters(prm);

                    //ReportDataSource rd = new ReportDataSource("MyDataSet", dealerlist);
                    //lr.DataSources.Add(rd);

                    lr.Refresh();

                    string reportTypeImage = "PDF";
                    string mimeTypeImage;
                    string encodingImage;
                    string fileNameExtensionImage;

                    //string deviceInforImage =

                    //"<DeviceInfo>" +
                    //"  <OutputFormat>PDF</OutputFormat>" +
                    //"  <PageWidth>8.27in</PageWidth>" +
                    //"  <PageHeight>11.69in</PageHeight>" +
                    //"  <MarginTop>0.25in</MarginTop>" +
                    //"  <MarginLeft>0.25in</MarginLeft>" +
                    //"  <MarginRight>0.25in</MarginRight>" +
                    //"  <MarginBottom>0.10in</MarginBottom>" +
                    //"</DeviceInfo>";

                    Warning[] warningsImage;
                    string[] streamsImage;
                    byte[] renderedBytesImage;
                    string deviceInforImage = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>8.27in</PageWidth>" +
                    "  <PageHeight>11.69in</PageHeight>" +
                    "  <MarginTop>0.25in</MarginTop>" +
                    "  <MarginLeft>0.4in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0.10in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";

                    renderedBytesImage = lr.Render(
                        reportTypeImage,
                        deviceInforImage,
                        out mimeTypeImage,
                        out encodingImage,
                        out fileNameExtensionImage,
                        out streamsImage,
                        out warningsImage);

                    //Save PDF to TEMP
                    var FileName = VehicleInfo.Chassis + "-" + COCNo + "_COC.pdf";
                    var pdfPath = Server.MapPath(string.Format("~/Reports/VRTempFiles/")) + FileName;
                    var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/Reports/VRTempFiles/")+ FileName));
            

                    using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                    {
                        fs.Write(renderedBytesImage, 0, renderedBytesImage.Length);
                    }

                    ParamountVehicleType paramountVehicleType = ParamountVehicleType.PC;
                    switch (invoice.VehicleClassificationID)
                    {
                        //Private Car
                        case 1:
                        case 8:
                            {
                                paramountVehicleType = ParamountVehicleType.PC;
                                break;
                            }
                        //Commercial Vehicle
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:

                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                            {
                                paramountVehicleType = ParamountVehicleType.CV;
                                break;
                            }
                        //Motorcycle
                        case 7:
                        case 14:
                            {
                                paramountVehicleType = ParamountVehicleType.MC;
                                break;
                            }

                    }

                    byte[] pdfBytes = System.IO.File.ReadAllBytes(filePath);


                    //creating application
                    var newCTPLApplication = new CTPLApplication();
                    newCTPLApplication.COC = COCNo;
                    newCTPLApplication.COCInceptionDate = InceptionDate;
                    newCTPLApplication.COCExpirationDate = COCExpirationDate;
                    newCTPLApplication.COCBasicPremium = CTPL.BasicPremium;
                    newCTPLApplication.COCVAT = CTPL.VAT;
                    newCTPLApplication.COCDST = CTPL.DST;
                    newCTPLApplication.COCLGT = CTPL.LGT;
                    newCTPLApplication.COCTaxes = CTPL.Taxes;
                    newCTPLApplication.COCAuthenticationFee = CTPL.AuthenticationFee;
                    newCTPLApplication.COCPremium = CTPL.GrossPremium;
                    newCTPLApplication.VehicleID = VehicleID;
                    newCTPLApplication.ClientAddressID = AddressID;
                    newCTPLApplication.ClientID = ClientID;
                    newCTPLApplication.RegistrationTypeID = RegistrationTypeID;
                    newCTPLApplication.COCByte = pdfBytes;
                    newCTPLApplication.COCContentType = "application/pdf";

                    db.CTPLApplication.Add(newCTPLApplication);
                    //db.SaveChanges();

                    return FileName;

                    //Generate Attachment
                    //if (Tools.Functions.FillParamountPolicyCondition(paramountVehicleType, invoice.COCPolicyNumber, "Makati City", Tools.Functions.AddOrdinal(Convert.ToInt32(dateFrom.ToString("dd"))), dateFrom.ToString("MMMM"), dateFrom.ToString("yy")))
                    //{
                    //    //Save merged pdf to Vehicle Info
                    //    byte[] pdfBytes = System.IO.File.ReadAllBytes(Server.MapPath(string.Format("~/Reports/VRTempFiles/")) + invoice.COCPolicyNumber + ".pdf");
                    //    using (db = new ECOCEntities())
                    //    {
                    //        var Update = db.VehicleInfo.Where(o => o.VehicleID == Vehicle.VehicleID).FirstOrDefault();

                    //        var newCTPLApplication = new CTPLApplication();
                    //        newCTPLApplication.VehicleID = VehicleID;
                    //        newCTPLApplication.ClientAddressID = AddressID;
                    //        newCTPLApplication.ClientID = ClientID;
                    //        newCTPLApplication.RegistrationTypeID = RegistrationTypeID;
                    //        newCTPLApplication.COCByte = pdfBytes;
                    //        newCTPLApplication.COCContentType = "application/pdf";

                    //        db.CTPLApplication.Add(newCTPLApplication);
                    //        db.SaveChanges();

                    //        return true;
                    //    }

                    //}
                    //else
                    //    return false;
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    return null;
                }
            }
     
        }
    }
}