using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirDemo.Domain;
using AirDemo.Service.Models;

namespace AirDemo.Service
{
    public interface IAirplaneService
    {
        Task<IEnumerable<AirplaneResponse>> GetAirplanes();
        Task<AirplaneResponse> GetAirplane(string serialNumber);
        Task RegisterNewAirplane(AirplaneAddRequest addRequest);
        Task FlyAirplane(string serialNumber, AirplaneFlyRequest flyRequest);
        Task LandAirplane(string serialNumber, AirplaneLandRequest landRequest);
        Task DeleteAirplane(string serialNumber);
    }
}
