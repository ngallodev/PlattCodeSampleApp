using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class VehicleResponseModel
    {
        [JsonProperty("count")]
        public string ResponseCount { get; set; }
        [JsonProperty("next")]
        public string ResponseNext { get; set; }
        public List<VehicleModel> vehicles { get; set; }
        public VehicleResponseModel()
        {
            vehicles = new List<VehicleModel>();
        }
    }
}