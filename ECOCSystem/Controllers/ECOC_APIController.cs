using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;
using ECOCSystem.Tools;

namespace ECOCSystem.Controllers
{
    public class ECOC_APIController : Controller
    {
        ECOCEntities db = new ECOCEntities();

        HttpClient ECOC_Client = new HttpClient();
        //Uri URI;
        UriBuilder builder = new UriBuilder();
        NameValueCollection query = new NameValueCollection();
        public ActionResult Index()
        {
            return View();
        }
    }
}