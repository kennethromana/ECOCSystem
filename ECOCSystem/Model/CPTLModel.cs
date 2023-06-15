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
            Client = new ClientModel();
            ClientAddress = new ClientAddressModel();
            Clients = new List<ClientList>();
        }
        [DisplayName("Client")]
        public int ClientID { get; set; }
        public ClientAddressModel ClientAddress { get; set; }
        public ClientModel Client { get; set; }
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
        public List<ClientList> Clients { get; set; }


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
    public class ClientModel
    {
        public ClientModel() 
        {
            Titles = new List<TitleList>();
        }
        public int ClientID { get; set; }
        [DisplayName("Title")]
        [Required]
        public Nullable<int> TitleID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [DisplayName("Corporate Name")]
        public string CorpName { get; set; }
        public string SexCode { get; set; }
        public string CivilStatusCode { get; set; }
        public string Citizenship { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string OrganizationName { get; set; }
        public string OrgarnizationAddress { get; set; }
        public string OrgarnizationTIN { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonNo { get; set; }
        public Nullable<int> CompanyID { get; set; }
        public Nullable<int> BranchID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public List<TitleList> Titles { get; set; }
    }
    public class TitleList
    {
        public int TitleID { get; set; }
        public string Title { get; set; }
    }
    public class ClientAddressModel 
    {
        public ClientAddressModel() 
        {
            AddressTypeList = new List<AddressTypeList>();
        }
        public int ClientAddressID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> AddressTypeID { get; set; }
        public string HouseBldgNo { get; set; }
        public string StreetSubdivision { get; set; }
        public string Barangay { get; set; }
        public string ZipCode { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public Nullable<int> BarangayID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public List<AddressTypeList> AddressTypeList { get; set; }

    }
    public class ClientList
    {
        public int ClientID { get; set; }
        public string ClientInfo { get; set; }
    }
    public class AddressTypeList
    {
        public int ID { get; set; }
        public string AddressTypeInfo { get; set; }
    }
}