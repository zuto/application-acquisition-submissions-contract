namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class Origin
    {
        public string Value { get; set; }

        public static Origin AppFormConversational => new Origin { Value = "AppForm-Conversational" };            
        public static Origin AppFormAffiliate => new Origin {Value = "AppForm-Affiliate"};        
        public static Origin ApiAffiliate => new Origin {Value = "Api-Affiliate"};

        public override bool Equals(object obj)
        {
            return ((Origin) obj)?.Value == Value;
        }
    }
}