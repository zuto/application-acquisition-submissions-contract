using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.EmploymentAddressTests
{
    class WhenValidatingEmploymentAddress
    {
        [Test]
        public void ItShouldRejectNullTownCity()
        {
            var employmentAddress = new EmploymentAddress();

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Is.EqualTo("The TownCity field is required."));
        }
    }
}
