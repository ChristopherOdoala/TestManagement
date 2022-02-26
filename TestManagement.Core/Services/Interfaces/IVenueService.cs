using System;
using System.Collections.Generic;
using System.Text;
using TestManagement.Core.ViewModels;

namespace TestManagement.Core.Services.Interfaces
{
    public interface IVenueService
    {
        ResultModel<List<GetVenueViewModel>> GetAllVenue();
        ResultModel<List<GetDateWithCapacityDetails>> GetAvailableDatePerMonth(VenueQueryModel model);
        ResultModel<string> AddVenue(CreateVenueViewModel model);
    }
}
