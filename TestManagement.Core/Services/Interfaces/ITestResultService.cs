using System;
using System.Collections.Generic;
using System.Text;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services.Interfaces
{
    public interface ITestResultService
    {
        ResultModel<string> UploadTestResultStatus(UploadTestResultStatusViewModel model);
        ResultModel<IndividualTestResultViewModel> GetIndividualTestResult(string identityCardNumber);
    }
}
