using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.ApplicationDetailsTests
{
    class WhenValidatingApplicationDetails
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
        
        [Test]
        public void ItShouldAcceptNullCreditLimit()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR"
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectZeroCreditLimit()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                CreditLimit = 0
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicationDetails, validationContext, true));
            Assert.That(exception.Message, Does.StartWith("CreditLimit should contain be between 0.01 and 99999999.00"));
        }

        [TestCase(0.01)]
        [TestCase(99999999)]
        public void ItShouldAcceptNonZeroCreditLimit(decimal creditLimit)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                CreditLimit = creditLimit
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }
    }
}
