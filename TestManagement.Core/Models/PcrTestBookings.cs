using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestManagement.Core.Models
{
    public class PcrTestBookings
    {
        [Key]
        public int PcrTestBookingId { get; set; }
        public int PcrTestVenueId { get; set; }
        public DateTime BookingDate { get; set; }
        public string IdentityCardNumber { get; set; }
        public int PcrTestBookingStatusId { get; set; }
        [ForeignKey("PcrTestBookingStatusId")]
        public PcrTestBookingStatuses PcrTestBookingStatuses { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDetails UserDetails { get; set; }
    }
}
