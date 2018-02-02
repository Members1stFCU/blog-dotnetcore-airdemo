using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirDemo.Service.Models;

namespace AirDemo.Service
{
    public interface IAirplaneService
    {
        Task<IEnumerable<AirplaneResponse>> GetAirplanes();
        Task<AirplaneResponse> GetAirplane(string serialNumber);
        Task RegisterNewAirplane(AirplaneAddRequest addRequest);
        Task<bool> FlyAirplane(string serialNumber, AirplaneFlyRequest flyRequest);
        Task<bool> LandAirplane(string serialNumber, AirplaneLandRequest landRequest);
        Task<bool> DeleteAirplane(string serialNumber);
    }
}
