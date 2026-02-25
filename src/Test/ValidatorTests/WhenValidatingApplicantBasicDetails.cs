using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingApplicantBasicDetails
    {
        [Test]
        public void ItShouldAcceptValidForename()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptFornameWithNumbers()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John123",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptFornameWithSpecialCharacters()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John!",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectFornameWhenTooLong()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = new string('A', 51),
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Forename"));
        }

        [Test]
        public void ItShouldAcceptValidMiddleNames()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael James",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptMiddleNamesWithNumbers()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael456",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptMiddleNamesWithSpecialCharacters()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael&",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectMiddleNamesWhenTooLong()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = new string('A', 101),
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("MiddleNames"));
        }

        [Test]
        public void ItShouldAcceptValidSurname()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptSurnameWithNumbers()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith789",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptSurnameWithSpecialCharacters()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith%",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectSurnameWhenTooLong()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = new string('A', 51),
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Surname"));
        }

        [TestCase("Jean-Pierre", "Marie-Claire", "O'Connor")]
        [TestCase("Mary", "Anne Louise", "Smith-Jones")]
        [TestCase("José", "María", "García")]
        [TestCase("John123", "Michael456", "Smith789")]
        public void ItShouldAcceptNamesWithValidCharacters(string forename, string middleNames, string surname)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = forename,
                MiddleNames = middleNames,
                Surname = surname,
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        // Gender field tests (AllowedValuesValidation: "MALE", "FEMALE")
        [TestCase("MALE")]
        [TestCase("FEMALE")]
        public void ItShouldAcceptValidGender(string gender)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = gender,
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase("Male")]
        [TestCase("Female")]
        [TestCase("OTHER")]
        [TestCase("UNKNOWN")]
        public void ItShouldRejectInvalidGender(string gender)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = gender,
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Gender"));
        }

        [Test]
        public void ItShouldRejectNullGender()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = null,
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Gender"));
        }

        // Title field tests (AllowedValuesValidation: "Mr", "Miss", "Mrs", "Ms")
        [TestCase("Mr")]
        [TestCase("Miss")]
        [TestCase("Mrs")]
        [TestCase("Ms")]
        public void ItShouldAcceptValidTitle(string title)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = title,
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase("MR")]
        [TestCase("MISS")]
        [TestCase("MRS")]
        [TestCase("MS")]
        [TestCase("Dr")]
        [TestCase("Prof")]
        public void ItShouldRejectInvalidTitle(string title)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = title,
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Title"));
        }

        [Test]
        public void ItShouldRejectNullTitle()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = null,
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Title"));
        }

        // ApplicantType field tests (AllowedValuesValidation: "PRIMARY", "JOINT")
        [TestCase("PRIMARY")]
        [TestCase("JOINT")]
        public void ItShouldAcceptValidApplicantType(string applicantType)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = applicantType
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase("Primary")]
        [TestCase("Joint")]
        [TestCase("SECONDARY")]
        [TestCase("TERTIARY")]
        public void ItShouldRejectInvalidApplicantType(string applicantType)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = applicantType
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ApplicantType"));
        }

        [Test]
        public void ItShouldRejectNullApplicantType()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = null
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ApplicantType"));
        }
    }
}
