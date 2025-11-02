using System.ComponentModel.DataAnnotations;

namespace Airbb.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a PhoneNo.")]
        public string PhoneNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a EmailAddress.")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a DOB.")]
        public string DOB { get; set; } = string.Empty;
    }
}
