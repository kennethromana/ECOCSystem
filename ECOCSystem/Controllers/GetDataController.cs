using System;
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
                                    id =  b.ID,
                                    text = a.Name
                                }).ToList();
                                      

                //var itemList = (from a in db.Customer
                //                join b in db.Title on a.TitleID equals b.TitleID into temp
                //                where
                //                a.Active == true &&
                //                a.DealerID == CurrentUser.Details.ReferenceID
                //                from temptbl in temp.DefaultIfEmpty()
                //                select new
                //                {
                //                    id = a.CustomerID,
                //                    text = temptbl.TitleTypeID == 1 ? a.LastName + ", " + a.FirstName + " " + a.MiddleName : a.CorpName,
                //                    a.CreatedDate
                //                }).OrderByDescending(o => o.CreatedDate).ToList();

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
                //int skip = page > 1 ? Convert.ToInt32(start) + 5 : 0;
                //int recordsTotal = 0;
                //bool more = true;
                int Individual = 1;
                int Corporate = 2;
                int CorporateWithAssignee = 3;

                var itemList = (from a in db.Client
                                from b in db.Title.Where(o => o.ID == a.TitleID).DefaultIfEmpty()
                                from c in db.TitleType.Where(o => o.ID == b.ID).DefaultIfEmpty()
                                where a.Active == true
                                select new
                                {
                                    id = a.ID,
                                    text = c.ID == Individual ? a.FirstName+" "+a.LastName+" "+a.MiddleName+" - "+c.Name 
                                          :c.ID == Corporate ? a.CorpName+" "+c.Name 
                                          :c.ID == CorporateWithAssignee ? a.FirstName + " " + a.LastName + " " + a.MiddleName +" " +a.CorpName+ " - " + c.Name 
                                          : "-"
                                }).ToList();


                //var itemList = (from a in db.Customer
                //                join b in db.Title on a.TitleID equals b.TitleID into temp
                //                where
                //                a.Active == true &&
                //                a.DealerID == CurrentUser.Details.ReferenceID
                //                from temptbl in temp.DefaultIfEmpty()
                //                select new
                //                {
                //                    id = a.CustomerID,
                //                    text = temptbl.TitleTypeID == 1 ? a.LastName + ", " + a.FirstName + " " + a.MiddleName : a.CorpName,
                //                    a.CreatedDate
                //                }).OrderByDescending(o => o.CreatedDate).ToList();

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
    }
}