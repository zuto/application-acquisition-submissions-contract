using System;
using System.Linq;
using System.Reflection;
using ApplicationAcquisitionSubmissions.Contract;
using NUnit.Framework;

namespace Test
{
    public class ApiSurfaceDocumentationTests
    {
        [Test]
        public void ItShouldDocumentPublicApiSurface()
        {
            var assembly = typeof(ApplicationAcquisitionSubmissions.Contract.V1.Vehicle).Assembly;
            var publicTypes = assembly.GetTypes()
                .Where(t => t.IsPublic && !t.Name.StartsWith("<"))
                .OrderBy(t => t.Namespace)
                .ThenBy(t => t.Name)
                .ToList();

            Assert.That(publicTypes.Count, Is.GreaterThan(0), "Should have public types");

            // Document the API surface
            TestContext.WriteLine("=== Public API Surface ===");
            TestContext.WriteLine($"Total public types: {publicTypes.Count}");
            TestContext.WriteLine("");

            foreach (var type in publicTypes)
            {
                TestContext.WriteLine($"Type: {type.FullName}");
                
                if (type.IsClass)
                {
                    TestContext.WriteLine("  Kind: Class");
                }
                else if (type.IsInterface)
                {
                    TestContext.WriteLine("  Kind: Interface");
                }
                else if (type.IsEnum)
                {
                    TestContext.WriteLine("  Kind: Enum");
                }

                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                if (properties.Length > 0)
                {
                    TestContext.WriteLine("  Properties:");
                    foreach (var prop in properties)
                    {
                        TestContext.WriteLine($"    - {prop.PropertyType.Name} {prop.Name}");
                    }
                }

                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
                if (methods.Length > 0)
                {
                    TestContext.WriteLine("  Methods:");
                    foreach (var method in methods)
                    {
                        var parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                        TestContext.WriteLine($"    - {method.ReturnType.Name} {method.Name}({parameters})");
                    }
                }

                TestContext.WriteLine("");
            }

            // Verify key types exist
            var expectedTypes = new[]
            {
                "ApplicationAcquisitionSubmissions.Contract.V1.Vehicle",
                "ApplicationAcquisitionSubmissions.Contract.V1.PhoneNumber",
                "ApplicationAcquisitionSubmissions.Contract.V1.Applicant",
                "ApplicationAcquisitionSubmissions.Contract.V1.ApplicantBasicDetails",
                "ApplicationAcquisitionSubmissions.Contract.V1.ApplicantAddress",
                "ApplicationAcquisitionSubmissions.Contract.V1.ApplicantEmployment",
                "ApplicationAcquisitionSubmissions.Contract.V1.ApplicationDetails",
                "ApplicationAcquisitionSubmissions.Contract.V1.FullApplication",
                "ApplicationAcquisitionSubmissions.Contract.V1.LeadApplication",
                "ApplicationAcquisitionSubmissions.Contract.V1.PartialApplication",
                "ApplicationAcquisitionSubmissions.Contract.DataAttributes.AllowedValuesValidation",
                "ApplicationAcquisitionSubmissions.Contract.DataAttributes.StringLengthRangeAttribute",
                "ApplicationAcquisitionSubmissions.Contract.DataAttributes.IntegerRangeAttribute",
                "ApplicationAcquisitionSubmissions.Contract.DataAttributes.MoneyRangeAttribute",
                "ApplicationAcquisitionSubmissions.Contract.DataAttributes.ArrayCountAttribute",
                "ApplicationAcquisitionSubmissions.Contract.DataAttributes.ValidateObjectAttribute"
            };

            foreach (var expectedType in expectedTypes)
            {
                var type = publicTypes.FirstOrDefault(t => t.FullName == expectedType);
                Assert.That(type, Is.Not.Null, $"Expected type {expectedType} not found in public API");
            }
        }
    }
}
