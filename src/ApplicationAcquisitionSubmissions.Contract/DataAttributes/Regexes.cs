namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public static class Regexes
    {
        public const string AlphaNumeric = "[a-zA-Z0-9]*";        
        public const string AlphaNumericWithUnderscoreAndDash = "[a-zA-Z0-9_\\-]*";
        public const string AlphaNumericWithUnderscoreAndDashAndSpace = "[a-zA-Z0-9 _\\-]*";
        public const string ApplicantNameRegex = @"^(?!.*['\u2019]{2})(?:[\p{L}\p{M}]+\.?)+(?:[ \-'\u2019](?:[\p{L}\p{M}]+\.?)+)*['\u2019]?$";

        /// <summary>
        /// Basic telephone number including STD - enforces leading zero with a sequence of digits
        /// </summary>
        public const string TelephoneNumber = "0\\d{1,19}";

    }
}