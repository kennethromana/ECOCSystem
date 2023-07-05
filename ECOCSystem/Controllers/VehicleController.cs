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
        public ActionResult GetMakeSeriesList(int MakeID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {


                    var tableData = db.VehicleSeries.Where(o => o.Active && o.VehicleMakeID == MakeID).ToList();


                    //Returning Json Data    
                    return Json(new { data = tableData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetSeriesList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {


                    var tableData = (from a in db.VehicleSeries
                                     where a.Active == true
                                     select new Series
                                     {
                                         SeriesID = a.VehicleSeriesID,
                                         Model = a.VehicleModelName,
                                         Variant = a.Variant,
                                         BodyType = a.Variant
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