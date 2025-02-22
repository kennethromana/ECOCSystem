﻿using System;
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
            RegistrationTypes = new List<RegistrationTypeList>();
            ClientVehicle = new MVModel();
        }
        [DisplayName("Client")]
        public int ClientID { get; set; }
        public int AddressID { get; set; }
        public int VehicleID { get; set; }
        public int SelectedRegistrationTypeID { get; set; }
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

        public List<RegistrationTypeList> RegistrationTypes { get; set; }
        public MVModel ClientVehicle { get; set; }


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
            ProvinceList = new List<ProvinceList>();
            CityList = new List<CityList>();
        }
        public int ClientAddressID { get; set; }
        public Nullable<int> ClientID { get; set; }
        [Required]
        [DisplayName("Address Type")]
        public Nullable<int> AddressTypeID { get; set; }
        [Required]
        [DisplayName("House Bldg No.")]
        public string HouseBldgNo { get; set; }
        [Required]
        [DisplayName("Street Subdivision")]
        public string StreetSubdivision { get; set; }
        [Required]
        [DisplayName("Barangay")]
        public string Barangay { get; set; }
        [Required]
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [DisplayName("Telephone No.")]
        public string TelephoneNo { get; set; }
        [DisplayName("Mobile No.")]
        public string MobileNo { get; set; }
        public Nullable<int> BarangayID { get; set; }
        [Required]
        [DisplayName("City")]
        public Nullable<int> CityID { get; set; }
        [Required]
        [DisplayName("Province")]
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public List<AddressTypeList> AddressTypeList { get; set; }
        public List<CityList> CityList { get; set; }
        public List<ProvinceList> ProvinceList { get; set; }

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
    public class RegistrationTypeList
    {
        public int ID { get; set; }
        public string RegistrationTypeInfo { get; set; }
    }
    public class CityList
    {
        public int ID { get; set; }
        public string CityInfo { get; set; }
    }
    public class ProvinceList
    {
        public int ID { get; set; }
        public string ProvinceInfo { get; set; }
    }
}