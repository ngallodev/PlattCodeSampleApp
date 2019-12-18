using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class PlanetResponseModel
    {
        //public class ResponseResults
        //{
        //    public List<PlanetModel> planets { get; set; }
        //}
        [JsonProperty("count")]
        public string ResponseCount { get; set; }
        [JsonProperty("next")]
        public string ResponseNext { get; set; }
     //   [JsonProperty("results")]
        public List<PlanetModel> planets { get; set; }
        public PlanetResponseModel()
        {
            planets = new List<PlanetModel>();
        }
    }
}