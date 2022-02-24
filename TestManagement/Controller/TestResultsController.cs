using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TestManagement.Core.Helpers;

namespace TestManagement.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestResultsController : BaseController
    {
        public TestResultsController()
        {

        }

        [HttpPost]
        public IActionResult GenerateTestResult()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetIndividualTestResult()
        {
            throw new NotImplementedException();
        }
    }
}
