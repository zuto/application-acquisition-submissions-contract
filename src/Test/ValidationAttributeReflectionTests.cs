using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using ApplicationAcquisitionSubmissions.Contract.V1;
using ApplicationAcquisitionSubmissions.Contract.DataAttributes;
using NUnit.Framework;

namespace Test
{
    public class ValidationAttributeReflectionTests
    {
        [Test]
        public void ItShouldRetrieveAllowedValuesValidationAttributeFromVehicle()
        {
            var vehicleType = typeof(Vehicle);
            var vehicleStatusProperty = vehicleType.GetProperty("VehicleStatus");
            
            Assert.That(vehicleStatusProperty, Is.Not.Null);
            
            var attributes = vehicleStatusProperty.GetCustomAttributes(typeof(AllowedValuesValidation), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as AllowedValuesValidation;
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Values, Contains.Item("DESIRED"));
            Assert.That(attribute.Values, Contains.Item("PX"));
        }

        [Test]
        public void ItShouldRetrieveAllowedValuesValidationAttributeFromPhoneNumber()
        {
            var phoneNumberType = typeof(PhoneNumber);
            var typeProperty = phoneNumberType.GetProperty("Type");
            
            Assert.That(typeProperty, Is.Not.Null);
            
            var attributes = typeProperty.GetCustomAttributes(typeof(AllowedValuesValidation), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as AllowedValuesValidation;
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Values, Contains.Item("HOME"));
            Assert.That(attribute.Values, Contains.Item("MOBILE"));
        }

        [Test]
        public void ItShouldRetrieveStringLengthRangeAttributeFromVehicle()
        {
            var vehicleType = typeof(Vehicle);
            var registrationProperty = vehicleType.GetProperty("Registration");
            
            Assert.That(registrationProperty, Is.Not.Null);
            
            var attributes = registrationProperty.GetCustomAttributes(typeof(StringLengthRangeAttribute), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as StringLengthRangeAttribute;
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.MinimumLength, Is.EqualTo(1));
            Assert.That(attribute.MaximumLength, Is.EqualTo(50));
        }

        [Test]
        public void ItShouldRetrieveIntegerRangeAttributeFromVehicle()
        {
            var vehicleType = typeof(Vehicle);
            var mileageProperty = vehicleType.GetProperty("Mileage");
            
            Assert.That(mileageProperty, Is.Not.Null);
            
            var attributes = mileageProperty.GetCustomAttributes(typeof(IntegerRangeAttribute), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as IntegerRangeAttribute;
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Minimum, Is.EqualTo(0));
            Assert.That(attribute.Maximum, Is.EqualTo(99999999));
        }

        [Test]
        public void ItShouldRetrieveMoneyRangeAttributeFromVehicle()
        {
            var vehicleType = typeof(Vehicle);
            var valueProperty = vehicleType.GetProperty("Value");
            
            Assert.That(valueProperty, Is.Not.Null);
            
            var attributes = valueProperty.GetCustomAttributes(typeof(MoneyRangeAttribute), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as MoneyRangeAttribute;
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Minimum, Is.EqualTo(0.01));
            Assert.That(attribute.Maximum, Is.EqualTo(99999999));
        }

        [Test]
        public void ItShouldRetrieveRequiredAttributeFromVehicle()
        {
            var vehicleType = typeof(Vehicle);
            var registrationProperty = vehicleType.GetProperty("Registration");
            
            Assert.That(registrationProperty, Is.Not.Null);
            
            var attributes = registrationProperty.GetCustomAttributes(typeof(RequiredAttribute), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
        }

        [Test]
        public void ItShouldRetrieveValidateObjectAttributeFromFullApplication()
        {
            var fullApplicationType = typeof(FullApplication);
            var applicantsProperty = fullApplicationType.GetProperty("Applicants");
            
            Assert.That(applicantsProperty, Is.Not.Null);
            
            var attributes = applicantsProperty.GetCustomAttributes(typeof(ValidateObjectAttribute), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as ValidateObjectAttribute;
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void ItShouldRetrieveArrayCountAttributeFromFullApplication()
        {
            var fullApplicationType = typeof(FullApplication);
            var applicantsProperty = fullApplicationType.GetProperty("Applicants");
            
            Assert.That(applicantsProperty, Is.Not.Null);
            
            var attributes = applicantsProperty.GetCustomAttributes(typeof(ArrayCountAttribute), false);
            Assert.That(attributes.Length, Is.GreaterThan(0));
            
            var attribute = attributes[0] as ArrayCountAttribute;
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute.Min, Is.EqualTo(1));
            Assert.That(attribute.Max, Is.EqualTo(2));
        }
    }
}
