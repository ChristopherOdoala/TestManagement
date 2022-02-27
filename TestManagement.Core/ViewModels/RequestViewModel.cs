using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestManagement.Core.Enums;
using TestManagement.Core.Models;

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

    public class GetBookingsViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool IsFrontLineWorker { get; set; }
        public string AnyHealthConditions { get; set; }
        public string IdentityCardNumber { get; set; }
        public int PcrTestVenueId { get; set; }
        public string BookingDate { get; set; }
        public string TestBookingStatus { get; set; }

        public static implicit operator GetBookingsViewModel(PcrTestBookings model)
        {
            var bookingStatus = (TestBookingStatuses)model.PcrTestBookingStatusId;
            return model == null ? null : new GetBookingsViewModel
            {
                UserId = model.UserId,
                FirstName = model.UserDetails.FirstName,
                LastName = model.UserDetails.LastName,
                MiddleName = model.UserDetails.MiddleName,
                EmailAddress = model.UserDetails.EmailAddress,
                Age = model.UserDetails.Age,
                IsFrontLineWorker = model.UserDetails.IsFrontLineWorker,
                AnyHealthConditions = model.UserDetails.AnyHealthConditions,
                IdentityCardNumber = model.IdentityCardNumber,
                PcrTestVenueId = model.PcrTestVenueId,
                BookingDate = model.BookingDate.ToString("yyyy-MM-dd"),
                TestBookingStatus = bookingStatus.ToString()
            };
        }
    }

    public class GetBookingsReportViewModel
    {
        public GetBookingsReportViewModel(int bookingStatus)
        {
            var bookingType = (TestBookingStatuses)bookingStatus;
            BookingStatus = bookingType.ToString();
        }
        public string BookingStatus { get; set; }
        public int Count { get; set; }
    }

    public class VenueBookingCapacityReportViewModel
    {
        public string VenueName { get; set; }
        public int BookingCount { get; set; }
    }

}
