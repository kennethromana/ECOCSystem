using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOCSystem.Model
{
    public class COCReturnModel
    {
    }
    #region [Paramount]
    public class ParamountCOCInsertReturn
    {
        public bool InsertSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string PolicyNumber { get; set; }
        public string StatusCode { get; set; }
        public string ResponseData { get; set; }
        public string StatusDescription { get; set; }
        public string Exception { get; set; }
    }
    public class ParamountQRCodeReturn
    {
        public bool InsertSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string PolicyNumber { get; set; }
        public string StatusCode { get; set; }
        public byte[] ResponseData { get; set; }
        public string StatusDescription { get; set; }
        public string Exception { get; set; }
    }
    #endregion [Paramount]

    #region [Goldplan]
    public class GPIS_RequiredReturn
    {
        public int code { get; set; }
        public string description { get; set; }
    }
    public class GPIS_InsertReturn
    {
        public GPIS_InsertReturn()
        {
            AssuredReturn = new GPIS_AssuredReturn();
            VehicleReturn = new GPIS_VehicleReturn();
        }
        public GPIS_AssuredReturn AssuredReturn { get; set; }
        public GPIS_VehicleReturn VehicleReturn { get; set; }
        public class GPIS_AssuredReturn
        {
            public string transaction { get; set; }
            public string message { get; set; }
            public string assured_code { get; set; }
            public string ssured_name { get; set; }
        }
        public class GPIS_VehicleReturn
        {
            public string transaction { get; set; }
            public string message { get; set; }
            public string vehicle_code { get; set; }
            public string owner_name { get; set; }
        }
    }
    public class GPIS_Vehicle_RequiredModel
    {
        public string OWNERS_NAME { get; set; }
        public int MAKE { get; set; }
        public int YEAR { get; set; }
        public int MODEL { get; set; }
        public int SERIES { get; set; }
        public string COLOR { get; set; }
        public string FUEL { get; set; }
        public string CHASSIS_NO { get; set; }
        public string MOTOR_NO { get; set; }
        public int BODY_TYPE { get; set; }
        public string PLATE_NO { get; set; }
        public string CONDUCTION_NO { get; set; }
        public string MV_NO { get; set; }
        public int DENOMINATION { get; set; }
        public string MORTGAGEE { get; set; }
        public decimal FMV { get; set; }
        public string UNLADEN_WEIGHT { get; set; }
        public string SEATS { get; set; }
        public int VEHICLE_PREMIUM_CLASS { get; set; }
        public string STORAGE_LOCATION { get; set; }
    }
    public class GPIS_Assured_RequiredModel
    {
        public string ASSURED_NAME { get; set; }
        public string SUFFIX { get; set; }
        public string ASSURED_NAME_2 { get; set; }
        public string TIN { get; set; }
        public int? TITLE { get; set; }
        public string PRI_HOUSE_UNIT { get; set; }
        public string PRI_STREET_NAME { get; set; }
        public string PRI_REGION { get; set; }
        public string PRI_PROVINCE { get; set; }
        public string PRI_MUNICIPALITY { get; set; }
        public string PRI_BARANGAY { get; set; }
        public int? PRI_POSTAL_CODE { get; set; }
        public string DEL_HOUSE_UNIT { get; set; }
        public string DEL_STREET_NAME { get; set; }
        public string DEL_REGION { get; set; }
        public string DEL_PROVINCE { get; set; }
        public string DEL_MUNICIPALITY { get; set; }
        public string DEL_BARANGAY { get; set; }
        public string DEL_POSTAL_CODE { get; set; }
        public long COM_MOBILE_NO_1 { get; set; }
        public int? COM_MOBILE_NO_2 { get; set; }
        public string COM_HOME_NO { get; set; }
        public string COM_OFFICE_NO { get; set; }
        public string COM_EMAIL { get; set; }
        public string CCD_CON_NAME { get; set; }
        public int? CCD_CON_NO { get; set; }
        public string CCD_CON_EMAIL { get; set; }
        public string PAYMENT_TERMS { get; set; }
        public string STORAGE_LOCATION { get; set; }
    }
    #endregion [Goldplan]

    #region [Malayan]
    public class Malayan_InsertReturn
    {
        public string Invoiceno { get; set; }
        public string Transactiondate { get; set; }
        public string Authno { get; set; }
        public string Cocno { get; set; }
        public string Policyno { get; set; }
        public string Inception { get; set; }
        public string Expiry { get; set; }
        public string Purepremium { get; set; }
        public string PremiumDue { get; set; }
        public string Coc { get; set; }
        public string Errorcode { get; set; }
        public string Errormessage { get; set; }
    }
    public class Malayan_Parameter_Model
    {
        //REQUIRED
        public string username { get; set; }
        //REQUIRED
        public string password { get; set; }
        //OPTIONAL
        public string regtype { get; set; }
        //OPTIONAL (for Reg) - REQUIRED (for Renewal)
        public string plateno { get; set; }
        //OPTIONAL (for Reg) - REQUIRED (for Renewal)
        public string mvfileno { get; set; }
        //REQUIRED
        public string engineno { get; set; }
        //REQUIRED
        public string chassisno { get; set; }
        //REQUIRED
        public DateTime inception { get; set; }
        //REQUIRED
        public DateTime expiry { get; set; }
        //OPTIONAL
        public string givenname { get; set; }
        //OPTIONAL
        public string middlename { get; set; }
        //REQUIRED
        public string surname { get; set; }
        //REQUIRED
        public string assuredaddress { get; set; }
        //OPTIONAL
        public string emailaddress { get; set; }
        //OPTIONAL
        public string mobileno { get; set; }
        //REQUIRED
        public int yearmodel { get; set; }
        //REQUIRED
        public string vehiclemake { get; set; }
        //REQUIRED
        public string vehiclemodel { get; set; }
        //OPTIONAL
        public string invoicenumber { get; set; }
        //REQUIRED
        public string vehicletype { get; set; }
        //REQUIRED
        public string vehicleusage { get; set; }
        //OPTIONAL
        public DateTime? dateissued { get; set; }
        //OPTIONAL
        public string branchcode { get; set; }
        //REQUIRED
        public string agentcode { get; set; }
        //OPTIONAL
        public string paymentreferenceno { get; set; }
    }
    #endregion [Malayan]
}