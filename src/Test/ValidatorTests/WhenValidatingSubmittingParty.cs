using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingSubmittingParty
    {
        // Name field tests (StringLengthRange 1-50, no regex)
        [Test]
        public void ItShouldAcceptWhenNameIsNull()
        {
            var submittingParty = new SubmittingParty
            {
                Name = null,
                Code = "CODE123",
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectWhenNameIsEmpty()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "",
                Code = "CODE123",
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Name"));
        }

        [Test]
        public void ItShouldRejectWhenNameIsTooLong()
        {
            var submittingParty = new SubmittingParty
            {
                Name = new string('A', 51),
                Code = "CODE123",
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Name"));
        }

        [TestCase("John Smith")]
        [TestCase("Mary-Jane")]
        [TestCase("Company123")]
        [TestCase("Name_With_Underscore")]
        [TestCase("Multi Word Name")]
        public void ItShouldAcceptNameWithValidCharacters(string name)
        {
            var submittingParty = new SubmittingParty
            {
                Name = name,
                Code = "CODE123",
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        // Code field tests (StringLengthRange 1-100, no regex)
        [Test]
        public void ItShouldAcceptNullCode()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = null,
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectWhenCodeIsEmpty()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "",
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Code"));
        }

        [Test]
        public void ItShouldRejectWhenCodeIsTooLong()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = new string('A', 101),
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Code"));
        }

        [TestCase("CODE123")]
        [TestCase("Code_With_Underscore")]
        [TestCase("Code-With-Dash")]
        [TestCase("Code With Space")]
        public void ItShouldAcceptValidCode(string code)
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = code,
                Reference = "REF001"
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        // Reference field tests (StringLengthRange 1-100, no regex)
        [Test]
        public void ItShouldAcceptNullReference()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = null
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectWhenReferenceIsEmpty()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = ""
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Reference"));
        }

        [Test]
        public void ItShouldRejectWhenReferenceIsTooLong()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = new string('A', 101)
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Reference"));
        }

        [TestCase("REF001")]
        [TestCase("Reference_With_Underscore")]
        [TestCase("Reference-With-Dash")]
        [TestCase("Reference With Space")]
        public void ItShouldAcceptValidReference(string reference)
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = reference
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        // Source field tests (StringLengthRange 1-255, AlphaNumericWithUnderscoreAndDashAndSpace regex)
        [Test]
        public void ItShouldAcceptNullSource()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Source = null
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectWhenSourceIsEmpty()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Source = ""
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Source"));
        }

        [Test]
        public void ItShouldRejectWhenSourceIsTooLong()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Source = new string('A', 256)
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Source"));
        }

        [TestCase("Source123")]
        [TestCase("Source_With_Underscore")]
        [TestCase("Source-With-Dash")]
        [TestCase("Source With Space")]
        public void ItShouldAcceptValidSource(string source)
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Source = source
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [TestCase("Source@Invalid")]
        [TestCase("Source!")]
        [TestCase("Source#")]
        [TestCase("Source$")]
        public void ItShouldRejectWhenSourceHasInvalidCharacters(string source)
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Source = source
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Source"));
        }

        // Campaign field tests (StringLength 255, AlphaNumericWithUnderscoreAndDashAndSpace regex)
        [Test]
        public void ItShouldAcceptNullCampaign()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Campaign = null
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectWhenCampaignIsTooLong()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Campaign = new string('A', 256)
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Campaign"));
        }

        [TestCase("Campaign123")]
        [TestCase("Campaign_With_Underscore")]
        [TestCase("Campaign-With-Dash")]
        [TestCase("Campaign With Space")]
        public void ItShouldAcceptValidCampaign(string campaign)
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Campaign = campaign
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [TestCase("Campaign@Invalid")]
        [TestCase("Campaign!")]
        [TestCase("Campaign#")]
        [TestCase("Campaign$")]
        public void ItShouldRejectWhenCampaignHasInvalidCharacters(string campaign)
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Campaign = campaign
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Campaign"));
        }

        // Gclid field tests (StringLengthRange 0-100, no regex)
        [Test]
        public void ItShouldAcceptNullGclid()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Gclid = null
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptEmptyGclid()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Gclid = ""
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectWhenGclidIsTooLong()
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Gclid = new string('A', 101)
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(submittingParty, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Gclid"));
        }

        [TestCase("GCLID123")]
        [TestCase("Gclid_With_Underscore")]
        [TestCase("Gclid-With-Dash")]
        [TestCase("Gclid With Space")]
        [TestCase("Gclid@Special!")]
        public void ItShouldAcceptValidGclid(string gclid)
        {
            var submittingParty = new SubmittingParty
            {
                Name = "Valid Name",
                Code = "CODE123",
                Reference = "REF001",
                Gclid = gclid
            };

            var validationContext = new ValidationContext(submittingParty, null, null);
            Assert.That(() => Validator.ValidateObject(submittingParty, validationContext, true), Throws.Nothing);
        }
    }
}
