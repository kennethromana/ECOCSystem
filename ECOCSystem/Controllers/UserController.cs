using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;

namespace ECOCSystem.Controllers
{
    public class UserController : Controller
    {   ECOCEntities db = new ECOCEntities();
        // GET: User
        public ActionResult Index()
        {
            //List<Account> Accounts = new List<Account>();

            var Accounts = db.Account.Where(o => o.Active == true).ToList();
            return View(Accounts);
        }

        public ActionResult Login()
        {
            var acc = db.Account.Where(o => o.Active == true).ToList();
            return View();
        }
    }
}