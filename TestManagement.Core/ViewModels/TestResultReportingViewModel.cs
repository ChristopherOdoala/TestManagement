using System;
using System.Collections.Generic;
using System.Text;
using TestManagement.Core.Enums;

namespace TestManagement.Core.ViewModels
{
    public class TestCaseViewModel
    {
        public int PcrTestResultId { get; set; }
        public int PcrTestBookingId { get; set; }
        public int PcrTestResultTypedId { get; set; }
        public int TotalCount { get; set; }
    }

    public class GetAllCaseTypeViewModel
    {
        public GetAllCaseTypeViewModel(int type)
        {
            var typeName = (TestResultTypes)type;
            Type = typeName.ToString();
        }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}
