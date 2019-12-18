using System.Collections.Generic;
using System.Threading.Tasks;
using PlattSampleApp.Models;

namespace PlattSampleApp.Services
{
    public interface IStarWarsApiService
    {
        Task<List<PlanetModel>> GetAllPlanetsAsync();
        Task<PlanetModel> GetPlanetByIdAsync(int id);
        Task<List<ResidentModel>> GetResidentsByPlanetNameAsync(string name);
        Task<List<VehicleModel>> GetAllVehiclesAsync();
    }
}