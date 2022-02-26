using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestManagement.Core.Helpers;

namespace TestManagement.Core.Models
{
    public class PcrTestBookings : BaseEntity
    {
        [Key]
        public int PcrTestBookingId { get; set; }
        public int PcrTestVenueId { get; set; }
        public DateTime BookingDate { get; set; }

        //Auto Generated
        public string IdentityCardNumber { get; set; }

        //The Id of the enum TestBookingStatuses
        public int PcrTestBookingStatusId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }
    }
}
