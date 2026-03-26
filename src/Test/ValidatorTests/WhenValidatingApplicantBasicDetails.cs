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
        // Forename field
        [Test]
        public void ItShouldRejectFornameWhenEmpty()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "",
                MiddleNames = "Michael",
                Surname = "Jones",
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
        
        [TestCase("John!")]
        [TestCase("john@hotmail.com")]
        [TestCase("Juan237")]
        [TestCase("Mike_")]
        [TestCase("-Anna")]
        [TestCase("'Anna")]
        [TestCase("Anna-")]
        [TestCase("John--Paul")]
        [TestCase("O''Connor")]
        [TestCase("123456")]
        [TestCase("<script>")]
        [TestCase("test@email.com")]
        [TestCase("O''Connor")]
        [TestCase("O’’Connor")]
        [TestCase("O'’Connor")]
        [TestCase("John..Doe")]
        [TestCase("'John")]
        public void ItShouldRejectInvalidForname(string forename)
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
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Forename"));
        }
        
        // MiddleNames field
        [Test]
        public void ItShouldRejectMiddleNamesWhenEmpty()
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "",
                Surname = "Jones",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "01611234567" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("MiddleNames"));
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
        
        [TestCase("John!")]
        [TestCase("john@hotmail.com")]
        [TestCase("Juan237")]
        [TestCase("Mike_")]
        [TestCase("-Anna")]
        [TestCase("'Anna")]
        [TestCase("Anna-")]
        [TestCase("John--Paul")]
        [TestCase("O''Connor")]
        [TestCase("123456")]
        [TestCase("<script>")]
        [TestCase("test@email.com")]
        [TestCase("O''Connor")]
        [TestCase("O’’Connor")]
        [TestCase("O'’Connor")]
        [TestCase("John..Doe")]
        [TestCase("'John")]
        public void ItShouldRejectInvalidMiddleNames(string middleNames)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "Michael",
                MiddleNames = middleNames,
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
        
        // Surname field
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
        
        [TestCase("John!")]
        [TestCase("john@hotmail.com")]
        [TestCase("Juan237")]
        [TestCase("Mike_")]
        [TestCase("-Anna")]
        [TestCase("'Anna")]
        [TestCase("Anna-")]
        [TestCase("John--Paul")]
        [TestCase("O''Connor")]
        [TestCase("123456")]
        [TestCase("<script>")]
        [TestCase("test@email.com")]
        [TestCase("O''Connor")]
        [TestCase("O’’Connor")]
        [TestCase("O'’Connor")]
        [TestCase("John..Doe")]
        [TestCase("'John")]
        public void ItShouldRejectInvalidSurname(string surname)
        {
            var basicDetails = new ApplicantBasicDetails
            {
                Forename = "Michael",
                MiddleNames = "Jones",
                Surname = surname,
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "01611234567" } },
                ApplicantType = "PRIMARY"
            };

            var validationContext = new ValidationContext(basicDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(basicDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Surname"));
        }

        // All name fields
        [TestCase("John", "Smith", "Jones")]
        [TestCase("Jean-Pierre", "Marie-Claire", "Smith-Jones")]
        [TestCase("O'Hara", "D'Arcy", "O'Connor")]
        [TestCase("O’Hara", "D’Arcy", "O’Connor")]
        [TestCase("Mary Luise", "Anne Louise", "Smith Jones")]
        [TestCase("José", "María", "García")]
        [TestCase("Zoë", "Zoë", "Zoë")]
        [TestCase("Łukasz", "Łukasz", "Łukasz")]
        [TestCase("Søren", "Søren", "Søren")]
        [TestCase("Anna'", "Anna'", "Anna'")]
        [TestCase("Anna'    ", "Anna'    ", "Anna'    ")]
        [TestCase("John  Paul", "John  Paul", "John  Paul")]
        [TestCase("John    Paul", "John    Paul", "John    Paul")]
        [TestCase("J.R.R. Tolkien", "J.R.R. Tolkien", "J.R.R. Tolkien")]
        [TestCase("J. R. R. Tolkien", "J. R. R. Tolkien", "J. R. R. Tolkien")]
        [TestCase("Lewis Jr.", "Lewis Jr.", "Lewis Jr.")]
        [TestCase("Aaron", "Callum", "Booths")]
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
