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
    public class VenuesController : BaseController
    {
        private readonly IVenueService _venueService;
        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpPost]
        public IActionResult CreateVenue([FromBody] CreateVenueViewModel model)
        {
            var result = _venueService.AddVenue(model);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data,codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetAllVenues()
        {
            var result = _venueService.GetAllVenue();
            if(result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR );

            return ApiResponse(result.Data, totalCount: result.Data == null ? 0 : result.Data.Count(), codes: ApiResponseCodes.OK);
        }

        [HttpGet]
        public IActionResult GetAvailableDatePerMonth([FromQuery]VenueQueryModel model )
        {
            var result = _venueService.GetAvailableDatePerMonth(model);
            if (result.ErrorMessages.Any())
                return ApiResponse(result.Data, errors: result.ErrorMessages.FirstOrDefault(), codes: ApiResponseCodes.ERROR);

            return ApiResponse(result.Data, codes: ApiResponseCodes.OK);
        }

    }
}
