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
            using (var db = new ECOCEntities())
            {
                var model = new MakeModel();
              
                return View(model);
            }

   
        }
        public ActionResult BodyType()
        {
            var model = new BodyTypeModel();
            return View(model);

        }
        public ActionResult Model()
        {
            var model = new VehicleModelModel();
            return View(model);

        }
        public ActionResult MakeForm(MakeModel model, string submitType)
        {
            int currentCompany = (int)CurrentUser.Details.CompanyID;
            int currentBranch = (int)CurrentUser.Details.CompanyBranchID;
            var Status = "Error";
            var PartialViewDataString = "";
            var Message = "";
            int MakeID = model.MakeID;
            //int? VehicleBodyTypeID = 0;
            //int? VehicleTypeID = 0;

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


                                Status = "Success";
                                Message = "New client Added Successfully!";
                                CurrentSubmit = "Client";

                            }
                            break;
                        case "EDITCLIENT":
                            {

                                Status = "Success";
                                Message = "Client Updated Successfully!";
                                CurrentSubmit = "Client";

                            }
                            break;
                        case "ADDADDRESS":
                            {
                                if (model.MakeID == 0)
                                {

                                    Status = "Info";
                                    Message = "Message: Select Client first!";
                                    //ViewData.TemplateInfo.HtmlFieldPrefix = "Address";
                                    //PartialViewDataString = PartialView("_Address", model.ClientAddress).PartialViewToString();
                                    //return RedirectToAction("Index");

                                }
                                else
                                {
                                   
                                    Status = "Success";
                                    Message = "New Address added Successfully!";

                                }
                                CurrentSubmit = "Address";


                            }
                            break;
                        case "ADDVEHICLE":
                            {
                                if (model.MakeID == 0)
                                {

                                    Status = "Info";
                                    Message = "Message: Select Client first!";
                                    //ViewData.TemplateInfo.HtmlFieldPrefix = "Address";
                                    //PartialViewDataString = PartialView("_Address", model.ClientAddress).PartialViewToString();
                                    //return RedirectToAction("Index");

                                }
                                else
                                {
                                    

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

            var jsonResult = Json(new { Status, Message, Data = PartialViewDataString, MakeID, CurrentSubmit, FileName });
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

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
        public ActionResult GetAllBodyTypeList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    var tableData = db.VehicleBodyType.Where(o => o.Active).ToList();


                    //Returning Json Data    
                    return Json(new { data = tableData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetMakeModelList(int MakeID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {


                    var tableData = db.VehicleModel.Where(o => o.Active && o.VehicleMakeID == MakeID).ToList();


                    //Returning Json Data    
                    return Json(new { data = tableData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetVariantList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {


                    var tableData = db.VehicleVariant.Where(o => o.Active == true).ToList();


                    //Returning Json Data    
                    return Json(new { data = tableData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetModelList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {


                    var tableData = (from a in db.VehicleModel
                                     from b in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == a.BodyTypeID).DefaultIfEmpty()
                                     where a.Active == true
                                     select new Models
                                     {
                                         ModelID = a.VehicleModelID,
                                         ModelName = a.VehicleModelName,
                                         BodyType = b.VehicleBodyTypeName
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
        public ActionResult GetBodyTypeList(int MakeID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {


                    var tableData = (from a in db.MakeBodyType
                                     from b in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == a.BodyTypeID).DefaultIfEmpty()
                                     where 
                                     a.Active == true &&
                                     a.MakeID == MakeID
                                     select new
                                     {
                                         BodyTypeID = a.BodyTypeID,
                                         BodyTypeName = b.VehicleBodyTypeName,
                                         BodyTypeAbbreviation = b.VehicleBodyAbbr,
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
        public ActionResult GetColorList(int MakeID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    var tableData = (from a in db.MakeColor
                                     from b in db.VehicleColor.Where(o => o.VehicleColorID == a.ColorID).DefaultIfEmpty()
                                     where a.Active == true &&
                                     a.MakeID == MakeID
                                     select new
                                     {
                                         ColorID = a.ColorID,
                                         ColorName = b.VehicleColorName
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