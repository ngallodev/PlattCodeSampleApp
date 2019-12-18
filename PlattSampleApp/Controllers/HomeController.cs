using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PlattSampleApp.Models;
using PlattSampleApp.Services;

namespace PlattSampleApp.Controllers
{
    public class HomeController : Controller
    {
        StarWarsApiService starWarsApi = new StarWarsApiService();
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetAllPlanets()
        {
            var model = new AllPlanetsViewModel();

            List<PlanetModel> planets = await starWarsApi.GetAllPlanetsAsync();
            planets = planets.OrderByDescending(o => o.PlanetDiameterDouble).ToList();
            model.AverageDiameter = planets
                                    .Where(o => o.PlanetDiameterDouble > 0)
                                    .Average(a => a.PlanetDiameterDouble);
            foreach( PlanetModel p in planets)
            {
                PlanetDetailsViewModel pdvm = new PlanetDetailsViewModel();
                int diameter = 0;
                int.TryParse(p.PlanetDiameter, out diameter);
                pdvm.Diameter = diameter;
                pdvm.Population =  p.PlanetPopulation;
                pdvm.Name = p.PlanetName;
                pdvm.Terrain = p.PlanetTerrain;
                pdvm.LengthOfYear = p.PlanetOrbitalPeriod;
                model.Planets.Add(pdvm);
            }
            

            return View(model);
        }

        public async Task<ActionResult> GetPlanetTwentyTwo(int planetid)
        {
            var model = new SinglePlanetViewModel();
            
            PlanetModel planet = await starWarsApi.GetPlanetByIdAsync(planetid);
            if (planet != null)
            {
                model.Climate = planet.PlanetClimate;
                model.Diameter = planet.PlanetDiameter;
                model.Gravity = planet.PlanetGravity;
                model.LengthOfDay = planet.PlanetDayLength;
                model.LengthOfYear = planet.PlanetOrbitalPeriod;
                model.Name = planet.PlanetName;
                model.Population = planet.PlanetPopulation;
                model.SurfaceWaterPercentage = planet.PlanetSurfaceWater;
            }
            return View(model);
        }

        public async Task<ActionResult> GetResidentsOfPlanetNaboo(string planetname)
        {
            var model = new PlanetResidentsViewModel();

            List<ResidentModel> residents = await starWarsApi.GetResidentsByPlanetNameAsync("Naboo");
            residents = residents.OrderBy(x => x.ResidentName).ToList();
            foreach(ResidentModel r in residents)
            {
                ResidentSummary rs = new ResidentSummary();
                rs.EyeColor = r.ResidentEyeColor;
                rs.Gender = r.ResidentGender;
                rs.HairColor = r.ResidentHairColor;
                rs.Height = r.ResidentHeight;
                rs.Name = r.ResidentName;
                rs.SkinColor = r.ResidentSkinColor;
                rs.Weight = r.ResidentWeight;

                model.Residents.Add(rs);
            }
            return View(model);
        }

        public async Task<ActionResult> VehicleSummary()
        {
            var model = new VehicleSummaryViewModel();

            List<VehicleModel> vehicles = await starWarsApi.GetAllVehiclesAsync();
            model.VehicleCount = vehicles.Where(x => x.VehicleCost != "unknown").Count();
            model.ManufacturerCount = vehicles.GroupBy(x => x.VehicleManufacturer).Count();
            //filter out vehicles where cost is unknown, order by manufacturer
            List<VehicleSummaryModel> vehicleSummary = vehicles.Where(v => v.VehicleCost != "unknown")
                                .GroupBy(v => v.VehicleManufacturer)
                                .Select(s1 => new VehicleSummaryModel
                                {
                                    ManufacturerName = s1.First().VehicleManufacturer,
                                    NumberOfVehicles = s1.Count(),
                                    AverageVehicleCost = s1.Average(x => Convert.ToDouble(x.VehicleCost)),
                                }).ToList();
            vehicleSummary = vehicleSummary.OrderByDescending(x => x.NumberOfVehicles)
                                            .ThenByDescending(x => x.AverageVehicleCost).ToList();

            foreach(VehicleSummaryModel vsm in vehicleSummary)
            {
                VehicleStatsViewModel vsvm = new VehicleStatsViewModel();
                vsvm.ManufacturerName = vsm.ManufacturerName;
                vsvm.VehicleCount = vsm.NumberOfVehicles;
                vsvm.AverageCost = vsm.AverageVehicleCost;
                model.Details.Add(vsvm);
            }                 
                                
            
            return View(model);
        }
    }
}