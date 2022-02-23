using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestManagement.Core.Helpers;

namespace TestManagement.Core.Models
{
    public class PcrTestVenueAllocations: BaseEntity
    {
        [Key]
        public int PcrTestVenueAllocationId { get; set; }
        public int PcrTestVenueId { get; set; }
        [ForeignKey("PcrTestVenueId")]
        public PcrTestVenues PcrTestVenues { get; set; }
        public DateTime AllocationDate { get; set; }

        // Assumption: Number of request booked
        public int NumberOfSpaces { get; set; }
    }
}
