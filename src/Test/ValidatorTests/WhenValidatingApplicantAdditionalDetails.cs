using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingApplicantAdditionalDetails
    {
        // MaritalStatus field tests (AllowedValuesValidation: "MARRIED", "COHABITING", "LIVING WITH PARTNER", "SINGLE", "SEPARATED", "DIVORCED", "WIDOWED", "UNKNOWN")
        [TestCase("MARRIED")]
        [TestCase("COHABITING")]
        [TestCase("LIVING WITH PARTNER")]
        [TestCase("SINGLE")]
        [TestCase("SEPARATED")]
        [TestCase("DIVORCED")]
        [TestCase("WIDOWED")]
        [TestCase("UNKNOWN")]
        public void ItShouldAcceptValidMaritalStatus(string maritalStatus)
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = maritalStatus,
                LicenceType = "FULL UK"
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            Assert.That(() => Validator.ValidateObject(additionalDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase("Married")]
        [TestCase("Single")]
        [TestCase("ENGAGED")]
        [TestCase("DOMESTIC PARTNERSHIP")]
        public void ItShouldRejectInvalidMaritalStatus(string maritalStatus)
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = maritalStatus,
                LicenceType = "FULL UK"
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(additionalDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("MaritalStatus"));
        }

        [Test]
        public void ItShouldRejectNullMaritalStatus()
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = null,
                LicenceType = "FULL UK"
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(additionalDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("MaritalStatus"));
        }

        // LicenceType field tests (AllowedValuesValidation: "FULL UK", "PROVISIONAL UK", "EUROPEAN", "INTERNATIONAL", "NONE", "CBT", "A2", "FULL A CLASS", "UNKNOWN")
        [TestCase("FULL UK")]
        [TestCase("PROVISIONAL UK")]
        [TestCase("EUROPEAN")]
        [TestCase("INTERNATIONAL")]
        [TestCase("NONE")]
        [TestCase("CBT")]
        [TestCase("A2")]
        [TestCase("FULL A CLASS")]
        [TestCase("UNKNOWN")]
        public void ItShouldAcceptValidLicenceType(string licenceType)
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = licenceType
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            Assert.That(() => Validator.ValidateObject(additionalDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase("Full UK")]
        [TestCase("Provisional UK")]
        [TestCase("FULL")]
        [TestCase("PROVISIONAL")]
        [TestCase("A1")]
        [TestCase("B")]
        public void ItShouldRejectInvalidLicenceType(string licenceType)
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = licenceType
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(additionalDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("LicenceType"));
        }

        [Test]
        public void ItShouldRejectNullLicenceType()
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = null
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(additionalDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("LicenceType"));
        }

        // ValidUkPassport field tests (nullable bool, no validation)
        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidUkPassport(bool validUkPassport)
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = "FULL UK",
                ValidUkPassport = validUkPassport
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            Assert.That(() => Validator.ValidateObject(additionalDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptNullValidUkPassport()
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = "FULL UK",
                ValidUkPassport = null
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
             Assert.That(() => Validator.ValidateObject(additionalDetails, validationContext, true), Throws.Nothing);
        }

        // OtherMonthlyIncome field tests (MoneyRange 0.01-99999999)
        [TestCase(0.01)]
        [TestCase(100.00)]
        [TestCase(99999999.00)]
        public void ItShouldAcceptValidOtherMonthlyIncome(decimal income)
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = "FULL UK",
                OtherMonthlyIncome = income
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            Assert.That(() => Validator.ValidateObject(additionalDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase(0)]
        [TestCase(0.00)]
        [TestCase(100000000)]
        public void ItShouldRejectInvalidOtherMonthlyIncome(decimal income)
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = "FULL UK",
                OtherMonthlyIncome = income
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(additionalDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("OtherMonthlyIncome"));
        }

        [Test]
        public void ItShouldAcceptNullOtherMonthlyIncome()
        {
            var additionalDetails = new ApplicantAdditionalDetails
            {
                MaritalStatus = "UNKNOWN",
                LicenceType = "FULL UK",
                OtherMonthlyIncome = null
            };

            var validationContext = new ValidationContext(additionalDetails, null, null);
            Assert.That(() => Validator.ValidateObject(additionalDetails, validationContext, true), Throws.Nothing);
        }
    }
}
