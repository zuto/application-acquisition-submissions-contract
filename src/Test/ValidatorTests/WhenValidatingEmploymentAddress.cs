using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.EmploymentAddressTests
{
    class WhenValidatingEmploymentAddress
    {
        [Test]
        public void ItShouldAcceptAllNullValues()
        {
            var employmentAddress = new EmploymentAddress
            {
                NameNumber = null,
                Street = null,
                TownCity = null,
                County = null,
                PostCode = null
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        // NameNumber field tests (StringLengthRange 1-50)
        [TestCase("1")]
        [TestCase("123")]
        [TestCase("123A")]
        [TestCase("Flat 5, 123 Main Street")]
        public void ItShouldAcceptValidNameNumber(string nameNumber)
        {
            var employmentAddress = new EmploymentAddress
            {
                NameNumber = nameNumber
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectNameNumberWhenTooLong()
        {
            var employmentAddress = new EmploymentAddress
            {
                NameNumber = new string('A', 51)
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NameNumber"));
        }

        [Test]
        public void ItShouldRejectNameNumberWhenEmpty()
        {
            var employmentAddress = new EmploymentAddress
            {
                NameNumber = ""
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NameNumber"));
        }

        [Test]
        public void ItShouldAcceptNullNameNumber()
        {
            var employmentAddress = new EmploymentAddress
            {
                NameNumber = null
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        // Street field tests (StringLengthRange 1-100)
        [TestCase("Main Street")]
        [TestCase("123 High Street")]
        [TestCase("Oxford Road, Building A")]
        public void ItShouldAcceptValidStreet(string street)
        {
            var employmentAddress = new EmploymentAddress
            {
                Street = street
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectStreetWhenTooLong()
        {
            var employmentAddress = new EmploymentAddress
            {
                Street = new string('A', 101)
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Street"));
        }

        [Test]
        public void ItShouldRejectStreetWhenEmpty()
        {
            var employmentAddress = new EmploymentAddress
            {
                Street = ""
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Street"));
        }

        [Test]
        public void ItShouldAcceptNullStreet()
        {
            var employmentAddress = new EmploymentAddress
            {
                Street = null
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        // TownCity field tests (StringLengthRange 1-100)
        [TestCase("London")]
        [TestCase("Manchester")]
        [TestCase("Newcastle upon Tyne")]
        public void ItShouldAcceptValidTownCity(string townCity)
        {
            var employmentAddress = new EmploymentAddress
            {
                TownCity = townCity
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectTownCityWhenTooLong()
        {
            var employmentAddress = new EmploymentAddress
            {
                TownCity = new string('A', 101)
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("TownCity"));
        }

        [Test]
        public void ItShouldRejectTownCityWhenEmpty()
        {
            var employmentAddress = new EmploymentAddress
            {
                TownCity = ""
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("TownCity"));
        }

        [Test]
        public void ItShouldAcceptNullTownCity()
        {
            var employmentAddress = new EmploymentAddress
            {
                TownCity = null
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        // County field tests (StringLengthRange 1-100)
        [TestCase("Greater London")]
        [TestCase("Greater Manchester")]
        [TestCase("West Yorkshire")]
        public void ItShouldAcceptValidCounty(string county)
        {
            var employmentAddress = new EmploymentAddress
            {
                County = county
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectCountyWhenTooLong()
        {
            var employmentAddress = new EmploymentAddress
            {
                County = new string('A', 101)
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("County"));
        }

        [Test]
        public void ItShouldRejectCountyWhenEmpty()
        {
            var employmentAddress = new EmploymentAddress
            {
                County = ""
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("County"));
        }

        [Test]
        public void ItShouldAcceptNullCounty()
        {
            var employmentAddress = new EmploymentAddress
            {
                County = null
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        // PostCode field tests (StringLengthRange 1-20)
        [TestCase("SW1A 1AA")]
        [TestCase("M1 1AE")]
        [TestCase("B33 8TH")]
        [TestCase("CR2 6XH")]
        public void ItShouldAcceptValidPostCode(string postCode)
        {
            var employmentAddress = new EmploymentAddress
            {
                PostCode = postCode
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectPostCodeWhenTooLong()
        {
            var employmentAddress = new EmploymentAddress
            {
                PostCode = new string('A', 21)
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("PostCode"));
        }

        [Test]
        public void ItShouldRejectPostCodeWhenEmpty()
        {
            var employmentAddress = new EmploymentAddress
            {
                PostCode = ""
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(employmentAddress, validationContext, true));
            Assert.That(exception.Message, Does.Contain("PostCode"));
        }

        [Test]
        public void ItShouldAcceptNullPostCode()
        {
            var employmentAddress = new EmploymentAddress
            {
                PostCode = null
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptCompleteEmploymentAddress()
        {
            var employmentAddress = new EmploymentAddress
            {
                NameNumber = "123",
                Street = "Main Street",
                TownCity = "London",
                County = "Greater London",
                PostCode = "SW1A 1AA"
            };

            var validationContext = new ValidationContext(employmentAddress, null, null);
            Assert.That(() => Validator.ValidateObject(employmentAddress, validationContext, true), Throws.Nothing);
        }
    }
}

