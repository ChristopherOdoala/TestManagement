using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TestManagement.Core.Helpers;

namespace TestManagement.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VenuesController : BaseController
    {
        public VenuesController()
        {

        }

        [HttpPost]
        public IActionResult CreateVenue()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetAvailableVenueAndDate()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult UpdateVenueCapacity()
        {
            throw new NotImplementedException();
        }
    }
}
