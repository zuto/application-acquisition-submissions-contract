using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class EmploymentAddress
    {
        [StringLengthRange(1, 50)]
        public string NameNumber { get; set; }

        [StringLengthRange(1, 100)]
        public string Street { get; set; }

        [StringLengthRange(1, 100)]
        public string TownCity { get; set; }

        [StringLengthRange(1, 100)]
        public string County { get; set; }

        [StringLengthRange(1, 20)]
        public string PostCode { get; set; }
    }
}