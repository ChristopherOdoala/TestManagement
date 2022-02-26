using System;
using System.Collections.Generic;
using System.Text;

namespace TestManagement.Core.ViewModels
{
    public class RequestViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; }
        public int Age { get; set; }
        public bool IsFrontLineWorker { get; set; }
        public string AnyHealthConditions { get; set; }
    }
}
