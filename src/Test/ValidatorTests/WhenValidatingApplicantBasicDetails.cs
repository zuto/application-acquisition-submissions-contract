using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingApplicantBasicDetails
    {
        
        [TestCase("John")]
        [TestCase("John!")]
        [TestCase("john@hotmail.com")]
        [TestCase("Juan237")]
        [TestCase(null)]
        public void ItShouldAcceptValidForname(string forename)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = forename,
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Forename"));
        }

        [TestCase("Michael James")]
        [TestCase("Michael James!")]
        [TestCase("mj@hotmail.com")]
        [TestCase("Michael456")]
        [TestCase(null)]
        public void ItShouldAcceptValidMiddleNames(string middlenames)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = middlenames,
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("MiddleNames"));
        }

        [TestCase("Smith")]
        [TestCase("Philpott-Smith")]
        [TestCase("Philpott Smith")]
        [TestCase("O'Hara")]
        [TestCase("mj@hotmail.com")]
        [TestCase("Philpott456")]
        [TestCase(null)]
        public void ItShouldAcceptValidSurname(string surname)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = surname,
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }
        
        [Test]
        public void ItShouldRejectSurnameWhenEmpty()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Surname"));
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
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
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
                ApplicantType = null
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ApplicantType"));
        }
        
        // DOB
        [Test]
        public void ItShouldRejectWhenApplicantIsUnder18yoDOB()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = DateTime.Today.AddYears(-17),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain($"Applicant must be at least 18 years old."));
        }
        
        [Test]
        public void ItShouldAcceptWhenApplicantIs18yoDOB()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Smith",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = DateTime.Today.AddYears(-18),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "07123456789" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            Assert.That(() => Validator.ValidateObject(basicDetails, validationContext, true), Throws.Nothing);
        }
    }
}
