using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.ApplicantTests
{
    class WhenValidatingApplicant
    {
        private static ApplicantBasicDetails CreateValidBasicDetails()
        {
            return new ApplicantBasicDetails
            {
                Forename = "John",
                MiddleNames = "Michael",
                Surname = "Doe",
                Gender = "MALE",
                Title = "Mr",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                ApplicantType = "PRIMARY"
            };
        }

        private static ApplicantAddress CreateValidApplicantAddress()
        {
            return new ApplicantAddress
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
        }

        private static ApplicantEmployment CreateValidApplicantEmployment()
        {
            return new ApplicantEmployment
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
        }

        // BasicDetails field tests (Required, ValidateObject with failOnNull:true)
        [Test]
        public void ItShouldRejectWhenBasicDetailsIsNull()
        {
            var applicant = new Applicant
            {
                BasicDetails = null,
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicant, validationContext, true));
            Assert.That(exception.Message, Does.Contain("BasicDetails"));
        }

        [Test]
        public void ItShouldAcceptValidBasicDetails()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectInvalidBasicDetails()
        {
            var applicant = new Applicant
            {
                BasicDetails = new ApplicantBasicDetails
                {
                    Forename = "John",
                    MiddleNames = "Michael",
                    Surname = "Doe",
                    Gender = "INVALID_GENDER", // Invalid gender
                    Title = "Mr",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" } },
                    ApplicantType = "PRIMARY"
                },
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicant, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Gender"));
        }

        // MarketingOptIn field tests (Optional, ValidateObject with failOnNull:false)
        [Test]
        public void ItShouldAcceptNullMarketingOptIn()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptValidMarketingOptIn()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = new MarketingOptIn
                {
                    Email = true,
                    Sms = false,
                    Phone = true,
                    ThirdPartyReferral = null
                },
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        // AdditionalDetails field tests (Optional, ValidateObject with failOnNull:false)
        [Test]
        public void ItShouldAcceptNullAdditionalDetails()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptValidAdditionalDetails()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = new ApplicantAdditionalDetails
                {
                    MaritalStatus = "SINGLE",
                    LicenceType = "FULL UK",
                    ValidUkPassport = true,
                    OtherMonthlyIncome = 500
                },
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        // ApplicantAddress array field tests (ArrayCount max:10 min:0, ValidateObject with failOnNull:true)
        [Test]
        public void ItShouldAcceptEmptyApplicantAddressArray()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptSingleValidApplicantAddress()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new[] { CreateValidApplicantAddress() },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptMultipleValidApplicantAddresses()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new[]
                {
                    CreateValidApplicantAddress(),
                    new ApplicantAddress
                    {
                        NameNumber = "456",
                        Street = "High Street",
                        TownCity = "Manchester",
                        County = "Greater Manchester",
                        PostCode = "M1 1AE",
                        Years = 2,
                        Months = 3,
                        ResidentialStatus = "PRIVATE TENANT"
                    }
                },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectApplicantAddressArrayWhenExceedsMaxCount()
        {
            var addresses = new ApplicantAddress[11];
            for (int i = 0; i < 11; i++)
            {
                addresses[i] = CreateValidApplicantAddress();
            }

            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = addresses,
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicant, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ApplicantAddress"));
        }

        [Test]
        public void ItShouldRejectInvalidApplicantAddressInArray()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new[]
                {
                    new ApplicantAddress
                    {
                        NameNumber = new string('A', 51), // Too long
                        Street = "Main Street",
                        TownCity = "London",
                        County = "Greater London",
                        PostCode = "SW1A 1AA",
                        Years = 5,
                        Months = 6,
                        ResidentialStatus = "HOME OWNER"
                    }
                },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicant, validationContext, true));
            Assert.That(exception.Message, Does.Contain("NameNumber"));
        }

        // ApplicantEmployment array field tests (ArrayCount max:10 min:0, ValidateObject with failOnNull:true)
        [Test]
        public void ItShouldAcceptEmptyApplicantEmploymentArray()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptSingleValidApplicantEmployment()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new[] { CreateValidApplicantEmployment() }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptMultipleValidApplicantEmployments()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new[]
                {
                    CreateValidApplicantEmployment(),
                    new ApplicantEmployment
                    {
                        Occupation = "Manager",
                        EmployerName = "Another Company",
                        EmploymentStatus = "SELF EMPLOYED",
                        Telephone = "01234567890",
                        Years = 3,
                        Months = 9,
                        NetMonthlyIncome = 4000,
                        EmploymentAddress = null
                    }
                }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectApplicantEmploymentArrayWhenExceedsMaxCount()
        {
            var employments = new ApplicantEmployment[11];
            for (int i = 0; i < 11; i++)
            {
                employments[i] = CreateValidApplicantEmployment();
            }

            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = employments
            };

            var validationContext = new ValidationContext(applicant, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicant, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ApplicantEmployment"));
        }

        [Test]
        public void ItShouldRejectInvalidApplicantEmploymentInArray()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new[]
                {
                    new ApplicantEmployment
                    {
                        Occupation = new string('A', 101), // Too long
                        EmployerName = "Tech Solutions Ltd",
                        EmploymentStatus = "FULL TIME PERMANENT",
                        Telephone = "02071838750",
                        Years = 5,
                        Months = 6,
                        NetMonthlyIncome = 3500,
                        EmploymentAddress = null
                    }
                }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicant, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Occupation"));
        }

        // Complete Applicant tests
        [Test]
        public void ItShouldAcceptCompleteApplicantWithAllFields()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = new MarketingOptIn
                {
                    Email = true,
                    Sms = false,
                    Phone = true,
                    ThirdPartyReferral = null
                },
                AdditionalDetails = new ApplicantAdditionalDetails
                {
                    MaritalStatus = "SINGLE",
                    LicenceType = "FULL UK",
                    ValidUkPassport = true,
                    OtherMonthlyIncome = 500
                },
                ApplicantAddress = new[] { CreateValidApplicantAddress() },
                ApplicantEmployment = new[] { CreateValidApplicantEmployment() }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptApplicantWithOnlyRequiredFields()
        {
            var applicant = new Applicant
            {
                BasicDetails = CreateValidBasicDetails(),
                MarketingOptIn = null,
                AdditionalDetails = null,
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };

            var validationContext = new ValidationContext(applicant, null, null);
            Assert.That(() => Validator.ValidateObject(applicant, validationContext, true), Throws.Nothing);
        }
    }
}
