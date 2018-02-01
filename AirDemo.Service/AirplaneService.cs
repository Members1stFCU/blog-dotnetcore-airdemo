using System;
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
            return await _context.Airplanes
                .Where(x => x.SerialNumber == serialNumber)
                .ProjectTo<AirplaneResponse>(_map)
                .ToAsyncEnumerable()                
                .FirstOrDefault();
        }

        public async Task RegisterNewAirplane(AirplaneAddRequest addRequest)
        {
            var airplane = new Airplane(addRequest.ModelNumber, addRequest.SerialNumber, addRequest.SeatCount ?? 0, addRequest.WeightInKilos ?? 0);
            _context.Add(airplane);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> FlyAirplane(string serialNumber, AirplaneFlyRequest flyRequest)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane == null)
            {
                return false;
            }

            airplane.Fly(flyRequest.EstimatedTripTime ?? new TimeSpan(0, 0, 0));
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> LandAirplane(string serialNumber, AirplaneLandRequest landRequest)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane == null)
            {
                return false;
            }

            airplane.Land(landRequest.AirportCode);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAirplane(string serialNumber)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane == null)
            {
                return false;
            }

            _context.Airplanes.Remove(airplane);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}