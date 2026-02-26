using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.ApplicantEmploymentTests
{
    class WhenValidatingApplicantEmployment
    {
        private static readonly string OccupationOver100 = new string('A', 101);
        private static readonly string EmployerNameOver100 = new string('A', 101);

        [Test]
        public void ItShouldAcceptMinimalValidValues()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Occupation = null,
                EmployerName = null,
                EmploymentStatus = "UNEMPLOYED",
                Telephone = null,
                Years = 0,
                Months = 0,
                NetMonthlyIncome = null,
                EmploymentAddress = null
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        // Occupation field tests (StringLengthRange 1-100)
        [TestCase("Software Developer")]
        [TestCase("Manager")]
        [TestCase("A")]
        [TestCase("Senior Software Engineer with specialization")]
        public void ItShouldAcceptValidOccupation(string occupation)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Occupation = occupation,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectOccupationWhenTooLong()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Occupation = OccupationOver100,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Occupation"));
        }

        // EmployerName field tests (StringLengthRange 1-100)
        [TestCase("Acme Corporation")]
        [TestCase("Tech Solutions Ltd")]
        [TestCase("X")]
        [TestCase("International Business Machines Corporation")]
        public void ItShouldAcceptValidEmployerName(string employerName)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                EmployerName = employerName,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectEmployerNameWhenTooLong()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                EmployerName = EmployerNameOver100,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("EmployerName"));
        }

        // EmploymentStatus field tests (AllowedValuesValidation)
        [TestCase("AGENCY")]
        [TestCase("FULL TIME PERMANENT")]
        [TestCase("PART TIME")]
        [TestCase("RETIRED")]
        [TestCase("SELF EMPLOYED")]
        [TestCase("STUDENT")]
        [TestCase("SUB CONTRACT")]
        [TestCase("CARER")]
        [TestCase("DISABILITY")]
        [TestCase("TEMPORARY")]
        [TestCase("UNEMPLOYED")]
        public void ItShouldAcceptValidEmploymentStatus(string employmentStatus)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                EmploymentStatus = employmentStatus
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectInvalidEmploymentStatus()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                EmploymentStatus = "INVALID_STATUS"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("EmploymentStatus"));
        }

        // Telephone field tests (RegularExpression - TelephoneNumber: 0\d{1,19})
        [TestCase("01234567890")]
        [TestCase("0123")]
        [TestCase("02071838750")]
        [TestCase("07")]
        public void ItShouldAcceptValidTelephone(string telephone)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Telephone = telephone,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }
        
        [TestCase("123456789")] // Missing leading 0
        [TestCase("0")] // too short
        [TestCase("12345678901234567890")] // too long
        public void ItShouldRejectInvalidTelephone(string invalidTelephone)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Telephone = invalidTelephone,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Telephone"));
        }

        [Test]
        public void ItShouldRejectTelephoneWithLetters()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Telephone = "0123ABC456",
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Telephone"));
        }

        // Years field tests (IntegerRange 0-100)
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void ItShouldAcceptValidYears(int years)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Years = years,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectYearsWhenNegative()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Years = -1,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Years"));
        }

        [Test]
        public void ItShouldRejectYearsWhenTooHigh()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Years = 101,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Years"));
        }

        // Months field tests (IntegerRange 0-11)
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(6)]
        [TestCase(11)]
        public void ItShouldAcceptValidMonths(int months)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Months = months,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectMonthsWhenNegative()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Months = -1,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Months"));
        }

        [Test]
        public void ItShouldRejectMonthsWhenTooHigh()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Months = 12,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Months"));
        }

        // NetMonthlyIncome field tests (MoneyRange 0-99999999, nullable)
        [TestCase(0)]
        [TestCase(1000)]
        [TestCase(50000)]
        [TestCase(99999999)]
        public void ItShouldAcceptValidNetMonthlyIncome(decimal income)
        {
            var applicantEmployment = new ApplicantEmployment
            {
                NetMonthlyIncome = income,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectNetMonthlyIncomeWhenNegative()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                NetMonthlyIncome = -1,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NetMonthlyIncome"));
        }

        [Test]
        public void ItShouldRejectNetMonthlyIncomeWhenTooHigh()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                NetMonthlyIncome = 100000000,
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NetMonthlyIncome"));
        }

        // EmploymentAddress field tests (ValidateObject, optional)
        [Test]
        public void ItShouldAcceptValidEmploymentAddress()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                EmploymentAddress = new EmploymentAddress
                {
                    NameNumber = "123",
                    Street = "Main Street",
                    TownCity = "London",
                    County = "Greater London",
                    PostCode = "SW1A 1AA"
                },
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectInvalidEmploymentAddress()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                EmploymentAddress = new EmploymentAddress
                {
                    NameNumber = new string('A', 51) // Too long
                },
                EmploymentStatus = "UNEMPLOYED"
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicantEmployment, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NameNumber"));
        }

        [Test]
        public void ItShouldAcceptCompleteApplicantEmployment()
        {
            var applicantEmployment = new ApplicantEmployment
            {
                Occupation = "Software Developer",
                EmployerName = "Tech Solutions Ltd",
                EmploymentStatus = "FULL TIME PERMANENT",
                Telephone = "02071838750",
                Years = 5,
                Months = 6,
                NetMonthlyIncome = 3500,
                EmploymentAddress = new EmploymentAddress
                {
                    NameNumber = "123",
                    Street = "Main Street",
                    TownCity = "London",
                    County = "Greater London",
                    PostCode = "SW1A 1AA"
                }
            };

            var validationContext = new ValidationContext(applicantEmployment, null, null);
            Assert.That(() => Validator.ValidateObject(applicantEmployment, validationContext, true), Throws.Nothing);
        }
    }
}
