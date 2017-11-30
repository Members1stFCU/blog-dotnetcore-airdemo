using System;

namespace AirDemo.Domain
{
    public class Airplane
    {
        private Airplane()
        {
        }

        public Airplane(string modelNumber, string serialNumber, int seatCount, decimal weightInKilos)
        {
            this.AirplaneId = Guid.NewGuid();
            this.ModelNumber = modelNumber;
            this.SerialNumber = serialNumber;
            this.SeatCount = seatCount;
            this.WeightInKilos = weightInKilos;
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