namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class MarketingOptIn
    {
        /// <summary>
        /// Whether the user has opted to receive marketing emails
        /// </summary>
        public bool? Email { get; set; }

        /// <summary>
        /// Whether the user has opted to receive marketing sms
        /// </summary>
        public bool? Sms { get; set; }

        /// <summary>
        /// Whether the user has opted to receive marketing phone calls
        /// </summary>
        public bool? Phone { get; set; }

        /// <summary>
        /// Whether the user has opted to be referred to third parties
        /// </summary>
        public bool? ThirdPartyReferral { get; set; }        
    }
}