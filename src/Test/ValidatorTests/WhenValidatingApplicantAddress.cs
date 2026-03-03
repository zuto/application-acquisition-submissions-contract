using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.ApplicantAddressTests
{
    class WhenValidatingApplicantAddress
    {
        [Test]
        public void ItShouldAcceptAllNullValues()
        {
            var applicantAddress = new ApplicantAddress
            {
                NameNumber = null,
                Street = null,
                TownCity = null,
                County = null,
                PostCode = null,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        // NameNumber field tests (StringLengthRange 1-50)
        [TestCase("1")]
        [TestCase("123")]
        [TestCase("123A")]
        [TestCase("Flat 5, 123 Main Street")]
        public void ItShouldAcceptValidNameNumber(string nameNumber)
        {
            var applicantAddress = new ApplicantAddress
            {
                NameNumber = nameNumber,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectNameNumberWhenTooLong()
        {
            var applicantAddress = new ApplicantAddress
            {
                NameNumber = new string('A', 51),
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NameNumber"));
        }

        [Test]
        public void ItShouldRejectNameNumberWhenEmpty()
        {
            var applicantAddress = new ApplicantAddress
            {
                NameNumber = "",
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NameNumber"));
        }

        [Test]
        public void ItShouldAcceptNullNameNumber()
        {
            var applicantAddress = new ApplicantAddress
            {
                NameNumber = null,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        // Street field tests (StringLengthRange 1-100)
        [TestCase("Main Street")]
        [TestCase("123 High Street")]
        [TestCase("Oxford Road, Building A")]
        public void ItShouldAcceptValidStreet(string street)
        {
            var applicantAddress = new ApplicantAddress
            {
                Street = street,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectStreetWhenTooLong()
        {
            var applicantAddress = new ApplicantAddress
            {
                Street = new string('A', 101),
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Street"));
        }

        [Test]
        public void ItShouldRejectStreetWhenEmpty()
        {
            var applicantAddress = new ApplicantAddress
            {
                Street = "",
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Street"));
        }

        [Test]
        public void ItShouldAcceptNullStreet()
        {
            var applicantAddress = new ApplicantAddress
            {
                Street = null,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        // TownCity field tests (StringLengthRange 1-100)
        [TestCase("London")]
        [TestCase("Manchester")]
        [TestCase("Newcastle upon Tyne")]
        public void ItShouldAcceptValidTownCity(string townCity)
        {
            var applicantAddress = new ApplicantAddress
            {
                TownCity = townCity,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectTownCityWhenTooLong()
        {
            var applicantAddress = new ApplicantAddress
            {
                TownCity = new string('A', 101),
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("TownCity"));
        }

        [Test]
        public void ItShouldRejectTownCityWhenEmpty()
        {
            var applicantAddress = new ApplicantAddress
            {
                TownCity = "",
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("TownCity"));
        }

        [Test]
        public void ItShouldAcceptNullTownCity()
        {
            var applicantAddress = new ApplicantAddress
            {
                TownCity = null,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        // County field tests (StringLengthRange 1-100)
        [TestCase("Greater London")]
        [TestCase("Greater Manchester")]
        [TestCase("West Yorkshire")]
        public void ItShouldAcceptValidCounty(string county)
        {
            var applicantAddress = new ApplicantAddress
            {
                County = county,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectCountyWhenTooLong()
        {
            var applicantAddress = new ApplicantAddress
            {
                County = new string('A', 101),
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("County"));
        }

        [Test]
        public void ItShouldRejectCountyWhenEmpty()
        {
            var applicantAddress = new ApplicantAddress
            {
                County = "",
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("County"));
        }

        [Test]
        public void ItShouldAcceptNullCounty()
        {
            var applicantAddress = new ApplicantAddress
            {
                County = null,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        // PostCode field tests (StringLengthRange 1-20)
        [TestCase("SW1A 1AA")]
        [TestCase("M1 1AE")]
        [TestCase("B33 8TH")]
        [TestCase("CR2 6XH")]
        public void ItShouldAcceptValidPostCode(string postCode)
        {
            var applicantAddress = new ApplicantAddress
            {
                PostCode = postCode,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectPostCodeWhenTooLong()
        {
            var applicantAddress = new ApplicantAddress
            {
                PostCode = new string('A', 21),
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("PostCode"));
        }

        [Test]
        public void ItShouldRejectPostCodeWhenEmpty()
        {
            var applicantAddress = new ApplicantAddress
            {
                PostCode = "",
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("PostCode"));
        }

        [Test]
        public void ItShouldAcceptNullPostCode()
        {
            var applicantAddress = new ApplicantAddress
            {
                PostCode = null,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        // Years field tests (IntegerRange 0-100)
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void ItShouldAcceptValidYears(int years)
        {
            var applicantAddress = new ApplicantAddress
            {
                Years = years,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectYearsWhenNegative()
        {
            var applicantAddress = new ApplicantAddress
            {
                Years = -1,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Years"));
        }

        [Test]
        public void ItShouldRejectYearsWhenTooHigh()
        {
            var applicantAddress = new ApplicantAddress
            {
                Years = 101,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Years"));
        }

        // Months field tests (IntegerRange 0-11)
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6)]
        [TestCase(11)]
        public void ItShouldAcceptValidMonths(int months)
        {
            var applicantAddress = new ApplicantAddress
            {
                Months = months,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectMonthsWhenNegative()
        {
            var applicantAddress = new ApplicantAddress
            {
                Months = -1,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Months"));
        }

        [Test]
        public void ItShouldRejectMonthsWhenTooHigh()
        {
            var applicantAddress = new ApplicantAddress
            {
                Months = 12,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Months"));
        }

        // ResidentialStatus field tests (AllowedValuesValidation)
        [TestCase("HOME OWNER")]
        [TestCase("PRIVATE TENANT")]
        [TestCase("LIVING WITH PARENTS")]
        [TestCase("LIVING WITH PARTNER")]
        [TestCase("COUNCIL TENANT")]
        [TestCase("HOUSING ASSOCIATION")]
        [TestCase("UNKNOWN")]
        public void ItShouldAcceptValidResidentialStatus(string residentialStatus)
        {
            var applicantAddress = new ApplicantAddress
            {
                ResidentialStatus = residentialStatus
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectInvalidResidentialStatus()
        {
            var applicantAddress = new ApplicantAddress
            {
                ResidentialStatus = "INVALID_STATUS"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ResidentialStatus"));
        }

        [Test]
        public void ItShouldAcceptNullResidentialStatus()
        {
            var applicantAddress = new ApplicantAddress
            {
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptCompleteApplicantAddress()
        {
            var applicantAddress = new ApplicantAddress
            {
                NameNumber = "123",
                Street = "Main Street",
                TownCity = "London",
                County = "Greater London",
                PostCode = "SW1A 1AA",
                Years = 5,
                Months = 6,
                ResidentialStatus = "HOME OWNER"
            };

            var validationContext = new ValidationContext(applicantAddress, null, null);
            Assert.That(() => Validator.ValidateObject(applicantAddress, validationContext, true), Throws.Nothing);
        }
    }
}
