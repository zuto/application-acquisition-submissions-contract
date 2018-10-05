using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test
{
    public class ValidatorTests
    {
        [Test]
        public void ItShouldAcceptLeadWithNoApplicationDetails()
        {

            var lead = new LeadApplication
            {
                Applicants = new[]
                {
                    new Applicant
                    {
                        BasicDetails = new ApplicantBasicDetails
                        {
                            Gender = "MALE",
                            Title = "Mr",
                            PhoneNumbers = new[] {new PhoneNumber { Type = "HOME", Value = "0123"}, },
                            ApplicantType = "PRIMARY"
                        },
                        ApplicantAddress = new ApplicantAddress[0],
                        ApplicantEmployment = new ApplicantEmployment[0]
                    }
                },
                SubmittingParty = new SubmittingParty() { Reference = "x" },
                ApplicationType = "LEAD"
            };
            var validationContext = new ValidationContext(lead, null, null);
            Validator.ValidateObject(lead, validationContext, true);
        }

        [Test]
        public void ItShouldRejectFullApplicationWithNoApplicationDetails()
        {

            var lead = new FullApplication
            {
                Applicants = new[]
                {
                    new Applicant
                    {
                        BasicDetails = new ApplicantBasicDetails
                        {
                            Gender = "MALE",
                            Title = "Mr",
                            PhoneNumbers = new[] {new PhoneNumber { Type = "HOME", Value = "0123"}, },
                            ApplicantType = "PRIMARY"
                        },
                        ApplicantAddress = new ApplicantAddress[0],
                        ApplicantEmployment = new ApplicantEmployment[0]
                    }
                },
                SubmittingParty = new SubmittingParty() { Reference = "x" },
                ApplicationType = "APPLICATION"
            };
            var validationContext = new ValidationContext(lead, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(lead, validationContext, true));
            Assert.That(exception.Message, Is.EqualTo("ApplicationDetails element is missing"));
        }
    }
}
