using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestManagement.Core.ViewModels
{
    public class RequestViewModel : IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public DateTime AllocationDate { get; set; }
        public int Age { get; set; }
        public bool IsFrontLineWorker { get; set; }
        public string AnyHealthConditions { get; set; }
        public int VenueId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Age <= 0)
            {
                yield return new ValidationResult("A valid Age should be inputed");
            }

            if(VenueId <= 0)
            {
                yield return new ValidationResult("A valid Venue Id should be inputed");
            }

            if(AllocationDate <= DateTime.Today)
            {
                yield return new ValidationResult("Allocations for today or previous days cannot be booked");
            }
        }
    }

}
