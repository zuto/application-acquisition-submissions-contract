using System;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class ApplicantBasicDetails
    {
        [AllowedValuesValidation(new[] { "MALE", "FEMALE" })]
        public string Gender { get; set; }

        [AllowedValuesValidation(new[] { "Mr", "Miss", "Mrs", "Ms" })]
        public string Title { get; set;}

        [StringLengthRange(1, 50)]
        public string Forename { get; set; }

        [StringLengthRange(1, 100)]
        public string MiddleNames { get; set; }

        [StringLengthRange(1, 50)]
        public string Surname { get; set; }

        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Must have at least one phone number - either Home OR Mobile OR BOTH.
        /// </summary>
        [ArrayCount(max: 2, min: 1)]
        [ValidateObject(failOnNull: false)]
        public PhoneNumber[] PhoneNumbers { get; set; }

        [StringLengthRange(1, 100)]
        public string Email { get; set; }

        [AllowedValuesValidation(new[] { "PRIMARY", "JOINT" })]
        public string ApplicantType { get; set; }
    }
}