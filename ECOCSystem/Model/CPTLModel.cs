using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECOCSystem.Model
{
    public class CPTLModel
    {
        public int CPTLID { get; set; }
        public string COCNumber { get; set; }
        public string PolicyNumber { get; set; }
        public Nullable<System.DateTime> EffectivityDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public bool isAuthenticated { get; set; }
        public string AuthenticatedNumber { get; set; }
        public Nullable<int> AuthenticatedDate { get; set; }
        public string AgentCode { get; set; }
        public Nullable<System.DateTime> SubmittedDate { get; set; }
        public string PolicyStatus { get; set; }
        public string CRName { get; set; }
        public string CRContentType { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> CompanyBranchID { get; set; }
        [DisplayName("Registration Type: New or Renewal")]
        public bool isRenewal { get; set; }

    }
}