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
            UserModel User = new UserModel();

            List<UserList> userlist = new List<UserList>();

            userlist = (from a in db.Account.Where(a => a.Active == true)
                        from b in db.UserType.Where(b => b.ID == a.UserTypeID && b.Active == true).DefaultIfEmpty()
                        from c in db.Company.Where(c => c.ID == a.CompanyID && b.Active == true).DefaultIfEmpty()
                        from d in db.CompanyBranch.Where(d => d.ID == a.CompanyBranchID && b.Active == true).DefaultIfEmpty()
                        select new UserList
                        {
                            UserID = a.ID,
                            FirstName = a.FirstName,
                            MiddleName = a.MiddleName,
                            LastName = a.LastName,
                            Email = a.Email,
                            UserTypeID = a.UserTypeID,
                            UserType = b.Name,
                            Company = c.Name,
                            CompanyBranch = d.Name
                        }
                        ).ToList();
            User.UserList = userlist;

            return View(User);
        }

        public ActionResult Login()
        {
            var acc = db.Account.Where(o => o.Active == true).ToList();
            var acc2 = db.UserType.Where(o => o.Active == true).ToList();
            return View();
        }
    }
}