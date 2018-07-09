using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public class AllowedValuesAttribute : ValidationAttribute
    {
        public string[] Values { get; }        

        public AllowedValuesAttribute(string[] values)
        {
            Values = values;
        }

        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var input = value as string;
            if(!Values.Contains(input))
                return new ValidationResult(validationContext.DisplayName + " contained '" + input + "', but should contain a values from this list: " + string.Join(",", Values));

            return ValidationResult.Success;
        }        
    }
}