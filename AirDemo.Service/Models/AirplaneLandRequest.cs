using System.ComponentModel.DataAnnotations;

namespace AirDemo.Service.Models
{
    public class AirplaneLandRequest
    {
        [Required]
        public string AirportCode { get; set; }
    }
}