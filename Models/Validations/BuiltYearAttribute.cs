using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Airbb.Models.Validations
{
    public class BuiltYearAttribute : ValidationAttribute, IClientModelValidator
    {
        private int maxYearsBack;

        public BuiltYearAttribute(int yearsBack = 150)
        {
            maxYearsBack = yearsBack;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext ctx)
        {
            if (value is DateTime builtDate)
            {
                var today = DateTime.Today;
                var minDate = today.AddYears(-maxYearsBack);

                // Check if it's in the past
                if (builtDate > today)
                {
                    return new ValidationResult(GetFutureMsg(ctx.DisplayName ?? "Built year"));
                }

                // Check if it's not too far in the past (within 150 years)
                if (builtDate < minDate)
                {
                    return new ValidationResult(GetTooOldMsg(ctx.DisplayName ?? "Built year"));
                }

                return ValidationResult.Success!;
            }

            return new ValidationResult($"{ctx.DisplayName ?? "Built year"} must be a valid date.");
        }

        public void AddValidation(ClientModelValidationContext ctx)
        {
            if (!ctx.Attributes.ContainsKey("data-val"))
                ctx.Attributes.Add("data-val", "true");

            ctx.Attributes.Add("data-val-builtyear-years", maxYearsBack.ToString());
            ctx.Attributes.Add("data-val-builtyear", GetGenericMsg());
        }

        private string GetFutureMsg(string name) =>
            ErrorMessage ?? $"{name} must be in the past.";

        private string GetTooOldMsg(string name) =>
            ErrorMessage ?? $"{name} cannot be more than {maxYearsBack} years ago.";

        private string GetGenericMsg() =>
            ErrorMessage ?? $"Built year must be a past year, but no more than {maxYearsBack} years ago.";
    }
}