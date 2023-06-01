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
            List<vwUserListModel> userlist = new List<vwUserListModel>();

            userlist = (from a in db.Account.Where(a => a.Active == true)
                        from b in db.UserType.Where(b => b.ID == a.UserTypeID && b.Active == true).DefaultIfEmpty()
                        select new vwUserListModel
                        {
                            UserID = a.ID,
                            FirstName = a.FirstName,
                            MiddleName = a.MiddleName,
                            LastName = a.LastName,
                            Email = a.Email,
                            UserTypeID = a.UserTypeID,
                            UserType = b.Name
                        }
                        ).ToList();

            return View(userlist);
        }

        public ActionResult Login()
        {
            var acc = db.Account.Where(o => o.Active == true).ToList();
            var acc2 = db.UserType.Where(o => o.Active == true).ToList();
            return View();
        }
    }
}