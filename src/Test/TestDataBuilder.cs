using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationAcquisitionSubmissions.Contract.V1;

namespace Test
{
    class TestDataBuilder<T> where T : FullApplication, new()
    {
        private readonly IList<Applicant> _applicants = new List<Applicant>();
        private SubmittingParty _submittingParty;
        private readonly TestDataType _testDataType;
        private ApplicationDetails _applicationDetails;

        public TestDataBuilder()
        {
            if (typeof(T) == typeof(LeadApplication))
            {
                _testDataType = TestDataType.Lead;
            }
        }

        public TestDataBuilder<T> WithDefaultApplicant()
        {
            _applicants.Add(new Applicant
            {
                BasicDetails = new ApplicantBasicDetails
                {
                    Gender = "MALE",
                    Title = "Mr",
                    PhoneNumbers = new[] { new PhoneNumber { Type = "HOME", Value = "0123" }, },
                    ApplicantType = "PRIMARY"
                },
                ApplicantAddress = new ApplicantAddress[0],
                ApplicantEmployment = new ApplicantEmployment[0]
            });
            return this;
        }

        public TestDataBuilder<T> WithDefaultSubmittingParty()
        {
            _submittingParty = new SubmittingParty() {Reference = "x"};
            return this;
        }

        public TestDataBuilder<T> WithApplicationDetails(ApplicationDetails applicationDetails)
        {
            _applicationDetails = applicationDetails;
            return this;
        }

        public T Build()
        {
            var testData = new T
            {
                Applicants = _applicants.ToArray(),
                ApplicationType = _testDataType == TestDataType.Application ? "APPLICATION" : "LEAD"
            };
            if (_submittingParty != null) testData.SubmittingParty = _submittingParty;
            if (_applicationDetails != null) testData.ApplicationDetails = _applicationDetails;
            return testData;
        }
    }

    enum TestDataType
    {
        Application,
        Lead
    }
}
