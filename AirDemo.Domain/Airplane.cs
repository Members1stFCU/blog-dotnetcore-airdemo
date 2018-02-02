using System;
using System.Linq;
using My.Feed.Services;

namespace AirDemo.Domain
{
    public class Airplane
    {
        private Airplane()
        {
            this.AirplaneId = Guid.NewGuid();
        }

        public static Result RegisterNewAirplane(AirplaneContext context, string modelNumber, string serialNumber, int seatCount, decimal weightInKilos)
        {
            var plane = new Airplane
            {
                ModelNumber = modelNumber,
                SerialNumber = serialNumber,
                SeatCount = seatCount,
                WeightInKilos = weightInKilos
            };

            var result = plane.IsDuplicate(context);
            if (result)
            {
                context.Airplanes.Add(plane);
            }

            return result;
        }

        public virtual Guid AirplaneId { get; private set; }
        
        public virtual string ModelNumber { get; private set; }
        
        public virtual string SerialNumber { get; private set; }
        
        public virtual string CurrentAirportCode { get; private set; }
        
        public virtual int SeatCount { get; private set; }
        
        public virtual decimal WeightInKilos { get; private set; }
        
        public virtual DateTimeOffset? LastTakeoffTime { get; private set; }
        
        public virtual DateTimeOffset? EstimatedLandingTime { get; private set; }

        public virtual void Fly(TimeSpan estimatedTripTime)
        {
            this.CurrentAirportCode = null;
            this.LastTakeoffTime = DateTimeOffset.Now;
            this.EstimatedLandingTime = DateTimeOffset.Now.Add(estimatedTripTime);
        }

        public virtual void Land(string airportCode)
        {
            this.CurrentAirportCode = airportCode;
            this.LastTakeoffTime = null;
            this.EstimatedLandingTime = null;
        }

        private Result IsDuplicate(AirplaneContext context)
        {
            if (context.Airplanes.Any(x => x.AirplaneId != this.AirplaneId && x.SerialNumber == this.SerialNumber))
            {
                return new FailResult($"The Serial # {this.SerialNumber} is already taken.");
            }
            
            return new SuccessResult();
        }
    }
}