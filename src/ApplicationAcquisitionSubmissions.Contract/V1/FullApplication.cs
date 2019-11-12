using ApplicationAcquisitionSubmissions.Contract.DataAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.V1
{
    /// <summary>
    /// Schema file to allow proposals to be sent to Zuto
    /// </summary>
    public class FullApplication
    {
        /// <summary>
        /// Origin of the application. This should be AppForm-Conversation, AppForm-Affiliate or Api-Affiliate
        /// </summary>
        public Origin Origin { get; set; }

        [ValidateObject(failOnNull: true)]
        public SubmittingParty SubmittingParty { get; set; }

        [ArrayCount(max: 2, min: 1)]
        [ValidateObject(failOnNull: false)]
        public Applicant[] Applicants { get; set; }

        [ValidateObject(failOnNull: true)]
        public ApplicationDetails ApplicationDetails { get; set; }

        [Required]
        [AllowedValues(new[] { "APPLICATION", "LEAD", "SHORTLEAD", "" })]
        public string ApplicationType { get; set; }

        [ArrayCount(max: 2, min: 0)]
        [ValidateObject(failOnNull: false)]
        public Vehicle[] Vehicles { get; set; }

        /// <summary>
        /// This element will be returned in the submission response
        /// </summary>
        [StringLengthRange(1, 50)]
        public string PublicReference { get; set; }

        public DateTime? DateApplied { get; set; }

        public DateTime? InitialVisitTime { get; set; }

        public long? ApplicationLeadId { get; set; }

        [StringLengthRange(1, 100)]
        public string WhereHeard { get; set; }

        [StringLengthRange(1, 3900)]
        public string Notes { get; set; }

        public string Device { get; set; }
        public string Session { get; set; }

        public string QuoteEventGuid { get; set; }
    }
}
