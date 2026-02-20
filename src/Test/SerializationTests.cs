using System.Text.Json;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test
{
    public class SerializationTests
    {
        [Test]
        public void ItShouldSerializeAndDeserializePhoneNumber()
        {
            var phoneNumber = new PhoneNumber
            {
                Type = "HOME",
                Value = "01234567890"
            };

            var json = JsonSerializer.Serialize(phoneNumber);
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.Contain("HOME"));
            Assert.That(json, Does.Contain("01234567890"));

            var deserialized = JsonSerializer.Deserialize<PhoneNumber>(json);
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.Type, Is.EqualTo("HOME"));
            Assert.That(deserialized.Value, Is.EqualTo("01234567890"));
        }

        [Test]
        public void ItShouldSerializeAndDeserializeApplicantBasicDetails()
        {
            var details = new ApplicantBasicDetails
            {
                Gender = "MALE",
                Title = "Mr",
                Forename = "John",
                Surname = "Doe",
                DateOfBirth = new System.DateTime(1990, 1, 1),
                Email = "john@example.com",
                ApplicantType = "PRIMARY",
                PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "01234567890" } }
            };

            var json = JsonSerializer.Serialize(details);
            Assert.That(json, Is.Not.Null);

            var deserialized = JsonSerializer.Deserialize<ApplicantBasicDetails>(json);
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.Gender, Is.EqualTo("MALE"));
            Assert.That(deserialized.Title, Is.EqualTo("Mr"));
            Assert.That(deserialized.Forename, Is.EqualTo("John"));
            Assert.That(deserialized.Surname, Is.EqualTo("Doe"));
            Assert.That(deserialized.Email, Is.EqualTo("john@example.com"));
            Assert.That(deserialized.ApplicantType, Is.EqualTo("PRIMARY"));
        }

        [Test]
        public void ItShouldSerializeAndDeserializeApplicationDetails()
        {
            var details = new ApplicationDetails
            {
                VehicleType = "CAR",
                CreditLimit = 10000,
                Deposit = 2000,
                FinanceType = "PCP"
            };

            var json = JsonSerializer.Serialize(details);
            Assert.That(json, Is.Not.Null);

            var deserialized = JsonSerializer.Deserialize<ApplicationDetails>(json);
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.VehicleType, Is.EqualTo("CAR"));
            Assert.That(deserialized.CreditLimit, Is.EqualTo(10000));
            Assert.That(deserialized.Deposit, Is.EqualTo(2000));
            Assert.That(deserialized.FinanceType, Is.EqualTo("PCP"));
        }
    }
}
