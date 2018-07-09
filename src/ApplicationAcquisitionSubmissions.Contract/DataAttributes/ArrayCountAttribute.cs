using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationAcquisitionSubmissions.Contract.DataAttributes
{
    /// <summary>
    /// Applys a minimum and maximum count on size of an array
    /// </summary>
    public class ArrayCountAttribute : ValidationAttribute
    {
        public int Min { get; }
        public int Max { get; }

        public ArrayCountAttribute(int min, int max)
        {
            Min = min;
            Max = max;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {                        
            if (value == null && Min > 0)
                return new ValidationResult(FormatErrorMessage(validationContext.MemberName));

            var count = (value as Array)?.Length;
            var result = !(count < Min) && !(count > Max);

            return result 
                ? ValidationResult.Success 
                : new ValidationResult(FormatErrorMessage(validationContext.MemberName));
        }

        public override string FormatErrorMessage(string name)
        {
            return name + " should contain a minimum of " + Min + " elements and a maximum of " + Max + " elements";
        }
    }
}