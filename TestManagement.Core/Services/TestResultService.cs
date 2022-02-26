using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestManagement.Core.Context;
using TestManagement.Core.Enums;
using TestManagement.Core.Models;
using TestManagement.Core.Services.Interfaces;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services
{
    public class TestResultService : ITestResultService
    {
        private readonly DataContext _dataContext;

        public TestResultService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ResultModel<IndividualTestResultViewModel> GetIndividualTestResult(string identityCardNumber)
        {
            var resultModel = new ResultModel<IndividualTestResultViewModel>();

            if (string.IsNullOrEmpty(identityCardNumber))
            {
                resultModel.AddError("Identity Number cannot be null");
                return resultModel;
            }

            try
            {
                var booking = _dataContext.PcrTestBookings.Where(x => x.IdentityCardNumber == identityCardNumber).Include(x => x.UserDetails).FirstOrDefault();

                if (booking == null)
                {
                    resultModel.AddError($"No record exist for Identity Card Number {identityCardNumber}");
                    return resultModel;
                }

                var testResult = _dataContext.PcrTestResults.Where(x => x.PcrTestBookingId == booking.PcrTestBookingId).FirstOrDefault();

                if (testResult == null)
                {
                    resultModel.AddError($"No Test Result found for Identity Card Number: {identityCardNumber}");
                    return resultModel;
                }

                var testResultType = (TestResultTypes)testResult.PcrTestResultTypedId;

                var result = new IndividualTestResultViewModel
                {
                    DateBooked = booking.BookingDate.ToString("yyyy-MM-dd"),
                    Email = booking.UserDetails.EmailAddress,
                    FirstName = booking.UserDetails.FirstName,
                    LastName = booking.UserDetails.LastName,
                    IdentityCardNumber = identityCardNumber,
                    TestStatus = testResultType.ToString()
                };

                resultModel.Data = result;
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;
        }

        public ResultModel<string> UploadTestResultStatus(UploadTestResultStatusViewModel model)
        {
            var resultModel = new ResultModel<string>();

            try
            {
                var booking = _dataContext.PcrTestBookings.Where(x => x.IdentityCardNumber == model.IdentityCardNumber).FirstOrDefault();

                if (booking == null)
                {
                    resultModel.AddError($"No record exist for Identity Card Number {model.IdentityCardNumber}");
                    return resultModel;
                }

                if((TestBookingStatuses)booking.PcrTestBookingStatusId != TestBookingStatuses.Performed)
                {
                    resultModel.AddError($"Test result cannot be updated for: {model.IdentityCardNumber} as Test has not been performed");
                    return resultModel;
                }

                var testResult = new PcrTestResults()
                {
                    CreatedDate = DateTime.Now,
                    PcrTestBookingId = booking.PcrTestBookingId,
                    PcrTestResultTypedId = model.TestResultStatus
                };

                booking.PcrTestBookingStatusId = (int)TestBookingStatuses.Outcome;
                booking.ModifiedDate = DateTime.Now;

                _dataContext.Add(testResult);
                _dataContext.Update(booking);

                _dataContext.SaveChanges();

                resultModel.Data = "Test Result Status updated Successfully";
            }
            catch (Exception ex)
            {
                resultModel.AddError(ex.Message);
            }

            return resultModel;

        }
    }
}
