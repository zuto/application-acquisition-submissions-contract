using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingDealer
    {
        [Test]
        public void ItShouldAcceptAllNullValues()
        {
            var dealer = new Dealer
            {
                Id = null,
                Name = null
            };

            var validationContext = new ValidationContext(dealer, null, null);
            Assert.That(() => Validator.ValidateObject(dealer, validationContext, true), Throws.Nothing);
        }

        [TestCase("DEALER001")]
        [TestCase("D123")]
        [TestCase("dealer-id-456")]
        public void ItShouldAcceptValidId(string id)
        {
            var dealer = new Dealer
            {
                Id = id
            };

            var validationContext = new ValidationContext(dealer, null, null);
            Assert.That(() => Validator.ValidateObject(dealer, validationContext, true), Throws.Nothing);
        }

        [TestCase("ABC Motors")]
        [TestCase("Smith & Sons Dealership")]
        [TestCase("Premier Auto Sales")]
        public void ItShouldAcceptValidName(string name)
        {
            var dealer = new Dealer
            {
                Name = name
            };

            var validationContext = new ValidationContext(dealer, null, null);
            Assert.That(() => Validator.ValidateObject(dealer, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptCompleteDealer()
        {
            var dealer = new Dealer
            {
                Id = "DEALER001",
                Name = "ABC Motors"
            };

            var validationContext = new ValidationContext(dealer, null, null);
            Assert.That(() => Validator.ValidateObject(dealer, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptDealerWithOnlyId()
        {
            var dealer = new Dealer
            {
                Id = "DEALER001",
                Name = null
            };

            var validationContext = new ValidationContext(dealer, null, null);
            Assert.That(() => Validator.ValidateObject(dealer, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptDealerWithOnlyName()
        {
            var dealer = new Dealer
            {
                Id = null,
                Name = "ABC Motors"
            };

            var validationContext = new ValidationContext(dealer, null, null);
            Assert.That(() => Validator.ValidateObject(dealer, validationContext, true), Throws.Nothing);
        }
    }
}
