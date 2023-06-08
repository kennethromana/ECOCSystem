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
                                where a.Active == true
                                select new
                                {
                                    id = a.ID,
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
    }
}