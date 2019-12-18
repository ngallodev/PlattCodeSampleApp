using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlattSampleApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace PlattSampleApp.Services
{
    public class StarWarsApiService : IStarWarsApiService
    {

        public StarWarsApiService()
        {
            if (ApiHelper.ApiClient == null)
                ApiHelper.InitializeClient();
        }

        public async Task<List<PlanetModel>> GetAllPlanetsAsync()
        {
            StringBuilder url = new StringBuilder();
            //base address https://swapi.co/api/
            url.Append(ApiHelper.ApiClient.BaseAddress.ToString());
            url.Append("planets");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<PlanetModel> planets = new List<PlanetModel>();

                    //get first page of planets
                    string responseString = await response.Content.ReadAsStringAsync();
                    JObject results = JsonConvert.DeserializeObject<JObject>(responseString);
                    planets.AddRange( 
                        results.Value<JArray>("results")
                                .ToObject<List<PlanetModel>>());
                    string next = results.GetValue("next").Value<string>();

                    //fetch the rest of the planet pages
                    while (!string.IsNullOrWhiteSpace(next) && next != "null")
                    {
                        PlanetResponseModel planetResponse = await GetPlanetPageAsync(next);
                        next = planetResponse.ResponseNext;
                        planets.AddRange(planetResponse.planets);
                    }

                    return planets;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

            
        }

        public async Task<PlanetResponseModel> GetPlanetPageAsync(string url)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    PlanetResponseModel planetResponse = new PlanetResponseModel();
                    
                    string responseString = await response.Content.ReadAsStringAsync();
                    JObject results = JsonConvert.DeserializeObject<JObject>(responseString);
                    planetResponse.planets.AddRange(
                        results.Value<JArray>("results")
                                .ToObject<List<PlanetModel>>());
                    planetResponse.ResponseNext = results.GetValue("next").Value<string>();

                    return planetResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            
        }

        public async Task<List<VehicleModel>> GetAllVehiclesAsync()
        {
            StringBuilder url = new StringBuilder();
            //base address https://swapi.co/api/
            url.Append(ApiHelper.ApiClient.BaseAddress.ToString());
            url.Append("vehicles");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<VehicleModel> vehicles = new List<VehicleModel>();

                    //get first page of planets
                    string responseString = await response.Content.ReadAsStringAsync();
                    JObject results = JsonConvert.DeserializeObject<JObject>(responseString);
                    vehicles.AddRange(
                        results.Value<JArray>("results")
                                .ToObject<List<VehicleModel>>());
                    string next = results.GetValue("next").Value<string>();

                    //fetch the rest of the planet pages
                    while (!string.IsNullOrWhiteSpace(next) && next != "null")
                    {
                        VehicleResponseModel vehicleResponse = await GetVehiclePageAsync(next);
                        next = vehicleResponse.ResponseNext;
                        vehicles.AddRange(vehicleResponse.vehicles);
                    }

                    return vehicles;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task<VehicleResponseModel> GetVehiclePageAsync(string url)
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    VehicleResponseModel vehicleResponse = new VehicleResponseModel();

                    string responseString = await response.Content.ReadAsStringAsync();
                    JObject results = JsonConvert.DeserializeObject<JObject>(responseString);
                    vehicleResponse.vehicles.AddRange(
                        results.Value<JArray>("results")
                                .ToObject<List<VehicleModel>>());
                    vehicleResponse.ResponseNext = results.GetValue("next").Value<string>();

                    return vehicleResponse;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public async Task<PlanetModel> GetPlanetByIdAsync(int id)
        {
            StringBuilder url = new StringBuilder();
            //base address https://swapi.co/api/
            url.Append(ApiHelper.ApiClient.BaseAddress.ToString());
            url.Append($"planets/{id}");

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url.ToString()))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    //response
                    PlanetModel planet = JsonConvert.DeserializeObject<PlanetModel>(responseString);
                    return planet;
                }
                else
                {
                    //Failed response
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<ResidentModel>> GetResidentsByPlanetNameAsync(string name)
        {
            List<ResidentModel> residents = new List<ResidentModel>();
            List<PlanetModel> planets = await GetAllPlanetsAsync();
            PlanetModel planet = planets.FirstOrDefault<PlanetModel>(x => x.PlanetName == name);
            
            foreach( string url in planet.PlanetResidents)
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        //response
                        ResidentModel resident = JsonConvert.DeserializeObject<ResidentModel>(responseString);
                        residents.Add(resident);
                    }
                    else
                    {
                        //Failed response
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }

            return residents;
        }
    }
}