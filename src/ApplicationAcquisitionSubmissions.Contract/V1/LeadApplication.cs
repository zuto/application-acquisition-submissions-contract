using ApplicationAcquisitionSubmissions.Contract.DataAttributes;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    /// <summary>
    /// Schema file to allow leads to be sent to Zuto
    /// </summary>
    public class LeadApplication : FullApplication
    {

        [ValidateObject(failOnNull: false)]
        public new ApplicationDetails ApplicationDetails { get; set; }
    }
}