using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingApplicantMarketingOptIn
    {
        [Test]
        public void ItShouldAcceptAllNullValues()
        {
            var marketingOptIn = new MarketingOptIn
            {
                Email = null,
                Sms = null,
                Phone = null,
                ThirdPartyReferral = null
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidEmailOptIn(bool? email)
        {
            var marketingOptIn = new MarketingOptIn
            {
                Email = email
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidSmsOptIn(bool? sms)
        {
            var marketingOptIn = new MarketingOptIn
            {
                Sms = sms
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidPhoneOptIn(bool? phone)
        {
            var marketingOptIn = new MarketingOptIn
            {
                Phone = phone
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidThirdPartyReferralOptIn(bool? thirdPartyReferral)
        {
            var marketingOptIn = new MarketingOptIn
            {
                ThirdPartyReferral = thirdPartyReferral
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptAllTrueValues()
        {
            var marketingOptIn = new MarketingOptIn
            {
                Email = true,
                Sms = true,
                Phone = true,
                ThirdPartyReferral = true
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptAllFalseValues()
        {
            var marketingOptIn = new MarketingOptIn
            {
                Email = false,
                Sms = false,
                Phone = false,
                ThirdPartyReferral = false
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptMixedOptInValues()
        {
            var marketingOptIn = new MarketingOptIn
            {
                Email = true,
                Sms = false,
                Phone = null,
                ThirdPartyReferral = true
            };

            var validationContext = new ValidationContext(marketingOptIn, null, null);
            Assert.That(() => Validator.ValidateObject(marketingOptIn, validationContext, true), Throws.Nothing);
        }
    }
}
