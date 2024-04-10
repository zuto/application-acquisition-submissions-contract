![Zuto](zuto.png?raw=true)

# application-acquisition-submissions contract

This repo contains the .net implementation of the contract for calling the Zuto Application Acquisition Submissions API. 

### Data Contract

Zuto Limited has created an api, that allows external organisations to submit applications and partial applications electronically.
To help you integrate with this system we have created this development guide.

Our api is a secure web service, which runs over https.
We use a whitelist, to allow access, so in order to use either the test or production web service, you will need to provide us with the IP addresses of the servers that will require access.

#### Compatibility

This assembly is .NetFramework 4.52 / dotnetcore compatible (dotnetcore 2.0+)

Because we needed to support a .NetFramework 4.52 application, we were unable to build the contract using .NetStandard, but it will work on version 2+ of dotnetcore, and has been tested to this effect.



## Index

- [Service Endpoint Information](#service-endpoint-information)
- Full Application
    - [Example Successful Response](#example-successful-response)
    - [Example Validation Failure Response](#example-validation-failure-response)
    - [Example System Failure Response](#example-system-failure-response)
- Populating the FullApplication contract
    - [FullApplication-SubmittingParty](#fullapplication-submittingparty)
    - [FullApplication-Applicants](#fullapplication-applicants)
    - [FullApplication-Applicants-BasicDetails](#fullapplication-applicants-basicdetails)
    - [FullApplication-Applicants-MarketingOptIn](#fullapplication-applicants-marketingoptin)
    - [FullApplication-Applicants-AdditionalDetails](#fullapplication-applicants-additionaldetails)
    - [FullApplication-Applicants-ApplicantAddress](#fullapplication-applicants-applicantaddress)
    - [FullApplication-Applicants-ApplicantEmployment](#fullapplication-applicants-applicantemployment)
    - [FullApplication-ApplicationDetails](#fullapplication-applicants-applicationdetails)
    - [An example full application payload](#an-example-full-application-payload)

### Service Endpoint Information

The application endpoint is located at:

- Testing: [https://application-acquisition-submissions.uat.zuto.cloud](https://application-acquisition-submissions.uat.zuto.cloud)
- Production: [https://application-acquisition-submissions.zuto.cloud](https://application-acquisition-submissions.zuto.cloud)

Please ensure you do not send to production until you have had sign-off in the UAT testing environment.

On the service, there are two methods available.

- FullApplication
- PartialApplication

e.g. for the testing environment:

```
https://application-acquisition-submissions.uat.zuto.cloud/fullapplication
https://application-acquisition-submissions.uat.zuto.cloud/partialapplication
```

When sending a request to the endpoint, use a POST, with the content type set to application/json 
e.g.

```
var url = "https://application-acquisition-submissions.uat.zuto.cloud/fullapplication";
var content = JsonConvert.SerializeObject(@contract);
return await new HttpClient().PostAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));
```

or 

```
curl -XPOST -H "Content-type: application/json" -d '{}' 'https://application-acquisition-submissions.uat.zuto.cloud/fullapplication'
```

Testing must be carried out against our test api. The initial phase of testing will ensure that your systems can communicate with ours

Once UAT has been successful, we will agree with you a date to switch over to our production web service

[^ Back to Top](#application-acquisition-submissions-contract)

### Full Application Contract

The fullapplication method allows you to submit an Application.

_On sending a submission, we assume that the applicant has agreed to our terms and conditions, as published on our website when an application is made, and will be subjected to certain levels of automatic processing that will include credit searching._

For this submission type, an applicant might not be contacted by us until they have been approved by one of the lenders on our panel. 

JSON messages received by this web method are subjected to validation against the schema, published as attributes within the data contract, or as described within this readme. 

- Messages that fail against this schema will return a validation message, along with a status code of BadRequest (400). Failed messages are not processed or stored through our systems.
- Successful messages will return a response code of Created (201) and will be processed through our system.

[^ Back to Top](#application-acquisition-submissions-contract)

#### Example Successful Response:

```
StatusCode: 201
Body:
{
    "AuthToken": "7C3AFF52-9084-4819-B085-22D5B2FE1F8E", 
    "Redirect":"https://www.zuto.com/summary/", 
    "Reference":"123457",
    "ApplicationAccepted": true    
}
```

- AuthToken: can be ignored, this is used by our internal systems to authenticate a user
- Redirect: is a link that can be followed for the user to the next stage. 
    - If the user already has an existing open application, the link will redirect to the sign in page, which will prompt the user to login as an existing user
    - If the user already applied but does not have an existing open application, the link will redirect to the sign in page, which will promt user to login as an existing user
    - If the user is new , a preauthentication link will be returned, which will log the user in, prompt the user to set a password, then redirect to the account area summary page.
- Reference: is the value sent to us, as the SubmittingPartyReference value
- ApplicationAccepted: is an internal field, which describes whether we have accepted the application or not.

[^ Back to Top](#application-acquisition-submissions-contract)

#### Example Validation Failure Response:

```
StatusCode: 400
Body:
{"Message":"SubmittingParty element is missing"}
```

- *Message*: will be returned, with the reason that the message was rejected.

[^ Back to Top](#application-acquisition-submissions-contract)

#### Example System Failure Response

```
StatusCode: 500
{"Message":"A server exception has occured."}
```

This will only occur, if our system is experiencing some kind of fault, or the contract you supplied does not follow the same format as described in this document. A test example is provided to help diagnose this issue.

[^ Back to Top](#application-acquisition-submissions-contract)

# Populating the FullApplication contract

Each of the sections describes a node within the json contract. If a property is not mentioned, then it can be left as null, or not included when submitting.

### FullApplication

At the top level of the contract, application details are required.

- Origin-Value {string}: Must always be set to `Api-Affiliate`
- ApplicationType {string}: Must always be set to `APPLICATION`
- DateApplied {string}: Should be set to null, or the time you took the application, in the format `yyyy-MM-ddThh:mm:ss+00:00` e.g. `2018-03-17T16:47:49+00:00`

e.g.

```
{
    "Origin": { "Value": "Api-Affiliate" },
    "ApplicationType": "APPLICATION",
    "DateApplied": "2018-01-01T00:00:00+00:00"
}
```
[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-SubmittingParty

In order for us to identify your submission as being from your organisation, you will provide the following data in your submission

SubmittingParty:    

- Reference {string}: This value is your unique reference for the submission
- Source {string}: This value will be the name of your organisation (content to be agreed)
- Medium {string}: This value should be set to `affiliate`
- Campaign {string}: This value represents our code for submissions from you. You may have multiple campaigns, but these must be agreed with us before use.

e.g.

```  
    "SubmittingParty": {
        "Reference": "1234567890",
        "Source": "MyOrganisation",
        "Medium": "affiliate",
        "Campaign": "MyCampaign"
    }
```

[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-Applicants

Supply the applicant(s) information, where you can have a minimum of 1 applicant, and a maximum of 2 applicants.

If two applicants are submitted, the first applicant will be treated as the primary applicant, and the second as the joint applicant.

```
    "Applicants": [{
        ...PrimaryApplicant...
    }, {
        ...JointApplicant...
    }]
```

[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-Applicants-BasicDetails

The basic details for the applicant is a required property.

- Gender {string}: Must be set to `MALE` or `FEMALE`        
- Title {string}: Must be set to `Mr`, `Miss`, `Mrs` or `Ms`. If `Miss`, `Mrs` or `Ms` please ensure these are mapped to `FEMALE` on Gender, not `MALE`.
- Forename {string}: Must be between 0 and 50 characters
- MiddleNames {string or null}: Must be null, or between 1 and 100 characters.
- Surname {string}: Must be between 0 and 50 characters
- DateOfBirth {string}: Must be supplied in the format `yyyy-MM-ddThh:mm:ss+00:001` e.g. `2018-01-01T00:00:00+00:001`
- PhoneNumbers-Type {string}: Must have a minimum of 1, and maximum of 2, with type set to `Home`, `Mobile` or Both (see example below). 
- PhoneNumbers-Value {string}: Must have a minimum of 1, and maximum of 2. `Home` numbers should be standard UK numbers and must start with 01 or 02. `Mobile` numbers must start with 07, and must be 11 digits.
- Email {string}: Must be between 1 and 100 characters. Please ensure there is ample validation, e.g. an @ symbol, and a check for a 'dot something' after the @ symbol.
- ApplicantType {string}: Must be set to `PRIMARY`or `JOINT`    

e.g.

```
    "BasicDetails": {
        "Gender": "FEMALE",
        "Title": "Mrs",
        "Forename": "Josephine",
        "MiddleNames": null,
        "Surname": "Bloggs",
        "DateOfBirth": "1985-07-16T00:00:00+00:001",
        "PhoneNumbers": [{
            "Type": "HOME",
            "Value": "01234567890"
        }, {
            "Type": "MOBILE",
            "Value": "07234567890"
        }],
        "Email": "test@zuto.com",
        "ApplicantType": "PRIMARY"
    }
```

[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-Applicants-MarketingOptIn

Api Affiliates do not need to collect Marketing opt in options. For Api Affiliates, this must be submitted as:

```diff
ThirdPartyReferral as Opt Out
SMS as Opt Out
Phone as Opt In
Email as Opt In
```

We manage consent on our side. We screen against the TPS for phone preferences, and we allow customers to manage their email opt-in as soon as they hit the Zuto environment.

```
    "Applicants": [{
        "MarketingOptIn": {
            "Email": true,
            "Sms": false,
            "Phone": true,
            "ThirdPartyReferral": false
        }
    }]
```

[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-Applicants-AdditionalDetails

The additional details for the applicant is a an optional property

- MaritalStatus {string}: Must be one of `MARRIED`, `COHABITING`, `LIVING WITH PARTNER`, `SINGLE`, `SEPARATED`, `DIVORCED`, `WIDOWED`, `UNKNOWN`
- LicenceType {string}: Must be one of `FULL UK`, `PROVISIONAL UK`, `EUROPEAN`, `INTERNATIONAL`, `NONE`, `CBT`, `A2`, `FULL A CLASS`, `UNKNOWN`
- ValidUkPassport {bool or null}: Must be true, false or null
- OtherMonthlyIncome {number or null}: Must be null, or greater than 0.01
    
e.g.
```
    "AdditionalDetails": {
        "MaritalStatus": "MARRIED",
        "LicenceType": "FULL UK",
        "ValidUkPassport": true,
        "OtherMonthlyIncome": 1000.00
    }
```

[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-Applicants-ApplicantAddress

You can supply between applicant addresses using this node, up to 10 historic adresses, with the most recent address first, ordered chronologically

- NameNumber {string}: The address line 1 of the property, between 1 and 100 characters. Do not pass empty string.
    - This field should capture sub-buildings (such as flats), building names and building numbers (where applicable), so it might be best to include separate fields in your form to capture this information and then concatenate accordingly.
    - Some providers will use a required Address Line 1 and an optional Address Line 2, and then concatenate the values to map to our NameNumber field.
    - Other providers will have separate fields for Flat Number, Building Name and Building Number where at least 1 of those fields is required. The ordering of these fields should always be `FLAT NUMBER` + `BUILDING NAME` + `BUILDING NUMBER`
    - When concatenating this information, it's important that NameNumber is sanitised so as not to pass null, additional empty characters or duplicate values.
    - A good test example would be: Apartment 5 (Flat number), Marsden House (Building name), 31 (Building number) .. this would be followed by the Street, e.g. Lower Reedley Road.
- Street {string or null}: Must be null or between 1 and 100 characters. Do not pass empty string.
- TownCity {string}: Must be between 1 and 100 characters. Do not pass empty string.
- County {string}: Must be between 1 and 100 characters. Do not pass empty string.
- PostCode {string}: Must be between 1 and 20 characters.
- Years {number}: Years spent at the address, between 0 and 100
- Months {number}: Months spent at address, between 0 and 11
- ResidentialStatus {string}: Must be one of `HOME OWNER`, `PRIVATE TENANT`, `LIVING WITH PARENTS`, `LIVING WITH PARTNER`, `COUNCIL TENANT`, `HOUSING ASSOCIATION`, `UNKNOWN`

Our lender panel requires 3 years address history, so multiple addresses that add up to 3 or more years will need to be passed to us. (We only accept up to 10 addresses)

e.g. for 1 address:

```
    "ApplicantAddress": [{
        "NameNumber": "Winterton House", 
        "Street": "Winterton Way", 
        "TownCity": Macclesfield,
        "County": "Cheshire East",
        "PostCode": "SK11 0LP",
        "Years": 3,
        "Months": 6,
        "ResidentialStatus": "HOME OWNER"
    }]
```

...and for multiple addresses (up to 10): 
```
    "ApplicantAddress": [{
        "NameNumber": "Winterton House", 
        "Street": "Winterton Way", 
        "TownCity": Macclesfield,
        "County": "Cheshire East",
        "PostCode": "SK11 0LP",
        "Years": 1,
        "Months": 6,
        "ResidentialStatus": "HOME OWNER"
    },{
        "NameNumber": "Apartment 5 Marsden House 31", 
        "Street": "Lower Reedley Road", 
        "TownCity": Manchester,
        "County": "Greater Manchester",
        "PostCode": "M2 7HA",
        "Years": 2,
        "Months": 6,
        "ResidentialStatus": "PRIVATE TENANT"
    }
]
```

[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-Applicants-ApplicantEmployment

You can supply applicant employment information using this node, with up to 10 historic employments, with the most recent employment first, ordered chronologically.

- Occupation {string}: Must be between 1 and 100 characters. See the below table for further guidance. Do not pass empty string.
- EmployerName {string}: Must be between 1 and 100 characters. See the below table for further guidance. Do not pass empty string.
- EmploymentStatus {string}: Must contain one of `AGENCY`, `FULL TIME PERMANENT`, `PART TIME`, `RETIRED`, `SELF EMPLOYED`, `STUDENT`, `SUB CONTRACT`, `CARER`, `DISABILITY`, `TEMPORARY`, `UNEMPLOYED`
- If the EmploymentStatus is one of the following: `UNEMPLOYED`, `STUDENT`, `DISABILITY`, `RETIRED`, `SUB CONTRACT` or `CARER` - Map the following Employment Details fields to the following:
      
      | Our Mapping | What to pass in EmployerName | What to pass in EmploymentAddress: { TownCity: value } | What to pass in Occupation |
      |-------------|-------------------------------|--------------------------------------------------------|-----------------------------|
      | UNEMPLOYED  | 'not employed'                | Applicant’s current address town                        | 'not employed'              |
      | STUDENT     | 'student'                     | Applicant’s current address town                        | 'student'                   |
      | DISABILITY  | 'disability'                  | Applicant’s current address town                        | 'disability'                |
      | RETIRED     | 'retired'                     | Applicant’s current address town                        | 'retired'                   |
      | SUB CONTRACT| 'sub contractor'              | Applicant’s current address town                        | 'sub contractor'            |
      | CARER       | 'carer'                       | Applicant’s current address town                        | 'carer'                     |

- Telephone {string}: Must be null or a standard UK phone number, starting with 0. 
- Years {number}: Years spent in employment, between 0 and 100. 
- Months {number}: Months spent in employment, between 0 and 11. 
- NetMonthlyIncome {number}: Must be null, or greater than or equal to 0. 
- EmploymentAddress: 
    - NameNumber {string or null}: The address line 1  name or number of the property, either null or between 1 and 100 characters. Do not pass empty string.
    - Street {string or null}: Must be null or be between 1 and 100 characters. Do not pass empty string.
    - TownCity {string}: Must be between 1 and 100 characters. Do not pass empty string.
    - County {string or null}: Must be null or between 1 and 100 characters. Do not pass empty string.
    - PostCode {string or null}: Must be null or between 1 and 20 characters. Do not pass empty string.

 Our lender panel requires 2 years employment history, so multiple employments that add up to 2 or more years will need to be passed to us. (We only accept up to 10 employment entries).
            
e.g. 1 for employment:

```
    "ApplicantEmployment": [{
        "Occupation": "Software Engineer",
        "EmployerName": "Zuto",
        "EmploymentStatus": "FULL TIME PERMANENT",
        "Telephone": "01234567890",
        "Years": 3,
        "Months": 6,
        "NetMonthlyIncome": 1000.00,
        "EmploymentAddress": {
            "NameNumber": "Winterton House", 
            "Street": "Winterton Way", 
            "TownCity": Manchester,
            "County": "Macclesfield",
            "PostCode": "SK11 0LP"        
        }
    }]
```

...and for multiple employments (up to 10) - this assumes the applicant's current home address TownCity is Manchester: 

```
    "ApplicantEmployment": [{
        "Occupation": "not employed",
        "EmployerName": "not employed",
        "EmploymentStatus": "UNEMPLOYED",
        "Telephone": "01234567890",
        "Years": 1,
        "Months": 6,
        "NetMonthlyIncome": 1300.00,
        "EmploymentAddress": {
            "TownCity": Manchester     
        }
    },{
        "Occupation": "Software Engineer",
        "EmployerName": "Zuto",
        "EmploymentStatus": "FULL TIME PERMANENT",
        "Telephone": "01234567890",
        "Years": 2,
        "Months": 7,
        "NetMonthlyIncome": 1000.00,
        "EmploymentAddress": {
            "NameNumber": "Floor 4, 8", 
            "Street": "Exchange Street", 
            "TownCity": Manchester,
            "County": "Greater Manchester",
            "PostCode": "M2 7HA"        
        }
    },
    ]
```

[^ Back to Top](#application-acquisition-submissions-contract)

### FullApplication-ApplicationDetails
    
You can supply information about the application

- CreditLimit {number}: The amount the customer would like to borrow, must be null or greater than 0.01
- VehicleType {string}: Must be one of `CAR`, `VAN`, `MOTORBIKE`, `OTHER`, `CARAVAN`, `MOTORHOME`, `TAXI`
- Deposit {number or null}: The amount of deposit the customer has, must be null or greater than 0.01
- HasGuarantor {bool or null}: Must be null, or true/false
- HasLicenceGuarantor {bool or null}: Must be null, or true/false
- FinanceType {string or null}: Must be one of null, `UNKNOWN`, `HP`, `PCP`, `PERSONAL LOAN`

e.g.
```
    "ApplicationDetails": {
        "CreditLimit": 10000.00,
        "VehicleType": "CAR",
        "Deposit": null,
        "HasGuarantor": false,
        "HasLicenceGuarantor": false,
        "FinanceType": "UNKNOWN"
    }
```

[^ Back to Top](#application-acquisition-submissions-contract)

## An example full application payload

```
curl -XPOST -H "Content-type: application/json" -d '{"Origin":{"Value":"Api-Affiliate"},"ApplicationType":"APPLICATION","DateApplied":"2018-03-17T16:47:49+00:00","SubmittingParty":{"Reference":"1234567890","Source":"MyOrganisation","Medium":"affiliate","Campaign":"MyCampaign"},"Applicants":[{"BasicDetails":{"Gender":"MALE","Title":"Mr","Forename":"Joe","MiddleNames":null,"Surname":"Bloggs","DateOfBirth":"1985-07-16T00:00:00+00:00","PhoneNumbers":[{"Type":"HOME","Value":"01234567890"},{"Type":"MOBILE","Value":"07234567890"}],"Email":"test@zuto.com","ApplicantType":"PRIMARY"},"MarketingOptIn":{"Email":true,"Sms":false,"Phone":true,"ThirdPartyReferral":false},"AdditionalDetails":{"MaritalStatus":"MARRIED","LicenceType":"FULL UK","ValidUkPassport":true,"OtherMonthlyIncome":1000},"ApplicantAddress":[{"NameNumber":"Winterton House","Street":"Winterton Way","TownCity":Manchester,"County":"Macclesfield","PostCode":"SK11 0LP","Years":1,"Months":6,"ResidentialStatus":"HOME OWNER"}],"ApplicantEmployment":[{"Occupation":"Software Engineer","EmployerName":"Zuto","EmploymentStatus":"FULL TIME PERMANENT","Telephone":"01234567890","Years":3,"Months":6,"NetMonthlyIncome":1000,"EmploymentAddress":{"NameNumber":"Winterton House","Street":"Winterton Way","TownCity":null,"County":"Macclesfield","PostCode":"SK11 0LP"}}]}],"AdditionalInformation":{"CreditLimit":10000,"VehicleType":"CAR","Deposit":null,"HasGuarantor":false,"HasLicenceGuarantor":false,"FinanceType":"UNKNOWN"}}' 'https://application-acquisition-submissions.uat.zuto.cloud'
```

```
{
    "Origin": { "Value": "Api-Affiliate" },
    "ApplicationType": "APPLICATION",
    "DateApplied": "2018-03-17T16:47:49+00:00",
    "SubmittingParty": {
        "Reference": "1234567890",
        "Source": "MyOrganisation",
        "Medium": "affiliate",
        "Campaign": "MyCampaign"
    },
    "Applicants": [{
        "BasicDetails": {
            "Gender": "MALE",
            "Title": "Mr",
            "Forename": "Joe",
            "MiddleNames": null,
            "Surname": "Bloggs",
            "DateOfBirth": "1985-07-16T00:00:00+00:00",
            "PhoneNumbers": [{
                "Type": "HOME",
                "Value": "01234567890"
            }, {
                "Type": "MOBILE",
                "Value": "07234567890"
            }],
            "Email": "test@zuto.com",
            "ApplicantType": "PRIMARY"
        },
        "MarketingOptIn": {
            "Email": true,
            "Sms": false,
            "Phone": true,
            "ThirdPartyReferral": false
        },
        "AdditionalDetails": {
            "MaritalStatus": "MARRIED",
            "LicenceType": "FULL UK",
            "ValidUkPassport": true,
            "OtherMonthlyIncome": 1000.00
        },
        "ApplicantAddress": [{
            "NameNumber": "Winterton House", 
            "Street": "Winterton Way", 
            "TownCity": Manchester,
            "County": "Macclesfield",
            "PostCode": "SK11 0LP",
            "Years": 1,
            "Months": 6,
            "ResidentialStatus": "HOME OWNER"
        }],
        "ApplicantEmployment": [{
            "Occupation": "Software Engineer",
            "EmployerName": "Zuto",
            "EmploymentStatus": "FULL TIME PERMANENT",
            "Telephone": "01234567890",
            "Years": 3,
            "Months": 6,
            "NetMonthlyIncome": 1000.00,
            "EmploymentAddress": {
                "NameNumber": "Winterton House", 
                "Street": "Winterton Way", 
                "TownCity": "Macclesfield",
                "County": "Cheshire",
                "PostCode": "SK11 0LP"        
            }
        }]        
    }],
    "ApplicationDetails": {
        "CreditLimit": 10000.00,
        "VehicleType": "CAR",
        "Deposit": null,
        "HasGuarantor": false,
        "HasLicenceGuarantor": false,
        "FinanceType": "UNKNOWN"
    }    
}
```

[^ Back to Top](#application-acquisition-submissions-contract)
