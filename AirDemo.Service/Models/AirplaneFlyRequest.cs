using System;
using System.ComponentModel.DataAnnotations;

namespace AirDemo.Service.Models
{
    public class AirplaneFlyRequest
    {
        [Required]
        public string SerialNumber { get; set; }

        [Required]
        public TimeSpan EstimatedTripTime { get; set; }
    }
}