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
    public class RequestsController : BaseController
    {
        private readonly IRequestService _requestService;
        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost]
        public IActionResult CreateBookingRequest([FromBody] RequestViewModel model)
        {
            var result = _requestService.BookForATest(model);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }

        [HttpPost]
        public IActionResult CancelBooking(string identityCardNumber)
        {
            var result = _requestService.CancelBooking(identityCardNumber);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }

        [HttpPost]
        public IActionResult UpdateTestAsPerformed(string identityCardNumber)
        {
            var result = _requestService.UpdateTestAsPerformed(identityCardNumber);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }
    }
}
