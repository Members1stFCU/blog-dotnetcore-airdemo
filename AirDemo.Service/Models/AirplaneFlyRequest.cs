using System;
using System.ComponentModel.DataAnnotations;

namespace AirDemo.Service.Models
{
    public class AirplaneFlyRequest
    {
        [Required]
        public TimeSpan EstimatedTripTime { get; set; }
    }
}