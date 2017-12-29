using System.ComponentModel.DataAnnotations;

namespace AirDemo.Service.Models
{
    public class AirplaneAddRequest
    {
        [Required]
        public string ModelNumber { get; set; }
        
        [Required]
        public string SerialNumber { get; set; }
        
        [Required]
        public int? SeatCount { get; set; }
        
        [Required]
        public decimal? WeightInKilos { get; set; }
    }
}