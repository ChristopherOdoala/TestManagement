using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestManagement.Core.Helpers;

namespace TestManagement.Core.Models
{
    public class PcrTestVenues : BaseEntity
    {
        [Key]
        public int PcrTestVenueId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int VenueCapacity { get; set; }
    }
}
