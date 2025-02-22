﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOCSystem.Model
{
    public class vwUserListModel
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Nullable<int> UserTypeID { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> CompanyBranchID { get; set; }
        public string UserType { get; set; }
    }

}