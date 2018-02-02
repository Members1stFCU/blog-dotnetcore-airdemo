using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirDemo.Service.Models;
using My.Feed.Services;

namespace AirDemo.Service
{
    public interface IAirplaneService
    {
        Task<IEnumerable<AirplaneResponse>> GetAirplanes();
        Task<AirplaneResponse> GetAirplane(string serialNumber);
        Task<Result> RegisterNewAirplane(AirplaneAddRequest addRequest);
        Task<Result> FlyAirplane(string serialNumber, AirplaneFlyRequest flyRequest);
        Task<Result> LandAirplane(string serialNumber, AirplaneLandRequest landRequest);
        Task<Result> DeleteAirplane(string serialNumber);
    }
}
