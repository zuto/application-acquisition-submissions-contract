using System;
using System.Reflection;
using ApplicationAcquisitionSubmissions.Contract;
using ApplicationAcquisitionSubmissions.Contract.V1;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;
using NUnit.Framework;

namespace Test
{
    public class LibraryLoadingTests
    {
        [Test]
        public void ItShouldLoadContractAssembly()
        {
            var assembly = typeof(Vehicle).Assembly;
            Assert.That(assembly, Is.Not.Null);
            Assert.That(assembly.GetName().Name, Is.EqualTo("ApplicationAcquisitionSubmissions.Contract"));
        }

        [Test]
        public void ItShouldLoadAllPublicTypes()
        {
            var assembly = typeof(Vehicle).Assembly;
            var types = assembly.GetTypes();
            
            Assert.That(types.Length, Is.GreaterThan(0));
            
            // Verify key types are accessible
            Assert.That(assembly.GetType("ApplicationAcquisitionSubmissions.Contract.V1.Vehicle"), Is.Not.Null);
            Assert.That(assembly.GetType("ApplicationAcquisitionSubmissions.Contract.V1.PhoneNumber"), Is.Not.Null);
            Assert.That(assembly.GetType("ApplicationAcquisitionSubmissions.Contract.V1.Applicant"), Is.Not.Null);
            Assert.That(assembly.GetType("ApplicationAcquisitionSubmissions.Contract.V1.FullApplication"), Is.Not.Null);
            Assert.That(assembly.GetType("ApplicationAcquisitionSubmissions.Contract.DataAttributes.AllowedValuesAttribute"), Is.Not.Null);
        }

        [Test]
        public void ItShouldInstantiateAllPublicDataContracts()
        {
            // Test Vehicle
            var vehicle = new Vehicle();
            Assert.That(vehicle, Is.Not.Null);

            // Test PhoneNumber
            var phoneNumber = new PhoneNumber();
            Assert.That(phoneNumber, Is.Not.Null);

            // Test Applicant
            var applicant = new Applicant();
            Assert.That(applicant, Is.Not.Null);

            // Test ApplicantBasicDetails
            var basicDetails = new ApplicantBasicDetails();
            Assert.That(basicDetails, Is.Not.Null);

            // Test ApplicantAddress
            var address = new ApplicantAddress();
            Assert.That(address, Is.Not.Null);

            // Test ApplicantEmployment
            var employment = new ApplicantEmployment();
            Assert.That(employment, Is.Not.Null);

            // Test ApplicationDetails
            var appDetails = new ApplicationDetails();
            Assert.That(appDetails, Is.Not.Null);

            // Test FullApplication
            var fullApp = new FullApplication();
            Assert.That(fullApp, Is.Not.Null);

            // Test LeadApplication
            var leadApp = new LeadApplication();
            Assert.That(leadApp, Is.Not.Null);

            // Test PartialApplication
            var partialApp = new PartialApplication();
            Assert.That(partialApp, Is.Not.Null);
        }

        [Test]
        public void ItShouldAccessAllPublicProperties()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED",
                Value = 15000.50m,
                IsUsed = true,
                ValuationRequired = false
            };

            Assert.That(vehicle.Registration, Is.EqualTo("ABC123"));
            Assert.That(vehicle.Mileage, Is.EqualTo(50000));
            Assert.That(vehicle.Make, Is.EqualTo("Toyota"));
            Assert.That(vehicle.Model, Is.EqualTo("Camry"));
            Assert.That(vehicle.VehicleStatus, Is.EqualTo("DESIRED"));
            Assert.That(vehicle.Value, Is.EqualTo(15000.50m));
            Assert.That(vehicle.IsUsed, Is.True);
            Assert.That(vehicle.ValuationRequired, Is.False);
        }

        [Test]
        public void ItShouldAccessValidationAttributes()
        {
            var vehicleType = typeof(Vehicle);
            var vehicleStatusProperty = vehicleType.GetProperty("VehicleStatus");
            
            var attributes = vehicleStatusProperty.GetCustomAttributes(typeof(AllowedValuesAttribute), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as AllowedValuesAttribute;
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Values, Is.Not.Null);
            Assert.That(attribute.Values.Length, Is.GreaterThan(0));
        }

        [Test]
        public void ItShouldHaveCorrectAssemblyVersion()
        {
            var assembly = typeof(Vehicle).Assembly;
            var version = assembly.GetName().Version;
            
            Assert.That(version, Is.Not.Null);
            Assert.That(version.Major, Is.EqualTo(2));
            Assert.That(version.Minor, Is.EqualTo(0));
        }
    }
}
