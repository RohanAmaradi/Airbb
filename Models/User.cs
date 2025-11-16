using System.ComponentModel.DataAnnotations;

namespace Airbb.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; } = string.Empty;

        [PhoneOrEmail] // Apply the custom validation
        public string? PhoneNo { get; set; }

        [PhoneOrEmail] // Apply the custom validation to both properties
        public string? EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter a DOB.")]
        public DateTime? DOB { get; set; }
        
        [Required(ErrorMessage = "Please enter a SSN.")]
        public string SSN { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter a UserType.")]
        public string UserType { get; set; } = string.Empty;
    }
}
