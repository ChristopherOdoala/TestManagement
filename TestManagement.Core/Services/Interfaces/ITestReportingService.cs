using System;
using System.Collections.Generic;
using System.Text;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services.Interfaces
{
    public interface ITestReportingService
    {
        ResultModel<int> GetCases(int resultTypeId);
        ResultModel<List<GetAllCaseTypeViewModel>> GetAllCaseType();
        ResultModel<List<GetAllTestViewModel>> GetAllTestsDetails(out int totalCount);
        ResultModel<List<GetVenueWithCapacity>> GetVenueWithCapacity(out int totalCount);
        ResultModel<List<VenueBookingCapacityReportViewModel>> GetVenueBookingCapacityReport(DateQueryModel model);
        ResultModel<List<GetBookingsReportViewModel>> GetBookingsReport();
    }
}
