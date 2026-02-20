using System;
using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test
{
    public class ValidationAttributeExecutionTests
    {
        [Test]
        public void ItShouldValidateAllowedValuesAttribute()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectInvalidAllowedValue()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "INVALID"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        public void ItShouldValidateStringLengthRangeAttribute()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectTooLongRegistration()
        {
            var vehicle = new Vehicle
            {
                Registration = new string('A', 51),
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        public void ItShouldValidateIntegerRangeAttribute()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectNegativeMileage()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = -1,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        public void ItShouldValidateMoneyRangeAttribute()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED",
                Value = 15000.50m
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectZeroValue()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED",
                Value = 0
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception, Is.Not.Null);
        }

        [Test]
        public void ItShouldValidateRequiredAttribute()
        {
            var vehicle = new Vehicle
            {
                Registration = "ABC123",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectMissingRequiredField()
        {
            var vehicle = new Vehicle
            {
                Mileage = 50000,
                Make = "Toyota",
                Model = "Camry",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception, Is.Not.Null);
        }
    }
}
