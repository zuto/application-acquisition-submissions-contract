using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class SubmittingParty
    {
        [StringLengthRange(1,50)]
        public string Name { get; set; }

        [StringLengthRange(1, 100)]
        public string Code { get; set; }

        /// <summary>
        /// This element will be returned in the submission response 
        /// </summary>
        [StringLengthRange(1, 50)]
        [Required]
        public string Reference { get; set; }

        [StringLengthRange(1, 255)]
        [RegularExpression(Regexes.AlphaNumericWithUnderscoreAndDashAndSpace)]
        public string Source { get; set; }

        [StringLengthRange(1, 255)]
        [RegularExpression(Regexes.AlphaNumericWithUnderscoreAndDashAndSpace)]
        public string Medium { get; set; }

        [StringLength(255)]
        [RegularExpression(Regexes.AlphaNumericWithUnderscoreAndDashAndSpace)]
        public string Campaign { get; set; }

        [StringLengthRange(0, 100)]
        public string Gclid { get; set; }
    }
}