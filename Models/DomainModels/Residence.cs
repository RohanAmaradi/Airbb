using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Airbb.Models.Validations;

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
        [RegularExpression(@"^\d+(\.5)?$", ErrorMessage = "Bathroom Number must end with .5 when fractional")]
        public int BathroomNumber { get; set; }
        
        [Required(ErrorMessage = "Please enter a BuiltYear.")]
        [BuiltYear(150, ErrorMessage = "Built Year must be no more than 150 year old.")]
        public DateTime? BuiltYear { get; set; }

        [Required(ErrorMessage = "Please enter a PricePerNight.")]
        public string PricePerNight { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Location.")]
        public int LocationId { get; set; }
        [ValidateNever]
        public Location Location { get; set; } = null!;
        
        [Required(ErrorMessage = "Please enter a UserId.")]
        [Remote("CheckOwnerId", "Validation", areaName: "")]
        public int UserId { get; set; }
        [ValidateNever]
        public User User { get; set; } = null!;
    }
}
