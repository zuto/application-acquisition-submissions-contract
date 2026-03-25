using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class ApplicantBasicDetails
    {
        private string _forename;
        private string _middlenames;
        private string _surname;

        [AllowedValuesValidation(new[] { "MALE", "FEMALE" })]
        public string Gender { get; set; }

        [AllowedValuesValidation(new[] { "Mr", "Miss", "Mrs", "Ms" })]
        public string Title { get; set;}

        [RegularExpression(Regexes.ApplicantNameRegex)]
        [StringLengthRange(1, 50)]
        public string Forename
        {
            get => _forename;
            set => _forename = value == null
                ? null
                : System.Text.RegularExpressions.Regex.Replace(value.Trim(), @"\s+", " ");
        }

        [RegularExpression(Regexes.ApplicantNameRegex)]
        [StringLengthRange(1, 100)]
        public string MiddleNames {
            get => _middlenames;
            set => _middlenames = value == null
                ? null
                : System.Text.RegularExpressions.Regex.Replace(value.Trim(), @"\s+", " ");
        }

        [RegularExpression(Regexes.ApplicantNameRegex)]
        [StringLengthRange(1, 50)]
        public string Surname {
            get => _surname;
            set => _surname = value == null
                ? null
                : System.Text.RegularExpressions.Regex.Replace(value.Trim(), @"\s+", " ");
        }

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