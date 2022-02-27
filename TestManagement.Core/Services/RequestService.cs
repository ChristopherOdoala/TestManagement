using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestManagement.Core.Context;
using TestManagement.Core.Enums;
using TestManagement.Core.Helpers;
using TestManagement.Core.Models;
using TestManagement.Core.Services.Interfaces;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services
{
    public class RequestService : IRequestService
    {
        private readonly DataContext _dataContext;

        public RequestService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultModel<string> BookForATest(RequestViewModel model)
        {
            var resultModel = new ResultModel<string>();

            var venue = _dataContext.PcrTestVenues.Find(model.VenueId);
            if (venue == null)
            {
                resultModel.AddError($"Venue with Id: {model.VenueId} does not exist");
                return resultModel;
            }

            try
            {

                var allocation = _dataContext.PcrTestVenueAllocations.Where(x => x.AllocationDate.Day == model.AllocationDate.Day
                                                                            && x.PcrTestVenueId == model.VenueId).FirstOrDefault();
                if (allocation != null)
                {
                    if (allocation.NumberOfSpaces >= venue.VenueCapacity)
                    {
                        resultModel.AddError($"Venue selected: {venue.Name} is full for the selected date: {model.AllocationDate.ToString("yyyy/MM/dd")}");
                        return resultModel;
                    }

                    allocation.NumberOfSpaces += 1;
                    allocation.ModifiedDate = DateTime.Now;

                    _dataContext.Update(allocation);
                }
                else
                {
                    var newAllocation = new PcrTestVenueAllocations()
                    {
                        PcrTestVenueId = model.VenueId,
                        AllocationDate = model.AllocationDate,
                        NumberOfSpaces = 1,
                        CreatedDate = DateTime.Now
                    };

                    _dataContext.Add(newAllocation);
                }

                var userDetail = new UserDetails()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MiddleName = model.MiddleName,
                    IsFrontLineWorker = model.IsFrontLineWorker,
                    AnyHealthConditions = model.AnyHealthConditions,
                    EmailAddress = model.EmailAddress,
                    Age = model.Age,
                    CreatedDate = DateTime.Now,
                };

                _dataContext.Add(userDetail);
                _dataContext.SaveChanges();

                var testBooking = new PcrTestBookings()
                {
                    BookingDate = model.AllocationDate,
                    PcrTestBookingStatusId = (int)TestBookingStatuses.Booked,
                    IdentityCardNumber = RandomString(),
                    UserId = userDetail.UserId,
                    PcrTestVenueId = model.VenueId,
                    CreatedDate = DateTime.Now
                };

                _dataContext.Add(testBooking);
                _dataContext.SaveChanges();

                resultModel.Data = testBooking.IdentityCardNumber;
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }
                        
            return resultModel;
        }

        public ResultModel<string> CancelBooking(string identityCardNumber)
        {
            var resultModel = new ResultModel<string>();

            if (string.IsNullOrEmpty(identityCardNumber))
            {
                resultModel.AddError("IdentityCardNumber is required");
                return resultModel;
            }

            try
            {
                var booking = _dataContext.PcrTestBookings.Where(x => x.IdentityCardNumber == identityCardNumber).FirstOrDefault();
                if (booking == null)
                {
                    resultModel.AddError($"Booking with Identity Card Number {identityCardNumber} does not exist");
                    return resultModel;
                }

                if(booking.BookingDate <= DateTime.Today)
                {
                    resultModel.AddError("This booking cannot be canceled as it is either in the past or due today");
                    return resultModel;
                }

                var allocation = _dataContext.PcrTestVenueAllocations.Where(x => x.PcrTestVenueId == booking.PcrTestVenueId
                                                                        && x.AllocationDate.Day == booking.BookingDate.Day).FirstOrDefault();
                allocation.NumberOfSpaces -= 1;
                allocation.ModifiedDate = DateTime.Now;

                booking.PcrTestBookingStatusId = (int)TestBookingStatuses.Cancelled;
                booking.ModifiedDate = DateTime.Now;

                _dataContext.Update(allocation);
                _dataContext.Update(booking);

                _dataContext.SaveChanges();

                resultModel.Data = "Booking Successfully Canceled";
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        public ResultModel<string> UpdateTestAsPerformed(string identityCardNumber)
        {
            var resultModel = new ResultModel<string>();

            if (string.IsNullOrEmpty(identityCardNumber))
            {
                resultModel.AddError("IdentityCardNumber is required");
                return resultModel;
            }

            try
            {
                var booking = _dataContext.PcrTestBookings.Where(x => x.IdentityCardNumber == identityCardNumber).FirstOrDefault();
                if (booking == null)
                {
                    resultModel.AddError($"Booking with Identity Card Number {identityCardNumber} does not exist");
                    return resultModel;
                }


                booking.PcrTestBookingStatusId = (int)TestBookingStatuses.Performed;
                booking.ModifiedDate = DateTime.Now;

                _dataContext.Update(booking);

                _dataContext.SaveChanges();

                resultModel.Data = "Test updated as performed";
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        public ResultModel<List<GetBookingsViewModel>> GetAllBookings(QueryViewModel model)
        {
            var resultModel = new ResultModel<List<GetBookingsViewModel>>();

            var pageSize = model.PageSize == null || model.PageSize < 1 ? 10 : model.PageSize.Value;
            var pageIndex = model.PageIndex == null || model.PageIndex < 1 ? 1 : model.PageIndex.Value;

            try
            {
                var bookings = _dataContext.PcrTestBookings.Include(x => x.UserDetails).AsQueryable();

                if (!bookings.Any())
                {
                    return resultModel;
                }

                resultModel.TotalCount = bookings.Count();

                resultModel.Data = bookings.Paginate(pageIndex, pageSize).Select(x => (GetBookingsViewModel)x).ToList();
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message);
            }
            
            return resultModel;
        }

        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 15)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        
    }
}
