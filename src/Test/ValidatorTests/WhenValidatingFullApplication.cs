using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.FullApplicationTests
{
    class WhenValidatingFullApplication
    {
        private static Applicant CreateValidApplicant(string title = "Mr", string forename = "John", string surname = "Doe", string gender = "MALE")
        {
            return new Applicant
            {
                BasicDetails = new ApplicantBasicDetails
                {
                    Title = title,
                    Forename = forename,
                    Surname = surname,
                    Gender = gender,
                    ApplicantType = "PRIMARY",
                    PhoneNumbers = new[] { new PhoneNumber { Value = "01234567890", Type = "MOBILE" } }
                },
                ApplicantAddress = new ApplicantAddress[] { },
                ApplicantEmployment = new ApplicantEmployment[] { }
            };
        }

        [Test]
        public void ItShouldAcceptMinimalValidFullApplication()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        // Origin field tests
        [Test]
        public void ItShouldAcceptOriginAppFormConversational()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptOriginAppFormAffiliate()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormAffiliate,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptOriginApiAffiliate()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.ApiAffiliate,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        // ApplicationType field tests
        [TestCase("APPLICATION")]
        [TestCase("LEAD")]
        [TestCase("SHORTLEAD")]
        public void ItShouldAcceptValidApplicationType(string applicationType)
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = applicationType
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectInvalidApplicationType()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "INVALID_TYPE"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(fullApplication, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ApplicationType"));
        }

        [Test]
        public void ItShouldRejectNullApplicationType()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = null
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(fullApplication, validationContext, true));
            Assert.That(exception.Message, Does.Contain("ApplicationType"));
        }

        // Applicants field tests
        [Test]
        public void ItShouldRejectWhenNoApplicants()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new Applicant[] { },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(fullApplication, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Applicants"));
        }

        [Test]
        public void ItShouldAcceptOneApplicant()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptTwoApplicants()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[]
                {
                    CreateValidApplicant("Mr", "John", "Doe", "MALE"),
                    CreateValidApplicant("Mrs", "Jane", "Doe", "FEMALE")
                },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectMoreThanTwoApplicants()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[]
                {
                    CreateValidApplicant("Mr", "John", "Doe", "MALE"),
                    CreateValidApplicant("Mrs", "Jane", "Doe", "FEMALE"),
                    CreateValidApplicant("Mr", "Jack", "Doe", "MALE")
                },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(fullApplication, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Applicants"));
        }

        // Vehicles field tests
        [Test]
        public void ItShouldAcceptNoVehicles()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION",
                Vehicles = new Vehicle[] { }
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptOneVehicle()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION",
                Vehicles = new[] { new Vehicle { Make = "Toyota", Model = "Corolla", Registration = "AB12 CDE", Mileage = 50000, VehicleStatus = "DESIRED" } }
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptTwoVehicles()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION",
                Vehicles = new[] { new Vehicle { Make = "Toyota", Model = "Corolla", Registration = "AB12 CDE", Mileage = 50000, VehicleStatus = "DESIRED" }, new Vehicle { Make = "Honda", Model = "Civic", Registration = "CD34 EFG", Mileage = 45000, VehicleStatus = "PX" } }
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectMoreThanTwoVehicles()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION",
                Vehicles = new[] { new Vehicle { Make = "Toyota", Model = "Corolla", Registration = "AB12 CDE", Mileage = 50000, VehicleStatus = "DESIRED" }, new Vehicle { Make = "Honda", Model = "Civic", Registration = "CD34 EFG", Mileage = 45000, VehicleStatus = "PX" }, new Vehicle { Make = "Ford", Model = "Focus", Registration = "EF56 GHI", Mileage = 40000, VehicleStatus = "DESIRED" } }
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(fullApplication, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Vehicles"));
        }

        [Test]
        public void ItShouldAcceptCompleteFullApplication()
        {
            var fullApplication = new FullApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty { Name = "Dealer Name", Code = "D001" },
                Applicants = new[]
                {
                    CreateValidApplicant("Mr", "John", "Doe", "MALE"),
                    CreateValidApplicant("Mrs", "Jane", "Doe", "FEMALE")
                },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "APPLICATION",
                Vehicles = new[] { new Vehicle { Make = "Toyota", Model = "Corolla", Registration = "AB12 CDE", Mileage = 50000, VehicleStatus = "DESIRED" } },
                PublicReference = "REF001",
                DateApplied = DateTime.Now,
                InitialVisitTime = DateTime.Now,
                ApplicationLeadId = 123456,
                Device = "Mobile",
                Session = "SESSION123",
                QuoteEventGuid = "GUID123",
                WhereHeard = "Google",
                Notes = "Customer interested in finance"
            };

            var validationContext = new ValidationContext(fullApplication, null, null);
            Assert.That(() => Validator.ValidateObject(fullApplication, validationContext, true), Throws.Nothing);
        }
    }
}
