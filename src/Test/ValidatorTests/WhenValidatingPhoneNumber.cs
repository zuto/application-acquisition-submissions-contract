using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.PhoneNumberTests
{
    class WhenValidatingPhoneNumber
    {
        // Type field tests (AllowedValuesValidation: HOME, MOBILE)
        [TestCase("HOME")]
        [TestCase("MOBILE")]
        public void ItShouldAcceptValidType(string type)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = type,
                Value = "07123456789"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            Assert.That(() => Validator.ValidateObject(phoneNumber, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectInvalidType()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "WORK",
                Value = "01234567890"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("PhoneNumber type contained 'WORK', but should contain a value from this list: HOME, MOBILE"));
        }

        [Test]
        public void ItShouldRejectNullType()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = null,
                Value = "01234567890"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Type"));
        }

        [Test]
        public void ItShouldRejectEmptyType()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "",
                Value = "07123456789"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Type"));
        }
        
        // PhoneNumber value tests
        [TestCase("07123456789", "HOME")] // mobile
        [TestCase("00447123456789", "HOME")] // mobile
        [TestCase("01611234567", "HOME")] // landline
        [TestCase("02012345678", "HOME")] // landline
        [TestCase("00441611234567", "HOME")] // landline
        [TestCase("00442012345678", "HOME")] // landline
        [TestCase("07123456789", "MOBILE")] // mobile
        [TestCase("00447123456789", "MOBILE")] // mobile
        [TestCase("01611234567", "MOBILE")] // landline
        [TestCase("02012345678", "MOBILE")] // landline
        [TestCase("00441611234567", "MOBILE")] // landline
        [TestCase("00442012345678", "MOBILE")] // landline
        public void ItShouldAcceptValidValue(string value, string type)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = type,
                Value = value
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            Assert.That(() => Validator.ValidateObject(phoneNumber, validationContext, true), Throws.Nothing);
        }

        [TestCase("7810555444", "MOBILE")] // Missing leading 0
        [TestCase("0", "MOBILE")] // Too short
        [TestCase("07", "MOBILE")] // Too short
        [TestCase("0123", "MOBILE")] // Too short
        [TestCase("004407810555444544544544544", "MOBILE")] // Too long
        [TestCase("7810555444", "HOME")] // Missing leading 0
        [TestCase("0", "HOME")] // Too short
        [TestCase("07", "HOME")] // Too short
        [TestCase("0123", "HOME")] // Too short
        [TestCase("004407810555444544544544544", "HOME")] // Too long
        public void ItShouldRejectInvalidValue(string invalidValue, string type)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = type,
                Value = invalidValue
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain($"PhoneNumber {type} should match"));
        }

        [TestCase("HOME")]
        [TestCase("MOBILE")]
        public void ItShouldRejectValueWithLetters(string type)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = type,
                Value = "0123ABC456"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain($"PhoneNumber {type} should match"));
        }

        [TestCase("HOME")]
        [TestCase("MOBILE")]
        public void ItShouldRejectNullValue(string type)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = type,
                Value = null
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Value"));
        }

        [TestCase("HOME")]
        [TestCase("MOBILE")]
        public void ItShouldRejectEmptyValue(string type)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = type,
                Value = ""
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Value"));
        }
    }
}
