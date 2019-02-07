using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class ApplicantEmployment
    {        
        [StringLengthRange(1, 100)]
        public string Occupation { get; set; }

        [StringLengthRange(1, 100)]
        public string EmployerName { get; set; }

        [AllowedValues(new[] { "AGENCY", "FULL TIME PERMANENT", "PART TIME", "RETIRED", "SELF EMPLOYED", "STUDENT", "SUB CONTRACT", "TEMPORARY", "UNEMPLOYED" })]
        public string EmploymentStatus { get; set; }

        [RegularExpression(Regexes.TelephoneNumber)]
        public string Telephone { get; set; }

        [IntegerRange(0, 100)]
        public int Years { get; set; }

        [IntegerRange(0, 11)]
        public int Months { get; set; }

        [MoneyRange(0, 99999999)]
        public decimal? NetMonthlyIncome { get; set; }

        [ValidateObject(failOnNull: false)]
        public EmploymentAddress EmploymentAddress { get; set; }
    }
}
