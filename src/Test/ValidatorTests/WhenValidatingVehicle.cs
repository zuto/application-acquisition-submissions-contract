using System.ComponentModel.DataAnnotations;
using ApplicationAcquisitionSubmissions.Contract.V1;
using NUnit.Framework;

namespace Test.ValidatorTests
{
    class WhenValidatingVehicle
    {
        [Test]
        public void ItShouldAcceptValidVehicle()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        // Registration field tests (Required, StringLengthRange 1-50)
        [TestCase("AB12 CDE")]
        [TestCase("AB12CDE")]
        [TestCase("A1")]
        public void ItShouldAcceptValidRegistration(string registration)
        {
            var vehicle = new Vehicle
            {
                Registration = registration,
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectNullRegistration()
        {
            var vehicle = new Vehicle
            {
                Registration = null,
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Registration"));
        }

        [Test]
        public void ItShouldRejectRegistrationTooLong()
        {
            var vehicle = new Vehicle
            {
                Registration = new string('A', 51),
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Registration"));
        }

        // Mileage field tests (Required, IntegerRange 0-99999999)
        [TestCase(0)]
        [TestCase(50000)]
        [TestCase(99999999)]
        public void ItShouldAcceptValidMileage(int mileage)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = mileage,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [TestCase(-1)]
        [TestCase(100000000)]
        public void ItShouldRejectInvalidMileage(int mileage)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = mileage,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Mileage"));
        }

        // Make field tests (Required, StringLengthRange 1-255)
        [TestCase("Toyota")]
        [TestCase("BMW")]
        [TestCase("Mercedes-Benz")]
        public void ItShouldAcceptValidMake(string make)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = make,
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectNullMake()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = null,
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Make"));
        }

        [Test]
        public void ItShouldRejectMakeTooLong()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = new string('A', 256),
                Model = "Corolla",
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Make"));
        }

        // Model field tests (Required, StringLengthRange 1-255)
        [TestCase("Corolla")]
        [TestCase("3 Series")]
        [TestCase("C-Class")]
        public void ItShouldAcceptValidModel(string model)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = model,
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldRejectNullModel()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = null,
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Model"));
        }

        [Test]
        public void ItShouldRejectModelTooLong()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = new string('A', 256),
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Model"));
        }

        // IsUsed field tests (nullable bool, no validation)
        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidIsUsed(bool? isUsed)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                IsUsed = isUsed,
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptNullIsUsed()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                IsUsed = null,
                VehicleStatus = "DESIRED"
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        // VehicleStatus field tests (Required, AllowedValuesValidation: "DESIRED", "PX")
        [TestCase("DESIRED")]
        [TestCase("PX")]
        public void ItShouldAcceptValidVehicleStatus(string vehicleStatus)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = vehicleStatus
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [TestCase("Desired")]
        [TestCase("TRADE-IN")]
        [TestCase("STOCK")]
        public void ItShouldRejectInvalidVehicleStatus(string vehicleStatus)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = vehicleStatus
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("VehicleStatus"));
        }

        [Test]
        public void ItShouldRejectNullVehicleStatus()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = null
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("VehicleStatus"));
        }

        // ValuationRequired field tests (nullable bool, no validation)
        [TestCase(true)]
        [TestCase(false)]
        public void ItShouldAcceptValidValuationRequired(bool? valuationRequired)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED",
                ValuationRequired = valuationRequired
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptNullValuationRequired()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED",
                ValuationRequired = null
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        // Value field tests (MoneyRange 0.01-99999999)
        [TestCase(0.01)]
        [TestCase(15000.00)]
        [TestCase(99999999.00)]
        public void ItShouldAcceptValidValue(decimal value)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED",
                Value = value
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [TestCase(0)]
        [TestCase(0.00)]
        [TestCase(100000000)]
        public void ItShouldRejectInvalidValue(decimal value)
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED",
                Value = value
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("Value"));
        }

        [Test]
        public void ItShouldAcceptNullValue()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED",
                Value = null
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        // Dealer and Advert fields (no validation)
        [Test]
        public void ItShouldAcceptNullDealer()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED",
                Dealer = null
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptNullAdvert()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                VehicleStatus = "DESIRED",
                Advert = null
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [Test]
        public void ItShouldAcceptCompleteVehicle()
        {
            var vehicle = new Vehicle
            {
                Registration = "AB12 CDE",
                Mileage = 50000,
                Make = "Toyota",
                Model = "Corolla",
                IsUsed = true,
                VehicleStatus = "DESIRED",
                ValuationRequired = false,
                Value = 15000.00m,
                Dealer = new Dealer { Id = "D001", Name = "ABC Motors" },
                Advert = new Advert { Url = "https://example.com/advert" }
            };

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }
        
        [TestCase("STOCK123")]
        [TestCase(null)]
        public void ItShouldAcceptValidStockId(string stockId)
        {
            var vehicle = ValidVehicle();
            vehicle.StockId = stockId;

            var validationContext = new ValidationContext(vehicle, null, null);
            Assert.That(() => Validator.ValidateObject(vehicle, validationContext, true), Throws.Nothing);
        }

        [TestCase("")]
        [TestCase("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA")]
        public void ItShouldRejectInvalidStockId(string stockId)
        {
            var vehicle = ValidVehicle();
            vehicle.StockId = stockId;

            var validationContext = new ValidationContext(vehicle, null, null);
            var exception = Assert.Throws<ValidationException>(() => Validator.ValidateObject(vehicle, validationContext, true));
            Assert.That(exception.Message, Does.Contain("StockId"));
        }

        private static Vehicle ValidVehicle() => new Vehicle
        {
            Registration = "AB12 CDE",
            Mileage = 50000,
            Make = "Toyota",
            Model = "Corolla",
            VehicleStatus = "DESIRED"
        };
    }
}
