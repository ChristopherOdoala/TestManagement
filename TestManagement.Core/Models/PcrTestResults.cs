using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestManagement.Core.Helpers;

namespace TestManagement.Core.Models
{
    public class PcrTestResults : BaseEntity
    {
        [Key]
        public int PcrTestResultId { get; set; }
        public int PcrTestBookingId { get; set; }
        [ForeignKey("PcrTestBookingId")]
        public PcrTestBookings PcrTestBookings { get; set; }

        //The Id of the enum TestResultTypes
        public int PcrTestResultTypedId { get; set; }
    }
}
