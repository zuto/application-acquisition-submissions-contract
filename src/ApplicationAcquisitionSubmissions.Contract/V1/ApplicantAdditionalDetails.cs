using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class ApplicantAdditionalDetails
    {        
        [AllowedValues(new[]{ "MARRIED", "COHABITING", "LIVING WITH PARTNER", "SINGLE", "SEPARATED", "DIVORCED", "WIDOWED", "UNKNOWN"})]
        public string MaritalStatus { get; set; }        

        [AllowedValues(new[]{ "FULL UK", "PROVISIONAL UK", "EUROPEAN", "INTERNATIONAL", "NONE", "CBT", "A2","FULL A CLASS", "UNKNOWN" })]
        public string LicenceType { get; set; }      

        public bool? ValidUkPassport { get; set; }

        [MoneyRange(0.01, 99999999)]
        public decimal? OtherMonthlyIncome { get; set; }      
    }
}
