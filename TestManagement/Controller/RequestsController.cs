using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TestManagement.Core.Helpers;

namespace TestManagement.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestsController : BaseController
    {
        public RequestsController()
        {

        }

        [HttpPost]
        public IActionResult CreateBookingRequest()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult CancelBooking()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult UpdateRequestAsPerformed()
        {
            throw new NotImplementedException();
        }
    }
}
