using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECOCSystem.Model
{
    public class DivisionModel
    {
        public DivisionModel() 
        {
            Region = new RegionModel();
            Province = new ProvinceModel();
            City = new CityModel();
        }
        public int SelectedRegionID { get; set; }
        public RegionModel Region { get; set; }
        public ProvinceModel Province { get; set; }
        public CityModel City { get; set; }
    }
    public class RegionModel
    {
        public int RegionID { get; set; }
        public string RegionNumber { get; set; }
        public string RegionName { get; set; }

    }
    public class ProvinceModel
    {
        public int ProvinceID { get; set; }
        public int RegionID { get; set; }
        public string ProvinceNumber { get; set; }
        public string ProvinceName { get; set; }

    }
    public class CityModel
    {

        public int CityID { get; set; }
        public int ProvinceID { get; set; }
        public string CityNumber { get; set; }
        public string CityName { get; set; }

    }
}