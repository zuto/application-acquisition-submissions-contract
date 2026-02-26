using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.PartialApplicationTests
{
    class WhenValidatingPartialApplication
    {
        [Test]
        public void ItShouldAcceptMinimalValidPartialApplication()
        {
            var partialApplication = new PartialApplication
            {
                Origin = Origin.AppFormConversational
            };

            var validationContext = new ValidationContext(partialApplication, null, null);
            Assert.That(() => Validator.ValidateObject(partialApplication, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptAllOriginTypes()
        {
            foreach (var origin in new[] { Origin.AppFormConversational, Origin.AppFormAffiliate, Origin.ApiAffiliate })
            {
                var partialApplication = new PartialApplication { Origin = origin };
                var validationContext = new ValidationContext(partialApplication, null, null);
                Assert.That(() => Validator.ValidateObject(partialApplication, validationContext, true), Throws.Nothing);
            }
        }

        [Test]
        public void ItShouldAcceptCompletePartialApplication()
        {
            var partialApplication = new PartialApplication
            {
                Origin = Origin.AppFormConversational,
                Source = "WebForm",
                Medium = "Online",
                Campaign = "Summer_Campaign-2024",
                Title = "Mr",
                Forename = "John",
                MiddleNames = "James",
                Surname = "Doe",
                Gender = "MALE",
                DateOfBirth = new DateTime(1990, 1, 1),
                ContactPoint = new ContactPoint
                {
                    Email = "john@example.com",
                    Phone = "01234567890"
                },
                Postcode = "SW1A 1AA",
                DateApplied = DateTime.Now,
                InitialVisitTime = DateTime.Now,
                PublicReference = "REF001"
            };

            var validationContext = new ValidationContext(partialApplication, null, null);
            Assert.That(() => Validator.ValidateObject(partialApplication, validationContext, true), Throws.Nothing);
        }
    }
}
