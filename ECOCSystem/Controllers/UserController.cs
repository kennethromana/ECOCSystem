﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECOCSystem.Model;
using ECOCSystem.Tools;



namespace ECOCSystem.Controllers
{
    public class UserController : Controller
    {
        ECOCEntities db = new ECOCEntities();
        // GET: User
        [AuthorizeUser]
        public ActionResult Index()
        {
            UserModel User = new UserModel();
         
            List<UserList> userlist = new List<UserList>();
            var currentCompany = CurrentUser.Details.CompanyID;

            userlist = (from a in db.Account.Where(a => a.Active == true)
                        from b in db.UserType.Where(b => b.ID == a.UserTypeID && b.Active == true).DefaultIfEmpty()
                        from c in db.Company.Where(c => c.ID == a.CompanyID && b.Active == true).DefaultIfEmpty()
                        from d in db.CompanyBranch.Where(d => d.ID == a.CompanyBranchID && b.Active == true).DefaultIfEmpty()
                        where a.CompanyID == currentCompany
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

            if (CurrentUser.Details != null)
                return RedirectToAction("Dashboard", "Home");
            return View();
        }
        public ActionResult Logout()
        {

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult LoginUser(LoginModel User)
        {
            try
            {
                if (User.Password.Trim() != string.Empty)
                {
                    using (db = new ECOCEntities())
                    {
                        var EncryptedPassword = User.Password.Encrypt(User.Email);
                        //var LoggedUser = db.Account.Where(o => o.Email == User.Email.Trim() && o.Password == EncryptedPassword && o.Active == true).FirstOrDefault();
                        var LoggedUser = (from a in db.Account
                                            from b in db.Company.Where(o => o.ID == a.CompanyID).DefaultIfEmpty()
                                            from c in db.CompanyBranch.Where(o => o.ID == a.CompanyBranchID).DefaultIfEmpty()
                                            where a.Active == true &&
                                            a.Email == User.Email.Trim() &&
                                            a.Password == EncryptedPassword
                                            select new AccountModel
                                            {
                                                ID = a.ID,
                                                Email = a.Email,
                                                FirstName = a.FirstName,
                                                LastName = a.LastName,
                                                MiddleName = a.LastName,
                                                CompanyID = a.CompanyID,
                                                CompanyBranchID = a.CompanyBranchID,
                                                UserTypeID = a.UserTypeID,
                                                CompanyName = b.Name,
                                                BranchName = c.Name,
                                                FullName = a.FirstName + " " + a.LastName + " " +a.MiddleName,
                                                CompanyEntityID = b.CompanyEntityID
                                            }
                                            ).FirstOrDefault();

                        if (LoggedUser != null)
                        {
                            CurrentUser.Details = LoggedUser;
                            if (Session["RedirectFromLogin"] != null)
                            {
                                string RedirectURL = Session["RedirectFromLogin"].ToString();
                                Session.Remove("RedirectFromLogin");
                                return new RedirectResult(RedirectURL);


                            }
                            else
                                return RedirectToAction("Dashboard", "Home");

                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Username and Password does not match.";
                        }
                    }
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Login Failed.";
                return RedirectToAction("Login");
            }
            return RedirectToAction("login");


        }
        public new ActionResult User(UserModel model, string submit)
        {
            var currentCompany = CurrentUser.Details.CompanyID;
            var currentBranch = CurrentUser.Details.CompanyBranchID;
            try
            {

                switch (submit) 
                {
                    case "ADDUSER":
                        {
                            var newAccount = new Account();

                            var validateUser = db.Account.Where(o => o.Email == model.Email.Trim()).FirstOrDefault();

                            if (validateUser == null)
                            {
                                newAccount.FirstName = model.FirstName.Trim();
                                newAccount.LastName = model.LastName.Trim();
                                newAccount.MiddleName = model.MiddleName == null ? "" : model.MiddleName.Trim();
                                newAccount.Email = model.Email.Trim();
                                newAccount.Password = model.Password.Encrypt(model.Email);
                                newAccount.CompanyID = currentCompany;
                                newAccount.CompanyBranchID = model.SelectedCompanyBranchID;

                                //check if current user is databridge company then create user with super admin user type
                                if (CurrentUser.Details.CompanyEntityID == (int)CompanyEntityEnum.DatabridgeAsia) 
                                    newAccount.UserTypeID = (int)UserTypeEnum.SuperAdmin;
                                else
                                    newAccount.UserTypeID = model.SelectedUserTypeID;


                                newAccount.Active = true;
                                newAccount.CreatedBy = CurrentUser.Details.ID;
                                newAccount.CreatedDate = DateTime.Now;


                                db.Account.Add(newAccount);
                                db.SaveChanges();

                                TempData["SuccessMessage"] = "New User added Succesfully!";
                            }
                            else
                                TempData["InfoMessage"] = "User Email already registered!";

                        }
                        break;
                    case "EDITUSER":
                        {
                            var updateUser = db.Account.Where(o => o.ID == model.UserID).FirstOrDefault();

                                updateUser.FirstName = model.FirstName.Trim();
                                updateUser.LastName = model.LastName.Trim();
                                updateUser.MiddleName = model.MiddleName == null ? "" : model.MiddleName.Trim();
                                updateUser.Email = model.Email.Trim();
                                updateUser.Password = model.Password.Encrypt(model.Email);
                                updateUser.CompanyID = currentCompany;


                            //check if current user is databridge company then create user with super admin user type
                            if (CurrentUser.Details.CompanyEntityID == (int)CompanyEntityEnum.DatabridgeAsia)
                            {
                                updateUser.UserTypeID = (int)UserTypeEnum.SuperAdmin;
                                //set the mainbranch of databridge which is 2
                                updateUser.CompanyBranchID = 2; 
                            }
                            else 
                            {
                                updateUser.UserTypeID = model.SelectedUserTypeID;
                                updateUser.CompanyBranchID = model.SelectedCompanyBranchID;
                            }
                                  


                                updateUser.Active = true;
                                updateUser.UpdatedBy = CurrentUser.Details.ID;
                                updateUser.UpdatedDate = DateTime.Now;

                                db.SaveChanges();

                                TempData["SuccessMessage"] = "User updated Succesfully!";


                        }
                        break;
                    default:
                        TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";
                        //Status = "Warning";
                        //Message = "Message: ErrorThere's something error. Please try again later.";
                        break;

                }
            }
            catch (Exception)
            {
                TempData["InfoMessage"] = "Message: ErrorThere's something error. Please try again later";

            }
            return RedirectToAction("Index");


        }

        public JsonResult GetUserInfo(int UserID)
        {
            using (var db = new ECOCEntities())
            {
                var UserInfo = (from a in db.Account
                                from b in db.UserType.Where(o => o.ID == a.UserTypeID).DefaultIfEmpty()
                                from c in db.CompanyBranch.Where(o => o.ID == a.CompanyBranchID).DefaultIfEmpty()
                                where
                                a.Active == true &&
                                a.ID == UserID
                                select new
                                {
                                    UserID = a.ID,
                                    Fname = a.FirstName,
                                    Lname = a.LastName,
                                    Mname = a.MiddleName,
                                    Email = a.Email,
                                    BranchID = c.ID,
                                    Branch = c.Name,
                                    UserTypeID = b.ID,
                                    UserType = b.Name,
                                }
                                ).FirstOrDefault();
                return Json(UserInfo, JsonRequestBehavior.AllowGet);

            }
            
        }
    }
   
}