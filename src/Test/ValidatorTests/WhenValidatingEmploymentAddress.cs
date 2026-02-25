using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.EmploymentAddressTests
{
    class WhenValidatingEmploymentAddress
    {
        [Test]
        public void ItShouldAcceptNullTownCity()
        {
            var employmentAddress = new EmploymentAddress();

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }
    }
}
