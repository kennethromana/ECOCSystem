using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECOCSystem.Model
{
    public class MVModel
    {
        public MVModel()
        {
            VehicleTypes = new List<DropdownModel>();
            VehicleColors = new List<DropdownModel>();
            VehicleMakeList = new List<DropdownModel>();
            VehicleBodyList = new List<DropdownModel>();
            VehicleSeriesList = new List<DropdownModel>();
            VehicleClassificationList = new List<DropdownModel>();
        }
        public int VehicleID { get; set; }
        [DisplayName("Vehicle Classification")]
        public int SelectedClassificationID { get; set; }
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
        [Required]
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
        public List<DropdownModel> VehicleTypes { get; set; }
        public List<DropdownModel> VehicleColors { get; set; }
        public List<DropdownModel> VehicleMakeList { get; set; }
        public List<DropdownModel> VehicleBodyList { get; set; }
        public List<DropdownModel> VehicleSeriesList { get; set; }
        public List<DropdownModel> VehicleClassificationList { get; set; }
        [Required]
        [DisplayName("Vehicle Make")]
        public string VehicleMake { get; set; }
        [Required]
        [DisplayName("Vehicle Model")]
        public string Model { get; set; }
        [Required]
        [DisplayName("Variant")]
        public string VehicleVariant { get; set; }


    }
  
    public class MakeModel
    {
        public MakeModel()
        {
            MakeInfo = new MVModel();
        }
        public int MakeID { get; set; }
        public MVModel MakeInfo { get; set; }

        public List<Models> ModelList { get; set; }
    }
    public class Series
    {
        public bool isChecked { get; set; }
        public int SeriesID { get; set; }
        public string Model { get; set; }
        public string Variant { get; set; }
        public string BodyType { get; set; }

    }

    public class Models
    {
        public bool isChecked { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }

    }

    public class VehicleModelModel
    {
        public VehicleModelModel()
        {
            ModelInfo = new MVModel();
        }
        public int ModelID { get; set; }
        public MVModel ModelInfo { get; set; }

    }
    public class BodyTypeModel
    {
        public BodyTypeModel()
        {
            BodyTypeInfo = new MVModel();
        }
        public int BodyTypeID { get; set; }
        public MVModel BodyTypeInfo { get; set; }

    }

}