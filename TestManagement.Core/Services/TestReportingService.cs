using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TestManagement.Core.Context;
using TestManagement.Core.Dapper.Interfaces;
using TestManagement.Core.Dapper.Services;
using TestManagement.Core.Enums;
using TestManagement.Core.Models;
using TestManagement.Core.Services.Interfaces;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services
{
    public class TestReportingService : Service<PcrTestResults>, ITestReportingService
    {
        private readonly DataContext _dataContext;
        public TestReportingService(DataContext dataContext, IUnitOfWork unitOfWork) :base(unitOfWork)
        {
            _dataContext = dataContext;
        }

        public ResultModel<int> GetCases(int resultTypeId)
        {
            var resultModel = new ResultModel<int>();
            var parameters = new DynamicParameters();

            parameters.Add("@PcrTestResultTypedId", resultTypeId, DbType.Int32);

            try
            {
                var cases = ExecuteStoredProcedure<TestCaseViewModel>("[GetNoOfTestCases]", parameters).Result.ToList();
                resultModel.Data = cases.FirstOrDefault()?.TotalCount ?? 0;
                return resultModel;
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
                resultModel.Data = 0;
            }

            return resultModel;
        }

        public ResultModel<List<GetAllTestViewModel>> GetAllTestsDetails(out int totalCount)
        {
            var resultModel = new ResultModel<List<GetAllTestViewModel>>();
            var parameters = new DynamicParameters();

            try
            {
                var tests = ExecuteStoredProcedure<GetAllTestViewModel>("[Sp_GetAllTest]", parameters).Result;
                totalCount = tests.FirstOrDefault().TotalCount;
                resultModel.Data = tests.ToList();
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message);
                totalCount = 0;
            }

            return resultModel;
        }

        public ResultModel<List<GetVenueWithCapacity>> GetVenueWithCapacity(out int totalCount)
        {
            var resultModel = new ResultModel<List<GetVenueWithCapacity>>();
            var parameters = new DynamicParameters();

            try
            {
                var tests = ExecuteStoredProcedure<GetVenueWithCapacity>("[Sp_GetAllVenue]", parameters).Result;
                totalCount = tests.FirstOrDefault().TotalCount;
                resultModel.Data = tests.ToList();
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
                totalCount = 0;
            }

            return resultModel;
        }

        public ResultModel<List<GetAllCaseTypeViewModel>> GetAllCaseType()
        {
            var resultModel = new ResultModel<List<GetAllCaseTypeViewModel>>();

            try
            {
                var testResults = _dataContext.PcrTestResults
                                    .GroupBy(x => x.PcrTestResultTypedId)
                                    .Select(t => new GetAllCaseTypeViewModel(t.Key)
                                    {
                                        TypeId = t.Key,
                                        Count = t.Count(),
                                    });

                resultModel.Data = testResults.ToList();
            }
            catch(Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;

        }

        public ResultModel<List<GetBookingsReportViewModel>> GetBookingsReport()
        {
            var resultModel = new ResultModel<List<GetBookingsReportViewModel>>();

            try
            {
                var bookingContext = _dataContext.PcrTestBookings;
                var bookings = bookingContext
                                .GroupBy(x => x.PcrTestBookingStatusId)
                                .Select(t => new GetBookingsReportViewModel(t.Key)
                                {
                                    Count = t.Count()
                                });
                resultModel.TotalCount = bookingContext.Count();
                resultModel.Data = bookings.ToList();
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        public ResultModel<List<VenueBookingCapacityReportViewModel>> GetVenueBookingCapacityReport(DateQueryModel model)
        {
            var resultModel = new ResultModel<List<VenueBookingCapacityReportViewModel>>();

            try
            {
                var bookingContext = _dataContext.PcrTestBookings.Include(x => x.PcrTestVenues).AsQueryable();
                if (model.fromDate != null)
                    bookingContext = bookingContext.Where(x => x.BookingDate.Day >= model.fromDate.Value.Day);
                if(model.toDate != null)
                    bookingContext = bookingContext.Where(x => x.BookingDate.Day <= model.toDate.Value.Day);

                var bookings = (from v in _dataContext.PcrTestVenues.AsQueryable()
                                join b in bookingContext on v.PcrTestVenueId equals b.PcrTestVenueId
                                select new
                                {
                                    VenueName = v.Name,
                                    BookingDate = b.BookingDate
                                })
                                .GroupBy(x => x.VenueName)
                                .Select(x => new VenueBookingCapacityReportViewModel
                                {
                                    BookingCount = x.Count(),
                                    VenueName = x.Key
                                }).ToList();
                                

                resultModel.Data = bookings.ToList();
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }
    }
}
