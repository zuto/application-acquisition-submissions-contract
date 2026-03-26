using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    [AllowedPhoneNumbersValidation(new[]{ "HOME", "MOBILE" })]
    public class PhoneNumber
    {   
        [Required]
        public string Type { get; set; }
        
        [Required]
        public string Value { get; set; }
    }
}
