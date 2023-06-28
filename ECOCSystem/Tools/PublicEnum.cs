using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOCSystem.Tools
{
    public enum TitleTypeEnum
    {
        Individual = 1,
        Corporate = 2,
        CorporateWithAssignee = 3
    }
    public enum UserTypeEnum
    {
        SuperAdmin = 1,
        CompanyAdministrator = 2,
        BranchUser = 3
    }
    public enum CompanyEntityEnum
    {
        DatabridgeAsia = 1,
        Insurance = 2
    }
    public enum ParamountVehicleType
    {
        CV,
        MC,
        PC
    }
}