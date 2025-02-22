﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Net.Http;
using ECOCSystem.Model;
using ECOCSystem.Tools;


namespace ECOCSystem.Controllers
{
    [AuthorizeUser]
    public class GetDataController : Controller
    {
        // GET: GetData
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetClientTitle(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {
                //int skip = page > 1 ? Convert.ToInt32(start) + 5 : 0;
                //int recordsTotal = 0;
                //bool more = true;


                var itemList = (from a in db.Title
                                from b in db.TitleType.Where(o => o.ID == a.TitleTypeID).DefaultIfEmpty()
                                where a.Active == true
                                select new
                                {
                                    id =  a.ID,
                                    text = a.Name
                                }).ToList();
              

                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpGet]
        public ActionResult GetClients(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {

                int Individual = 1;
                int Corporate = 2;
                int CorporateWithAssignee = 3;

                int curCompany = (int)CurrentUser.Details.CompanyID;
                int curBranch = (int)CurrentUser.Details.CompanyBranchID;

                var itemList = (from a in db.Client
                                from b in db.Title.Where(o => o.ID == a.TitleID).DefaultIfEmpty()
                                from c in db.TitleType.Where(o => o.ID == b.TitleTypeID).DefaultIfEmpty()
                                where a.Active == true &&
                                a.CompanyID == curCompany &&
                                a.BranchID == curBranch
                                select new
                                {
                                    id = a.ID,
                                    text = c.ID == Individual ? b.TitleAbbreviation+" "+ a.FirstName+" "+a.LastName+" "+a.MiddleName + " - " + c.Name
                                          : c.ID == Corporate ? a.CorpName + " - " + c.Name
                                          : c.ID == CorporateWithAssignee ? a.FirstName + " " + a.LastName + " " + a.MiddleName +" - " +a.CorpName +" - "+c.Name
                                          : "-"
                                }).ToList();

                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpGet]
        public ActionResult GetAddressTypes(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.AddressType
                                select new
                                {
                                    id = a.ID,
                                    text = a.Name
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpGet]
        public ActionResult GetRegistrationTypes(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {
                //int skip = page > 1 ? Convert.ToInt32(start) + 5 : 0;
                //int recordsTotal = 0;
                //bool more = true;


                var itemList = (from a in db.RegistrationType
                                select new
                                {
                                    id = a.ID,
                                    text = a.Name
                                }).ToList();

                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpGet]
        public ActionResult GetCitylist(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {
                //int skip = page > 1 ? Convert.ToInt32(start) + 5 : 0;
                //int recordsTotal = 0;
                //bool more = true;


                var itemList = (from a in db.City
                                select new
                                {
                                    id = a.CityID,
                                    text = a.CityName
                                }).ToList();

                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpGet]
        public ActionResult GetProvinceList(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {
                //int skip = page > 1 ? Convert.ToInt32(start) + 5 : 0;
                //int recordsTotal = 0;
                //bool more = true;


                var itemList = (from a in db.Province
                                select new
                                {
                                    id = a.ProvinceID,
                                    text = a.ProvinceName
                                }).ToList();

                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public ActionResult GetUserTypes(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.UserType.Where(o => o.ID != (int)UserTypeEnum.SuperAdmin)
                                select new
                                {
                                    id = a.ID,
                                    text = a.Name
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public ActionResult GetCompanyList(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.Company
                                select new
                                {
                                    id = a.ID,
                                    text = a.Name
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public ActionResult GetCompanyBranchList(string search, int page, int pageSize,int CompanyID)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.CompanyBranch
                                where a.CompanyID  == CompanyID
                                select new
                                {
                                    id = a.ID,
                                    text = a.Name
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpGet]
        public JsonResult GetTitleTypeID(int TitleID)
        {
            using (var db = new ECOCEntities())
            {
                var TitletTypeID = (from a in db.Title.Where(o => o.ID == TitleID)
                                    from b in db.TitleType.Where(o => o.ID == a.TitleTypeID).DefaultIfEmpty()
                                    where a.Active == true
                                    select new
                                    {
                                        b.ID
                                    }).FirstOrDefault();

                return Json(TitletTypeID, JsonRequestBehavior.AllowGet);
            }
                      
        }
        public ActionResult GetVehicleTypes(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.VehicleType
                                select new
                                {
                                    id = a.VehicleTypeID,
                                    text = a.VehicleCode+" - "+a.VehicleTypeDescription
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public ActionResult GetVehicleClassificationList(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.VehicleClassification
                                where a.Active
                                select new
                                {
                                    id = a.VehicleClassificationID,
                                    text = a.VehicleClassificationName
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public ActionResult GetVehicleMakeList(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.VehicleMake
                                where a.Active == true
                                select new
                                {
                                    id = a.VehicleMakeID,
                                    text = a.VehicleMakeName
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        
        public ActionResult GetVehicleBodyList2(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.VehicleBodyType
                                where a.Active == true
                                select new
                                {
                                    id = a.VehicleBodyTypeID,
                                    text = a.VehicleBodyTypeName
                                }).ToList();





                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

        public ActionResult GetVehicleColors(string search, int page, int pageSize)
        {
            using (var db = new ECOCEntities())
            {


                var itemList = (from a in db.VehicleColor

                                where a.Active == true
                                select new
                                {
                                    id = a.VehicleColorID,
                                    text = a.VehicleColorName
                                }).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        
        public ActionResult GetVehicleVariants(string search, int page, int pageSize, int MakeID)
        {
            using (var db = new ECOCEntities())
            {

                var itemList = (from a in db.VehicleVariant
                                from b in db.VehicleModel.Where(o => o.VehicleModelID == a.VehicleModelID).DefaultIfEmpty()
                                from c in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == b.BodyTypeID).DefaultIfEmpty()
                                where
                                a.Active == true &&
                                b.VehicleMakeID == MakeID
                                select new 
                                {
                                    id = a.VariantID,
                                    text = b.VehicleModelName + "  " + a.VariantName + "  -   " + c.VehicleBodyTypeName
                                }
                                ).ToList();



                //Search itemList
                if (!string.IsNullOrWhiteSpace(search))
                {
                    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                }

                //Total size of itemList
                var itemsTotal = itemList.Count();

                //check next page itemList
                itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();

                var jsonResult = Json(new { items = itemList, page = page, pageSize = pageSize, total_count = itemsTotal }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public JsonResult GetAddressInfo(int AddressID)
        {
            using (var db = new ECOCEntities())
            {
                var AddressInfo = (from a in db.ClientAddress
                                   from b in db.Client.Where(o => o.ID == a.ClientID).DefaultIfEmpty()
                                   from c in db.City.Where(o => o.CityID == a.CityID).DefaultIfEmpty()
                                   from d in db.Province.Where(o => o.ProvinceID == a.ProvinceID).DefaultIfEmpty()
                                   from e in db.AddressType.Where(o => o.ID == a.AddressTypeID).DefaultIfEmpty()
                                   where
                                   a.Active == true &&
                                   a.ID == AddressID
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
                                       TelephoneNo = a.TelephoneNo,
                                       CityID = a.CityID,
                                       ProvinceID = a.ProvinceID,
                                       AddressTypeID = a.AddressTypeID

                                   }).FirstOrDefault();



                return Json(AddressInfo, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetClientInfo(int ClientID)
        {
            using (var db = new ECOCEntities())
            {
                var ClientInfo = (from a in db.Client
                                  from b in db.Title.Where(o => o.ID == a.TitleID).DefaultIfEmpty()
                                  from c in db.TitleType.Where(o => o.ID == b.TitleTypeID).DefaultIfEmpty()
                                  where
                                  a.Active == true &&
                                  a.ID == ClientID
                                  select new
                                  {
                                      ID = a.ID,
                                      corpName = a.CorpName,
                                      firstName = a.FirstName,
                                      lastName = a.LastName,
                                      middleName = a.MiddleName,
                                      titleType = c.Name,
                                      TitleTypeID = c.ID,
                                      email = a.EmailAddress,
                                      bphone = a.BusinessPhone,
                                      mphone = a.MobileNo,
                                      titleID =  a.TitleID,
                                      titleName = b.Name,
                                  }).FirstOrDefault();

                return Json(ClientInfo, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetVehicleInfo(int VehicleID)
        {
            using (var db = new ECOCEntities())
            {
            
                var VehicleInfo = (from a in db.VehicleInfo
                                   from b in db.VehicleMake.Where(o => o.VehicleMakeID == a.MakeID).DefaultIfEmpty()
                                   from c in db.VehicleVariant.Where(o => o.VariantID == a.VariantID).DefaultIfEmpty()
                                   from d in db.VehicleModel.Where(o => o.VehicleModelID == c.VehicleModelID).DefaultIfEmpty()
                                   from e in db.VehicleBodyType.Where(o => o.VehicleBodyTypeID == d.BodyTypeID).DefaultIfEmpty()
                                   from f in db.VehicleColor.Where(o => o.VehicleColorID == a.VehicleColorID).DefaultIfEmpty()
                                   from g in db.VehicleType.Where(o => o.VehicleTypeID == a.VehicleTypeID).DefaultIfEmpty()
                                   from h in db.VehicleClassification.Where(o => o.VehicleClassificationID == a.ClassificationID).DefaultIfEmpty()
                                   where a.Active == true && a.VehicleID == VehicleID
                                   select new
                                   {
                                       VehicleID = a.VehicleID,
                                       Engine = a.EngineNumber,
                                       Chassis = a.ChassisNumber,
                                       MVFile = a.MVFileNumber,
                                       Plate = a.PlateNumber,
                                       Year = a.Year,
                                       Make = b.VehicleMakeName,
                                       BodyType = e.VehicleBodyTypeName,
                                       Color = f.VehicleColorName,
                                       Series = d.VehicleModelName + " " + c.VariantName,
                                       VType = g.VehicleTypeDescription,
                                       Classification = h.VehicleClassificationName,
                                       BodyTypeID = e.VehicleBodyTypeID,
                                       ColorID = f.VehicleColorID,
                                       VariantID = c.VariantID,
                                       VTypeID = g.VehicleTypeID,
                                       ClassificationID = h.VehicleClassificationID,
                                       MakeID = b.VehicleMakeID,
                                   }).FirstOrDefault();

                return Json(VehicleInfo, JsonRequestBehavior.AllowGet);
            }
        }

    }
}