using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class VehicleSummaryModel
    {
        public string  ManufacturerName { get; set; }

        public int NumberOfVehicles { get; set; }
        public double AverageVehicleCost { get; set; }
    }
}