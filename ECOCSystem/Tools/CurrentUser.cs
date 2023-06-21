using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECOCSystem.Model;

namespace ECOCSystem.Tools
{
    public class CurrentUser
    {
        public static AccountModel Details
        {
            get
            {
                return (AccountModel)HttpContext.Current.Session["VRCurrentUser"];
            }
            set
            {
                HttpContext.Current.Session["VRCurrentUser"] = value;
            }
        }

    }
}