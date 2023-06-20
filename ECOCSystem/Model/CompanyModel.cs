using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECOCSystem.Model
{
    public class CompanyModel
    {
        public CompanyModel()
        {
            CompanyListView = new List<Company>();
        }
        public int CompanyID { get; set; }
        [Required]
        [DisplayName("Company Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Business Phone")]
        public string BusinessPhone { get; set; }
        [DisplayName("Mobile Phone")]
        public string MobilePhone { get; set; }
        [DisplayName("Fax Number")]
        public string FaxNumber { get; set; }
        [DisplayName("TIN")]
        public string TIN { get; set; }
        public string Website { get; set; }
        public string ZipCode { get; set; }

        public List<Company> CompanyListView { get; set; }
    }
    public class BranchModel 
    {
        public BranchModel() 
        {
            BranchList = new List<BranchList>();
            CompanyList = new List<DropdownModel>();
        }
        [Required]
        [DisplayName("Company")]
        public int CompanyID { get; set; }
        public int BranchID { get; set; }
        [Required]
        [DisplayName("Branch Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Address")]
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string BusinessPhone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public string TIN { get; set; }
        public string Website { get; set; }
        public string ZipCode { get; set; }
        [Required]
        [DisplayName("Accreditation Number")]
        public string AccreditationNumber { get; set; }
        public List<BranchList> BranchList { get; set; }
        public List<DropdownModel> CompanyList { get; set; }

    }
    public class BranchList 
    {
      
        public int BranchID { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string BusinessPhone { get; set; }
        public string MobilePhone { get; set; }
        public string FaxNumber { get; set; }
        public string TIN { get; set; }
        public string Website { get; set; }
        public string ZipCode { get; set; }
        public string AccreditationNumber { get; set; }
    }
   
}