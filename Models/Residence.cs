using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Airbb.Models
{
    public class Residence
    {
        public int ResidenceId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a ResidencePicture.")]
        public string ResidencePicture { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a GuestNumber.")]
        public int GuestNumber { get; set; }

        [Required(ErrorMessage = "Please enter a BedroomNumber.")]
        public int BedroomNumber { get; set; }

        [Required(ErrorMessage = "Please enter a BathroomNumber.")]
        public int BathroomNumber { get; set; }

        [Required(ErrorMessage = "Please enter a PricePerNight.")]
        public string PricePerNight { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Location.")]
        public int LocationId { get; set; }
        [ValidateNever]
        public Location Location { get; set; } = null!;
    }
}
