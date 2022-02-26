using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestManagement.Core.ViewModels
{
    public class UploadTestResultStatusViewModel : IValidatableObject
    {
        public int TestResultStatus { get; set; }
        public string IdentityCardNumber { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(TestResultStatus <= 0)
            {
                yield return new ValidationResult("A valid test result status should be inputted");
            }

            if (string.IsNullOrEmpty(IdentityCardNumber))
            {
                yield return new ValidationResult("Identity Card Number cannot be null");
            }
        }
    }

    public class IndividualTestResultViewModel
    {
        public string IdentityCardNumber { get; set;}
        public string TestStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DateBooked { get; set; }
    }
}
