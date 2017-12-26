using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirDemo.Domain;
using AirDemo.Service.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AirDemo.Service
{
    public class AirplaneService : IAirplaneService
    {
        private readonly AirplaneContext _context;
        private readonly MapperConfiguration _map;

        public AirplaneService(
            AirplaneContext context,
            MapperConfiguration map)
        {
            _context = context;
            _map = map;
        }

        public async Task<IEnumerable<AirplaneResponse>> GetAirplanes()
        {
            return await _context.Airplanes.ProjectTo<AirplaneResponse>(_map).ToAsyncEnumerable().ToList();
        }

        public async Task<AirplaneResponse> GetAirplane(string serialNumber)
        {
            return await _context.Airplanes.ProjectTo<AirplaneResponse>(_map).ToAsyncEnumerable()
                .Where(x => x.SerialNumber == serialNumber)
                .FirstOrDefault();
        }

        public async Task RegisterNewAirplane(AirplaneAddRequest addRequest)
        {
            var airplane = new Airplane(addRequest.ModelNumber, addRequest.SerialNumber, addRequest.SeatCount, addRequest.WeightInKilos);
            _context.Add(airplane);
            await _context.SaveChangesAsync();
        }

        public async Task FlyAirplane(string serialNumber, AirplaneFlyRequest flyRequest)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane != null)
            {
                airplane.Fly(flyRequest.EstimatedTripTime);
            }

            await _context.SaveChangesAsync();
        }

        public async Task LandAirplane(string serialNumber, AirplaneLandRequest landRequest)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane != null)
            {
                airplane.Land(landRequest.AirportCode);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAirplane(string serialNumber)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane != null)
            {
                _context.Airplanes.Remove(airplane);
            }

            await _context.SaveChangesAsync();
        }
    }
}