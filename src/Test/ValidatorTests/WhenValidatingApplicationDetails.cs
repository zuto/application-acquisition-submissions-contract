using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests.ApplicationDetailsTests
{
    class WhenValidatingApplicationDetails
    {
        // CreditLimit field tests (MoneyRange 0.01-99999999)
        [TestCase(0.01)]
        [TestCase(100.00)]
        [TestCase(99999999.00)]
        public void ItShouldAcceptValidCreditLimit(decimal creditLimit)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                CreditLimit = creditLimit
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase(0)]
        [TestCase(0.00)]
        [TestCase(100000000)]
        public void ItShouldRejectInvalidCreditLimit(decimal creditLimit)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                CreditLimit = creditLimit
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicationDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("CreditLimit"));
        }

        [Test]
        public void ItShouldAcceptNullCreditLimit()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                CreditLimit = null
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        // VehicleType field tests (Required, AllowedValuesValidation)
        [TestCase("CAR")]
        [TestCase("VAN")]
        [TestCase("MOTORBIKE")]
        [TestCase("OTHER")]
        [TestCase("CARAVAN")]
        [TestCase("MOTORHOME")]
        [TestCase("TAXI")]
        public void ItShouldAcceptValidVehicleType(string vehicleType)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = vehicleType
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase("Car")]
        [TestCase("Van")]
        [TestCase("TRUCK")]
        [TestCase("BICYCLE")]
        public void ItShouldRejectInvalidVehicleType(string vehicleType)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = vehicleType
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicationDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("VehicleType"));
        }

        [Test]
        public void ItShouldRejectNullVehicleType()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = null
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicationDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("VehicleType"));
        }

        // Deposit field tests (MoneyRange 0.01-99999999)
        [TestCase(0.01)]
        [TestCase(100.00)]
        [TestCase(99999999.00)]
        public void ItShouldAcceptValidDeposit(decimal deposit)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                Deposit = deposit
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase(0)]
        [TestCase(0.00)]
        [TestCase(100000000)]
        public void ItShouldRejectInvalidDeposit(decimal deposit)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                Deposit = deposit
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicationDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Deposit"));
        }

        [Test]
        public void ItShouldAcceptNullDeposit()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                Deposit = null
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        // HasGuarantor field tests (nullable bool, no validation)
        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidHasGuarantor(bool? hasGuarantor)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                HasGuarantor = hasGuarantor
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptNullHasGuarantor()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                HasGuarantor = null
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        // HasLicenceGuarantor field tests (nullable bool, no validation)
        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidHasLicenceGuarantor(bool? hasLicenceGuarantor)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                HasLicenceGuarantor = hasLicenceGuarantor
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptNullHasLicenceGuarantor()
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                HasLicenceGuarantor = null
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        // FinanceType field tests (AllowedValuesValidation: null, "UNKNOWN", "HP", "PCP", "PERSONAL LOAN")
        [TestCase(null)]
        [TestCase("UNKNOWN")]
        [TestCase("HP")]
        [TestCase("PCP")]
        [TestCase("PERSONAL LOAN")]
        public void ItShouldAcceptValidFinanceType(string financeType)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                FinanceType = financeType
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            Assert.That(() => Validator.ValidateObject(applicationDetails, validationContext, true), Throws.Nothing);
        }

        [TestCase("Unknown")]
        [TestCase("Hp")]
        [TestCase("LEASE")]
        [TestCase("CASH")]
        public void ItShouldRejectInvalidFinanceType(string financeType)
        {
            var applicationDetails = new ApplicationDetails
            {
                VehicleType = "CAR",
                FinanceType = financeType
            };

            var validationContext = new ValidationContext(applicationDetails, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(applicationDetails, validationContext, true));
            Assert.That(exception.Message, Does.Contain("FinanceType"));
        }
    }
}
