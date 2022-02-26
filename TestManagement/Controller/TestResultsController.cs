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
    public class TestResultsController : BaseController
    {
        private readonly ITestResultService _testResultService;
        public TestResultsController(ITestResultService testResultService)
        {
            _testResultService = testResultService;
        }

        [HttpPost]
        public IActionResult UploadTestResultStatus([FromBody] UploadTestResultStatusViewModel model)
        {
            var result = _testResultService.UploadTestResultStatus(model);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetIndividualTestResult(string identityCardNumber)
        {
            var result = _testResultService.GetIndividualTestResult(identityCardNumber);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }
    }
}
