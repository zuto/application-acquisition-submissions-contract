using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class ApplicationDetails
    {        
        [MoneyRange(0.01, 99999999)]
        public decimal? CreditLimit { get; set; }

        [Required]
        [AllowedValues(new[] { "CAR", "VAN", "MOTORBIKE", "OTHER", "CARAVAN", "MOTORHOME", "TAXI" })]
        public string VehicleType { get; set; }

        [MoneyRange(0.01, 99999999)]
        public decimal? Deposit { get; set; }

        public bool? HasGuarantor { get; set; }
        public bool? HasLicenceGuarantor { get; set; }

        [AllowedValues(new[] {null, "UNKNOWN", "HP", "PCP", "PERSONAL LOAN" })]
        public string FinanceType { get; set; }
    }
}
