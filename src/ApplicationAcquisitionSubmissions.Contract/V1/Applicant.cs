using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    public class Applicant
    {        
        [ValidateObject(failOnNull:true)]        
        public ApplicantBasicDetails BasicDetails { get; set; }

        [ValidateObject(failOnNull: false)]
        public MarketingOptIn MarketingOptIn { get; set; }

        [ValidateObject(failOnNull: false)]
        public ApplicantAdditionalDetails AdditionalDetails { get; set; }

        [ArrayCount(max: 10, min:0)]
        [ValidateObject(failOnNull: true)]
        public ApplicantAddress[] ApplicantAddress { get; set; }

        [ArrayCount(max: 10, min: 0)]
        [ValidateObject(failOnNull: true)]
        public ApplicantEmployment[] ApplicantEmployment { get; set; }
    }    
}