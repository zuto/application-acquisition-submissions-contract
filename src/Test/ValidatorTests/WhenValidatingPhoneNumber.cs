using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.PhoneNumberTests
{
    class WhenValidatingPhoneNumber
    {
        [Test]
        public void ItShouldAcceptValidHomePhoneNumber()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "HOME",
                Value = "01234567890"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            Assert.That(() => Validator.ValidateObject(phoneNumber, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptValidMobilePhoneNumber()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "MOBILE",
                Value = "07123456789"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            Assert.That(() => Validator.ValidateObject(phoneNumber, validationContext, true), Throws.Nothing);
        }

        // Type field tests (AllowedValuesValidation: HOME, MOBILE)
        [TestCase("HOME")]
        [TestCase("MOBILE")]
        public void ItShouldAcceptValidType(string type)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = type,
                Value = "01234567890"
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
            Assert.That(exception.Message, Does.Contain("Type"));
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
                Value = "01234567890"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Type"));
        }

        // Value field tests (RegularExpression - TelephoneNumber: 0\d{1,19})
        [TestCase("01234567890")]
        [TestCase("0123")]
        [TestCase("02071838750")]
        [TestCase("07")]
        [TestCase("0" + "1234567890123456789")] // Exactly 20 digits (0 + 19 digits)
        public void ItShouldAcceptValidValue(string value)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "MOBILE",
                Value = value
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            Assert.That(() => Validator.ValidateObject(phoneNumber, validationContext, true), Throws.Nothing);
        }

        [TestCase("123456789")] // Missing leading 0
        [TestCase("0")] // Too short
        [TestCase("12345678901234567890")] // Too long
        public void ItShouldRejectInvalidValue(string invalidValue)
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "MOBILE",
                Value = invalidValue
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Value"));
        }

        [Test]
        public void ItShouldRejectValueWithLetters()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "MOBILE",
                Value = "0123ABC456"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Value"));
        }

        [Test]
        public void ItShouldRejectNullValue()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "MOBILE",
                Value = null
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Value"));
        }

        [Test]
        public void ItShouldRejectEmptyValue()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "MOBILE",
                Value = ""
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(phoneNumber, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Value"));
        }

        [Test]
        public void ItShouldAcceptCompletePhoneNumber()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "MOBILE",
                Value = "07700900123"
            };

            var validationContext = new ValidationContext(phoneNumber, null, null);
            Assert.That(() => Validator.ValidateObject(phoneNumber, validationContext, true), Throws.Nothing);
        }
    }
}
