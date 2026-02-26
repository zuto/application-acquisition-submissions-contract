using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.LeadApplicationTests
{
    class WhenValidatingLeadApplication
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
        public void ItShouldAcceptMinimalValidLeadApplication()
        {
            var leadApplication = new LeadApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = null, // LeadApplication allows null ApplicationDetails
                ApplicationType = "LEAD"
            };

            var validationContext = new ValidationContext(leadApplication, null, null);
            Assert.That(() => Validator.ValidateObject(leadApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptAllOriginTypes()
        {
            foreach (var origin in new[] { Origin.AppFormConversational, Origin.AppFormAffiliate, Origin.ApiAffiliate })
            {
                var leadApplication = new LeadApplication
                {
                    Origin = origin,
                    SubmittingParty = new SubmittingParty(),
                    Applicants = new[] { CreateValidApplicant() },
                    ApplicationDetails = null,
                    ApplicationType = "LEAD"
                };

                var validationContext = new ValidationContext(leadApplication, null, null);
                Assert.That(() => Validator.ValidateObject(leadApplication, validationContext, true), Throws.Nothing);
            }
        }

        [Test]
        public void ItShouldAcceptNullApplicationDetails()
        {
            var leadApplication = new LeadApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = null,
                ApplicationType = "LEAD"
            };

            var validationContext = new ValidationContext(leadApplication, null, null);
            Assert.That(() => Validator.ValidateObject(leadApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptValidApplicationDetails()
        {
            var leadApplication = new LeadApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = new ApplicationDetails { VehicleType = "CAR" },
                ApplicationType = "LEAD"
            };

            var validationContext = new ValidationContext(leadApplication, null, null);
            Assert.That(() => Validator.ValidateObject(leadApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectWhenNoApplicants()
        {
            var leadApplication = new LeadApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty(),
                Applicants = new Applicant[] { },
                ApplicationDetails = null,
                ApplicationType = "LEAD"
            };

            var validationContext = new ValidationContext(leadApplication, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(leadApplication, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Applicants"));
        }

        [Test]
        public void ItShouldAcceptCompleteLeadApplication()
        {
            var leadApplication = new LeadApplication
            {
                Origin = Origin.AppFormConversational,
                SubmittingParty = new SubmittingParty { Name = "Dealer Name", Code = "D001" },
                Applicants = new[] { CreateValidApplicant() },
                ApplicationDetails = null,
                ApplicationType = "LEAD",
                Vehicles = new[] { new Vehicle { Make = "Toyota", Model = "Corolla", Registration = "AB12 CDE", Mileage = 50000, VehicleStatus = "DESIRED" } },
                PublicReference = "LEAD001",
                DateApplied = DateTime.Now,
                InitialVisitTime = DateTime.Now,
                ApplicationLeadId = 123456,
                Device = "Mobile",
                Session = "SESSION123",
                QuoteEventGuid = "GUID123",
                WhereHeard = "Google",
                Notes = "Lead interested in vehicle finance"
            };

            var validationContext = new ValidationContext(leadApplication, null, null);
            Assert.That(() => Validator.ValidateObject(leadApplication, validationContext, true), Throws.Nothing);
        }
    }
}
