﻿using System;
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
        public ActionResult Classification()
        {
            var model = new ClassificationModel();
            return View(model);

        }
        public ActionResult VehicleType()
        {
            var model = new VehicleTypeModel();
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
            int ModelID = model.ModelInfo.ModelID;
            int VariantID = model.ModelInfo.VariantID;
            //int? VehicleBodyTypeID = 0;
            //int? VehicleTypeID = 0;

            int Individual = Convert.ToInt32(TitleTypeEnum.Individual);
            int Corporate = Convert.ToInt32(TitleTypeEnum.Corporate);
            int CorporateWithAssignee = Convert.ToInt32(TitleTypeEnum.CorporateWithAssignee);

            var CurrentSubmit = "";


            using (var db = new ECOCEntities())
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    switch (submitType)
                    {
                        case "ADDMODEL":
                            {
                                if (model.ModelInfo.SelectedBodyTypeID == 0)
                                {
                                    Status = "Info";
                                    Message = "Message: Model Body Type is Required!";
                                }
                                else 
                                {
                                    if (MakeID == 0)
                                    {
                                        Status = "Info";
                                        Message = "Message: No Vehicle Make Selected";
                                    }
                                    else 
                                    {
                                        var newModel = new VehicleModel();
                                        newModel.VehicleMakeID = MakeID;
                                        newModel.VehicleModelName = model.ModelInfo.Model;
                                        newModel.BodyTypeID = model.ModelInfo.SelectedBodyTypeID;
                                        newModel.Active = true;
                                        newModel.CreatedBy = CurrentUser.Details.ID;
                                        newModel.CreatedDate = DateTime.Now;
                                        db.VehicleModel.Add(newModel);

                                        db.SaveChanges();
                                        dbTransaction.Commit();
                                        Status = "Success";
                                        Message = "New model Added Successfully!";
                                    }
                                   
                                }
                          
                      
                                CurrentSubmit = "Model";

                            }
                            break;
                        case "UPDATEMODEL":
                            {
                                if (model.ModelInfo.SelectedBodyTypeID == 0)
                                {
                                    Status = "Info";
                                    Message = "Message: Model Body Type is Required!";
                                }
                                else
                                {
                                  

                                    var updateModel = db.VehicleModel.Where(o => o.VehicleModelID == ModelID).FirstOrDefault();
                                    updateModel.VehicleModelName = model.ModelInfo.Model;
                                    updateModel.BodyTypeID = model.ModelInfo.SelectedBodyTypeID;
                                    updateModel.UpdatedBy = CurrentUser.Details.ID;
                                    updateModel.UpdatedDate = DateTime.UtcNow;

                                    db.SaveChanges();
                                    dbTransaction.Commit();


                                    Status = "Success";
                                    Message = "model Updated Successfully!";
                                    

                                }


                                CurrentSubmit = "Model";

                            }
                            break;
                        case "ADDVARIANT":
                            {
                                if (model.ModelInfo.ModelID == 0)
                                {
                                    Status = "Info";
                                    Message = "Message: No Vehicle Model Selected";
                                }
                                else
                                {

                                    var newVariant = new VehicleVariant();
                                    newVariant.VehicleModelID = ModelID;
                                    newVariant.VariantName = model.ModelInfo.VehicleVariant;
                                    newVariant.Active = true;
                                    newVariant.CreatedBy = CurrentUser.Details.ID;
                                    newVariant.CreatedDate = DateTime.Now;
                                    db.VehicleVariant.Add(newVariant);

                                    db.SaveChanges();
                                    dbTransaction.Commit();
                                    Status = "Success";
                                    Message = "New Variant Added Successfully!";
                                    

                                }

                                CurrentSubmit = "Variant";

                            }
                            break;
                        case "UPDATEVARIANT":
                            {
                                if (model.ModelInfo.ModelID == 0)
                                {
                                    Status = "Info";
                                    Message = "Message: No Vehicle Model Selected";
                                }
                                else
                                {

                                    var updateVariant = db.VehicleVariant.Where(o => o.VariantID == VariantID).FirstOrDefault();
                                    updateVariant.VariantName = model.ModelInfo.VehicleVariant;
                                    updateVariant.UpdatedBy = CurrentUser.Details.ID;
                                    updateVariant.UpdatedDate = DateTime.UtcNow;

                                    db.SaveChanges();
                                    dbTransaction.Commit();


                                    Status = "Success";
                                    Message = "Model Variant updated Successfully!";


                                }

                                CurrentSubmit = "Variant";

                            }
                            break;
                        case "ADDMAKE":
                            {

                                var newMake = new VehicleMake();
                                newMake.VehicleMakeName = model.MakeInfo.VehicleMake;
                                newMake.Active = true;
                                newMake.CreatedBy = CurrentUser.Details.ID;
                                newMake.CreatedDate = DateTime.Now;
                                db.VehicleMake.Add(newMake);

                                db.SaveChanges();
                                dbTransaction.Commit();

                                Status = "Success";
                                Message = "New Vehicle Make Added Successfully!";
                              
                                CurrentSubmit = "Make";

                            }
                            break;
                        case "UPDATEMAKE":
                            {


                                var updateMake = db.VehicleMake.Where(o => o.VehicleMakeID == MakeID).FirstOrDefault();
                                updateMake.VehicleMakeName = model.MakeInfo.VehicleMake;

                                updateMake.UpdatedBy = CurrentUser.Details.ID;
                                updateMake.UpdatedDate = DateTime.UtcNow;

                                db.SaveChanges();
                                dbTransaction.Commit();

                                Status = "Success";
                                Message = "Vehicle Make updated Successfully!";

                                CurrentSubmit = "Make";

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
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    //TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";
                    Status = "Warning";
                    Message = "Error. Please contact Databridge support to assist you.";
                }
            }

            var jsonResult = Json(new { Status, Message, Data = PartialViewDataString, MakeID, ModelID, CurrentSubmit,  });
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
        public ActionResult GetClassificationList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    //select all insurance comapny exclude databridge asia which has ID = 1
                    var tableData = db.VehicleClassification.Where(o => o.Active).ToList();


                    //Returning Json Data    
                    return Json(new { data = tableData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetVehicleTypeList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    //select all insurance comapny exclude databridge asia which has ID = 1
                    var tableData = db.VehicleType.Where(o => o.Active).ToList();


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


                    var tableData = (from a in db.VehicleModel
                                     from b in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == a.BodyTypeID).DefaultIfEmpty()
                                     where
                                     a.Active == true &&
                                     a.VehicleMakeID == MakeID
                                     select new
                                     {
                                         VehicleModelID = a.VehicleModelID,
                                         VehicleModelName = a.VehicleModelName,
                                         BodyTypeID = a.BodyTypeID ?? 0,
                                         BodyTypeName = b.VehicleBodyTypeName ?? "No Body Type",
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
        public ActionResult GetModelVariantList(int ModelID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    var tableData = db.VehicleVariant.Where(o => o.VehicleModelID == ModelID).ToList();
                  
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

    }
    
}