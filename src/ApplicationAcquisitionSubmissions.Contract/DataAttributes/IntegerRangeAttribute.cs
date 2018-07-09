using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public class IntegerRangeAttribute : ValidationAttribute
    {
        public int Minimum { get; }
        public int Maximum { get; }

        public IntegerRangeAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var integer = (int)value;
            if (integer < Minimum || integer > Maximum)
                return new ValidationResult(validationContext.DisplayName + " should contain be between " + Minimum + " and " + Maximum + ", but was " + integer);

            return ValidationResult.Success;
        }
    }
}