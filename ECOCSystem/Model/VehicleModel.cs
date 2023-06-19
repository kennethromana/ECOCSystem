using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECOCSystem.Model
{
    public class VehicleModel
    {
        public VehicleModel()
        {
            VehicleTypes = new List<VehicleTypes>();
            VehicleColors = new List<VehicleColors>();
            VehicleMakeList = new List<VehicleMakeList>();
            VehicleBodyList = new List<VehicleBodyList>();
            VehicleSeriesList = new List<VehicleSeriesList>();

        }
        public int VehicleID { get; set; }
        [Required]
        [DisplayName("Vehicle Type")]
        public int SelectedVehicleTypeID { get; set; }
        [Required]
        [DisplayName("Make")]
        public int SelectedMakeID { get; set; }
        [Required]
        [DisplayName("Body Type")]
        public int SelectedBodyTypeID { get; set; }
        [Required]
        [DisplayName("Color")]
        public int SelectedColorID { get; set; }
        //[Required]
        [DisplayName("Series")]
        public int SelectedSeriesID { get; set; }
        [Required]
        [DisplayName("Engine Number")]
        public string EngineNumber { get; set; }
        [Required]
        [DisplayName("Chassis Number")]
        public string ChassisNumber { get; set; }
        [Required]
        [DisplayName("Plate Number")]
        public string PlateNumber { get; set; }
        [Required]
        [DisplayName("Year")]
        public string Year { get; set; }
        public string Color { get; set; }
        public string MVFileNumber { get; set; }
        public Nullable<int> MVTypeID { get; set; }
        public Nullable<int> MakeID { get; set; }
        public Nullable<int> SeriesID { get; set; }
        public Nullable<int> BodyTypeID { get; set; }
        public List<VehicleTypes> VehicleTypes { get; set; }
        public List<VehicleColors> VehicleColors { get; set; }
        public List<VehicleMakeList> VehicleMakeList { get; set; }
        public List<VehicleBodyList> VehicleBodyList { get; set; }
        public List<VehicleSeriesList> VehicleSeriesList { get; set; }

    }
    public class VehicleTypes
    {
        public int ID { get; set; }
        public string Info { get; set; }
    }
    public class VehicleColors
    {
        public int ID { get; set; }
        public string Info { get; set; }
    }
    public class VehicleMakeList
    {
        public int ID { get; set; }
        public string Info { get; set; }
    }
    public class VehicleBodyList
    {
        public int ID { get; set; }
        public string Info { get; set; }
    }
    public class VehicleSeriesList
    {
        public int ID { get; set; }
        public string Info { get; set; }
    }


}