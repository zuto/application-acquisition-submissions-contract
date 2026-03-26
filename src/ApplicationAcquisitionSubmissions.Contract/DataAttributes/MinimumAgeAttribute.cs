using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;
        public MinimumAgeAttribute(int minimumAge = 18)
        {
            _minimumAge = minimumAge;
            ErrorMessage = $"Applicant must be at least {_minimumAge} years old.";
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            
            if (value is not DateTime dateOfBirth)
                return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
       
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
                age--;
            
            if (age < _minimumAge)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }

    }
}
