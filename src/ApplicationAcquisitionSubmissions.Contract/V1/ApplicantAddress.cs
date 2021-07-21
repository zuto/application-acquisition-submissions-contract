using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class ApplicantAddress
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

        [IntegerRange(0, 100)]
        public int Years { get; set; }

        [IntegerRange(0, 11)]
        public int Months { get; set; }

        [AllowedValues(new[]{ "HOME OWNER", "PRIVATE TENANT", "LIVING WITH PARENTS", "LIVING WITH PARTNER", "COUNCIL TENANT", "HOUSING ASSOCIATION", "UNKNOWN" })]
        public string ResidentialStatus { get; set; }
    }  
}