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
            var model = new CompanyModel();

            model.CompanyListView = db.Company.Where(o => o.Active == true).ToList();

            return View(model);
        }
        public ActionResult Company()
        {
            var model = new CompanyModel();

            model.CompanyListView = db.Company.Where(o => o.Active == true).ToList();

            return View(model);
        }
        public ActionResult CompanyForm(CompanyModel model,string submit)
        {
            using (var db = new ECOCEntities())
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    switch (submit)
                    {
                        case "ADDCOMPANY":
                            {
                                var newCompany = new Company();
                                newCompany.Name = model.CompanyInfo.Name;
                                newCompany.Address = model.CompanyInfo.Address;
                                newCompany.EmailAddress = model.CompanyInfo.EmailAddress;
                                newCompany.BusinessPhone = model.CompanyInfo.BusinessPhone;
                                newCompany.MobilePhone = model.CompanyInfo.MobilePhone;
                                newCompany.FaxNumber = model.CompanyInfo.FaxNumber;
                                newCompany.TIN = model.CompanyInfo.TIN;

                                newCompany.Active = true;
                                newCompany.CreatedBy = CurrentUser.Details.ID;
                                newCompany.CreatedDate = DateTime.Now;

                                db.Company.Add(newCompany);
                                db.SaveChanges();

                                var newBranch = new CompanyBranch();
                                newBranch.CompanyID = newCompany.ID;
                                newBranch.Name = "Main Branch";
                                newBranch.Address = model.Address;            

                                newBranch.Active = true;
                                newBranch.CreatedBy = CurrentUser.Details.ID;
                                newBranch.CreatedDate = DateTime.Now;

                                db.CompanyBranch.Add(newBranch);


                                db.SaveChanges();
                                dbTransaction.Commit();

                                TempData["SuccessMessage"] = "New Company was created Succesfully!";

                            }
                            break;
                        default:
                            TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";

                            break;
                    }
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";

                }

            }

            return RedirectToAction("Index");

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

        public ActionResult BranchInfo(BranchModel model)
        {
            var newBranch = new CompanyBranch();

            newBranch.CompanyID = model.CompanyID;
            newBranch.Name = model.Name;
            newBranch.Address = model.Address;
            newBranch.EmailAddress = model.EmailAddress;
            newBranch.BusinessPhone = model.BusinessPhone;
            newBranch.MobilePhone = model.MobilePhone;
            newBranch.FaxNumber = model.FaxNumber;
            newBranch.AccreditationNumber = model.AccreditationNumber;
            newBranch.TIN = model.TIN;

            newBranch.Active = true;
            newBranch.CreatedBy = CurrentUser.Details.ID;
            newBranch.CreatedDate = DateTime.Now;

            db.CompanyBranch.Add(newBranch);
            db.SaveChanges();

            TempData["SuccessMessage"] = "New Company Branch added Succesfully!";


            return RedirectToAction("Branch");
        }
    }
}