using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public class MoneyRangeAttribute : ValidationAttribute
    {
        public decimal Minimum { get; }
        public decimal Maximum { get; }

        public MoneyRangeAttribute(double minimum, double maximum)
        {
            Minimum = (Decimal)TypeDescriptor.GetConverter(typeof(Decimal)).ConvertFrom(minimum.ToString("F2"));
            Maximum = (Decimal)TypeDescriptor.GetConverter(typeof(Decimal)).ConvertFrom(maximum.ToString("F2"));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
                return ValidationResult.Success;

            var money = (decimal)value;
            if (money < Minimum || money > Maximum) 
                return new ValidationResult(validationContext.DisplayName + " should contain be between " + Minimum + " and " + Maximum  + ", but was " + money.ToString("F2"));

            return ValidationResult.Success;
        }                       
    }
}