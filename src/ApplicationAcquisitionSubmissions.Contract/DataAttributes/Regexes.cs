namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    public static class Regexes
    {
        public const string AlphaNumeric = "[a-zA-Z0-9]*";        
        public const string AlphaNumericWithUnderscoreAndDash = "[a-zA-Z0-9_\\-]*";
        public const string AlphaNumericWithUnderscoreAndDashAndSpace = "[a-zA-Z0-9 _\\-]*";
        public const string MobileNumber = @"(?:07\d{9}|00447\d{9})";
        public const string NonMobileUkNumber = @"(?:(?:0(?:1|2|3|5|8|9)\d{9,10})|(?:0044(?:1|2|3|5|8|9)\d{9,10}))";
        public const string UkCallableNumber = $"^(?:{MobileNumber}|{NonMobileUkNumber})$";
        public const string ApplicantNameRegex = @"^(?!.*['\u2019]{2})(?:[\p{L}\p{M}]+\.?)+(?:[ \-'\u2019](?:[\p{L}\p{M}]+\.?)+)*['\u2019]?$";
    }
}