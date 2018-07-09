using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public class StringLengthRangeAttribute : ValidationAttribute
    {
        public int MinimumLength { get; }
        public int MaximumLength { get; }

        public StringLengthRangeAttribute(int minimumLength, int maximumLength)
        {
            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var output = (string)value;
            if (output.Length < MinimumLength || output.Length > MaximumLength)
                return new ValidationResult(validationContext.DisplayName + " should have a length of between " + MinimumLength + " and " + MaximumLength + ", but value '" + output + "' has a length of " + output.Length);

            return ValidationResult.Success;
        }
    }
}