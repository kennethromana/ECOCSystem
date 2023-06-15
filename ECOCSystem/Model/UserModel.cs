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
        }
        public List<UserList> UserList { get; set; }

        public int UserID { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

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
}