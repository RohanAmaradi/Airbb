using System.ComponentModel.DataAnnotations;

namespace Airbb.Models
{
    public class PhoneOrEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            // Check if at least one contact method is provided
            bool hasPhone = !string.IsNullOrWhiteSpace(user.PhoneNo);
            bool hasEmail = !string.IsNullOrWhiteSpace(user.EmailAddress);

            if (!hasPhone && !hasEmail)
            {
                return new ValidationResult("Either a phone number or an email address must be provided as a contact method.");
            }

            return ValidationResult.Success;
        }
    }
}