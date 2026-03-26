using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using ApplicationAcquisitionSubmissions.Contract.V1;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public class AllowedPhoneNumbersValidation : ValidationAttribute
    {
        public string[] TypeValues { get; }      

        public AllowedPhoneNumbersValidation(string[] types)
        {
            TypeValues = types;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            var phoneNumber = (PhoneNumber)value;
           
            if(phoneNumber.Type != null && !TypeValues.Contains(phoneNumber.Type))
                return new ValidationResult(validationContext.DisplayName + " type contained '" + phoneNumber.Type + "', but should contain a value from this list: " + string.Join(", ", TypeValues));
            
            var regex = Regexes.UkCallableNumber;

            if (!Regex.IsMatch(phoneNumber.Value, regex))
                return new ValidationResult($"PhoneNumber {phoneNumber.Type} should match {regex}");
            
            return ValidationResult.Success; 
               
        }        
    }
}