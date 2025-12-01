using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Airbb.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Please enter a StartDate.")]
        public DateTime ReservationStartDate { get; set; }

        [Required(ErrorMessage = "Please enter a EndDate.")]
        public DateTime ReservationEndDate { get; set; }

        [Required(ErrorMessage = "Please enter a Residence.")]
        public int ResidenceId { get; set; }
        [ValidateNever]
        public Residence Residence { get; set; } = null!;
    }
}
