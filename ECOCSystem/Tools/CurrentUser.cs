using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOCSystem.Model;

namespace ECOCSystem.Tools
{
    public class CurrentUser
    {
        public static Account Details
        {
            get
            {
                return (Account)HttpContext.Current.Session["VRCurrentUser"];
            }
            set
            {
                HttpContext.Current.Session["VRCurrentUser"] = value;
            }
        }

    }
}