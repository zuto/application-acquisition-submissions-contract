using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.ApplicationDetailsTests
{
    class WhenValidatingApplicationDetails
    {
        [Test]
        public void ItShouldRejectNullCreditLimit()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR"
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicationDetails, validationContext, true));
            Assert.That(exception.Message, Is.EqualTo("The CreditLimit field is required."));

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
            StringAssert.StartsWith("CreditLimit should contain be between 0.01 and 99999999.00", exception.Message);
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
