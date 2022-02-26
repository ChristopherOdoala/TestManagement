using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TestManagement.Core.Context;
using TestManagement.Core.Models;
using TestManagement.Core.Services.Interfaces;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services
{
    public class VenueService : IVenueService
    {
        private readonly DataContext _dbContext;
        public VenueService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResultModel<string> AddVenue(CreateVenueViewModel model)
        {
            var resultModel = new ResultModel<string>();

            if(model.VenueCapacity <= 0)
            {
                resultModel.AddError("Venue capacity cannot be less than 1");
                return resultModel;
            }

            if (string.IsNullOrWhiteSpace(model.Code))
            {
                resultModel.AddError("Venue Code cannot be null");
                return resultModel;
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                resultModel.AddError("Venue Name cannot be null");
                return resultModel;
            }

            var venue = new PcrTestVenues
            {
                Code = model.Code,
                Name = model.Name,
                VenueCapacity = model.VenueCapacity,
                CreatedDate = DateTime.Now
            };

            try
            {
                _dbContext.Add(venue);
                _dbContext.SaveChanges();
                resultModel.Data = "Successful";
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        public ResultModel<List<GetVenueViewModel>> GetAllVenue()
        {
            var resultModel = new ResultModel<List<GetVenueViewModel>>();

            var venues = _dbContext.PcrTestVenues.Select(x => (GetVenueViewModel)x);

            if(!venues.Any())
                return resultModel;

            resultModel.Data = venues.ToList();
            
            return resultModel;

        }

        public ResultModel<List<GetDateWithCapacityDetails>> GetAvailableDatePerMonth(VenueQueryModel model)
        {
            var resultModel = new ResultModel<List<GetDateWithCapacityDetails>>();
            try
            {
                
                if (model.VenueId <= 0)
                {
                    resultModel.AddError("A valid Venue Id should be passed");
                    return resultModel;
                }

                if (string.IsNullOrWhiteSpace(model.Month))
                {
                    resultModel.AddError("The month cannot be null");
                    return resultModel;
                }

                if (model.Year <= 0)
                {
                    resultModel.AddError("A valid year was not passed");
                    return resultModel;
                }

                int month = DateTime.ParseExact(model.Month, "MMMM", CultureInfo.CurrentCulture).Month;

                var daysInMonth = GetDates(model.Year, month);
                var venue = _dbContext.PcrTestVenues.Find(model.VenueId);

                if (venue is null)
                {
                    resultModel.AddError($"Venue with Id : {model.VenueId} could not be found");
                    return resultModel;
                }

                var allocations = _dbContext.PcrTestVenueAllocations.Where(x => x.PcrTestVenueId == model.VenueId);

                if (!allocations.Any())
                {
                    resultModel.AddError($"No bookings made");
                    return resultModel;
                }


                var bookingsAllocation = from days in daysInMonth
                                         join allocation in allocations on days equals allocation.AllocationDate into emp
                                         from res in emp.DefaultIfEmpty()
                                         select new GetDateWithCapacityDetails
                                         {
                                             MaxCapacity = venue.VenueCapacity,
                                             CurrentCapacity = res == null ? venue.VenueCapacity : venue.VenueCapacity - res.NumberOfSpaces,
                                             Date = days.ToString("yyyy/MM/dd")
                                         };

                resultModel.Data = bookingsAllocation.ToList();

                return resultModel;
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        private static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }
    }
}
