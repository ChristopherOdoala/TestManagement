using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TestManagement.Core.Enums;
using TestManagement.Core.Helpers;
using TestManagement.Core.Services.Interfaces;
using TestManagement.Core.ViewModels;

namespace TestManagement.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : BaseController
    {
        private readonly ITestReportingService _testReportingService;
        public ReportsController(ITestReportingService testReportingService)
        {
            _testReportingService = testReportingService;
        }

        [HttpGet]
        public IActionResult GetNoOfNegativeTestCases()
        {
            var result = _testReportingService.GetCases((int)TestResultTypes.Negative);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data.ToString(), errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetNoOfPositiveTestCases()
        {
            var result = _testReportingService.GetCases((int)TestResultTypes.Positive);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data.ToString(), errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetAllCaseType()
        {
            var result = _testReportingService.GetAllCaseType();
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data.ToString(), errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetAllTest()
        {
            int totalCount;
            var result = _testReportingService.GetAllTestsDetails(out totalCount);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data.ToString(), errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, totalCount: totalCount, codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetBookingsReport()
        {
            var result = _testReportingService.GetBookingsReport();
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data.ToString(), errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, totalCount: result.Data == null ? 0 : result.TotalCount, codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetVenueWithCapacity()
        {
            int totalCount;
            var result = _testReportingService.GetVenueWithCapacity(out totalCount);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data.ToString(), errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, totalCount: totalCount, codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetVenueBookingCapacityReport([FromQuery]DateQueryModel model)
        {
            var result = _testReportingService.GetVenueBookingCapacityReport(model);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data.ToString(), errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }
    }
}
