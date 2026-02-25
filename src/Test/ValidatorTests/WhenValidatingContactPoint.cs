using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingContactPoint
    {
        [Test]
        public void ItShouldAcceptAllNullValues()
        {
            var contactPoint = new ContactPoint
            {
                Phone = null,
                Email = null,
                AdditionalPhoneNumber = null
            };

            var validationContext = new ValidationContext(contactPoint, null, null);
            Assert.That(() => Validator.ValidateObject(contactPoint, validationContext, true), Throws.Nothing);
        }

        [TestCase("0123456789")]
        [TestCase("+441234567890")]
        [TestCase("01234 567890")]
        [TestCase("0")]
        [TestCase("+")]
        [TestCase("_")]
        [TestCase("test@example.com")]
        [TestCase("test@example")]
        public void ItShouldAcceptValidPhone(string phone)
        {
            var contactPoint = new ContactPoint
            {
                Phone = phone
            };

            var validationContext = new ValidationContext(contactPoint, null, null);
            Assert.That(() => Validator.ValidateObject(contactPoint, validationContext, true), Throws.Nothing);
        }

        [TestCase("test@example.com")]
        [TestCase("user.name@example.co.uk")]
        [TestCase("email+tag@example.com")]
        [TestCase("test@example")]
        [TestCase("0")]
        [TestCase("+")]
        [TestCase("_")]
        public void ItShouldAcceptValidEmail(string email)
        {
            var contactPoint = new ContactPoint
            {
                Email = email
            };

            var validationContext = new ValidationContext(contactPoint, null, null);
            Assert.That(() => Validator.ValidateObject(contactPoint, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptNullEmail()
        {
            var contactPoint = new ContactPoint
            {
                Email = null
            };

            var validationContext = new ValidationContext(contactPoint, null, null);
            Assert.That(() => Validator.ValidateObject(contactPoint, validationContext, true), Throws.Nothing);
        }

        [TestCase("0987654321")]
        [TestCase("+441987654321")]
        [TestCase("07700 900000")]
        [TestCase("0")]
        [TestCase("+")]
        [TestCase("_")]
        [TestCase("test@example.com")]
        [TestCase("test@example")]
        public void ItShouldAcceptValidAdditionalPhoneNumber(string additionalPhoneNumber)
        {
            var contactPoint = new ContactPoint
            {
                AdditionalPhoneNumber = additionalPhoneNumber
            };

            var validationContext = new ValidationContext(contactPoint, null, null);
            Assert.That(() => Validator.ValidateObject(contactPoint, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptCompleteContactPoint()
        {
            var contactPoint = new ContactPoint
            {
                Phone = "0123456789",
                Email = "test@example.com",
                AdditionalPhoneNumber = "0987654321"
            };

            var validationContext = new ValidationContext(contactPoint, null, null);
            Assert.That(() => Validator.ValidateObject(contactPoint, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptPartialContactPoint()
        {
            var contactPoint = new ContactPoint
            {
                Phone = "0123456789",
                Email = null,
                AdditionalPhoneNumber = null
            };

            var validationContext = new ValidationContext(contactPoint, null, null);
            Assert.That(() => Validator.ValidateObject(contactPoint, validationContext, true), Throws.Nothing);
        }
    }
}
