using System.ComponentModel.DataAnnotations;

namespace BLL_DogsHouse.Models.Requests
{
    public class DogRequest
    {
        [MinLength(3), MaxLength(50, ErrorMessage = "Incorrect Name")]
        public string Name { get; set; }
        [MinLength(3), MaxLength(50, ErrorMessage = "Incorrect Color")]
        public string Color { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Incorrect Tail Length")]
        public double Tail_Length { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Incorrect Weight")]
        public double Weight { get; set; }
    }
}
