using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class VehicleModel
    {
        [JsonProperty("manufacturer")]
        public string VehicleManufacturer { get; set; }
        [JsonProperty("cost_in_credits")]
        public string VehicleCost { get; set; }
        [JsonProperty("name")]
        public string VehicleName { get; set; }
    }
}