using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public class ValidateObjectAttribute : ValidationAttribute
    {
        public bool FailOnNull { get; }
        public ValidateObjectAttribute(bool failOnNull)
        {
            FailOnNull = failOnNull;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            if (value == null && FailOnNull)
                return new ValidationResult(validationContext.DisplayName + " element is missing");

            if(value == null)
                return ValidationResult.Success;

            if (value.GetType().IsArray)
            {
                foreach (var item in (value as Array))
                {
                    var context = new ValidationContext(item);
                    Validator.ValidateObject(item, context, true);                    
                }
            }
            else
            {
                var context = new ValidationContext(value);
                Validator.ValidateObject(value, context, true);                
            }
            return ValidationResult.Success;
        }
    }
}