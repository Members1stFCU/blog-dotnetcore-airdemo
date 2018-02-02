using System;
using System.Linq;

namespace AirDemo.Domain
{
    public class Airplane
    {
        private Airplane()
        {
            this.AirplaneId = Guid.NewGuid();
        }

        public static Airplane RegisterNewAirplane(AirplaneContext context, string modelNumber, string serialNumber, int seatCount, decimal weightInKilos)
        {
            var plane = new Airplane
            {
                ModelNumber = modelNumber,
                SerialNumber = serialNumber,
                SeatCount = seatCount,
                WeightInKilos = weightInKilos
            };

            if (context.Airplanes.Where(x => x.SerialNumber == serialNumber).Any())
            {
                return null;
            }
            else
            {
                context.Airplanes.Add(plane);
                return plane;
            }
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
    }
}