using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECOCSystem.Model
{
    public class UserModel
    {
        public UserModel()
        {
            UserList = new List<UserList>();
            UserTypes = new List<UserTypeList>();
            CompanyList = new List<CompanyList>();
            CompanyBranchList = new List<CompanyBranchList>();
        }
        [Required]
        [DisplayName("User Type")]
        public int SelectedUserType { get; set; }
        [Required]
        [DisplayName("Company")]
        public int SelectedCompanyID { get; set; }
        [Required]
        [DisplayName("Branch")]
        public int SelectedCompanyBranchID { get; set; }
        public List<UserList> UserList { get; set; }
        public int UserID { get; set; }
        [Required]
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<UserTypeList> UserTypes { get; set; }
        public List<CompanyList> CompanyList { get; set; }
        public List<CompanyBranchList> CompanyBranchList { get; set; }

    }

    public class UserList
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string Company { get; set; }
        public string CompanyBranch { get; set; }

        public Nullable<int> UserTypeID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> CompanyBranchID { get; set; }
        public string UserType { get; set; }

    }
    public class UserTypeList
    {
        public int ID { get; set; }
        public string Info { get; set; }

    }
    public class CompanyList
    {
        public int ID { get; set; }
        public string Info { get; set; }

    }
    public class CompanyBranchList
    {
        public int ID { get; set; }
        public string Info { get; set; }

    }
}