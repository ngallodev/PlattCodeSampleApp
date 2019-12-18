using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlattSampleApp.Models
{
    public class ResidentModel
    {
        [JsonProperty("name")]
        public string ResidentName { get; set; }
        [JsonProperty("eye_color")]
        public string ResidentEyeColor { get; set; }
        [JsonProperty("hair_color")]
        public string ResidentHairColor { get; set; }
        [JsonProperty("skin_color")]
        public string ResidentSkinColor { get; set; }
        [JsonProperty("gender")]
        public string ResidentGender { get; set; }
        [JsonProperty("height")]
        public string ResidentHeight { get; set; }
        [JsonProperty("mass")]
        public string ResidentWeight { get; set; }

    }
}