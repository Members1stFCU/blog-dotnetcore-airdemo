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
        Task RegisterNewAirplane(AirplaneAddRequest plane);
        Task FlyAirplane(AirplaneFlyRequest flyRequest);
        Task LandAirplane(AirplaneLandRequest landRequest);
    }
}
