using System;
using System.Collections.Generic;
using System.Text;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services.Interfaces
{
    public interface IRequestService
    {
        ResultModel<string> BookForATest(RequestViewModel model);
        ResultModel<string> CancelBooking(string identityCardNumber);
        ResultModel<string> UpdateTestAsPerformed(string identityCardNumber);
        ResultModel<List<GetBookingsViewModel>> GetAllBookings(QueryViewModel model);
    }
}
