using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class PlanetModel
    {
        //found information regarding mapping at 
        //https://stackoverflow.com/questions/15915503/net-newtonsoft-json-deserialize-map-to-a-different-property-name
        [JsonProperty("name")]
        public string PlanetName { get; set; }
        [JsonProperty("diameter")]
        public string PlanetDiameter { get;
            set; }
        public double PlanetDiameterDouble { get {
                    double d = 0;
                    Double.TryParse(PlanetDiameter, out d);
                    return d;
            }}
        [JsonProperty("orbital_Period")]
        public string PlanetOrbitalPeriod { get; set; }
        [JsonProperty("population")]
        public string PlanetPopulation { get; set; }
        [JsonProperty("terrain")]
        public string PlanetTerrain { get; set; }
        [JsonProperty("rotation_period")]
        public string PlanetDayLength { get; set; }
        [JsonProperty("climate")]
        public string PlanetClimate { get; set; }
        [JsonProperty("surface_water")]
        public string PlanetSurfaceWater { get; set; }
        [JsonProperty("gravity")]
        public string PlanetGravity { get; set; }
        [JsonProperty("residents")]
        public List<string> PlanetResidents { get; set; }
    }
}