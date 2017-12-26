using System;

namespace AirDemo.Service.Models
{
    public class AirplaneResponse
    {
        public string ModelNumber { get; set; }
        
        public string SerialNumber { get; set; }
        
        public string CurrentAirportCode { get; set; }
        
        public int SeatCount { get; set; }
        
        public decimal WeightInKilos { get; set; }
        
        public DateTimeOffset? LastTakeoffTime { get; set; }
        
        public DateTimeOffset? EstimatedLandingTime { get; set; }
    }
}