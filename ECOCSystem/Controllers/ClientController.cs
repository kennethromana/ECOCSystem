using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;
using ECOCSystem.Tools;

namespace ECOCSystem.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        [AuthorizeUser]
        public ActionResult Index()
        {
            var clientModel = new ClientModel();

            int Individual = 1;
            int Corporate = 2;
            int CorporateWithAssignee = 3;

            using (var db = new ECOCEntities())
            {
               
                clientModel.ClientViewList = (from a in db.Client
                                              from b in db.Title.Where(o => o.ID == a.TitleID).DefaultIfEmpty()
                                              from c in db.Company.Where(o => o.ID == a.CompanyID).DefaultIfEmpty()
                                              from d in db.CompanyBranch.Where(o => o.ID == a.BranchID).DefaultIfEmpty()
                                              from e in db.TitleType.Where(o => o.ID == b.TitleTypeID).DefaultIfEmpty()
                                              where
                                              a.Active == true
                                                select new ClientView
                                                {
                                                    ClientID = a.ID,
                                                    Title = b.Name,
                                                    Client = e.ID == Individual ? a.FirstName + " " + a.LastName + " " + a.MiddleName 
                                                            : e.ID == Corporate ? a.CorpName 
                                                            : e.ID == CorporateWithAssignee ? a.FirstName + " " + a.LastName + " " + a.MiddleName
                                                            : "-",
                                                    Company = c.Name,
                                                    Branch = d.Name
                                                }).ToList();


            }

            return View(clientModel);
        }
        public ActionResult ClientRegistrationForm()
        {
            return View();
        }
    }
}