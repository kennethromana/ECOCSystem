//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ECOCSystem.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientAddress
    {
        public int ID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> AddressTypeID { get; set; }
        public string HouseBldgNo { get; set; }
        public string StreetSubdivision { get; set; }
        public string Barangay { get; set; }
        public string ZipCode { get; set; }
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
        public string EmailAddress { get; set; }
    }
}
