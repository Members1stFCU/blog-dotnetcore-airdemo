using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirDemo.Domain;
using AirDemo.Service.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using My.Feed.Services;

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
                .SingleOrDefault();
        }

        public async Task<Result> RegisterNewAirplane(AirplaneAddRequest addRequest)
        {
            var result = Airplane.RegisterNewAirplane(_context, addRequest.ModelNumber, addRequest.SerialNumber, addRequest.SeatCount ?? 0, addRequest.WeightInKilos ?? 0);

            // Check if action was successful
            if (result)
            {
                await _context.SaveChangesAsync();
                var updatedPlane = await this.GetAirplane(addRequest.SerialNumber);

                // Pass back a DataResult with the inner Data being the AirplaneResponse queried above
                return SuccessResult.WithData(updatedPlane);
            }

            return result;
        }

        public async Task<Result> FlyAirplane(string serialNumber, AirplaneFlyRequest flyRequest)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane == null)
            {
                return new FailResult($"Airplane with Serial # {serialNumber} not found.");
            }

            var result = airplane.Fly(flyRequest.EstimatedTripTime ?? new TimeSpan(0, 0, 0));
            if (result)
            {
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result> LandAirplane(string serialNumber, AirplaneLandRequest landRequest)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane == null)
            {
                return new FailResult($"Airplane with Serial # {serialNumber} not found.");
            }

            var result = airplane.Land(landRequest.AirportCode);
            if (result)
            {
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<Result> DeleteAirplane(string serialNumber)
        {
            var airplane = _context.Airplanes.Where(x => x.SerialNumber == serialNumber).FirstOrDefault();
            if (airplane == null)
            {
                return new FailResult($"Airplane with Serial # {serialNumber} not found.");
            }

            _context.Airplanes.Remove(airplane);
            await _context.SaveChangesAsync();
            return new SuccessResult();
        }
    }
}