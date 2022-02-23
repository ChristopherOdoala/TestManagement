using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TestManagement.Core.Helpers;

namespace TestManagement.Core.Models
{
    public class UserDetails : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool IsFrontLineWorker { get; set; }
        public string AnyHealthConditions { get; set; }
    }
}
