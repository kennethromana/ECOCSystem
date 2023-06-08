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

                //var itemList = db.vwMAIVehicleMake
                //    .Where(o =>
                //    o.MAIID == CurrentUser.Details.SubReferenceID &&
                //    o.Active == true &&
                //    o.VehicleMakeName != null).Select(o => new
                //    {
                //        id = o.VehicleMakeID,
                //        text = o.VehicleMakeName
                //    }).OrderBy(o => o.text).ToList();

                //Search itemList
                //if (!string.IsNullOrWhiteSpace(search))
                //{
                //    itemList = itemList.Where(m => m.text != null && m.text.ToLower().Contains(search.ToLower())).ToList();
                //}

                ////Total size of itemList
                //var itemsTotal = itemList.Count();

                ////check next page itemList
                //itemList = itemList.Skip((page * pageSize) - pageSize).Take(page * pageSize).ToList();


                //}
                //else
                //{

                //    return Json("", JsonRequestBehavior.AllowGet);  
                //}


                return Json("", JsonRequestBehavior.AllowGet);

            }
        }
    }
}