using System;
using System.Collections.Generic;
using System.Text;
using TestManagement.Core.Models;

namespace TestManagement.Core.ViewModels
{
    public class GetVenueViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int VenueCapacity { get; set; }
        public int VenueId { get; set; }

        public static implicit operator GetVenueViewModel(PcrTestVenues model)
        {
            return model == null ? null : new GetVenueViewModel
            {
                Code = model.Code,
                Name = model.Name,
                VenueCapacity = model.VenueCapacity,
                VenueId = model.PcrTestVenueId
            };
        }
    }

    public class GetDateWithCapacityDetails
    {
        public int MaxCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public string Date { get; set; }
    }

    public class VenueQueryModel
    {
        public string Month { get; set; }
        public int Year { get; set; }
        public int VenueId { get; set; }
    }

    public class CreateVenueViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int VenueCapacity { get; set; }
    }

    public class GetVenueWithCapacity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int VenueCapacity { get; set; }
        public int TotalCount { get; set; }
    }
}
