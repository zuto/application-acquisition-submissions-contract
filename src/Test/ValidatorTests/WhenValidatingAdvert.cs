using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingAdvert
    {
        [Test]
        public void ItShouldAcceptNullUrl()
        {
            var advert = new Advert
            {
                Url = null
            };

            var validationContext = new ValidationContext(advert, null, null);
            Assert.That(() => Validator.ValidateObject(advert, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptEmptyUrl()
        {
            var advert = new Advert
            {
                Url = ""
            };

            var validationContext = new ValidationContext(advert, null, null);
            Assert.That(() => Validator.ValidateObject(advert, validationContext, true), Throws.Nothing);
        }

        [TestCase("https://example.com")]
        [TestCase("http://www.example.com/path")]
        [TestCase("ftp://files.example.com")]
        [TestCase("https://example.com?param=value")]
        public void ItShouldAcceptValidUrl(string url)
        {
            var advert = new Advert
            {
                Url = url
            };

            var validationContext = new ValidationContext(advert, null, null);
            Assert.That(() => Validator.ValidateObject(advert, validationContext, true), Throws.Nothing);
        }
    }
}
