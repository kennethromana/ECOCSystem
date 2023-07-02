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
        [AuthorizeUser]
        public ActionResult Index()
        {
            var model = new CompanyModel();
            //select all insurance comapny exclude databridge asia which has ID = 1
            model.CompanyListView = db.Company.Where(o => o.Active == true && o.ID != 1).ToList();

            return View(model);
        }
        public ActionResult Company()
        {
            var model = new CompanyModel();

            model.CompanyListView = db.Company.Where(o => o.Active == true).ToList();

            return View(model);
        }
        public ActionResult CompanyForm(CompanyModel model, string submitType)
        {

            int currentCompany = (int)CurrentUser.Details.CompanyID;
            int currentBranch = (int)CurrentUser.Details.CompanyBranchID;
            var Status = "Error";
            var PartialViewDataString = "";
            var Message = "";
            var currentForm = "";
            int CompanyID = model.SelectedCompanyID;



            int Individual = Convert.ToInt32(TitleTypeEnum.Individual);
            int Corporate = Convert.ToInt32(TitleTypeEnum.Corporate);
            int CorporateWithAssignee = Convert.ToInt32(TitleTypeEnum.CorporateWithAssignee);

            using (var db = new ECOCEntities())
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    switch (submitType)
                    {
                        case "ADDCOMPANY":
                            {
                                var newCompany = new Company();
                                newCompany.Name = model.CompanyInfo.Name.Trim();
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
                                newBranch.Address = model.CompanyInfo.Address;

                                newBranch.Active = true;
                                newBranch.CreatedBy = CurrentUser.Details.ID;
                                newBranch.CreatedDate = DateTime.Now;

                                db.CompanyBranch.Add(newBranch);


                                db.SaveChanges();
                                dbTransaction.Commit();

                                currentForm = "Company";
                                Status = "Success";
                                Message = "New Company was created successfully!";
                                CompanyID = newCompany.ID;

                            }
                            break;
                        case "EDITCOMPANY":
                            {
                                var updateCompany = db.Company.Where(o => o.ID == CompanyID).FirstOrDefault();

                                updateCompany.Name = model.CompanyInfo.Name.Trim();
                                updateCompany.Address = model.CompanyInfo.Address;
                                updateCompany.EmailAddress = model.CompanyInfo.EmailAddress;
                                updateCompany.BusinessPhone = model.CompanyInfo.BusinessPhone;
                                updateCompany.MobilePhone = model.CompanyInfo.MobilePhone;
                                updateCompany.FaxNumber = model.CompanyInfo.FaxNumber;
                                updateCompany.TIN = model.CompanyInfo.TIN;

                                updateCompany.UpdatedBy = CurrentUser.Details.ID;
                                updateCompany.UpdatedDate = DateTime.Now;

                                db.SaveChanges();
                                dbTransaction.Commit();

                                currentForm = "Company";
                                Status = "Success";
                                Message = "Company was updated successfully!";

                            }
                            break;
                        case "ADDBRANCH":
                            {
                                var newBranch = new CompanyBranch();

                                newBranch.CompanyID = CompanyID;
                                newBranch.Name = model.CompanyBranch.Name;
                                newBranch.Address = model.CompanyBranch.Address;
                                newBranch.EmailAddress = model.CompanyBranch.EmailAddress;
                                newBranch.BusinessPhone = model.CompanyBranch.BusinessPhone;
                                newBranch.MobilePhone = model.CompanyBranch.MobilePhone;
                                newBranch.FaxNumber = model.CompanyBranch.FaxNumber;
                                newBranch.AccreditationNumber = model.CompanyBranch.AccreditationNumber;
                                newBranch.TIN = model.CompanyBranch.TIN;

                                newBranch.Active = true;
                                newBranch.CreatedBy = CurrentUser.Details.ID;
                                newBranch.CreatedDate = DateTime.Now;

                                db.CompanyBranch.Add(newBranch);
                                db.SaveChanges();
                                dbTransaction.Commit();

                                currentForm = "Branch";
                                Status = "Success";
                                Message = "New Branch was created successfully!";


                            }
                            break;
                        case "ADDUSER":
                            {

                                var checkifExisting = db.Account.Where(o => o.Email == model.CompanyUser.Email.Trim() && o.Active == true).FirstOrDefault();

                                if (checkifExisting == null)
                                {
                                    var newAccount = new Account();

                                    newAccount.FirstName = model.CompanyUser.FirstName.Trim();
                                    newAccount.LastName = model.CompanyUser.LastName.Trim();
                                    newAccount.MiddleName = model.CompanyUser.MiddleName == null ? "" : model.CompanyUser.MiddleName.Trim();
                                    newAccount.Email = model.CompanyUser.Email.Trim();
                                    newAccount.Password = model.CompanyUser.Password.Encrypt(model.CompanyUser.Email);
                                    newAccount.UserTypeID = model.CompanyUser.SelectedUserTypeID;
                                    newAccount.CompanyID = CompanyID;
                                    newAccount.CompanyBranchID = model.CompanyUser.SelectedCompanyBranchID;

                                    newAccount.Active = true;
                                    newAccount.CreatedBy = CurrentUser.Details.ID;
                                    newAccount.CreatedDate = DateTime.Now;


                                    db.Account.Add(newAccount);
                                    db.SaveChanges();
                                    dbTransaction.Commit();

                                    currentForm = "User";
                                    Status = "Success";
                                    Message = "New User was created successfully!";

                                }
                                else 
                                {
                                    currentForm = "User";
                                    Status = "Info";
                                    Message = "Email provided already registered.";
                                }

                            }
                            break;
                        default:
                            Status = "Error";
      
                            Message = "There's something error. Please try again later.";

                            break;
                    }
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    Status = "Error";
                    Message = "Error. Please contact Databridge support to assist you.";

                }

            }

            var jsonResult = Json(new { Status, Message, Data = PartialViewDataString , currentForm ,CompanyID});
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

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
        [HttpPost]
        public ActionResult GetCompanyBatchList(int CompanyID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {
                   
                    var tableData = db.CompanyBranch.Where(o => o.CompanyID == CompanyID).ToList();

                    //Returning Json Data    
                    return Json(new { data = tableData });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult GetCompanyList()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    //select all insurance comapny exclude databridge asia which has ID = 1
                    var tableData = db.Company.Where(o => o.Active == true && o.ID != 1).ToList();


                    //Returning Json Data    
                    return Json(new { data = tableData }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ActionResult GetCompanyUserList(int CompanyID,int BranchID)
        {
            try
            {
                //Creating instance of DatabaseContext class  
                using (var db = new ECOCEntities())
                {

                    var tableData = (from a in db.Account
                                     from b in db.UserType.Where(o => o.ID == a.UserTypeID).DefaultIfEmpty()
                                     from c in db.CompanyBranch.Where(o => o.ID == a.CompanyBranchID).DefaultIfEmpty()
                                     where a.Active == true &&
                                     a.CompanyID == CompanyID &&
                                     a.CompanyBranchID == BranchID
                                     select new
                                     {
                                         ID = a.ID,
                                         Email = a.Email,
                                         Fname = a.FirstName,
                                         Lname = a.LastName,
                                         Mname = a.MiddleName,
                                         UserType = b.Name,
                                         Branch = c.Name
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

        public JsonResult GetCompanyInfo(int CompanyID)
        {
            using (var db = new ECOCEntities())
            {
                var CompanyInfo = db.Company.Where(o => o.ID == CompanyID).FirstOrDefault();

                return Json(CompanyInfo, JsonRequestBehavior.AllowGet);
            }
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