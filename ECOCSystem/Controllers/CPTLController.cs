using System;
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
                                    NewClient.MiddleName = model.Client.MiddleName.Trim();
                                }

                                NewClient.EmailAddress = model.Client.EmailAddress;
                                NewClient.BusinessPhone = model.Client.BusinessPhone;
                                NewClient.MobileNo = model.Client.MobileNo;
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
        public async Task<bool> CoCReport(int VehicleID)
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
                    return false;
                }


           

                var dateFrom = (DateTime)DateTime.Now;
                var dateTo = (DateTime)DateTime.Now;

                //string imagepath = Path.Combine(Server.MapPath("~/Content"), "DataBridge Asia Inc Logo-01.jpg");
                //var base64 = Convert.ToBase64String(dealerss.LogoByte);
                //var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                //lr.EnableExternalImages = true;

                //convert byte array to base64string

                //string base64String = Convert.ToBase64String(Insurance.LogoByte);
                //var imgSrc = String.Format("data:image/png;base64,{0}", base64String);
                //var logoimage = "data:image/gif;base64," + base64String;

                //Image image;
                //using (MemoryStream ms = new MemoryStream(Insurance.LogoByte))
                //{
                //    image = Image.FromStream(ms);
                //}
                //var randomFileName = "Insurance-Logo" + Vehicle.VehicleID + ".png";
                //var fullPath = Path.Combine(Server.MapPath("~/scripts/Img/"), randomFileName);
                //image.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);


                //string imagePath = new Uri().AbsoluteUri;
                lr.EnableExternalImages = true;
                lr.EnableHyperlinks = true;

                ReportParameter[] prm = new ReportParameter[24];
                prm[0] = new ReportParameter("NameParameter", "");
                prm[1] = new ReportParameter("AddressParameter", "");
                prm[2] = new ReportParameter("AuthenticationParameter", "");
                prm[3] = new ReportParameter("PolicyParameter","");
                prm[4] = new ReportParameter("BusinessParameter", "");
                prm[5] = new ReportParameter("CoCParameter","");
                prm[6] = new ReportParameter("DateIssuedParameter", dateFrom.ToString("MMM dd, yyyy"));
                prm[7] = new ReportParameter("ORParameter", "");
                prm[8] = new ReportParameter("PeriodFromParameter", dateFrom.ToString("MMM dd, yyyy"));
                prm[9] = new ReportParameter("PeriodToParameter", dateTo.ToString("MMM dd, yyyy"));
                prm[10] = new ReportParameter("ModelParameter", "");
                prm[11] = new ReportParameter("MakeParameter", "");
                prm[12] = new ReportParameter("BodyParameter", "");
                prm[13] = new ReportParameter("ColorParameter", "");
                prm[14] = new ReportParameter("MVFileNoParameter", "");
                prm[15] = new ReportParameter("PlateParameter", "");
                prm[16] = new ReportParameter("SerialOrChassisParameter","");
                prm[17] = new ReportParameter("MotorParameter", "");
                prm[18] = new ReportParameter("CapacityParameter", "");
                prm[19] = new ReportParameter("UnLadenWghtParameter", "");
                prm[20] = new ReportParameter("LiabilityParameter", "100,000.00");
                prm[21] = new ReportParameter("PremiumParameter","");
                prm[22] = new ReportParameter("InsuranceAddress", "");
                prm[23] = new ReportParameter("InsuranceLogoParameter", "");
                lr.SetParameters(prm);

                //ReportDataSource rd = new ReportDataSource("MyDataSet", dealerlist);
                //lr.DataSources.Add(rd);

                lr.Refresh();

                string reportTypeImage = "PDF";
                string mimeTypeImage;
                string encodingImage;
                string fileNameExtensionImage;

                string deviceInforImage =

                "<DeviceInfo>" +
                "  <OutputFormat>PDF</OutputFormat>" +
                "  <PageWidth>8.27in</PageWidth>" +
                "  <PageHeight>11.69in</PageHeight>" +
                "  <MarginTop>0.25in</MarginTop>" +
                "  <MarginLeft>0.25in</MarginLeft>" +
                "  <MarginRight>0.25in</MarginRight>" +
                "  <MarginBottom>0.10in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warningsImage;
                string[] streamsImage;
                byte[] renderedBytesImage;

                renderedBytesImage = lr.Render(
                    reportTypeImage,
                    deviceInforImage,
                    out mimeTypeImage,
                    out encodingImage,
                    out fileNameExtensionImage,
                    out streamsImage,
                    out warningsImage);

                //Save PDF to TEMP
                var pdfPath = Server.MapPath(string.Format("~/Reports/VRTempFiles/")) +"_Policy.pdf";

                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    fs.Write(renderedBytesImage, 0, renderedBytesImage.Length);
                }

                //    ParamountVehicleType paramountVehicleType = ParamountVehicleType.PC;
                //    switch (invoice.VehicleClassificationID)
                //    {
                //        Private Car
                //        case 1:
                //        case 8:
                //            {
                //        paramountVehicleType = ParamountVehicleType.PC;
                //        break;
                //    }
                //    Commercial Vehicle
                //        case 2:
                //        case 3:
                //        case 4:
                //        case 5:
                //        case 6:

                //        case 9:
                //        case 10:
                //        case 11:
                //        case 12:
                //        case 13:
                //            {
                //        paramountVehicleType = ParamountVehicleType.CV;
                //        break;
                //    }
                //    Motorcycle
                //        case 7:
                //        case 14:
                //            {
                //        paramountVehicleType = ParamountVehicleType.MC;
                //        break;
                //    }

                //}
                //Generate Attachment
                //if (Tools.Functions.FillParamountPolicyCondition(paramountVehicleType, invoice.COCPolicyNumber, "Makati City", Tools.Functions.AddOrdinal(Convert.ToInt32(dateFrom.ToString("dd"))), dateFrom.ToString("MMMM"), dateFrom.ToString("yy")))
                //{
                //    //Save merged pdf to Vehicle Info
                //    byte[] pdfBytes = System.IO.File.ReadAllBytes(Server.MapPath(string.Format("~/Reports/VRTempFiles/")) + invoice.COCPolicyNumber + ".pdf");
                //    using (db = new VRSystemEntities())
                //    {
                //        var Update = db.VehicleInfo.Where(o => o.VehicleID == Vehicle.VehicleID).FirstOrDefault();
                //        var UpdateInvoice = db.DealerInvoice.Where(o => o.VehicleID == Vehicle.VehicleID).FirstOrDefault();

                //        Update.CertificateOfConformity = pdfBytes;
                //        Update.COCContentType = "application/pdf";
                //        UpdateInvoice.COCByte = pdfBytes;
                //        UpdateInvoice.COCContentType = "application/pdf";


                //        db.SaveChanges();
                //        return true;
                //    }

                //}
                //else
                //    return false;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}