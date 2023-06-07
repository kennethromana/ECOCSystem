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
        public CPTLModel() 
        {
            ApplicationSummary = new ApplicationSummary();
        }
        public ApplicationSummary ApplicationSummary { get; set; }
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
        [DisplayName("New Registration or Renewal")]
        public bool isRenewal { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }


    }
    public class ApplicationSummary
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string CorporateName { get; set; }
        public string Street { get; set; }
        public string Barangay { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleBody { get; set; }
        public string VehicleType { get; set; }
        public string Renewal { get; set; }
        public string NewRegistration { get; set; }
        public string AddressType { get; set; }
        public string ClientType { get; set; }
      
    }
}