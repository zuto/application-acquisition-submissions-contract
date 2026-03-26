using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class PartialApplication
    {
        private string _forename;
        private string _middlenames;
        private string _surname;
        public Origin Origin { get; set; }

        [RegularExpression(Regexes.AlphaNumeric)]
        [StringLengthRange(1, 255)]
        public string Source { get; set; }

        [RegularExpression(Regexes.AlphaNumeric)]
        [StringLengthRange(1, 255)]
        public string Medium { get; set; }

        [RegularExpression(Regexes.AlphaNumericWithUnderscoreAndDash)]
        [StringLengthRange(1, 255)]
        public string Campaign { get; set; }

        [AllowedValuesValidation(new[] { null, "Mr", "Miss", "Mrs", "Ms" })]
        public string Title { get; set; }

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

        [AllowedValuesValidation(new[] { null, "MALE", "FEMALE" })]
        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [ValidateObject(failOnNull: false)]
        public ContactPoint ContactPoint { get; set; }

        [StringLengthRange(1, 20)]
        public string Postcode { get; set; }

        public DateTime? DateApplied { get; set; }

        public DateTime? InitialVisitTime { get; set; }

        public string PublicReference { get; set; }
    }
}
