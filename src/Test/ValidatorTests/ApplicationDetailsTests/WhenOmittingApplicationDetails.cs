using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.ApplicationDetailsTests
{
    public class WhenOmittingApplicationDetails
    {
        [Test]
        public void ItShouldAcceptLeadWithNoApplicationDetails()
        {
            var lead = new TestDataBuilder<LeadApplication>()
                .WithDefaultSubmittingParty()
                .WithDefaultApplicant()
                .Build();

            var validationContext = new ValidationContext(lead, null, null);
            Validator.ValidateObject(lead, validationContext, true);
        }

        [Test]
        public void ItShouldRejectFullApplicationWithNoApplicationDetails()
        {
            var application = new TestDataBuilder<FullApplication>()
                .WithDefaultSubmittingParty()
                .WithDefaultApplicant()
                .Build();

            var validationContext = new ValidationContext(application, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(application, validationContext, true));
            Assert.That(exception.Message, Is.EqualTo("ApplicationDetails element is missing"));
        }
    }
}
