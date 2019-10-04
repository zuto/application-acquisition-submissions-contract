using ApplicationAcquisitionSubmissions.Contract.DataAttributes;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class Vehicle
    {
        [Required]
        [StringLengthRange(1, 50)]
        public string Registration { get; set; }

        [Required]
        [IntegerRange(0, 99999999)]
        public int Mileage { get; set; }

        [Required]
        [StringLengthRange(1, 255)]
        public string Make { get; set; }

        [Required]
        [StringLengthRange(1, 255)]
        public string Model { get; set; }

        public bool? IsUsed { get; set; }

        [Required]
        [AllowedValues(new[] { "DESIRED", "PX" })]
        public string VehicleStatus { get; set; }

        public bool? ValuationRequired { get; set; }

        [MoneyRange(0.01, 99999999)]
        public decimal? Value { get; set; }

        public Dealer Dealer { get; set; }
    }
}
