using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestManagement.Core.Helpers;

namespace TestManagement.Core.Models
{
    public class PcrTestBookingStatuses : BaseEntity
    {
        [Key]
        public int PcrTestBookingStatusId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

    }
}
