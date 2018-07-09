using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class PhoneNumber
    {   
        [Required]
        [AllowedValues(new[]{ "HOME", "MOBILE" })]
        public string Type { get; set; }


        /// <summary>
        /// Basic telephone number including STD - enforces leading zero with a sequence of digits
        /// </summary>
        [Required]
        [RegularExpression(Regexes.TelephoneNumber)]
        public string Value { get; set; }
    }
}
