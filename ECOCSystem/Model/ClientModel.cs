using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECOCSystem.Model
{
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
        public List<ClientView> ClientViewList { get; set; }
    }
    public class ClientView 
    {

        public int ClientID { get; set; }
        public string Title { get; set; }
        public string Client { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }

    }
}