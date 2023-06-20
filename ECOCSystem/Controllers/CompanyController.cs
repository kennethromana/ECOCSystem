using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;
using ECOCSystem.Tools;

namespace ECOCSystem.Controllers
{
    public class CompanyController : Controller
    {
        ECOCEntities db = new ECOCEntities();
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Company()
        {
            var model = new CompanyModel();

            model.CompanyListView = db.Company.Where(o => o.Active == true).ToList();

            return View(model);
        }
        public ActionResult Branch()
        {
            var model = new BranchModel();

            model.BranchList = (from a in db.CompanyBranch
                                from b in db.Company.Where(o => o.ID == a.CompanyID)
                                where a.Active == true
                                select new BranchList
                                {
                                    BranchID = a.ID,
                                    CompanyName = b.Name,
                                    BranchName = a.Name,
                                    Address = a.Address,
                                    EmailAddress = a.EmailAddress,
                                    BusinessPhone = a.BusinessPhone,
                                    MobilePhone = a.MobilePhone,
                                    FaxNumber = a.FaxNumber,
                                    AccreditationNumber = a.AccreditationNumber,
                                    TIN = a.TIN,
                                }
                                ).ToList();

            return View(model);
        }
        public ActionResult CompanyInfo(Company model)
        {
            var newCompany = new Company();
            newCompany.Name = model.Name;
            newCompany.Address = model.Address;
            newCompany.EmailAddress = model.EmailAddress;
            newCompany.BusinessPhone = model.BusinessPhone;
            newCompany.MobilePhone = model.MobilePhone;
            newCompany.FaxNumber = model.FaxNumber;
            newCompany.TIN = model.TIN;

            newCompany.Active = true;
            newCompany.CreatedBy = CurrentUser.Details.ID;
            newCompany.CreatedDate = DateTime.Now;

            db.Company.Add(newCompany);
            db.SaveChanges();

            TempData["SuccessMessage"] = "New Company added Succesfully!";


            return RedirectToAction("Company");
        }
    }
}